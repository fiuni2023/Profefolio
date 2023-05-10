import React, { useState, useEffect } from 'react'
import axios from "axios";
import { useGeneralContext } from "../../../context/GeneralContext";
import APILINK from '../../../components/link';
import Modal from '../../../components/Modal';
import { toast } from 'react-hot-toast';
import ModalMensajeAlumno from './ModalMensajeAlumno';

function ModalAlumnos({
    show = false, 
    setShow = () => {},
    fetchFunc = ()=>{},
    selected_data,
    editing=false,
    onHide= () => {}
}) {
    const { getToken } = useGeneralContext()
    const disabled = false
    console.log(selected_data)
    console.log(editing)
    const [alumno, setAlumno] = useState("")
    const [openAviso, setOpenAviso] = useState(false)
    const handleCreateSubmit = (e) => {
        e.preventDefault()
        console.log("creando")
        const nombre = document.getElementById("nombreAlu").value;
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
                if (response.status === 230){
                    console.log(response)
                    setAlumno(response.data)
                    setOpenAviso(true)
                }
                else if (response.status === 200) {
                    handleHide();
                    fetchFunc()
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
    const handleCancelAviso = ()=>{
        setOpenAviso(false)
    }
    
    const handleEditSubmit = (e) => {
        e.preventDefault()
        console.log("editando")
        const nombre = document.getElementById("nombreAlu").value;
        const apellido = document.getElementById("apellido").value;
        const fecha = document.getElementById("fecha").value;
        const email = document.getElementById("email").value;
        const direccion = document.getElementById("direccion").value;
        const genero = document.getElementById("genero").value;
        const documento = document.getElementById("documento").value;
        const tipoDocumento = document.getElementById("tipoDocumento").value;
        let data = JSON.stringify({
            "id": selected_data.id,
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
            method: 'put',
            maxBodyLength: Infinity,
            url: `${APILINK}/api/Alumnos/${selected_data.id}`,
            headers: {
                'Authorization': `Bearer ${getToken()}`,
                'Content-Type': 'application/json'
            },
            data: data
        };
        axios(config)
            .then(function (response) {
                if (response.status >= 200) {
                    handleHide();
                    fetchFunc()
                    toast.success("Editado correctamente");
                }
            })
            .catch(function (error) {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                }
            });
        // fetchFunc()
    }

    const handleDelete = () => {
        console.log("borrando")
        let config = {
            method: 'delete',
            maxBodyLength: Infinity,
            url: `${APILINK}/api/Alumnos/${selected_data.id}`,
            headers: {
                'Authorization': `Bearer ${getToken()}`,
                'Content-Type': 'application/json'
            },
        };
        axios(config)
            .then(function (response) {
                if (response.status >= 200) {
                    handleHide();
                    fetchFunc()
                    toast.success("Borrado correctamente");
                }
            })
            .catch(function (error) {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                }
            });
        fetchFunc()
    }

    useEffect(()=>{
        if(selected_data){
            document.getElementById("nombreAlu").value = selected_data.nombre
            document.getElementById("apellido").value = selected_data.apellido
            document.getElementById("fecha").value = selected_data.nacimiento.split('T')[0]
            document.getElementById("email").value = selected_data.email
            document.getElementById("direccion").value = selected_data.direccion
            document.getElementById("genero").value = selected_data.genero === "Masculino"? "M": "F"
            document.getElementById("documento").value = selected_data.documento 
            document.getElementById("tipoDocumento").value = selected_data.documentoTipo
        }
    },[selected_data])

    const [datosModal, setDatosModal] = useState(null);
    const [deleting, setDeleting] = useState(false)
    const noAction =(e)=>{
        e.preventDefault()
    }
    useEffect(() => {
        setDatosModal({
            header: selected_data? deleting? "ELIMINAR ALUMNO?" : "Editar Alumno" : "Agregar Alumno",
            form: {
                onSubmit: deleting ? {action: (e)=>{noAction(e)}} : editing ? {action: ()=>{ handleEditSubmit()}} : {action: ()=>{handleCreateSubmit()}},
                inputs: [
                    {
                        key: "nombreAlu", label: "Nombre del Alumno",
                        type: "text", placeholder: "Ingrese el nombre",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un nombre",
                    },
                    {
                        key: "apellido", label: "Apellido del Alumno",
                        type: "text", placeholder: "Ingrese el apellido",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un apellido",
                    },
                    {
                        key: "fecha", label: "Fecha de nacimiento",
                        type: "date", placeholder: "Seleccione la fecha",
                        disabled: disabled,
                        maxDate: true,
                        required: true,
                    },
                    {
                        key: "email", label: "Correo Electónico",
                        type: "email", placeholder: "Ingrese correo electónico",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un correo electónico válido",
                    },
                    {
                        key: "direccion", label: "Dirrección",
                        type: "text", placeholder: "Ingrese la dirrección",
                        disabled: disabled,
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
                buttons: selected_data?
                    !deleting?
                    [
                        {
                            style: "text",
                            type: "danger",
                            onclick: {action: ()=>{setDeleting(true)}}
                        },
                        {
                            style: "text",
                            type: "save",
                            submit: true,
                        },
                    ]
                    :
                    [  
                        {
                            style: "text",
                            type: 'cancel',
                            onclick: {action: ()=>{setDeleting(false)}}
                        },
                        {
                            style: "text",
                            type: "danger",
                            onclick: {action: ()=>{handleDelete()}}
                        },
                    ]
                :
                [
                    {
                        style: "text",
                        type: "create",
                        submit: true,
                    },
                ]
            }
        })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [ disabled, selected_data, deleting ]);

    const handleHide = () => {
        setDeleting(false)
        onHide()
    }

    const addNewExistingSuccess=()=>{
        handleHide()
        fetchFunc()
    }
    return (
        <>
            <Modal show={show} onHide={handleHide} datosModal={datosModal}/>
            <ModalMensajeAlumno isOpen={openAviso} onAdd={onHide} student={alumno} onCancel={handleCancelAviso} onSuccess={addNewExistingSuccess}/>
        </>
    )
}
export default ModalAlumnos