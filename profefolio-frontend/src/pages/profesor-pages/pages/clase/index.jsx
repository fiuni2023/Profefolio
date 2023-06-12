import React, { useEffect, useState } from "react";
import ContainerColegios from "../../components/ContainerColegios";
import Horarios from "../../components/Horarios";
import Eventos from "../../components/Eventos";
import EventosColegio from "../../components/EventosTabla/eventosColegio";
import EventosTabla from "../../components/EventosTabla";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";
import Spinner from "../../../../components/componentsStyles/SyledSpinner";


const ProfesorClase = () => {
    const {setPage, dataSet, stateController} = useModularContext()

    const {clases,eventos, loading, currColegio} = dataSet
    const {setClaseId, setCurrClase} = stateController

    const handleClickCards = (id, nombre) => {
        setCurrClase(nombre)
        setClaseId(id)
        setPage("materia")
    }

    const [clasesMapped, setClasesMapped] = useState([])

    
    useEffect(()=>{
        console.log("...",eventos);
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
            <Eventos tablaEventos={<EventosColegio has_colegio={false} has_clase={true} lista={eventos} />} />
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