import { useFormik } from "formik";
import React, { useContext, useState } from "react";
import { createContext } from "react";

const AdminContext = createContext();

export const useAdminContext = () =>{
    return useContext(AdminContext)
}

export const AdminProvider = ({children}) => {

    const adminFormik = useFormik({
        initialValues: {
            id: "",
            nombre: "asd",
            apellido: "",
            nacimiento: new Date().toISOString(),
            documento: "",
            documentoTipo: "",
            genero: "",
            direccion: "",
            telefono: "",
            email: ""
        }
    })
    const [showAdmin, setShowAdmin] = useState(false)

    const resetAdmin = () => {
        adminFormik.resetForm()
    }

    const selectedAdmin = adminFormik.values

    const changeAdminData = (area, value) => {
        adminFormik.setFieldValue(area, value)
    }

    const setSelectedAdmin = (admin) => {
        adminFormik.setFieldValue("id", admin.id)
        adminFormik.setFieldValue("nombre", admin.nombre)
        adminFormik.setFieldValue("apellido", admin.apellido)
        adminFormik.setFieldValue("nacimiento", `${admin.nacimiento.split("T")[0]}`)
        adminFormik.setFieldValue("documento", admin.documento)
        adminFormik.setFieldValue("documentoTipo", admin.documentoTipo)
        adminFormik.setFieldValue("genero", admin.genero)
        adminFormik.setFieldValue("direccion", admin.direccion)
        adminFormik.setFieldValue("telefono", admin.telefono)
        adminFormik.setFieldValue("email", admin.email)
    }

    const values = {
        selectedAdmin,
        changeAdminData,
        setSelectedAdmin,
        showAdmin,
        setShowAdmin,
        resetAdmin,
    }

    return (
        <AdminContext.Provider value={values}>
            <>
               {children}
            </>
        </AdminContext.Provider>
    )
}