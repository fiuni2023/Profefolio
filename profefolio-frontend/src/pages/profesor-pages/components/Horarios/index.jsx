import React, { memo, useCallback, useEffect, useId, useState } from 'react'
import styled from 'styled-components'
import { groupBy, map, find } from "lodash"
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent';
import axios from 'axios';
import APILINK from '../../../../components/link';
import { useGeneralContext } from '../../../../context/GeneralContext';

import { Calendar, momentLocalizer, dateFnsLocalizer } from 'react-big-calendar'
import moment from 'moment'

import "react-big-calendar/lib/css/react-big-calendar.css";


const STablaHorarios = styled.table`

`
const getColors = (color) => {
    switch (color) {
        case "bluesky":
            return "#C1E1FA";
        case "purple":
            return "#C8BFD9";
        case "yellow":
            return "#F6E7A7";
        case "orange":
            return "#FCC6AC";
        case "blue":
            return "#8DACE1";
        case "green":
            return "#59C8A4";
        case "pink":
            return "#F2BFD3";
        case "salmon":
            return "#F5918E"
        default:
            return "#C2C2C2"
    }
}
const TablaHorarios = () => {
    const { getToken } = useGeneralContext();

    const [calendar, setCalendar] = useState([]);

    const peticionCalendar = useCallback(async () => {
        const response = await axios.get(`${APILINK}/api/HorasCatedrasMaterias`, {
            headers: {
                Authorization: "Bearer " + getToken(),
            }
        });
        return response.status === 200 ? response.data : null
    }, [getToken])

    useEffect(() => {
        const getHorarios = async () => {
            console.log("Pidiendo Horario...")
            const result = await peticionCalendar();
            console.log("RESULT", result);
            if (result !== null) {
                setCalendar(result)
                /* setCalendar(map(result, (e) => e.fin.startsWith("08") ? (() => {
                    e.fin = "08:20"; 
                    return e
                })() : e)) */

            } else {
                console.log("Error al obtener el horario")
            }
        }
        getHorarios();
    }, [peticionCalendar]);

    /* const colorsColegios = [];
    console.log(Calendar)
    const addColorColegios = (idColegio) => {
        const listColorsName = ["bluesky", "yellow", "green", "orange", "blue", "purple", "pink", "salmon"]

        const colegio = find(colorsColegios, a => a.idColegio === idColegio)

        if (!!colegio) {
            return getColors(colegio.colorName)
        }
        else {
            const colorName = listColorsName[colorsColegios.length]
            const colorColegio = { idColegio: idColegio, colorName: colorName }
            colorsColegios.push(colorColegio)

            return getColors(colorName)
        }
    }
    const grupo = groupBy(Calendar, a => a.dia)
    console.log("GRUPO", grupo)
    const headers = []

    for (const key in grupo) {
        headers.push(key);
    }
    // recorrer cada key y verificar si hay un elemento para cada hora y minuto especifico
    // si hay coincidencia se agrega  a la lista de rows(lista de listas ) 
    // se detiene cuando ninguna key no tenga mas elementos 
    const IdGen = () => {
        //const idGenerator = useId()
        //return idGenerator
        return Math.random() * 10000;
    };

    let bandera = true;
    let elementoPosition = 0;
    let hora = 7;
    let minuto = 0;
    let rows = [];


    let maxLength = 0;
    while (bandera) {

        const rowDias = []
        if (minuto === 0) {
            rowDias.push(<td className={`cells-hours ${hora % 2 !== 0 ? "cell-striped" : ""}`} key={IdGen()} rowSpan={6}>{hora}</td>)
        }
        console.log(maxLength)
        //rowDias.push(<td key={IdGen()}>{`${hora < 10 ? `0${hora}` : hora}:${minuto < 10 ? `0${minuto}` : minuto}`}</td>)
        rowDias.push(<td className={`cells-minutes ${hora % 2 !== 0 ? "cell-striped" : ""}`} key={IdGen()}>{`${minuto < 10 ? `0${minuto}` : minuto}`}</td>)
        for (const key in grupo) {
            maxLength = maxLength < grupo[key].length ? grupo[key].length : maxLength;
            if (grupo[key][elementoPosition]) {
                const element = grupo[key][elementoPosition]
                const horaMinuto = element.inicio.split(":")
                const horaMinutoFin = element.fin.split(":")

                const fechaInicio = new Date();
                const fechaFin = new Date();

                fechaInicio.setHours(parseInt(horaMinuto[0]), parseInt(horaMinuto[1]), 0, 0)
                fechaFin.setHours(parseInt(horaMinutoFin[0]), parseInt(horaMinutoFin[1]), 0, 0)


                // se verifica si la hora de inicio de la materia coincide con el de la hora actual 
                // si es asi se agrega la celda de con la cantidad de filas que ocupa
                if ((parseInt(horaMinuto[0]) === hora && parseInt(horaMinuto[1]) === minuto)) {
                    // se calcula la cantidad de filas que ocupara la materia en la tabla
                    const rowSpan = Math.ceil(Math.abs(fechaInicio - fechaFin) / 60000 / 10) + 1;

                    rowDias.push(<td
                        key={IdGen()}
                        rowSpan={rowSpan}
                        style={{ "backgroundColor": addColorColegios(grupo[key][elementoPosition].colegioId), "fontWeight": "bold" }}
                    >
                        {
                            grupo[key][elementoPosition].nombreColegio
                        }
                    </td>)
                } else {
                    // para comprobar si la hora esta dentro del rango del inicio y fin de una materia
                    // si esta no se agrega ninguna celda
                    const horaActual = new Date();
                    horaActual.setHours(hora, minuto, 0, 0, 0)
                    if (!(horaActual >= fechaInicio && horaActual <= fechaFin)) {
                        rowDias.push(<td className={`${hora % 2 !== 0 ? "cell-striped" : ""}`} key={IdGen()}></td>)
                    }
                }
            } else {
                rowDias.push(<td className={`${hora % 2 !== 0 ? "cell-striped" : ""}`} key={IdGen()}></td>)
            }
        }

        // para que la hora se vaya aumentando si se llega a pasar 50 minutos
        minuto += 10;
        if (minuto > 50) {
            minuto = 0;
            hora++;
        }

        elementoPosition++;

        if (maxLength <= elementoPosition || hora === 20) {
            bandera = false;
        } else {
            elementoPosition = 0;
        }

        rows.push(rowDias);
    } */

    const events = [
        {
            id: 1,
            title: 'Evento 1',
            start: new Date(2023, 4, 8, 10, 0), // Fecha y hora de inicio del evento
            end: new Date(2023, 4, 8, 12, 0), // Fecha y hora de finalización del evento
            // Otras propiedades personalizadas del evento, como color, descripción, etc.
        },
        {
            id: 2,
            title: 'Evento 2',
            start: new Date(2023, 4, 9, 14, 0),
            end: new Date(2023, 4, 9, 16, 0),
            color: "#F6E7A7"
            // Otras propiedades personalizadas del evento
        },
        // Otros eventos...
    ];
    const localizer = momentLocalizer(moment)

    const minTime = new Date();
    minTime.setHours(6, 0, 0); // Establecer el mínimo horario a las 06:00 de la mañana

    const maxTime = new Date();
    maxTime.setHours(20, 0, 0); // Establecer el máximo horario a las 20:00 (8:00 PM)

    const formats = {
        timeGutterFormat: (date, culture, localizer) =>
          localizer.format(date, 'H:mm', culture), // Formato de las horas en la columna lateral
        eventTimeRangeFormat: ({ start, end }, culture, localizer) =>
          `${localizer.format(start, 'H:mm', culture)} - ${localizer.format(end, 'H:mm', culture)}`, // Formato de las horas en los eventos
        slotLabelFormat: (date, culture, localizer) =>
          localizer.format(date, 'H:mm', culture), // Formato de las horas en las etiquetas de los intervalos de tiempo
    };
    return <>
        <Calendar
            localizer={localizer}
            events={events}
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
        {/* <Calendar
            events={events}
            step={60}
            views={['work_week']}
            defaultDate={new Date(2015, 3, 1)}
            popup={false}
            onShowMore={(events, date) => this.setState({ showModal: true, events })}
        /> */}
        {/* <STablaHorarios>
            <thead>
                <tr>
                    <th className='col-horas' colSpan={2}>Horas</th>
                    {headers.map((header, i) => <th key={i + header}>{header}</th>)}
                </tr>
            </thead>
            <tbody>
                {map(rows, (e, i) => <tr key={i}>
                    {e}
                </tr>)
                }

            </tbody>
        </STablaHorarios> */}
        {/* <style jsx="true">
            {
                `
                        .cell-striped{
                            background-color: rgb(238, 238, 238);
                        }

                        td:hover, th:hover{
                            transform: scale(1.1);
                        } 
                    `
            }
        </style> */}
    </>
}
const Horarios = memo(() => {
    return <>

        <SCard>
            <SHeader>Horarios</SHeader>
            <SBody>
                <div className="container-visualizacion">
                    <TablaHorarios />
                </div>
            </SBody>
        </SCard>

        {/* <style jsx="true">
            {
                `
                    .container-visualizacion{
                        border-radius: 5px;
                        background-color: white;
                        min-height: 300px;
                        padding: 1rem;
                        max-height: 500px;
                        overflow-y:auto;
                        overflow-x:hidden;
                    }

                    .container-visualizacion::-webkit-scrollbar {
                        width: 10px;
                    }
                    .container-visualizacion::-webkit-scrollbar-track
                    {
                        margin-top: 16px;
                        margin-bottom: 16px;
                        
                    }
                    .container-visualizacion::-webkit-scrollbar-thumb {
                        background: rgb(180, 180, 180); 
                        border-radius: 10px;
                    }

                    table, th, td {
                        border: 1px solid rgb(200, 200, 200);
                        border-collapse: collapse;
                    }
                    table {
                        width: 100%;
                        table-layout: auto;
                    }

                    
                    td, th{
                        min-width: 40px;
                        max-width: 100px;
                        overflow: hidden;
                    }

                    .col-horas{
                        max-width: 30px;
                    }

                    .cells-hours{
                        min-width:9;
                        max-width:10;
                    }
                    .cells-minutes{
                        min-width:19;
                        max-width:20;
                    }
                    tbody > tr:nth-child(6n + 1) > td:nth-child(2), tbody > tr:nth-child(n + 1) > td:nth-child(1){
                        font-weight: bold; 
                    } 

                    tbody {
                        max-height: 300px;
                        overflow-y: auto;
                        display block;
                    }
                `
            }
        </style> */}
    </>
})

export default Horarios