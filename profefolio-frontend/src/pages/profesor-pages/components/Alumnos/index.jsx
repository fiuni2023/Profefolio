import React, { useEffect, useState } from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import Tabla from '../../../../components/Tabla';
import { useModularContext } from '../../context';

const Alumnos = () => {

    const { dataSet } = useModularContext()
    const { alumnos } = dataSet

    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "adminsList",
        titulos: [{ titulo: "Orden" }, { titulo: "Apellidos" }, { titulo: "Nombre" }],
    });


    useEffect(() => {
        console.log(alumnos); 
        const newList = alumnos.map((a, i) => {
            return {
                datos: [
                    { dato: `${i+1}` },
                    { dato: `${a.apellidos}` },
                    { dato: `${a.nombres}` }
                ]
            }

        })
        setDatosTabla({
            tituloTabla: "adminsList",
            titulos: [{ titulo: "Orden" }, { titulo: "Apellidos" }, { titulo: "Nombre" }],
            filas: newList
        })
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