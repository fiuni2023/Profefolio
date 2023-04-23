import React from 'react'
import ScrolleableTable from '../../../../components/ScrolleableTable'

const AlumnosInscriptos = () => {

    const Alumnos = {
        onSubmit: () => console.log("Guardado"),
        enabled: true,
        header: {
            title: "Lista de Alumnos inscriptos",
        },
        addTitle: "Agregar alumnos",
        selectTitle: "Seleccionar alumno",
        options: [
            { label: "Carlos", value: 1 },
            { label: "Gabriela", value: 1 }
        ],
        list: [
            { name: "John", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Pepe", lastName: "Apellido", document: "1.234.567", status: "new" },
            { name: "Laura", lastName: "Apellido", document: "1.234.567", status: "reload" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Pepe", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Laura", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Pepe", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Laura", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "reload" },
            { name: "Pepe", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Laura", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Pepe", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Laura", lastName: "Apellido", document: "1.234.567", status: "new" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Pepe", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Laura", lastName: "Apellido", document: "1.234.567", status: "" },
            { name: "Mery", lastName: "Apellido", document: "1.234.567", status: "" }
        ]
    }
    return <>
        <ScrolleableTable studentsList={Alumnos} />
    </>

}

export default AlumnosInscriptos