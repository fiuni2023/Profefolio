import { TD, TH, TR, Table, Tbody, Thead } from '../../../../components/componentsStyles/StyledTable'
import Tools from '../../helpers/Tools.js'
import { map } from "lodash"
import { memo } from "react";
import { SContainerScrollable } from '../ComponentStyles/ComponentsEvent';

const TableEvents = memo(({ style = {} }) => {

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
                    {map(eventos, (e, i) => <TR key={i} style={{ backgroundColor: Tools.GetColorEvento(e.tipo) }}>
                        <TD className='col-tipo'>{e.tipo}</TD>
                        <TD>{e.fecha.toLocaleDateString("en-US", { day: "2-digit", month: "2-digit", year: "numeric" })}</TD>
                        <TD className="col-materia">{e.materia}</TD>
                        <TD>{e.clase.split(" ").length === 2 ? `${e.clase.split(" ")[0].at(0)}.${e.clase.split(" ")[1].at(0)}.` : e.clase.split("")}</TD>
                        <TD className="col-colegio">{e.colegio}</TD>
                    </TR>)}

                </Tbody>
            </Table>
        </SContainerScrollable>


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
})

export { TableEvents }