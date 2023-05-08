import React, { useId } from 'react'
import styled from 'styled-components'
import { Calendar } from '../../api/calendar';
import { orderBy, groupBy, sortBy, map } from "lodash"
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent';


const STablaHorarios = styled.table`

`
const TablaHorarios = () => {


    const grupo = groupBy(Calendar, a => a.dia)

    const headers = []

    for (const key in grupo) {
        headers.push(key);
    }
    // recorrer cada key y verificar si hay un elemento para cada hora y minuto especifico
    // si hay coincidencia se agrega  a la lista de rows(lista de listas ) 
    // se detiene cuando ninguna key no tenga mas elementos 
    const IdGen = () => {
        const idGenerator = useId()
        return idGenerator
    };

    let bandera = true;
    let elementoPosition = 0;
    let hora = 7;
    let minuto = 0;
    let rows = [];


    while (bandera) {

        const rowDias = []
        if (minuto === 0) {
            rowDias.push(<td key={IdGen()} rowSpan={6}>{hora}</td>)
        }
        let maxLength = 0;
        //rowDias.push(<td key={IdGen()}>{`${hora < 10 ? `0${hora}` : hora}:${minuto < 10 ? `0${minuto}` : minuto}`}</td>)
        rowDias.push(<td key={IdGen()}>{`${minuto < 10 ? `0${minuto}` : minuto}`}</td>)
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

                    rowDias.push(<td key={IdGen()} rowSpan={rowSpan}>{grupo[key][elementoPosition].nombreColegio}</td>)
                } else {
                    // para comprobar si la hora esta dentro del rango del inicio y fin de una materia
                    // si esta no se agrega ninguna celda
                    const horaActual = new Date();
                    horaActual.setHours(hora, minuto, 0, 0, 0)
                    if (!(horaActual >= fechaInicio && horaActual <= fechaFin)) {
                        rowDias.push(<td key={IdGen()}></td>)
                    }
                }
            } else {
                rowDias.push(<td key={IdGen()}></td>)
            }
        }

        // para que la hora se vaya aumentando si se llega a pasar 50 minutos
        minuto += 10;
        if (minuto > 50) {
            minuto = 0;
            hora++;
        }

        elementoPosition++;

        if (maxLength <= elementoPosition || hora === 23) {
            bandera = false;
        } else {
            elementoPosition = 0;
        }

        rows.push(rowDias);
    }

    return <>
        <STablaHorarios>
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
        </STablaHorarios>
    </>
}
const Horarios = () => {
    return <>

        <SCard>
            <SHeader>Horarios</SHeader>
            <SBody>
                <div className="container-visualizacion">
                    <TablaHorarios />
                </div>
            </SBody>
        </SCard>

        <style jsx="true">
            {
                `
                    .container-visualizacion{
                        border: 1px solid black;
                        border-radius: 20px;
                        background-color: rgb(238, 238, 238);
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
                        background: gray; 
                        border-radius: 10px;
                    }

                    table, th, td {
                        border: 1px solid black;
                        border-collapse: collapse;
                    }
                    table {
                        width: 100%;
                        table-layout: auto;
                    }
                    .col-horas{
                        max-width: 50px;
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
        </style>
    </>
}

export default Horarios