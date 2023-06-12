import React from 'react'
import ContainerColegios from '../../components/ContainerColegios.jsx'
import { SRow } from '../../../../components/componentsStyles/StyledDashComponent.jsx'
import ShowContainer from '../../../clases/components/ShowContainer.jsx'
import Horarios from '../../components/Horarios/index.jsx'
import Eventos from '../../components/Eventos/index.jsx'
import EventosTabla from "../../components/EventosTabla";
import { useGeneralContext } from '../../../../context/GeneralContext.jsx'
import { useModularContext } from '../../context/index.jsx'
import Spinner from '../../../../components/componentsStyles/SyledSpinner.jsx'

const ProfesorPage = () => {
    const {getUserName} = useGeneralContext()
    const {setPage, dataSet, stateController} = useModularContext()

    const { setColegioId, setCurrColegio } = stateController 
    const { colegios, loading } = dataSet

    const handleClickCards = (id, nombre) => {
        setCurrColegio(nombre); 
        setColegioId(id)
        setPage("clase")
    }

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} `,
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={colegios}/>
            </SRow>,
            <Horarios/>,
            <Eventos tablaEventos={<EventosTabla has_colegio={true} has_clase={true} />} />
        ]
    };
    return <>
        {loading ? <Spinner height={"calc(100vh - 90px)"}></Spinner> : 
            <ShowContainer data={componentes}/>
        }
    </>
}

export default ProfesorPage