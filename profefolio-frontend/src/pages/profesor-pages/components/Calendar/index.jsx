import React, { memo } from 'react'
import { Calendar, momentLocalizer} from 'react-big-calendar'
import moment from 'moment'
import "react-big-calendar/lib/css/react-big-calendar.css";


const SCalendar = memo(({events = []}) => {
    const localizer = momentLocalizer(moment)

    const minTime = new Date();
    minTime.setHours(7, 0, 0);

    const maxTime = new Date();
    maxTime.setHours(20, 0, 0); // Establecer el máximo horario a las 20:00 (8:00 PM)

    // formato de hora
    const formats = {
        timeGutterFormat: (date, culture, localizer) =>
            localizer.format(date, 'H:mm', culture), // Formato de las horas en la columna lateral
        eventTimeRangeFormat: ({ start, end }, culture, localizer) =>
            `${localizer.format(start, 'H:mm', culture)} - ${localizer.format(end, 'H:mm', culture)}`, // Formato de las horas en los eventos
        slotLabelFormat: (date, culture, localizer) =>
            localizer.format(date, 'H:mm', culture), // Formato de las horas en las etiquetas de los intervalos de tiempo
    };

    //stylos de celdas
    const eventStyleGetter = (event) => {
        // Personalizar los estilos según las propiedades del evento
        const backgroundColor = event.color; // Propiedad "color" en el evento
        const style = {
            backgroundColor,
            color: "black",
            border: "1px solid rgb(180, 180, 180)",
            borderCollapse: "collapse"
        };
        return {
            style
        };
    };



    return <>
        <Calendar
            localizer={localizer}
            events={events}
            eventPropGetter={eventStyleGetter}
            defaultView="week"
            startAccessor="start"
            endAccessor="end"
            formats={formats}
            style={{ height: 400 }}
            step={10}
            min={minTime}
            max={maxTime}
            onShowMore={(events, date) => this.setState({ showModal: true, events })}
            timeslots={1} // number of per section
            toolbar={false}
            popup={true}
        />
        <style jsx="true">
            {
                `
                .rbc-day-slot .rbc-events-container{
                        margin-right: 0px;
                }

                .rbc-row-content{
                    display: none;
                }

                .rbc-time-header.rbc-overflowing{
                    margin-right: 8px !important;
                }

                .rbc-time-content::-webkit-scrollbar {
                    width: 10px;
                }
                
                .rbc-time-content::-webkit-scrollbar-thumb {
                    background: rgb(180, 180, 180); 
                    border-radius: 10px;
                }
                `
            }
        </style>
    </>
})
export default SCalendar