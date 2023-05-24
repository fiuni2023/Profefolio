import React, { useEffect, useState } from "react";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import PromedioPuntaje from "../../components/PromedioPuntaje";
import PromedioAsistencia from "../../components/PromedioAsistencia";
import MateriaCards from "../../components/MateriaCards";
import BackButton from "../../components/BackButton";

const ProfesorMateriaShow = () => {
    const {getUserName} = useGeneralContext()
    const {setPage, dataSet} = useModularContext()

    const { materiaShow, materiaName } = dataSet

    console.log(materiaShow)

    // const handleClickCards = () => {
    //     setPage("dashboard")
    // }

    const [materiaMapped, setMateriaMapped] = useState({})

    useEffect(()=>{
        if(materiaShow && materiaName){
            const newMateria ={
                anotations: materiaShow.anotaciones,
                name: materiaName,
                calification_count: materiaShow.calificaciones.calificaciones,
                event_yet: materiaShow.calificaciones.sinCalificaciones,
                classes_yet: 1,
                documents: materiaShow.documentos,
                asistencias: materiaShow.asistencias
            }
            setMateriaMapped(newMateria)
        }
    }, [materiaShow, materiaName])


    const config = {
        onAnotation: ()=>{setPage("anotacion")}
    }

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} Materia`,
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
            <BackButton to="materia" />
            <ShowContainer data={componentes}/>
        </>
    )
}

export default ProfesorMateriaShow