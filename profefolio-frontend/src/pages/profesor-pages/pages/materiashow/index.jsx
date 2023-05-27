import React, { useEffect, useState } from "react";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import PromedioPuntaje from "../../components/PromedioPuntaje";
import PromedioAsistencia from "../../components/PromedioAsistencia";
import MateriaCards from "../../components/MateriaCards";
import BackButton from "../../components/BackButton";
import MateriaHorario from "../../components/MateriaHorario";

const ProfesorMateriaShow = () => {
    const {getUserName} = useGeneralContext()
    const {setPage, dataSet} = useModularContext()

    const { materiaShow, materiaName } = dataSet

    // const handleClickCards = () => {
    //     setPage("dashboard")
    // }

    const [materiaMapped, setMateriaMapped] = useState({
                anotations: 0,
                name: "",
                calification_count: 0,
                event_yet: 0,
                classes_yet: 0,
                documents: 0,
                asistencias: 0
            })

    useEffect(()=>{
        if(materiaShow && materiaName){
            const newMateria ={
                anotations: materiaShow.anotaciones,
                name: materiaName,
                calification_count: materiaShow.calificaciones?.calificaciones,
                event_yet: materiaShow.calificaciones?.sinCalificaciones,
                classes_yet: 1,
                documents: materiaShow.documentos,
                asistencias: materiaShow.asistencias
            }
            setMateriaMapped(newMateria)
        }
    }, [materiaShow, materiaName])


    const config = {
        onAnotation: ()=>{setPage("anotacion")},
        onAsistencia: ()=>{setPage("asistencia")},
        onDocumento: ()=>{setPage("documento")},
        onEvaluacion: ()=>{setPage("evaluaciones")}
    }

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} - ${materiaName}`,
        componentes: [
            <SRow>
                <MateriaCards materia={materiaMapped} configuration={config} />
            </SRow>,
            <PromedioPuntaje/>,
            <PromedioAsistencia/>
        ]
    };
    return (
        <>  
            <div className="d-flex align-items-center justify-content-between">
                <BackButton to="materia" />
                <MateriaHorario />
            </div>
            <ShowContainer data={componentes}/>
        </>
    )
}

export default ProfesorMateriaShow