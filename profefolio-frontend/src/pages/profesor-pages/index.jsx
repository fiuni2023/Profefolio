import React from 'react'
import ContainerColegios from './components/ContainerColegios.jsx'
import { SRow } from '../../components/componentsStyles/StyledDashComponent.jsx'
import ShowContainer from '../clases/components/ShowContainer.jsx'
import Horarios from './components/Horarios/index.jsx'
import Eventos from './components/Eventos/index.jsx'
import { useGeneralContext } from '../../context/GeneralContext.jsx'

const ProfesorPage = () => {
    const {getUserName} = useGeneralContext()

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()}`,
        componentes: [
            <SRow>
                <ContainerColegios/>
            </SRow>,
            <Horarios/>,
            <Eventos/>
        ]
    };
    return <>
        <ShowContainer data={componentes}/>
    </>
}

export default ProfesorPage