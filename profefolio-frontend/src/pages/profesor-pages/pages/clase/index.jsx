import React, { useEffect, useState } from "react";
import ContainerColegios from "../../components/ContainerColegios";
import Horarios from "../../components/Horarios";
import Eventos from "../../components/Eventos";
import EventosTabla from "../../components/EventosTabla";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";
import Spinner from "../../../../components/componentsStyles/SyledSpinner";


const ProfesorClase = () => {
    const {setPage, dataSet, stateController} = useModularContext()

    const {clases,eventosColegio, loading, currColegio} = dataSet
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
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={clasesMapped}/>
            </SRow>,
            <Horarios/>,
            <Eventos tablaEventos={<EventosTabla has_colegio={false} has_clase={true} lista={eventosColegio} />} />
        ]
    };
    return (
        <>
            <div className="d-flex align-items-center gap-4 ms-4 mt-4">
                <BackButton to="dashboard" />
                <h5 className="m-0">{`${currColegio} - Lista de Clases del colegio`}</h5>
            </div>
            {loading ? 
                    <Spinner height={"calc(100vh - 90px)"}></Spinner>
                :   <ShowContainer data={componentes}/>
            }
        </>
    )
}

export default ProfesorClase