import React, { useState } from 'react'
import { useGeneralContext } from '../../../../context/GeneralContext'
import { AiOutlinePlus } from 'react-icons/ai'
import { AddButton, MainContainer, TableContainer } from '../../../alumnos/styles/Styles'
import Tabla from '../../../../components/Tabla';
import { toast } from "react-hot-toast";
import APILINK from '../../../../components/link';
import axios from 'axios';
import { useFetchEffect } from '../../../../components/utils/useFetchEffect';
import { Container, Resumen, SideSection } from './componentes/StyledResumenAsistencia';
import DayMonthPicker from './componentes/DayMonthPicker';
import { HiCheckCircle } from 'react-icons/hi'
import {sum} from 'lodash'
import { useModularContext } from '../../context';
import { RxCross1 } from 'react-icons/rx';
import BackButton from '../../components/BackButton';
import styled from 'styled-components';
import AsistenciaUnit from './componentes/AsistenciaUnit';

const FlexDiv = styled.div`
    display: flex;
    align-items: center;
    width: 100%;
    gap: 10px;
`

const InvisibleSelect = styled.select`
    width: 100%;
    margin-top: 10px;
    margin-bottom: 10px;
    outline: none;
    border: none;
    appearance: none;
    background-color: #FFFFFF;
    text-align:center;
    &:hover{
        filter: brightness(0.8);
    }
`;

