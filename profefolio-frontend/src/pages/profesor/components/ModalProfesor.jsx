import React, { useState, useEffect } from 'react'
import { useGeneralContext } from "../../../context/GeneralContext";
import Modal from '../../../components/Modal';
import { toast } from 'react-hot-toast';
import axios from 'axios';
import APILINK from '../../../components/link';
import ModalMensajeProfesor from './ModalMensajeProfesor';
import ProfesorService from '../helpers/ProfeHelper';
function ModalProfesor({
    selected_data,
    show = false,
    onHide = () => { },
    fetchFunc = () => { }
}) {
    const { getToken } = useGeneralContext()
    const { createProfesor } = ProfesorService
    const disabled = false
    const [openAviso, setOpenAviso] = useState(false)
    const [existingpProfesor, setProfesor] = useState("")

    const handleCreateSubmit = async(e) => {
        const nombre = document.getElementById("nombre").value;
        const apellido = document.getElementById("apellido").value;
        const fecha = document.getElementById("fecha").value;
        const email = document.getElementById("email").value;
        const direccion = document.getElementById("direccion").value;
        const genero = document.getElementById("genero").value;
        const documento = document.getElementById("documento").value;
        const tipoDocumento = document.getElementById("tipoDocumento").value;
        const telefono = document.getElementById("telefono").value;
        const pass = document.getElementById("pass").value;
        const passConf = document.getElementById("passConf").value;

        let data = {
            "nombre": nombre,
            "apellido": apellido,
            "nacimiento": fecha,
            "documento": documento,
            "genero": genero,
            "direccion": direccion,
            "email": email,
            "telefono": telefono,
            "password": pass,
            "confirmPassword": passConf,
            "documentoTipo": tipoDocumento
        }

        try {
            const result = await createProfesor(data, getToken());
            if (result && result.status === 200) {
                handleHide();
                fetchFunc();
                toast.success("Guardado correctamente");
            } else if (result && result.status === 230) {
                setProfesor(result.data);
                setOpenAviso(true);
            }
        }catch (error) {
                if (typeof (error.response.data) === "string" ? true : false) {
                   
                    if (error.response.data !== "El email al cual quiere registrarse ya existe") {
                        //Solo debe quedar este toast, el resto esta solo para referencia
                        toast.error(error.response.data)
                    } else {
                        setOpenAviso(true)
                    }
                } else {
                    let errArr = error.response.data?.errors
                    let single = ""
                   
                    if (errArr?.Apellido) single = errArr.Apellido[0]
                    if (errArr?.Password) single = errArr.Password[0]
                    if (errArr?.ConfirmPassword) single = errArr.ConfirmPassword[0]
                    if (errArr?.Documento) single = errArr.Genero[0]
                    if (errArr?.DocumentoTipo) single = errArr.DocumentoTipo[0]
                    if (errArr?.Email) single = errArr.Email[0]
                    if (errArr?.Genero) single = errArr.Genero[0]
                    if (errArr?.Nombre) single = errArr.Nombre[0]
                    if (errArr?.Telefono) single = errArr.Telefono[0]
                    if (errArr["$.nacimiento"]) single = "La fecha de nacimiento es requerida"
                    toast.error(single)
                }
            };
    }

    const handleEditSubmit = () => {
        const nombre = document.getElementById("nombre").value;
        const apellido = document.getElementById("apellido").value;
        const fecha = document.getElementById("fecha").value;
        const email = document.getElementById("email").value;
        const direccion = document.getElementById("direccion").value;
        const genero = document.getElementById("genero").value;
        const documento = document.getElementById("documento").value;
        const tipoDocumento = document.getElementById("tipoDocumento").value;
        const telefono = document.getElementById("telefono").value;

        let data = {
            "nombre": nombre,
            "apellido": apellido,
            "nacimiento": fecha,
            "documento": documento,
            "genero": genero,
            "direccion": direccion,
            "email": email,
            "telefono": telefono,
            "documentoTipo": tipoDocumento
        }

        axios.put(`${APILINK}/api/profesor/${selected_data.id}`, data, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })
            .then(response => {
                toast.success("Guardado exitoso");
                handleHide()

            })
            .catch(error => {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                    toast.error(error.response.data?.errors.Password ? error.response.data?.errors.Password[0] : error.response.data?.errors.Email[0])
                }
            });
    }

    const handleDelete = () => {
        axios.delete(`${APILINK}/api/profesor/${selected_data.id}`, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })
            .then(response => {
                toast.success("Borrado exitoso");
                handleHide()

            })
            .catch(error => {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                    toast.error(error.response.data?.errors.Password ? error.response.data?.errors.Password[0] : error.response.data?.errors.Email[0])
                }
            });
    }

    useEffect(() => {
        if (selected_data) {
            document.getElementById("nombre").value = selected_data.nombre;
            document.getElementById("apellido").value = selected_data.apellido;
            document.getElementById("fecha").value = selected_data.nacimiento.split("T")[0];
            document.getElementById("email").value = selected_data.email;
            document.getElementById("direccion").value = selected_data.direccion;
            document.getElementById("genero").value = selected_data.genero === "Masculino" ? "M" : "F";
            document.getElementById("documento").value = selected_data.documento;
            document.getElementById("tipoDocumento").value = selected_data.documentoTipo === "dni" ? "DNI" : selected_data.documentoTipo;
            document.getElementById("telefono").value = selected_data.telefono;
        }
    }, [selected_data])

    const [datosModal, setDatosModal] = useState(null);
    const [deleting, setDeleting] = useState(false)

    const getInputs = () => {
        if (selected_data) return [
            {
                md: 6, lg: 6,
                key: "nombre", label: "Nombre del Profesor",
                type: "text", placeholder: "Ingrese el nombre",
                disabled: disabled, required: true,
                invalidText: "Ingrese un nombre",
            },
            {
                md: 6, lg: 6,
                key: "apellido", label: "Apellido del Profesor",
                type: "text", placeholder: "Ingrese el apellido",
                disabled: disabled, required: true,
                invalidText: "Ingrese un apellido",
            },
            {
                key: "email", label: "Correo Electónico",
                type: "text", placeholder: "Ingrese correo electónico",
                disabled: disabled, required: true,
                invalidText: "Ingrese un correo electónico válido",
            },
            {
                key: "fecha", label: "Fecha de nacimiento",
                type: "date", placeholder: "Seleccione la fecha",
                disabled: disabled,
                required: true,
            },
            {
                key: "direccion", label: "Dirección",
                type: "text", placeholder: "Ingrese la dirección",
                disabled: disabled,
            },
            {
                key: "telefono", label: "Telefono",
                type: "text", placeholder: "Ingrese su Telefono",
                disabled: disabled,
                invalidText: "Ingrese un telefono válido",
            },
            {
                key: "genero", label: "Genero",
                type: "select",
                disabled: disabled, required: true,
                invalidText: "Seleccione una opción",
                select: {
                    default: "Seleccione el género",
                    options: [
                        {
                            value: "F",
                            text: "Femenino"
                        },
                        {
                            value: "M",
                            text: "Masculino"
                        }
                    ],

                }
            },
            {
                md: 6, lg: 6,
                key: "tipoDocumento", label: "Tipo de Documento",
                type: "select",
                disabled: disabled, required: true,
                invalidText: "Seleccione un Tipo",
                select: {
                    default: "Seleccione un Tipo",
                    options: [
                        {
                            value: "Cedula de Identidad",
                            text: "Cedula de Identidad"
                        },
                        {
                            value: "DNI",
                            text: "DNI"
                        },
                        {
                            value: "Pasaporte",
                            text: "Pasaporte"
                        }
                    ],
                    
                }
            },
            {
                md: 6, lg: 6,
                key: "documento", label: "Documento",
                type: "text", placeholder: "Ingrese el número de documento",
                disabled: disabled, required: true,
                invalidText: "Ingrese un número",
            },
        ]
        return [
            {
                md: 6, lg: 6,
                key: "nombre", label: "Nombre del Profesor",
                type: "text", placeholder: "Ingrese el nombre",
                disabled: disabled, required: true,
                invalidText: "Ingrese un nombre",
            },
            {
                md: 6, lg: 6,
                key: "apellido", label: "Apellido del Profesor",
                type: "text", placeholder: "Ingrese el apellido",
                disabled: disabled, required: true,
                invalidText: "Ingrese un apellido",
            },
            {
                key: "email", label: "Correo Electónico",
                type: "text", placeholder: "Ingrese correo electónico",
                disabled: disabled, required: true,
                invalidText: "Ingrese un correo electónico válido",
            },
            {
                md: 6, lg: 6,
                key: "pass", label: "Contraseña",
                type: "password", placeholder: "Ingrese su Contraseña",
                disabled: disabled, required: true,
                invalidText: "Ingrese una Contraseña válida",
            },
            {
                md: 6, lg: 6,
                key: "passConf", label: "Confirme Contraseña",
                type: "password", placeholder: "Confirme su Contraseña",
                disabled: disabled, required: true,
                invalidText: "Ingrese una Contraseña válida",
            },
            {
                key: "fecha", label: "Fecha de nacimiento",
                type: "date", placeholder: "Seleccione la fecha",
                disabled: disabled,
                required: true,
            },
            {
                key: "direccion", label: "Dirección",
                type: "text", placeholder: "Ingrese la dirección",
                disabled: disabled,
            },
            {
                
                key: "telefono", label: "Telefono",
                type: "text", placeholder: "Ingrese su Telefono",
                disabled: disabled, required: true,
                invalidText: "Ingrese un telefono válido",
            },
            {
                
                key: "genero", label: "Genero",
                type: "select",
                disabled: disabled, required: true,
                invalidText: "Seleccione una opción",
                select: {
                    default: "Seleccione el género",
                    options: [
                        {
                            value: "F",
                            text: "Femenino"
                        },
                        {
                            value: "M",
                            text: "Masculino"
                        }
                    ],

                }
            },
            {
                md: 6, lg: 6,
                key: "tipoDocumento", label: "Tipo de Documento",
                type: "select",
                disabled: disabled, required: true,
                invalidText: "Seleccione un Tipo",
                select: {
                    default: "Seleccione un Tipo",
                    options: [
                        {
                            value: "Cedula de Identidad",
                            text: "Cedula de Identidad"
                        },
                        {
                            value: "DNI",
                            text: "DNI"
                        },
                        {
                            value: "Pasaporte",
                            text: "Pasaporte"
                        }
                    ],
                    
                }
            },
            {
                md: 6, lg: 6,
                key: "documento", label: "Numero de documento",
                type: "text", placeholder: "Ingrese el número de documento",
                disabled: disabled, required: true,
                invalidText: "Ingrese un número",
            },
        ]
    }

    useEffect(() => {
        setDatosModal({
            header: selected_data ? deleting ? "ELIMINAR PROFESOR?" : "Editar Profesor" : "Agregar Profesor",
            form: {
                onSubmit: { action: () => { } },
                inputs: getInputs(),
                buttons: selected_data ?
                    !deleting ?
                        [
                            {
                                style: "text",
                                type: "danger",
                                onclick: { action: () => { setDeleting(true) } }
                            },
                            {
                                style: "text",
                                type: "save",
                                onclick: { action: (e) => { 
                                    e.preventDefault()
                                    handleEditSubmit() 
                                } }
                            },
                        ]
                        :
                        [
                            {
                                style: "text",
                                type: 'cancel',
                                onclick: { action: () => { setDeleting(false) } }
                            },
                            {
                                style: "text",
                                type: "danger",
                                onclick: { action: () => { handleDelete() } }
                            },
                        ]
                    :
                    [
                        {
                            style: "text",
                            type: "accept",
                            onclick: { action: (e) => { 
                                e.preventDefault()
                                handleCreateSubmit()
                            } }
                        },
                    ]
            }
        })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [disabled, selected_data, deleting]);

    const handleHide = () => {
        document.getElementById("nombre").value = "";
        document.getElementById("apellido").value = "";
        document.getElementById("fecha").value = "";
        document.getElementById("email").value = "";
        document.getElementById("direccion").value = "";
        document.getElementById("genero").value = "";
        document.getElementById("documento").value = "";
        document.getElementById("tipoDocumento").value = "";
        document.getElementById("telefono").value = "";
        if (!selected_data) {
            document.getElementById("pass").value = "";
            document.getElementById("passConf").value = "";
        }

        setDeleting(false)
        fetchFunc()
        onHide()
    }

    const addNewExistingSuccess = () => {
        handleHide()
        fetchFunc()
    }

    const handleCancelAviso = () => {
        setOpenAviso(false)
    }



    return (
        <>
            <Modal show={show} onHide={handleHide} datosModal={datosModal} />
            <ModalMensajeProfesor
                isOpen={openAviso}
                onAdd={onHide}
                profesor={existingpProfesor}
                onCancel={handleCancelAviso}
                onSuccess={addNewExistingSuccess}
            />
        </>
    )
}
export default ModalProfesor