import React, { useState, useEffect } from 'react'
import axios from "axios";
import { useGeneralContext } from "../../../context/GeneralContext";
import APILINK from '../../../components/link';
import Modal from '../../../components/Modal';
import { toast } from 'react-hot-toast';

function ModalAlumnos({ tituloModal, isOpen, disabled, triggerState = () => { } }) {
    const { getToken } = useGeneralContext()
    const [open, setOpen] = useState(isOpen ? isOpen : false);
    const [ModalTitle, setModalTitle] = useState(tituloModal ? tituloModal : "");
    const [isDisabled, setDisabled] = useState(disabled ? disabled : false);

    useEffect(() => { setModalTitle(ModalTitle) }, [ModalTitle]);
    useEffect(() => { setOpen(isOpen) }, [isOpen]);
    useEffect(() => { setDisabled(disabled) }, [disabled]);
    const handleSubmit = () => {
        const nombre = document.getElementById("nombre").value;
        const apellido = document.getElementById("apellido").value;
        const fecha = document.getElementById("fecha").value;
        const email = document.getElementById("email").value;
        const direccion = document.getElementById("direccion").value;
        const genero = document.getElementById("genero").value;
        const documento = document.getElementById("documento").value;
        const tipoDocumento = document.getElementById("tipoDocumento").value;

        let data = JSON.stringify({
            "nombre": nombre,
            "apellido": apellido,
            "nacimiento": fecha,
            "documento": documento,
            "genero": genero,
            "direccion":direccion,
            "email": email,
            "documentoTipo": tipoDocumento
        });
        let config = {
            method: 'post',
            maxBodyLength: Infinity,
            url: `${APILINK}/api/Alumnos`,
            headers: {
                'Authorization': `Bearer ${getToken()}`,
                'Content-Type': 'application/json'
            },
            data: data
        };

        axios(config)
            .then(function (response) {
                if (response.status >= 200) {
                    setOpen(false);
                    triggerState(response.data);
                    toast.success("Guardado correctamente");
                }
            })
            .catch(function (error) {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                }
            });
    }

    const [datosModal, setDatosModal] = useState(null);

    useEffect(() => {
        setDatosModal({
            header: ModalTitle,
            form: {
                onSubmit: { action: handleSubmit },
                inputs: [
                    {
                        key: "nombre", label: "Nombre del Alumno",
                        type: "text", placeholder: "Ingrese el nombre",
                        disabled: isDisabled, required: true,
                        invalidText: "Ingrese un nombre",
                    },
                    {
                        key: "apellido", label: "Apellido del Alumno",
                        type: "text", placeholder: "Ingrese el apellido",
                        disabled: isDisabled, required: true,
                        invalidText: "Ingrese un apellido",
                    },
                    {
                        key: "fecha", label: "Fecha de nacimiento",
                        type: "date", placeholder: "Seleccione la fecha",
                        disabled: isDisabled,
                        required: true,
                    },
                    {
                        key: "email", label: "Correo Electónico",
                        type: "text", placeholder: "Ingrese correo electónico",
                        disabled: isDisabled, required: true,
                        invalidText: "Ingrese un correo electónico válido",
                    },
                    {
                        key: "direccion", label: "Dirrección",
                        type: "text", placeholder: "Ingrese la dirrección",
                        disabled: isDisabled,
                    },
                    {
                        key: "genero", label: "Genero",
                        type: "select",
                        disabled: isDisabled, required: true,
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
                            disabled: "Seleccione el género"

                        }
                    },
                    {
                        key: "documento", label: "Documento",
                        type: "text", placeholder: "Ingrese el número de documento",
                        disabled: isDisabled, required: true,
                        invalidText: "Ingrese un número",
                    },
                    {
                        key: "tipoDocumento", label: "Tipo de Documento",
                        type: "select",
                        disabled: isDisabled, required: true,
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
                            disabled: "Seleccione un Tipo"

                        }
                    },
                ],
                buttons: [
                    {
                        style: "text",
                        type: "accept",
                        submit: true,
                    },
                    {
                        style: "text",
                        type: "cancel",
                        onclick: { action: (() => setOpen(false)) },
                    },
                ]
            }
        })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [ ModalTitle, isDisabled ]);

    return (
        <>
            <Modal isOpen={open} datosModal={datosModal}></Modal>
        </>
    )
}
export default ModalAlumnos;