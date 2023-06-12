import React, { useEffect, useState } from "react";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import PromedioPuntaje from "../../components/PromedioPuntaje";
import PromedioAsistencia from "../../components/PromedioAsistencia";
import MateriaCards from "../../components/MateriaCards";
import BackButton from "../../components/BackButton";
import MateriaHorario from "../../components/MateriaHorario";
import Spinner from "../../../../components/componentsStyles/SyledSpinner";
import axios from 'axios';
import { useNavigate } from 'react-router';
import APILINK from "../../../../components/link";
import { useGeneralContext } from "../../../../context/GeneralContext";
const ProfesorMateriaShow = () => {
    const { setPage, dataSet, stateController } = useModularContext();
    const { getToken, cancan, verifyToken } = useGeneralContext();
    const { materiaId } = stateController
    const { materiaShow, materiaName, loading, currColegio, currClase } = dataSet
    const [datosDashboard, setDatosDashboard] = useState([]);
    const nav = useNavigate()

    useEffect(() => {
        verifyToken()
        if (!cancan("Profesor")) {
            nav("/")
        } else {

            let data = JSON.stringify({
                opcion: "cards-materia",
                id: materiaId,                              // id materiaLista
                anho: 2023
            });
            axios.post(`${APILINK}/api/DashboardProfesor`, data, {
                headers: {
                    Authorization: `Bearer ${getToken()}`,
                    "Content-Type": "application/json"
                }
            })
                .then(response => {
                    setDatosDashboard(response.data)
                    console.log(datosDashboard)

                })
                .catch(error => {
                    console.error(error);

                });

        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [cancan, verifyToken, getToken]);

    const [materiaMapped, setMateriaMapped] = useState({
        anotations: 0,
        name: "",
        calification_count: 0,
        event_yet: 0,
        classes_yet: 0,
        documents: 0,
        asistencias: 0
    })

    useEffect(() => {
        if (materiaShow && materiaName) {
            const newMateria = {
                anotations: materiaShow.anotaciones,
                name: materiaName,
                calification_count: materiaShow.calificaciones,
                event_yet: materiaShow.calificaciones?.sinCalificaciones,
                classes_yet: 1,
                documents: materiaShow.documentos,
                asistencias: materiaShow.asistencias
            }
            setMateriaMapped(newMateria)
        }
    }, [materiaShow, materiaName])


    const config = {
        onAnotation: () => { setPage("anotacion") },
        onAsistencia: () => { setPage("asistencia") },
        onDocumento: () => { setPage("documento") },
        onEvaluacion: () => { setPage("evaluaciones") }
    }

    const componentes = {
        title: `${currColegio} - ${currClase} - ${materiaName}`,
        componentes: [
            <SRow>
                <MateriaCards materia={materiaMapped} configuration={config} />
            </SRow>,
            <PromedioPuntaje />,
            <PromedioAsistencia />
        ]
    };
    return (
        <>
            <div className="d-flex align-items-center justify-content-between">
                <BackButton to="materia" />
                <MateriaHorario />
            </div>
            {loading ?
                <Spinner height={"calc(100vh - 90px)"}></Spinner>
                : <ShowContainer data={componentes} />
            }
        </>
    )
}

export default ProfesorMateriaShow