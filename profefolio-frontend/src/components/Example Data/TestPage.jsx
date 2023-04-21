import React from 'react'
import TextButton from '../TextButton'
import IconButton from '../IconButton'
import ScrolleableTable from '../ScrolleableTable'
const TestPage = () => {
  const miFuncion = () => {
    console.log("Editando")
  }
  //Datos de prueba
  const Alumnos = {
    xs: 12, sm: 12, md: 6, lg: 3,
    background: "white",
    hover: true,
    goto: '',
    header: {
      title: "Alumnos",
    },
    body: {
      table: {
        small: true,
        titulos: [{ titulo: "Nombre" }, { titulo: "Creado en" }],
        filas: [
          { datos: [{ dato: "John" }, { dato: "12/12/2017" }] },
          { datos: [{ dato: "Mery" }, { dato: "02/02/2017" }] },
          { datos: [{ dato: "Pepe" }, { dato: "01/10/2017" }] },
          { datos: [{ dato: "Laura" }, { dato: "31/01/2017" }] }
        ]
      }
    }

  }


  return (
    <div>
      <div>
        <TextButton enabled={true} buttonType='accept' onClick={() => console.log('Aceptando')} />
        <IconButton enabled={true} buttonType='edit' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='delete' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='arrowL' onClick={() => console.log("Izquierda")} />
        <IconButton enabled={true} buttonType='arrowR' onClick={() => console.log("Derecha")} />
        <IconButton enabled={true} buttonType='close' onClick={() => console.log("Saliendo")} />
        <TextButton enabled={true} buttonType='cancel' onClick={() => console.log('Cancelando')} />
        <TextButton enabled={true} buttonType='save' onClick={() => console.log('Guardando')} />
        <TextButton enabled={true} buttonType='confirm' onClick={() => console.log('Confirmando')} />
      </div>
      <div>
        <ScrolleableTable cardInfo={Alumnos}/>
      </div>
    </div>
  )
}

export default TestPage