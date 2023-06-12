import React, { useState } from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import Tabla from '../../../../components/Tabla';
import { useModularContext } from '../../context';
import { useNavigate} from 'react-router-dom';

const Alumnos = () => {

    const { dataSet } = useModularContext()
    const { alumnos } = dataSet
    const navigate = useNavigate();
   
    const handleRowClick = (id) => {
        console.log(alumnos);
      };
      return <>
      <SCard>
          <SHeader>Alumnos</SHeader>
          <SBody>
              <Tabla
              
              datosTabla={{
                  tituloTabla: 'adminList',
                  titulos: [{ titulo: "Orden" }, { titulo: "Apellidos" }, { titulo: "Nombre" }],
                  clickable: { action: handleRowClick},
  
                  filas: alumnos.map((alumno) => ({
                    fila: alumno,
                    datos: [
                      { dato: alumno.id },
                      { dato: alumno.apellidos },
                      { dato: alumno.nombres}
  
                    ],
                  })),
                }}
             
                />
          </SBody>
      </SCard>
  </>
}

export default Alumnos