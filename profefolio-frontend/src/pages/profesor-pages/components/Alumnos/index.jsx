import React, { useEffect, useState } from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import Tabla from '../../../../components/Tabla';

const Alumnos = () => {

    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "adminsList", 
        titulos: [{titulo: "Orden"}, {titulo: "Apellidos"}, {titulo: "Nombre"}],
    });

    useEffect(()=>{
        setDatosTabla({
            tituloTabla: "adminsList", 
            titulos: [{titulo: "Orden"}, {titulo: "Apellidos"}, {titulo: "Nombre"}],
            filas: [
                {
                    datos: [
                        {dato: "1"},
                        {dato: "2"},
                        {dato: "3"}
                    ]
                }
            ]
        })
    },[])

    return <>
        <SCard>
            <SHeader>Alumnos</SHeader>
            <SBody>
                <Tabla datosTabla = {datosTabla} />
            </SBody>
        </SCard>
    </>
}

export default Alumnos