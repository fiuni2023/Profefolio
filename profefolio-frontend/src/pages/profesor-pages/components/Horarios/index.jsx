import React, { useId } from 'react'
import styled from 'styled-components'
import { Calendar } from '../../api/calendar';
import { orderBy, groupBy, sortBy, map } from "lodash"


const STablaHorarios = styled.table`

`
const TablaHorarios = () => {

    //const diasSemana = ["lunes", "martes", "miércoles", "jueves", "viernes", "sábado", "domingo"];

    /* const ordenados = sortBy(Calendar, function (e) {
        const orden = ["Lunes", "Martes", "Miércoles", "Miercoles", "Jueves", "Viernes", "Sábado", "Sabado", "Domingo"];
        return orden.indexOf(e.dia);
    });

    console.table(ordenados) */

    const grupo = groupBy(Calendar, a => a.dia)

    console.log(grupo)
    const headers = []

    //const diff = Math.abs(fecha2 - fecha1) / 60000; // Convierte la diferencia en minutos

    for (const key in grupo) {
        headers.push(key);
    }
    // recorrer cada key y verificar si hay un elemento para cada hora y minuto especifico
    // si hay coincidencia se agrega  a la lista de rows(lista de listas ) 
    // se detiene cuando ninguna key no tenga mas elementos 

    let bandera = true;
    let elementoPosition = 0;
    let hora = 7;
    let minuto = 0;
    let rows = [];
    let reten = 0;
    let skey = useId();
    while (bandera) {
        reten++;
        const rowDias = []
        if (minuto === 0) {
            rowDias.push(<td rowSpan={6}>{hora}</td>)
        }
        let maxLength = 0;
        //rowDias.push(<td key={skey++}>{`${hora < 10 ? `0${hora}`: hora}:${minuto < 10 ? `0${minuto}`: minuto}`}</td>)

        rowDias.push(<td>{`${hora < 10 ? `0${hora}` : hora}:${minuto < 10 ? `0${minuto}` : minuto}`}</td>)
        for (const key in grupo) {
            //console.log(`${hora < 10 ? `0${hora}`: hora}:${minuto < 10 ? `0${minuto}`: minuto}`)
            maxLength = maxLength < grupo[key].length ? grupo[key].length : maxLength;

            if (grupo[key][elementoPosition]) {
                const element = grupo[key][elementoPosition]
                const horaMinuto = element.inicio.split(":")

                if (parseInt(horaMinuto[0]) === hora && parseInt(horaMinuto[1]) === minuto) {
                    //rowDias.push(<td key={skey++}>{grupo[key][elementoPosition].nombre}</td>)
                    rowDias.push(<td>{grupo[key][elementoPosition].nombre}</td>)
                } else {
                    //rowDias.push(<td key={skey++}></td>)
                    rowDias.push(<td>_</td>)
                }
            } else {
                //rowDias.push(<td key={skey++}></td>)
                rowDias.push(<td>_</td>)
            }
        }
        console.log(`${hora < 10 ? `0${hora}` : hora}:${minuto < 10 ? `0${minuto}` : minuto}`)

        minuto += 10;
        if (minuto > 50) {
            minuto = 0;
            hora++;
        }

        elementoPosition++;

        if (maxLength <= elementoPosition || hora === 14) {
            bandera = false;
        } else {
            elementoPosition = 0;
        }


        rows.push(rowDias);
        console.log(rowDias)
    }
    //console.table(rows)
    return <>
        <STablaHorarios>
            <thead>
                <tr>
                    <th className='col-horas' colSpan={2}>Horas</th>
                    {headers.map((header, i) => <th key={i + header}>{header}</th>)}
                </tr>
            </thead>
            <tbody>
                {map(rows, (e, i) => <><tr key={i}>
                        {e}
                    </tr></>)
                }
                {/* {map(rows, (e, i) => <>
                    <tr key={i + e.length}>
                        <td key={(i) * 100 + 101 } rowSpan={6}>{7 + i} asda sd </td>
                        {
                            map(e, (el, idx) => <td key={1000 * (idx + 1) * (i+1)}>{el}</td>)
                        }
                    </tr></>)
                } */}

                {/* <tr>
                    <td rowSpan={6}>7</td>
                    <td>00</td>

                </tr>
 */}

            </tbody>
        </STablaHorarios>
    </>
}
const Horarios = () => {
    return <>
        <div className="container-visualizacion">
            <TablaHorarios />
        </div>

        <style jsx="true">
            {
                `
                    .container-visualizacion{
                        border: 1px solid black;
                        border-radius: 20px;
                        background-color: gray;
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
                    tr > td:nth-child(1), tr > td:nth-child(2){
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