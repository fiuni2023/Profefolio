import React, { useState, useEffect } from 'react'
import axios from "axios";
import { useGeneralContext } from "../../../context/GeneralContext";
import APILINK from '../../../components/link';
import Modal from './Modal';
import { toast } from 'react-hot-toast';

function ModalAlumnos({ selectedData = false, tituloModal = "Alumnos", isOpen = false, disabled = true, triggerState = () => { }, handleClose = () => { } }) {
    const { getToken } = useGeneralContext()
    const [open, setOpen] = useState(isOpen ? isOpen : false);
    const [ModalTitle, setModalTitle] = useState(tituloModal ? tituloModal : "");
    const [isDisabled, setDisabled] = useState(disabled ? disabled : false);

    const initialData = {
        nombre: "",
        apellido: "",
        nacimiento: new Date().toISOString().slice(0,10),
        documento: "",
        genero: "",
        direccion: "",
        email: "",
        documentoTipo: ""
    }
    const [data, setData] = useState(selectedData || initialData)
    console.log(selectedData)

    useEffect(() => { setModalTitle(ModalTitle) }, [ModalTitle]);
    useEffect(() => { setOpen(isOpen) }, [isOpen]);
    useEffect(() => { setDisabled(disabled) }, [disabled]);
    useEffect(() => { setData(selectedData ? selectedData : initialData) }, [selectedData]);

    const handleChangeData = e => {
        console.log(e.target.id)
        console.log(e.target.value)
        setData({
            ...data,
            [e.target.id]: e.target.value
        })
        console.log(data)
    }


    const handleSubmit = (e) => {
        let submitData = JSON.stringify({
            "nombre": data.nombre,
            "apellido": data.apellido,
            "nacimiento": data.fecha,
            "documento": data.documento,
            "genero": data.genero,
            "direccion": data.direccion,
            "email": data.email,
            "documentoTipo": data.documentoTipo
        });
        let config = {
            method: 'post',
            maxBodyLength: Infinity,
            url: `${APILINK}/api/Alumnos`,
            headers: {
                'Authorization': `Bearer ${getToken()}`,
                'Content-Type': 'application/json'
            },
            data: submitData
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
                        key: "nombre",
                        label: "Nombre del Alumno",
                        type: "text",
                        placeholder: "Ingrese el nombre",
                        disabled: isDisabled,
                        required: isDisabled || true,
                        value: data.nombre,
                        onChange: { action: handleChangeData },
                        invalidText: "Ingrese un nombre",
                    }
                    ,
                    {
                        key: "apellido", label: "Apellido del Alumno",
                        type: "text", placeholder: "Ingrese el apellido",
                        disabled: isDisabled, required: true,
                        value: data.apellido,
                        onChange: { action: handleChangeData },
                        invalidText: "Ingrese un apellido",
                    },
                    {
                        key: "fecha", label: "Fecha de nacimiento",
                        type: "date",
                        disabled: isDisabled,
                        required: true,
                        value: data.fecha,
                        onChange: { action: handleChangeData },
                    },
                    {
                        key: "email", label: "Correo Electónico",
                        type: "email", placeholder: "Ingrese correo electónico",
                        disabled: isDisabled, required: true,
                        value: data.email,
                        onChange: { action: handleChangeData },
                        invalidText: "Ingrese un correo electónico válido",
                    },
                    {
                        key: "direccion", label: "Dirección",
                        type: "text", placeholder: "Ingrese la dirección",
                        disabled: isDisabled,
                        value: data.direccion,
                        onChange: { action: handleChangeData },
                    },
                    {
                        key: "genero", label: "Genero",
                        type: "select",
                        disabled: isDisabled, required: true,
                        invalidText: "Seleccione una opción",
                        value: data.genero,
                        onChange: { action: handleChangeData },
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
                        value: data.documento,
                        onChange: { action: handleChangeData },
                        invalidText: "Ingrese un número",
                    },
                    {
                        key: "documentoTipo", label: "Tipo de Documento",
                        type: "select",
                        disabled: isDisabled, required: true,
                        invalidText: "Seleccione un Tipo",
                        value: data.tipoDocumento,
                        onChange: { action: handleChangeData },
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
                    isDisabled ?
                        {
                            style: "icon",
                            type: "edit",
                            enabled: true,
                            onclick: { action: ((e) => { e.preventDefault(); setDisabled(false) }) },
                            submit: false,
                        }
                        :
                        {
                            style: "text",
                            type: "save",
                            submit: true,
                        }
                ]
            }
        })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [ModalTitle, isDisabled, data]);

    return (
        <>
            <Modal isOpen={open} datosModal={datosModal} handleClose={handleClose} ></Modal>
        </>
    )
}
export default ModalAlumnos;