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
import StudentHelper from '../../../alumnos/helpers/StudentHelper'
import { useClaseContext } from '../../../clases/context/ClaseContext';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

const Asistencia = ({ materia = { id: 1, nombre: "Matemáticas" } }) => {
    const { getToken, cancan, verifyToken } = useGeneralContext()
    const { getClaseSelectedId } = useClaseContext();
    const [condFetch, setCondFetch] = useState(true)
    const [datosTabla, setDatosTabla] = useState([]);
    const [listaAlumnos, setListaAlumnos] = useState([])
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
            return axios.get(`${APILINK}/api/Asistencia/${materia?.id}`, {
                headers: {
                    Authorization: 'Bearer ' + getToken()
                }
            }).then(response => response.data);
        },
        [],
        {
            condition: condFetch,
            handleSuccess: (r) => {
                console.log(r)
                setListaAlumnos(r)
                setDatosTabla({
                    tituloTabla: "Asistencias",
                    titulos: [
                        { titulo: "Nombre Alumno" },
                        ...(r[0].asistencias?.length > 0
                            ? r[0].asistencias.map((fecha, index) => {
                                return { key: index, dato: fecha?.fecha ? fecha.fecha : "" };
                            })
                            : [])],
                    clickable: { action: console.log("seleccionado") },
                    filas: r.map((dato) => {
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
            handleError: (r) => {
                if (!loading) {
                    { console.log(r) }
                }
            }
        }
    );

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