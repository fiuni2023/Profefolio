import React, { useEffect, useState } from "react";
import ContainerColegios from "../../components/ContainerColegios";
import Horarios from "../../components/Horarios";
import Eventos from "../../components/Eventos";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";
import Spinner from "../../../../components/componentsStyles/SyledSpinner";

const ProfesorClase = () => {
    const {getUserName} = useGeneralContext()
    const {setPage, dataSet, stateController} = useModularContext()

    const {clases, loading} = dataSet
    const {setClaseId} = stateController

    const handleClickCards = (id) => {
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
        title: `Bienvenido Prof. ${getUserName()} - Lista de Clases`,
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
            {loading ? 
                    <Spinner height={"calc(100vh - 90px)"}></Spinner>
                :   <ShowContainer data={componentes}/>
            }
        </>
    )
}

export default ProfesorClase