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
    list:[
       { name: "John", lastName: "Apellido",document: "1.234.567",status: "" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Pepe" ,lastName: "Apellido",document: "1.234.567", status: "new" } ,
       { name: "Laura",lastName: "Apellido",document: "1.234.567",  status: "reload" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Pepe" ,lastName: "Apellido",document: "1.234.567", status: "" },
       { name: "Laura",lastName: "Apellido",document: "1.234.567",  status: "" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Pepe" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Laura",lastName: "Apellido",document: "1.234.567",  status: "" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "reload" } ,
       { name: "Pepe" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Laura",lastName: "Apellido",document: "1.234.567",  status: "" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Pepe" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Laura",lastName: "Apellido",document: "1.234.567",  status: "new" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Pepe" ,lastName: "Apellido",document: "1.234.567", status: "" } ,
       { name: "Laura",lastName: "Apellido",document: "1.234.567",  status: "" } ,
       { name: "Mery" ,lastName: "Apellido",document: "1.234.567", status: "" } 
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