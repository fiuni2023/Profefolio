import React, { useState } from 'react'
import { useGeneralContext } from '../../../../context/GeneralContext'
import { AiOutlinePlus } from 'react-icons/ai'
import { AddButton, MainContainer, TableContainer } from '../../../alumnos/styles/Styles'
import Tabla from '../../../../components/Tabla';
import { toast } from "react-hot-toast";
import StyleComponentBreadcrumb from '../../../../components/StyleComponentBreadcrumb';
import APILINK from '../../../../components/link';
import axios from 'axios';
import { useFetchEffect } from '../../../../components/utils/useFetchEffect';
import { Container, Resumen, SideSection } from './componentes/StyledResumenAsistencia';
import { useNavigate, useParams } from 'react-router-dom';
import { useEffect } from 'react';
import DayMonthPicker from './componentes/DayMonthPicker';
// import {HiXCircle} from 'react-icons/hi'
import { HiCheckCircle, HiXCircle } from 'react-icons/hi'
import {sum} from 'lodash'
import { useModularContext } from '../../context';

// const Asistencia = ({ materia = { id: 1, nombre: "Matemáticas" } }) => {
const Asistencia = React.memo(() => {
    const { getToken, cancan, verifyToken } = useGeneralContext()
    const [condFetch, setCondFetch] = useState(true)
    const {dataSet} = useModularContext()
    const { materiaName, currColegio, currClase } = dataSet

    // const [listaAsistencias, setListaAsistencias] = useState([])
    // const [listaNueva, setListaNueva] = useState([])

    const [tablaAsistencia, setDatosTabla] = useState([]);
    const [datosTablaOriginales, setDatosTablaOriginales] = useState([]);

    const [nuevaAsistencia, setNuevaAsistencia] = useState([])
    const [adding, setAdding] = useState(false)
    const [cantAlumnos, setCantAlumnos] = useState(0)
    const [cantClases, setCantClases] = useState(0)
    const [porcentajes, setPorcentajes] = useState([])
    const { idMateriaLista } = useParams()
    const nombre = "Matemáticas"

    const nav = useNavigate()

    useEffect(() => {
        verifyToken()
        if (!cancan("Profesor")) {
            nav('/')
        } else {
            setCondFetch(true)
        }
    }, [cancan, verifyToken, nav])
    const { doFetch, loading, error } = useFetchEffect(
        () => {
            // return axios.get(`${APILINK}/api/Asistencia/${materia?.id}`, {
            let body = {
                "idMateriaLista": idMateriaLista,
                "mes": 0
            }
            return axios.post(`${APILINK}/api/Asistencia`, body, {
                headers: {
                    Authorization: 'Bearer ' + getToken(),
                    "Content-Type": "application/json"
                },
            }).then(response => response.data);
        },
        [],
        {
            condition: condFetch,
            handleSuccess: (dataAsistencia) => {
                console.log(dataAsistencia)
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
                        setPorcentajes([dato.porcentajePresentes, ...porcentajes])
                        return {
                            fila: dato,
                            datos: [
                                { dato: dato?.nombre ? `${dato.apellido}, ${dato.nombre} ` : " " },
                                ...(dato.asistencias?.length > 0
                                    ? dato.asistencias.map((fecha) => {
                                        return { dato: fecha?.estado ? fecha.estado : "" };
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
                url: `${APILINK}/api/Asistencia/${idMateriaLista}`,
                headers: {
                    'Authorization': `Bearer ${getToken()}`,
                    'Content-Type': 'application/json'
                },
                data: data
            };
            console.log(data)
            axios(config)
                .then(function (response) {
                    if (response.status >= 400) {
                        toast.error("Hubo un error")
                    }
                    else if (response.status >= 200) {
                        toast.success("Guardado correctamente")
                        console.log(response)
                        setAdding(false)
                        doFetch()
                    }
                })
                .catch(function (error) {
                    if (!!typeof (error.response?.data) === "string") {
                        toast.error(error.response.data)
                    } else {
                        console.log(error)
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
            console.log(nuevaAsistencia)
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
            console.log(tablaAsistencia)
            console.log(columnHandler)

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
    const columnHandler = [
        { key: 'datePicker', componente: <DayMonthPicker /> },
        { key: 'xbt', componente: <HiXCircle size={18} color='red' />, action: addDate },
        { key: 'okbt', componente: <HiCheckCircle size={18} color='green' />, action: handleSubmitAddAsistencia }
    ]

    return (
        <>
            <MainContainer>
                {/* <StyleComponentBreadcrumb nombre={`Registro de Asistencia - ${materia?.nombre}`} /> */}
                <StyleComponentBreadcrumb nombre={`${currColegio} - ${currClase} - ${materiaName} - Registro de Asistencia`} />
                <Container>
                    <SideSection>
                        <TableContainer>
                            <Tabla datosTabla={tablaAsistencia} />
                            <AddButton onClick={addDate}>
                                <AiOutlinePlus size={"35px"} />
                            </AddButton>
                        </TableContainer >
                    </SideSection>
                    <SideSection>
                        {!error && !loading &&
                            <Resumen>
                                <p>{cantAlumnos} alumnos</p>
                                <p>{cantClases} {cantClases < 1 ? "Aún no hay clases" : cantClases > 1 ? "clases" : "clase"}</p>
                                <p>{(sum(porcentajes)/(porcentajes?.length ? porcentajes.length : 1)).toFixed(2)} promedio de asistencias</p>
                            </Resumen>
                        }
                    </SideSection>
                </Container>
            </MainContainer >
        </>
    )
})

export default Asistencia