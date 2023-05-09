import React, { memo, useEffect, useState} from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent';
import { useGeneralContext } from '../../../../context/GeneralContext';


import HorarioService from "../../helpers/HorariosHelpers.js"
import SCalendar from '../Calendar';


const Horarios = memo(() => {
    const { getToken } = useGeneralContext();
    const [events, setEvents] = useState([]);

    useEffect(() => {
        const getHorarios = async () => {
            
            const result = await HorarioService.getHorariosColegios(getToken());
            if (result !== null) {
                setEvents(HorarioService.mapperHorariosByColegio(result))
            } else {
                console.log("Error al obtener el horario")
            }
        }
        getHorarios();
    }, [getToken]);


    return <>
        <SCard>
            <SHeader>Horarios de Clases</SHeader>
            <SBody>
                <SCalendar events={events}/>
            </SBody>
        </SCard>
    </>
})

export default Horarios