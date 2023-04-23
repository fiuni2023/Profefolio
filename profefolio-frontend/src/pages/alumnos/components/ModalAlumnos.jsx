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

    const [nombre, setNombre] = useState(selectedData? selectedData.nombre :'')
    const [apellido, setApellido] = useState(selectedData? selectedData.apellido :'')
    const [fecha, setFecha] = useState(selectedData? selectedData.fecha :'')
    const [email, setEmail] = useState(selectedData? selectedData.email :'')
    const [direccion, setDireccion] = useState(selectedData? selectedData.direccion :'')
    const [genero, setGenero] = useState(selectedData? selectedData.genero :'')
    const [documento, setDocumento] = useState(selectedData? selectedData.genero :'')
    const [tipoDocumento, setTipoD] = useState(selectedData? selectedData.tipoDocumento :'')

    useEffect(() => { setModalTitle(ModalTitle) }, [ModalTitle]);
    useEffect(() => { setOpen(isOpen) }, [isOpen]);
    useEffect(() => { setDisabled(disabled) }, [disabled]);

    const handleNombreChange = (event) => {
        const newValue = event.target.value;
        console.log(newValue)
        setNombre(newValue);
        console.log(nombre)
      };

    const handleSubmit = () => {

        let data = JSON.stringify({
            "nombre": nombre,
            "apellido": apellido,
            "nacimiento": fecha,
            "documento": documento,
            "genero": genero,
            "direccion": direccion,
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
                        key: "nombre",
                        label: "Nombre del Alumno",
                        type: "text",
                        placeholder: "Ingrese el nombre",
                        disabled: isDisabled,
                        required: isDisabled || true,
                        value: nombre,
                        onChange: { action:  handleNombreChange},
                        invalidText: "Ingrese un nombre",
                    }
                    ,
                    {
                        key: "apellido", label: "Apellido del Alumno",
                        type: "text", placeholder: "Ingrese el apellido",
                        disabled: isDisabled, required: true,
                        value: apellido,
                        onChange: (e)=> setApellido(e.target.value),
                        invalidText: "Ingrese un apellido",
                    },
                    {
                        key: "fecha", label: "Fecha de nacimiento",
                        type: "date", placeholder: "Seleccione la fecha",
                        disabled: isDisabled,
                        required: true,
                        value: fecha,
                        onChange: (e)=> setFecha(e.target.value),
                        max: new Date().toISOString().slice(0, 10),
                    },
                    {
                        key: "email", label: "Correo Electónico",
                        type: "email", placeholder: "Ingrese correo electónico",
                        disabled: isDisabled, required: true,
                        value: email,
                        onChange: (e)=> setEmail(e.target.value),
                        invalidText: "Ingrese un correo electónico válido",
                    },
                    {
                        key: "direccion", label: "Dirrección",
                        type: "text", placeholder: "Ingrese la dirrección",
                        disabled: isDisabled,
                        value: direccion,
                        onChange: (e)=> setDireccion(e.target.value),
                    },
                    {
                        key: "genero", label: "Genero",
                        type: "select",
                        disabled: isDisabled, required: true,
                        invalidText: "Seleccione una opción",
                        value: genero,
                        onChange: (e)=> setGenero(e.target.value),
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
                        value: documento,
                        onChange: { action: ((e)=> setDocumento(e.target.value))},
                        invalidText: "Ingrese un número",
                    },
                    {
                        key: "tipoDocumento", label: "Tipo de Documento",
                        type: "select",
                        disabled: isDisabled, required: true,
                        invalidText: "Seleccione un Tipo",
                        value: tipoDocumento,
                        onChange: { action: ((e)=> setTipoD(e.target.value))},
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
                    disabled ?
                        {
                            style: "icon",
                            type: "edit",
                            enabled: true,
                            onclick: { action: ((e) => { e.preventDefault(); console.log("edit") }) },
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
    }, [ModalTitle, isDisabled]);

    return (
        <>
            <Modal isOpen={open} datosModal={datosModal} handleClose={handleClose} ></Modal>
        </>
    )
}
export default ModalAlumnos;