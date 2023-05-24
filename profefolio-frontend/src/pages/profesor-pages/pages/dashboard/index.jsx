import React from 'react'
import ContainerColegios from '../../components/ContainerColegios.jsx'
import { SRow } from '../../../../components/componentsStyles/StyledDashComponent.jsx'
import ShowContainer from '../../../clases/components/ShowContainer.jsx'
import Horarios from '../../components/Horarios/index.jsx'
import Eventos from '../../components/Eventos/index.jsx'
import { useGeneralContext } from '../../../../context/GeneralContext.jsx'
import { useModularContext } from '../../context/index.jsx'

const ProfesorPage = () => {
    const {getUserName} = useGeneralContext()
    const {setPage, dataSet, stateController} = useModularContext()

    const { setColegioId } = stateController 
    const { colegios } = dataSet

    const handleClickCards = (id) => {
        setColegioId(id)
        setPage("clase")
    }

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} Dashboard`,
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={colegios}/>
            </SRow>,
            <Horarios/>,
            <Eventos has_clase={true} has_colegio={true} />
        ]
    };
    return <>
        <ShowContainer data={componentes}/>
    </>
}

export default ProfesorPage