// const Asistencia = ({ materia = { id: 1, nombre: "Matemáticas" } }) => {
const Asistencia = React.memo(() => {

    const VALUEMES = [
        {value: 1, text: "ENERO"},
        {value: 2, text: "FEBRERO"},
        {value: 3, text: "MARZO"},
        {value: 4, text: "ABRIL"},
        {value: 5, text: "MAYO"},
        {value: 6, text: "JUNIO"},
        {value: 7, text: "JULIO"},
        {value: 8, text: "AGOSTO"},
        {value: 9, text: "SEPTIEMBRE"},
        {value: 10, text: "OCTUBRE"},
        {value: 11, text: "NOVIEMBRE"},
        {value: 12, text: "DICIEMBRE"},
    ]

    const { getToken/*, cancan, verifyToken*/ } = useGeneralContext()
    //const [condFetch, setCondFetch] = useState(true)
    const {dataSet} = useModularContext()
    const { materiaName, currColegio, currClase } = dataSet

    // const [listaAsistencias, setListaAsistencias] = useState([])
    // const [listaNueva, setListaNueva] = useState([])

    const [tablaAsistencia, setDatosTabla] = useState([]);
    const [datosTablaOriginales, setDatosTablaOriginales] = useState([]);

    const [disableEdit, setDisableEdit] = useState(false)

    const [nuevaAsistencia, setNuevaAsistencia] = useState([])
    const [adding, setAdding] = useState(false)
    const [cantAlumnos, setCantAlumnos] = useState(0)
    const [cantClases, setCantClases] = useState(0)
    const [porcentajes, setPorcentajes] = useState([])
    const {stateController} = useModularContext()
    const {materiaId} = stateController

    const [ mesValue, setMesValue ] = useState(0)
    // const { idMateriaLista } = useParams()
    // const nombre = "Matemáticas"

    const { doFetch, loading, error } = useFetchEffect(
        () => {
            setDisableEdit(true)
            // return axios.get(`${APILINK}/api/Asistencia/${materia?.id}`, {
            let body = {
                "idMateriaLista": materiaId,
                "mes": mesValue
            }
            console.log(body)
            return axios.post(`${APILINK}/api/Asistencia`, body, {
                headers: {
                    Authorization: 'Bearer ' + getToken(),
                    "Content-Type": "application/json"
                },
            }).then(response => response.data);
        },
        [mesValue],
        {
            condition: true,
            handleSuccess: (dataAsistencia) => {
                setPorcentajes((before)=> {return []})
                setCantAlumnos(dataAsistencia.length)
                setCantClases(dataAsistencia[0]?.asistencias?.length)
                // setListaAsistencias(dataAsistencia)
                // setListaNueva(dataAsistencia)
                const DatosAsistencia = {
                    tituloTabla: "Asistencias",
                    titulos: [
                        { titulo: "Alumnos" },
                        ...(dataAsistencia[0]?.asistencias?.length > 0
                            ? dataAsistencia[0].asistencias.map((fecha) => {
                                return { titulo: fecha?.fecha ? formatAsistenciaDate(fecha.fecha) : "" };
                            })
                            : []),
                        { titulo: "%" }
                    ],
                    filas: dataAsistencia.map((dato) => {
                        console.log(dato)
                        setPorcentajes((before)=>{return [...before, dato.porcentajePresentes]})
                        return {
                            fila: dato,
                            datos: [
                                { dato: dato?.nombre ? `${dato.apellido}, ${dato.nombre} ` : " " },
                                ...(dato.asistencias?.length > 0
                                    ? dato.asistencias.map((fecha) => {
                                        return { dato: fecha?.estado ? fecha.estado : "", componente: <AsistenciaUnit disable={disableEdit} fetchFunc = {()=>{doFetch()}} fecha={fecha}/> };
                                    })
                                    : []),
                                { dato: dato?.porcentajePresentes.toFixed(2) }
                            ],
                        }
                    })
                }
                setDatosTablaOriginales(DatosAsistencia)
                setDatosTabla(DatosAsistencia)
                setNuevaAsistencia(DatosAsistencia.filas.map((fila) => {
                    return {
                        id: fila.fila.id,
                        estado: "A",
                        accion: "N",
                        observacion: ""
                    }
                })
                )
                setDisableEdit(false)
            },
            handleError: () => {
                if (!loading) {
                    setDatosTabla([])
                    toast.error("No se han podido obtener los datos. Intente recargar la página.")
                }
            }
        }
    );

    const handleSubmitAddAsistencia = () => {

        const fecha = document.getElementById("fechaSeleccionada").value;
        if (!(new Date(fecha) instanceof Date) || isNaN(new Date(fecha))) {
            toast.error("Seleccione una fecha válida");
            return
        }
        else {

            let data = JSON.stringify(nuevaAsistencia.map((asistencia) => {
                return { ...asistencia, fecha: fecha }
            }
            ));

            let config = {
                method: 'put',
                maxBodyLength: Infinity,
                url: `${APILINK}/api/Asistencia/${materiaId}`,
                headers: {
                    'Authorization': `Bearer ${getToken()}`,
                    'Content-Type': 'application/json'
                },
                data: data
            };
            axios(config)
                .then(function (response) {
                    if (response.status >= 400) {
                        toast.error("Hubo un error")
                    }
                    else if (response.status >= 200) {
                        toast.success("Guardado correctamente")
                        setAdding(false)
                        doFetch()
                    }
                })
                .catch(function (error) {
                    if (!!typeof (error.response?.data) === "string") {
                        toast.error(error.response.data)
                    } else {
                        toast.error("Error al agregar la asistencia, revise la fecha a agregar")
                    }
                });
        }
    }

    const formatAsistenciaDate = (date) => {
        const newDate = new Date(date);
        const day = newDate.getDate();
        const month = newDate.getMonth() + 1;
        return `${day < 10 ? '0' : ''}${day}/${month < 10 ? '0' : ''}${month}`;
    }

    const handleSelectNuevoEstado = (indice, nuevoValor) => {
        setNuevaAsistencia((prevAsistencia) => {
            const nuevaAsistencia = [...prevAsistencia];
            nuevaAsistencia[indice].estado = nuevoValor;
            return nuevaAsistencia;
        });
    };

    const selectEstado = (estado, id) => {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', width: '100%' }}>
                <select defaultValue={estado} onChange={(event) => handleSelectNuevoEstado(id, event.target.value)}>
                    <option value="A">A</option>
                    <option value="P">P</option>
                    <option value="J">J</option>
                </select>
            </div>
        )
    }
    const addDate = () => {
        if (!adding) {

            let prevTitulos = tablaAsistencia.titulos?.length > 1 ? tablaAsistencia.titulos.slice(0, -1) : tablaAsistencia.titulos
            const nuevosTitulos = [
                ...prevTitulos,
                { componentes: columnHandler }
            ];

            const nuevasFilas = tablaAsistencia.filas.map((fila, index) => {
                let datosPrev = fila.datos.length > 1 ? fila.datos.slice(0, -1) : fila.datos
                return { datos: [...datosPrev, { componente: selectEstado(nuevaAsistencia[index], index) }], fila: fila.fila }
            });

            setDatosTabla(prevState => ({
                ...prevState,
                titulos: nuevosTitulos,
                filas: nuevasFilas
            }));
        } else {
            setDatosTabla(datosTablaOriginales)
        }
        setAdding(prevState => !prevState)
    }

    const getPromedios = () => {
        return (sum(porcentajes)/(porcentajes?.length ? porcentajes.length : 1)).toFixed(2)
    }

    const columnHandler = [
        { key: 'datePicker', componente: <DayMonthPicker /> },
        { key: 'okbt', componente: <HiCheckCircle size={18} color='green' />, action: handleSubmitAddAsistencia }
    ]

    return (
        <>
            <MainContainer>
                {/* <StyleComponentBreadcrumb nombre={`Registro de Asistencia - ${materia?.nombre}`} /> */}
                <div className='m-4'>
                    <FlexDiv>
                        <BackButton to="materiashow"/>
                        <h5 className="m-0">
                            {currColegio} - {currClase} - {materiaName} - Registro de Asistencia 
                        </h5>
                    </FlexDiv>
                    <Container>
                        <SideSection>
                            <TableContainer>
                                <Tabla datosTabla={tablaAsistencia} />
                                <AddButton onClick={addDate}>
                                    {!adding?
                                        <AiOutlinePlus size={"35px"} />
                                        :
                                        <RxCross1 size={"30px"}/>
                                    }
                                </AddButton>
                            </TableContainer >
                        </SideSection>
                        <SideSection>
                            {!error && !loading &&
                                <Resumen>
                                    <InvisibleSelect value={mesValue} onChange={(e)=>{setMesValue(Number(e.target.value))}}>
                                        <option value={0}>Mes Actual</option>
                                        {VALUEMES.map((v,i)=>{
                                            return <option key={i} value={v.value}>{v.text}</option>
                                        })

                                        }
                                    </InvisibleSelect>
                                    <p>{cantAlumnos} alumnos</p>
                                    <p>{cantClases} {cantClases < 1 ? "Aún no hay clases" : cantClases > 1 ? "clases" : "clase"}</p>
                                    <p>{getPromedios()} promedio de asistencias</p>
                                </Resumen>
                            }
                        </SideSection>
                    </Container>
                </div>
            </MainContainer >
        </>
    )
})

export default Asistencia