import React, { memo } from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { ListTypeEvent } from '../ComponentStyles/ComponentsEvent';
import { TableEvents } from './TableEvents'
import TagEvent from './TagEvent.jsx';




const Eventos = memo(() => {
    return <>
        <SCard>
            <SHeader>Proximos Eventos</SHeader>
            <SBody style={{ height: "430px" }}>
                <TableEvents />
                <ListTypeEvent>
                    <TagEvent name={"Evento"} />
                    <TagEvent name={"Parcial"} />
                    <TagEvent name={"Prueba Sumativa"} />
                    <TagEvent name={"Examen"} />
                </ListTypeEvent>
            </SBody>
        </SCard>
    </>
})

export default Eventos