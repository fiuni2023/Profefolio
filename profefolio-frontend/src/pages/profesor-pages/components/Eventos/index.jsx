import React from 'react'
import { BsCircleFill } from 'react-icons/bs';
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { TD, TH, TR, Table, Tbody, Thead } from '../../../../components/componentsStyles/StyledTable'
import styled from 'styled-components';

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
    return <>
        <Table style={{ width: "100%" }}>
            <Thead>
                <TR>
                    <TH>Tipo</TH>
                    <TH>Fecha</TH>
                    <TH>Materia</TH>
                    <TH>Clase</TH>
                    <TH>Colegio</TH>
                </TR>
            </Thead>
            <Tbody>
                <TR>
                    <TD>Ex</TD>
                    <TD>12/05/2023</TD>
                    <TD>Matematicas</TD>
                    <TD>Quinto Grado</TD>
                    <TD>San Juan</TD>
                </TR>
                <TR>
                    <TD>Ex</TD>
                    <TD>12/05/2023</TD>
                    <TD>Matematicas</TD>
                    <TD>Quinto Grado</TD>
                    <TD>San Juan</TD>
                </TR>
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