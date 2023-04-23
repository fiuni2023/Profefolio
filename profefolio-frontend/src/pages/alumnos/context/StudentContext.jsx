import { useFormik } from "formik";
import React, { useContext, useState } from "react";
import { createContext } from "react";

const StudentContext = createContext();

export const useStudentContext = () => {
    return useContext(StudentContext)
}

export const StudentProvider = ({ children }) => {

    const studentFormik = useFormik({
        initialValues: {
            id: "",
            nombre: "nombre",
            apellido: "",
            nacimiento: new Date().toISOString(),
            documento: "",
            documentoTipo: "",
            genero: "",
            direccion: "",
            email: ""
        }
    })
    const [showSudent, setShowStudent] = useState(false)

    const resetStudent = () => {
        studentFormik.resetForm()
    }

    const selectedStudent = studentFormik.values

    const changeStudentData = (area, value) => {
        studentFormik.setFieldValue(area, value)
    }

    const getGenero = (genero) => {
        if (genero === "Femenino") {
            return "F"
        }
        if (genero === "Masculino") {
            return "M"
        }
        return ""
    }

    const setSelectedStudent = (student) => {
            studentFormik.setFieldValue("id", student.id)
            studentFormik.setFieldValue("nombre", student.nombre)
            studentFormik.setFieldValue("apellido", student.apellido)
            studentFormik.setFieldValue("nacimiento", student.nacimiento)
            studentFormik.setFieldValue("documento", student.documento)
            studentFormik.setFieldValue("documentoTipo", student.documentoTipo)
            studentFormik.setFieldValue("genero", getGenero(student.genero))
            studentFormik.setFieldValue("direccion", student.direccion)
            studentFormik.setFieldValue("email", student.email)
    }

    const values = {
        selectedStudent,
        changeStudentData,
        setSelectedStudent,
        showSudent,
        setShowStudent,
        resetStudent,
    }

    return (
        <StudentContext.Provider value={values}>
            <>
                {children}
            </>
        </StudentContext.Provider>
    )
}