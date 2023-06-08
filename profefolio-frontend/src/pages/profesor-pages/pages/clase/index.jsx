import React, { useEffect, useState } from "react";
import ContainerColegios from "../../components/ContainerColegios";
import Horarios from "../../components/Horarios";
import Eventos from "../../components/Eventos";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";

const ProfesorClase = () => {
    const {setPage, dataSet, stateController} = useModularContext()

    const {clases, currColegio} = dataSet
    const {setClaseId, setCurrClase} = stateController

    const handleClickCards = (id, nombre) => {
        setCurrClase(nombre)
        setClaseId(id)
        setPage("materia")
    }

    const [clasesMapped, setClasesMapped] = useState([])

    useEffect(()=>{
        if(clases){
            setClasesMapped(clases.map(c=>{return {...c, duracionHrs: ""}}))
        }
    }, [clases])

    const componentes = {
        title: `${currColegio} - Lista de Clases del colegio`,
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={clasesMapped}/>
            </SRow>,
            <Horarios/>,
            <Eventos/>
        ]
    };
    return (
        <>
            <BackButton to="dashboard" />
            <ShowContainer data={componentes}/>
        </>
    )
}

export default ProfesorClase