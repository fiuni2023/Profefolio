import React, { useState, useEffect } from 'react'
import { useGeneralContext } from "../../../context/GeneralContext";
import Modal from '../../../components/Modal';
import { toast } from 'react-hot-toast';
import AdminService from '../../../sevices/administrador';

function ModalAdmin({
    show = false,
    onHide = () => { },
    fetchFunc = () => { },
    selected_data
}) {
    const { getToken } = useGeneralContext()
    const disabled = false

    const handleCreateSubmit = () => {
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
            "direccion":direccion,
            "email": email,
            "telefono": telefono,
            "password": pass,
            "confirmPassword": passConf,
            "documentoTipo": tipoDocumento
        }

        AdminService.createAdmin(data, getToken())
                .then(r => {
                    toast.success("creado con exito!!")
                    handleHide()
                })
                .catch(error => {
                    if (typeof (error.response.data) === "string" ? true : false) {
                        toast.error(error.response.data)
                    } else {
                        toast.error(error.response.data?.Password ? error.response.data?.Password[0] : error.response.data?.Email[0])
                    }
                })

    }

    useEffect(() => {
        if (selected_data) {
        }
    }, [selected_data])

    const [datosModal, setDatosModal] = useState(null);
    const [deleting, setDeleting] = useState(false)

    useEffect(() => {
        setDatosModal({
            header: selected_data ? deleting ? "ELIMINAR COLEGIO?" : "Editar Colegio" : "Agregar Colegio",
            form: {
                onSubmit: { action: () => { } },
                inputs: [
                    {
                        key: "nombre", label: "Nombre del Administrador",
                        type: "text", placeholder: "Ingrese el nombre",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un nombre",
                    },
                    {
                        key: "apellido", label: "Apellido del Administrador",
                        type: "text", placeholder: "Ingrese el apellido",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un apellido",
                    },
                    {
                        key: "fecha", label: "Fecha de nacimiento",
                        type: "date", placeholder: "Seleccione la fecha",
                        disabled: disabled,
                        required: true,
                    },
                    {
                        key: "email", label: "Correo Electónico",
                        type: "text", placeholder: "Ingrese correo electónico",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un correo electónico válido",
                    },
                    {
                        key: "direccion", label: "Dirrección",
                        type: "text", placeholder: "Ingrese la dirrección",
                        disabled: disabled,
                    },
                    {
                        key: "pass", label: "Contraseña",
                        type: "password", placeholder: "Ingrese su Contraseña",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese una Contraseña válida",
                    },
                    {
                        key: "passConf", label: "Confirme Contraseña",
                        type: "password", placeholder: "Confirme su Contraseña",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese una Contraseña válida",
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
                        key: "documento", label: "Documento",
                        type: "text", placeholder: "Ingrese el número de documento",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un número",
                    },
                    {
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
                ],
                buttons: [
                    {
                        style: "text",
                        type: "accept",
                        onclick: { action: () => { handleCreateSubmit() } }
                    }
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
        document.getElementById("pass").value = "";
        document.getElementById("passConf").value = "";
        setDeleting(false)
        onHide()
    }

    return (
        <>
            <Modal show={show} onHide={handleHide} datosModal={datosModal} />
        </>
    )
}
export default ModalAdmin