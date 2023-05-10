import React from 'react'
import { BsCircleFill } from 'react-icons/bs';
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { TD, TH, TR, Table, Tbody, Thead } from '../../../../components/componentsStyles/StyledTable'
import styled from 'styled-components';
import Tools from '../../helpers/Tools.js'
import {map} from "lodash"

const ListTypeEvent = styled.div`
    display: flex;
    margin-top: 1rem;
    justify-content: space-between;
    flex-wrap: wrap;
    column-gap: 2rem;
    align-items: center;
    padding-left: 1rem;
    padding-right: 1rem;
`

const TypeEventTarget = styled.div`
    min-width: 60px;
    text-align: left;
    display: flex;
    column-gap: 0.5rem;
    align-items: center;
`

const TableEvents = () => {
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
        }
    ]   

    return <>
        <Table style={{ width: "100%" }}>
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
                {map(eventos, (e, i) => <TR key={i}>
                    <TD className='col-tipo'>{e.tipo}</TD>
                    <TD>{e.fecha.toLocaleDateString("en-US", {day: "2-digit", month: "2-digit", year: "numeric"})}</TD>
                    <TD>{e.materia}</TD>
                    <TD>{e.clase}</TD>
                    <TD>{e.colegio}</TD>
                </TR>)}
                
            </Tbody>
        </Table>
        <ListTypeEvent>
            <TypeEventTarget>
                <BsCircleFill />Evento
            </TypeEventTarget>
            <TypeEventTarget>
                <BsCircleFill />Parcial
            </TypeEventTarget>
            <TypeEventTarget>
                <BsCircleFill />Prueba Sumativa
            </TypeEventTarget>
            <TypeEventTarget>
                <BsCircleFill />Examen
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
                `
            }
        </style>
    </>
}


const Eventos = () => {
    return <>
        <SCard>
            <SHeader>Proximos Eventos</SHeader>
            <SBody>
                <TableEvents />
            </SBody>
        </SCard>
    </>
}

export default Eventos