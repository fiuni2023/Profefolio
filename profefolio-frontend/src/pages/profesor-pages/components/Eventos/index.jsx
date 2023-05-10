import React from 'react'
import { BsCircleFill } from 'react-icons/bs';
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { TD, TH, TR, Table, Tbody, Thead } from '../../../../components/componentsStyles/StyledTable'
import styled from 'styled-components';
import Tools from '../../helpers/Tools.js'
import { map } from "lodash"

const ListTypeEvent = styled.div`
    display: flex;
    margin-top: 1rem;
    justify-content: space-between;
    flex-wrap: wrap;
    column-gap: 2rem;
    row-gap: 0.5rem;
    align-items: center;
    padding-left: 1rem;
    padding-right: 1rem;
`

const SContainerScrollable = styled.div`
    background-color: transparent;
    border-radius: 20px;
    max-height: 330px;
    overflow-y: auto;
    scrollbar-width: thin;
    ::-webkit-scrollbar {
        width: 10px;
    }
    ::-webkit-scrollbar-track{
        margin-top: 40px;
    }

    ::-webkit-scrollbar-thumb {
        background: rgb(180, 180, 180); 
        border-radius: 10px;
    }
`

const TypeEventTarget = styled.div`
    min-width: 60px;
    text-align: left;
    display: flex;
    column-gap: 0.5rem;
    align-items: center;
    align-items: stretch;
`

const TableEvents = ({ style = {} }) => {

    const getColorEvento = (evento = "") => {
        switch (evento.toLowerCase()) {
            case "evento":
                return Tools.SelectColor("purple");
            case "examen":
                return Tools.SelectColor("yellow");
            case "parcial":
                return Tools.SelectColor("bluesky");
            case "prueba sumativa":
                return Tools.SelectColor("salmon");
            default:
                return "#DDDDDD";
        }
    }
    const eventos = [
        {
            id: 1,
            tipo: "Examen",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 2,
            tipo: "Evento",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 3,
            tipo: "Parcial",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 4,
            tipo: "Prueba Sumativa",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 5,
            tipo: "Parcial",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 6,
            tipo: "Prueba Sumativa",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 7,
            tipo: "Prueba Sumativa",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 8,
            tipo: "Examen",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 7,
            tipo: "Prueba Sumativa",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        },
        {
            id: 8,
            tipo: "Examen",
            fecha: new Date(),
            materia: "Matematicas",
            clase: "Primer Grado",
            colegio: "San Juan"
        }
    ]

    return <>
        <SContainerScrollable>
            <Table style={{ ...style, width: "100%" }}>
                <Thead>
                    <TR>
                        <TH className='col-tipo'>Tipo</TH>
                        <TH>Fecha</TH>
                        <TH>Materia</TH>
                        <TH>Clase</TH>
                        <TH>Colegio</TH>
                    </TR>
                </Thead>
                <Tbody>
                    {map(eventos, (e, i) => <TR key={i} style={{ backgroundColor: getColorEvento(e.tipo) }}>
                        <TD className='col-tipo'>{e.tipo}</TD>
                        <TD>{e.fecha.toLocaleDateString("en-US", { day: "2-digit", month: "2-digit", year: "numeric" })}</TD>
                        <TD className="col-materia">{e.materia}</TD>
                        <TD>{e.clase.split(" ").length === 2 ? `${e.clase.split(" ")[0].at(0)}.${e.clase.split(" ")[1].at(0)}.` : e.clase.split("")}</TD>
                        <TD className="col-colegio">{e.colegio}</TD>
                    </TR>)}

                </Tbody>
            </Table>
        </SContainerScrollable>
        <ListTypeEvent>
            <TypeEventTarget >
                <BsCircleFill style={{ color: getColorEvento("Evento"), width: "20px", height: "20px" }} />Evento
            </TypeEventTarget>
            <TypeEventTarget>
                <BsCircleFill style={{ color: getColorEvento("Parcial"), width: "20px", height: "20px" }} />Parcial
            </TypeEventTarget>
            <TypeEventTarget>
                <BsCircleFill style={{ color: getColorEvento("Prueba Sumativa"), width: "20px", height: "20px" }} />Prueba Sumativa
            </TypeEventTarget>
            <TypeEventTarget>
                <BsCircleFill style={{ color: getColorEvento("Examen"), width: "20px", height: "20px" }} />Examen
            </TypeEventTarget>
        </ListTypeEvent>
        <style jsx="true">
            {
                `
                    .col-tipo{
                        max-width: 80px;
                        overflow: hidden;
                        white-space: nowrap;
                        text-overflow: ellipsis;
                    }
                    .col-materia{
                        max-width: 100px;
                        overflow: hidden;
                        white-space: nowrap;
                        text-overflow: ellipsis;
                    }

                    .col-colegio{
                        max-width: 60px;
                        overflow: hidden;
                        white-space: nowrap;
                        text-overflow: ellipsis;
                    }
                `
            }
        </style>
    </>
}


const Eventos = () => {
    return <>
        <SCard>
            <SHeader>Proximos Eventos</SHeader>
            <SBody style={{ height: "430px" }}>
                <TableEvents />
            </SBody>
        </SCard>
    </>
}

export default Eventos