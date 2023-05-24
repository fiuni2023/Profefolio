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
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

const Asistencia = ({ materia = { id: 1, nombre: "Matemáticas" } }) => {
    const { getToken, cancan, verifyToken } = useGeneralContext()
    const [condFetch, setCondFetch] = useState(true)
    const [datosTabla, setDatosTabla] = useState([]);
    // const [listaAsistencias, setListaAsistencias] = useState([])
    // const [listaNueva, setListaNueva] = useState([])
    const nav = useNavigate()

    useEffect(() => {
        verifyToken()
        if (!cancan("Profesor")) {
            nav('/')
        } else {
            setCondFetch(true)
        }
    }, [cancan, verifyToken, nav])
    const { loading } = useFetchEffect(
        () => {
            return axios.get(`${APILINK}/api/Asistencia/${materia?.id}`, {
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
                // setListaAsistencias(dataAsistencia)
                // setListaNueva(dataAsistencia)
                setDatosTabla({
                    tituloTabla: "Asistencias",
                    titulos: [
                        { titulo: "Nombre Alumno" },
                        ...(dataAsistencia[0]?.asistencias?.length > 0
                            ? dataAsistencia[0].asistencias.map((fecha) => {
                                return { titulo: fecha?.fecha ? formatDate(fecha.fecha) : "" };
                            })
                            : [])],
                    clickable: { action: console.log("seleccionado") },
                    filas: dataAsistencia.map((dato) => {
                        return {
                            fila: dato,
                            datos: [
                                { dato: dato?.nombre ? `${dato.apellido}, ${dato.nombre} ` : " " },
                                ...(dato.asistencias?.length > 0
                                    ? dato.asistencias.map((fecha, index) => {
                                        return { key: index, dato: fecha?.estado ? fecha.estado : "" };
                                    })
                                    : [])
                            ],
                        }
                    })
                });
            },
            handleError: () => {
                if (!loading) {
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

    // const addDate = () =>{
    //     const fechaActual = new Date();
    // }

    return (
        <>
            <MainContainer>
                <StyleComponentBreadcrumb nombre={`Registro de Asistencia - ${materia?.nombre}`} />
                <Container>

                    <SideSection>
                        <TableContainer>
                            <Tabla datosTabla={datosTabla} />
                            <AddButton onClick={() => { console.log("setShow(true)") }}>
                                <AiOutlinePlus size={"35px"} />
                            </AddButton>
                        </TableContainer >
                    </SideSection>
                    <SideSection>
                        <Resumen>
                            <p>20 alumnos</p>
                            <p>5 clases - 1 semana</p>
                            <p>75% promedio de asistencias</p>
                        </Resumen>
                    </SideSection>
                </Container>
            </MainContainer >
        </>
    )
}

export default Asistencia