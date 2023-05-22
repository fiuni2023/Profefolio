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

const Asistencia = () => {
    const { getToken } = useGeneralContext()
    // const [materia, setMateria] = useState("Matematicas")
    // const [materiaLista, setMateriaLista] = useState(1)
    // const [condFetch, setCondFetch] = useState(true)
    let materia = "Matemáticas"
    let materiaLista = 2
    let condFetch = true
    const [datosTabla, setDatosTabla] = useState([]);



    // const nav = useNavigate()

    // useEffect(() => {
    //     verifyToken()
    //     if (!cancan("Profesor")) {
    //         nav('/')
    //     } else {
    //         setCondFetch(true)
    //     }
    // }, [cancan, verifyToken, nav])

    const { loading, error } = useFetchEffect(
        () => {
            return axios.get(`${APILINK}/api/Asistencia/${materiaLista}`, {
                headers: {
                    Authorization: getToken()
                }
            }).then(response => response.data);
        },
        [materiaLista, getToken, condFetch],
        {
            condition: condFetch,
            handleSuccess: (r) => {
                setDatosTabla({
                    ...datosTabla,
                    clickable: { action: console.log("seleccionado") },
                    filas: r.data.dataList.map((dato) => {
                        return {
                            fila: dato,
                            datos: dato.asistencias.map((asistencia) => {
                                return { dato: asistencia.estado }
                            })
                        }
                    })
                });
            },
            handleError: () => {
                if (!loading) {
                    toast.error('No se pudieron obtener los datos. Intente recargar la página');
                }
            }
        }
    );

    return (
        <>
            <MainContainer>
                <StyleComponentBreadcrumb nombre={`Registro de Asistencia - ${materia}`} />
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