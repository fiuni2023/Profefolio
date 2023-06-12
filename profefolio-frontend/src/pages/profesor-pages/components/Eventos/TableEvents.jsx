import { TD, TH, TR, Table, Tbody, Thead } from '../../../../components/componentsStyles/StyledTable'
import Tools from '../../helpers/Tools.js'
import { map } from "lodash"
import { memo, useEffect, useState } from "react";
import { SContainerScrollable } from '../ComponentStyles/ComponentsEvent';
import EventosService from '../../helpers/EventosHelpers';
import { useGeneralContext } from '../../../../context/GeneralContext';
import { useModularContext } from '../../context';
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import Tabla from '../../../../components/Tabla';

const TableEvents = memo(({ style = {} }) => {
    const { dataSet } = useModularContext()
    const { eventos } = dataSet

    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "adminsList",
        titulos: [{ titulo: "Tipo" },
        { titulo: "Fecha" },
        { titulo: "Materia" },
        { titulo: "Clase" },
        { titulo: "Colegio" }
        ],
    });

    useEffect(() => {
        const newList = eventos.map((a) => {
            return {
                datos: [
                    { dato: `${a.tipo}` },
                    { dato: `${a.fecha}` },
                    { dato: `${a.nombreMateria}` },
                    { dato: `${a.nombreClase}` },
                    { dato: `${a.nombreColegio}` }
                ]
            }

        })
        setDatosTabla({
            tituloTabla: "adminsList",
            titulos: [{ titulo: "Tipo" },
            { titulo: "Fecha" },
            { titulo: "Materia" },
            { titulo: "Clase" },
            { titulo: "Colegio" }],
            filas: newList
        })
    }, [eventos])
    
    return <>
        <SCard>
            <SBody>
                <Tabla datosTabla={datosTabla} />
            </SBody>
        </SCard>

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