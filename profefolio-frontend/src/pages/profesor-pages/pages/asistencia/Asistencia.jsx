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

// const Asistencia = ({ materia = { id: 1, nombre: "Matemáticas" } }) => {
const Asistencia = () => {
    const { getToken, cancan, verifyToken } = useGeneralContext()
    const [condFetch, setCondFetch] = useState(true)

    // const [listaAsistencias, setListaAsistencias] = useState([])
    // const [listaNueva, setListaNueva] = useState([])

    const [tablaAsistencia, setDatosTabla] = useState([]);
    const [datosTablaOriginales, setDatosTablaOriginales] = useState([]);

    const [nuevaAsistencia, setNuevaAsistencia] = useState([])
    const [adding, setAdding] = useState(false)
    const [cantAlumnos, setCantAlumnos] = useState(0)
    const [cantClases, setCantClases] = useState(0)
    const { idMateriaLista } = useParams()
    const nombre = "Matemáticas"

    //Fechas
    const [selectedDate, setSelectedDate] = useState(new Date());

    const handleDateChange = (event) => {
        const { value } = event.target;
        const [year, month, day] = value.split('-');
        const newDate = new Date(year, month - 1, day);
        setSelectedDate(newDate);
    };


    const nav = useNavigate()

    useEffect(() => {
        verifyToken()
        if (!cancan("Profesor")) {
            nav('/')
        } else {
            setCondFetch(true)
        }
    }, [cancan, verifyToken, nav])
    const { loading, error } = useFetchEffect(
        () => {
            // return axios.get(`${APILINK}/api/Asistencia/${materia?.id}`, {
            return axios.get(`${APILINK}/api/Asistencia/${idMateriaLista}`, {
                headers: {
                    Authorization: 'Bearer ' + getToken()
                }
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
                        { titulo: "Nombre Alumno" },
                        ...(dataAsistencia[0]?.asistencias?.length > 0
                            ? dataAsistencia[0].asistencias.map((fecha) => {
                                return { titulo: fecha?.fecha ? formatDate(fecha.fecha) : "" };
                            })
                            : []),
                        { titulo: "%" }
                    ],
                    filas: dataAsistencia.map((dato) => {
                        return {
                            fila: dato,
                            datos: [
                                { dato: dato?.nombre ? `${dato.apellido}, ${dato.nombre} ` : " " },
                                ...(dato.asistencias?.length > 0
                                    ? dato.asistencias.map((fecha, index) => {
                                        return { dato: fecha?.estado ? fecha.estado : "" };
                                    })
                                    : []),
                                { dato: 50 }
                            ],
                        }
                    })
                }
                setDatosTablaOriginales(DatosAsistencia)
                setDatosTabla(DatosAsistencia)
                setNuevaAsistencia(DatosAsistencia.filas.map((fila) => {
                    return {
                        id: fila.fila.id,
                        fecha: selectedDate,
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

    const formatDate = (date) => {
        const newDate = new Date(date);
        const day = newDate.getDate();
        const month = newDate.getMonth() + 1;
        return `${day < 10 ? '0' : ''}${day}/${month < 10 ? '0' : ''}${month}`;
    }

    const handleSelect = (indice, nuevoValor) => {
        setNuevaAsistencia((prevAsistencia) => {
            const nuevaAsistencia = [...prevAsistencia];
            nuevaAsistencia[indice] = nuevoValor;
            return nuevaAsistencia;
        });
    };

    const selectEstado = (estado, id) => {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', width: '100%' }}>
                <select defaultValue={estado} onChange={(event) => handleSelect(id, event.target.value)}>
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
            const nuevosTitulos = [
                ...tablaAsistencia.titulos.slice(0, -1),
                { componente: <DayMonthPicker handleDateChange={handleDateChange} selectedDate={selectedDate} /> }
            ];
            const nuevasFilas = tablaAsistencia.filas.map((fila, index) => {
                return { datos: [...fila.datos.slice(0, -1), { componente: selectEstado(nuevaAsistencia[index], index) }], fila: fila.fila }
            });

            setDatosTabla(prevState => ({
                ...prevState,
                titulos: nuevosTitulos,
                filas: nuevasFilas
            }));
        } else{
            setDatosTabla(datosTablaOriginales)
        }
        setAdding(prevState => !prevState)
    }

    return (
        <>
            <MainContainer>
                {/* <StyleComponentBreadcrumb nombre={`Registro de Asistencia - ${materia?.nombre}`} /> */}
                <StyleComponentBreadcrumb nombre={`Registro de Asistencia - ${nombre}`} />
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
                                <p>{cantClases} {cantClases > 1 ? "clases" : "clase"}</p>
                                <p>75% promedio de asistencias</p>
                            </Resumen>
                        }
                    </SideSection>
                </Container>
            </MainContainer >
        </>
    )
}

export default Asistencia