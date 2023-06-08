import React, { useEffect, useState } from "react";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";
import Alumnos from "../../components/Alumnos";
import Eventos from "../../components/Eventos";
import ContainerColegios from "../../components/ContainerColegios";

const ProfesorMateria = () => {
    const {setPage, dataSet, stateController} = useModularContext()

    const {materias, currColegio, currClase} = dataSet
    const {setMateriaId, setMateriaName} = stateController

    const handleClickCards = (id) => {
        const {idM, nombreM} = id
        setMateriaName(nombreM)
        setMateriaId(idM)
        setPage("materiashow")
    }

    const [ materiasMapped, setMateriasMapped ] = useState([])

    useEffect(()=>{
        setMateriasMapped(materias?.map((m)=>{
            return {
                id: {idM:m.id, nombreM: m.nombre},
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

    const componentes = {
        title: `${currColegio} - ${currClase} - Lista de Materias de la clase`,
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