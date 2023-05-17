import { TD, TH, TR, Table, Tbody, Thead } from '../../../../components/componentsStyles/StyledTable'
import Tools from '../../helpers/Tools.js'
import { map } from "lodash"
import { memo, useEffect, useState } from "react";
import { SContainerScrollable } from '../ComponentStyles/ComponentsEvent';
import EventosService from '../../helpers/EventosHelpers';
import { useGeneralContext } from '../../../../context/GeneralContext';

const TableEvents = memo(({ style = {} }) => {

    const { getToken } = useGeneralContext();

    const [eventos, setEventos] = useState([]);
    useEffect(() => {
        const getData = async () => {
            const data = await EventosService.getEventos(getToken());
            if (data !== null) {
                setEventos(data)
            } else {
                console.log("Fallo en la obtencio de eventos")
            }
        };
        getData();
    }, [getToken]);
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
                    {
                        !!eventos && eventos.length > 0
                            ?
                            map(eventos, (e, i) => <TR key={i} style={{ backgroundColor: Tools.GetColorEvento(e.tipo) }}>
                                <TD className='col-tipo'>{e.tipo}</TD>
                                <TD>{new Date(e.fecha).toLocaleDateString("en-US", { day: "2-digit", month: "2-digit", year: "numeric" })}</TD>
                                <TD className="col-materia">{e.nombreMateria}</TD>
                                <TD>{e.nombreClase.split(" ").length === 2 ? `${e.nombreClase.split(" ")[0].at(0)}.${e.nombreClase.split(" ")[1].at(0)}.` : e.nombreClase.split("")}</TD>
                                <TD className="col-colegio">{e.nombreColegio}</TD>
                            </TR>)
                            :
                            <TR>
                                <TD style={{ textAlign: "center", fontWeight: "bold" }} colSpan={5}>No hay eventos futuros.</TD>
                            </TR>}

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