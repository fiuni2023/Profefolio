import React, { useEffect, useState } from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import Tabla from '../../../../components/Tabla';
import { useModularContext } from '../../context';

const Alumnos = () => {

    const { dataSet, setPage, stateController } = useModularContext()
    const {setAlumnoId}=stateController;
    const { alumnos } = dataSet
    
    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "Lista_de_alumnos",
        titulos: [{ titulo: "Orden" }, { titulo: "Apellidos" }, { titulo: "Nombre" }],
    });
    const clickAlumno = (dato) => {
        setAlumnoId(dato.id);
        setPage("detalleAlumno");

    }

    useEffect(() => {
        const newList = alumnos.map((a, i) => {
            return {
                fila:a,
                datos: [
                    { dato: `${i + 1}` },
                    { dato: `${a.apellidos}` },
                    { dato: `${a.nombres}` }
                ]
            }

        })
        setDatosTabla({
            tituloTabla: "Lista_de_alumnos",
            titulos: [{ titulo: "Orden" }, { titulo: "Apellidos" }, { titulo: "Nombre" }],
            clickable:{ action: clickAlumno},
            filas: newList
        })
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [alumnos])

    return <>
        <SCard>
            <SHeader>Alumnos</SHeader>
            <SBody>
                <Tabla datosTabla={datosTabla} />
            </SBody>
        </SCard>
    </>
}

export default Alumnos