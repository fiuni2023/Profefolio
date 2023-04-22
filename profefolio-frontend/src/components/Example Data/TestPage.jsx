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
    onSubmit: () => console.log("Guardado"),
    enabled: true,
    header: {
      title: "Lista de Alumnos inscriptos",
    },
    addTitle:"Agregar alumnos",
    selectTitle:"Seleccionar alumno",
    options: [
      { label: "Carlos", value: 1 },
      { label: "Gabriela", value: 1 }
    ],
    list: [
       { name: "John", status: "" } ,
       { name: "Mery" , status: "" } ,
       { name: "Pepe" , status: "new" } ,
       { name: "Laura",  status: "reload" } ,
       { name: "Mery" , status: "" } ,
       { name: "Pepe" , status: "" },
       { name: "Laura",  status: "" } ,
       { name: "Mery" , status: "" } ,
       { name: "Mery" , status: "" } ,
       { name: "Pepe" , status: "" } ,
       { name: "Laura",  status: "" } ,
       { name: "Mery" , status: "reload" } ,
       { name: "Pepe" , status: "" } ,
       { name: "Laura",  status: "" } ,
       { name: "Mery" , status: "" } ,
       { name: "Mery" , status: "" } ,
       { name: "Pepe" , status: "" } ,
       { name: "Laura",  status: "new" } ,
       { name: "Mery" , status: "" } ,
       { name: "Pepe" , status: "" } ,
       { name: "Laura",  status: "" } ,
       { name: "Mery" , status: "" } 
    ]
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
        <TextButton enabled={true} buttonType='save-changes' onClick={() => console.log('Confirmando')} />
      </div>
      <div>
        <ScrolleableTable cardInfo={Alumnos} />
      </div>
    </div>
  )
}

export default TestPage