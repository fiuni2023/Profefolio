import React from 'react'

import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { TD, TH, TR, Table, Tbody, Thead } from '../../../../components/componentsStyles/StyledTable'

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
    </>
}

const Eventos = () => {
    return <>
        <SCard>
            <SHeader>Horarios de Clases</SHeader>
            <SBody>
                <TableEvents />
            </SBody>
        </SCard>
    </>
}

export default Eventos