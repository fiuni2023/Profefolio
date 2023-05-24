import React, { useEffect, useState } from "react";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";
import Alumnos from "../../components/Alumnos";
import Eventos from "../../components/Eventos";
import ContainerColegios from "../../components/ContainerColegios";

const ProfesorMateria = () => {
    const {getUserName} = useGeneralContext()
    const {setPage, dataSet, stateController} = useModularContext()

    const {materias} = dataSet
    const {setMateriaId} = stateController

    const handleClickCards = (id) => {
        setMateriaId(id)
        setPage("materiashow")
    }

    const [ materiasMapped, setMateriasMapped ] = useState([])

    useEffect(()=>{
        setMateriasMapped(materias?.map((m)=>{
            return {
                id: m.id,
                nombre: m.nombre,
                anotaciones: m.anotaciones,
                calificaciones: m.calificaciones,
                eventos: m.eventos,
                horario : m.horario.dia === ""? null : [
                    m.horario
                ],
                duracionHrs: ""
            }
        }))
    }, [materias])

    console.log(materiasMapped)

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} Materia`,
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={materiasMapped}/>
            </SRow>,
            <Alumnos />,
            <Eventos />
        ]
    };
    return (
        <>  
            <BackButton to="clase" />
            <ShowContainer data={componentes}/>
        </>
    )
}

export default ProfesorMateria