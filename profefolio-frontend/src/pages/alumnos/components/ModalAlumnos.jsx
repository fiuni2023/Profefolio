import React, { useState, useEffect } from 'react'
import { useGeneralContext } from "../../../context/GeneralContext";
import Modal from '../../../components/Modal';
import { toast } from 'react-hot-toast';
import ModalMensajeAlumno from './ModalMensajeAlumno';
import StudentsService from '../helpers/StudentHelper';
function ModalAlumnos({
    selected_data,
    show = false,
    fetchFunc = () => { },
    onHide = () => { }
}) {
    const { getToken } = useGeneralContext()
    const { updateStudent, createStudent, deleteStudent } = StudentsService
    const disabled = false
    const [alumno, setAlumno] = useState("")
    const [openAviso, setOpenAviso] = useState(false)

    useEffect(() => {
        if (selected_data) {
            document.getElementById("nombreAlu").value = selected_data.nombre
            document.getElementById("apellido").value = selected_data.apellido
            document.getElementById("fecha").value = selected_data.nacimiento.split('T')[0]
            document.getElementById("email").value = selected_data.email
            document.getElementById("direccion").value = selected_data.direccion
            document.getElementById("genero").value = selected_data.genero === "Masculino" ? "M" : "F"
            document.getElementById("documento").value = selected_data.documento
            document.getElementById("tipoDocumento").value = selected_data.documentoTipo
        }
    }, [selected_data])

    let nombre, apellido, fecha, email, direccion, genero, documento, tipoDocumento;

    const setValues = () => {
        nombre = document.getElementById("nombreAlu").value;
        apellido = document.getElementById("apellido").value;
        fecha = document.getElementById("fecha").value;
        email = document.getElementById("email").value;
        direccion = document.getElementById("direccion").value;
        genero = document.getElementById("genero").value;
        documento = document.getElementById("documento").value;
        tipoDocumento = document.getElementById("tipoDocumento").value;
    };


    const handleCreateSubmit = async (e) => {
        e.preventDefault()
        setValues();

        const body = JSON.stringify({
            nombre: nombre,
            apellido: apellido,
            nacimiento: fecha,
            documento: documento,
            genero: genero,
            direccion: direccion,
            email: email,
            documentoTipo: tipoDocumento
        });

        try {
            const result = await createStudent(body, getToken());
            if (result && result.status === 200) {
                handleHide();
                fetchFunc();
                toast.success("Guardado correctamente");
            } else if (result && result.status === 230) {
                setAlumno(result.data);
                setOpenAviso(true);
            }
        } catch (error) {
            if (typeof error.response.data === "string") toast.error(error.response.data);
        }
    };
    const handleCancelAviso = () => {
        setOpenAviso(false)
    }

    const handleEditSubmit = async (e) => {
        e.preventDefault()
        setValues();
        const body = JSON.stringify({
            id: selected_data.id,
            nombre: nombre,
            apellido: apellido,
            nacimiento: fecha,
            documento: documento,
            genero: genero,
            direccion: direccion,
            email: email,
            documentoTipo: tipoDocumento
        });

        try {
            const result = await updateStudent(selected_data.id, body, getToken());
            if (result && result.status === 200) {
                handleHide();
                fetchFunc();
                toast.success("Editado correctamente");
            }
        } catch (error) {
            if (typeof error.response.data === "string") toast.error(error.response.data);
        }

        fetchFunc();
    };

    const handleDelete = async () => {
        try {
            const result = await deleteStudent(selected_data.idColegioAlumno, getToken());
            if (result && result.status === 200) {
                handleHide();
                fetchFunc();
                toast.success("Borrado correctamente");
            }
        } catch (error) {
            if (typeof error.response.data === "string") {
                toast.error(error.response.data);
            }
        }
    };


    const [datosModal, setDatosModal] = useState(null);
    const [deleting, setDeleting] = useState(false)
    const noAction = (e) => {
        e.preventDefault()
    }
    useEffect(() => {
        setDatosModal({
            header: selected_data ? deleting ? "¿DESEA ELIMINAR EL ALUMNO?" : "Editar Alumno" : "Agregar Alumno",
            form: {
                onSubmit: deleting ? { action: (e) => { noAction(e) } } : selected_data ? { action: (e) => { handleEditSubmit(e) } } : { action: (e) => { handleCreateSubmit(e) } },
                inputs: [
                    {
                        md: 6, lg: 6,
                        key: "nombreAlu", label: "Nombre del Alumno",
                        type: "text", placeholder: "Ingrese el nombre",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un nombre",
                    },
                    {
                        md: 6, lg: 6,
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
                        key: "direccion", label: "Dirección",
                        type: "text", placeholder: "Ingrese la dirección",
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
                        lg: 6, md: 6,
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
                        lg: 6, md: 6,
                        key: "documento", label: "Documento",
                        type: "text", placeholder: "Ingrese el número de documento",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un número",
                    },
                ],
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
                                submit: true,
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
                                type: "confirm",
                                onclick: { action: () => { handleDelete() } }
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
    }, [disabled, selected_data, deleting]);

    const handleHide = () => {
        setDeleting(false)
        onHide()
    }

    const addNewExistingSuccess = () => {
        handleHide()
        fetchFunc()
    }
    return (
        <>
            <Modal show={show} onHide={handleHide} datosModal={datosModal} />
            <ModalMensajeAlumno isOpen={openAviso} onAdd={onHide} student={alumno} onCancel={handleCancelAviso} onSuccess={addNewExistingSuccess} />
        </>
    )
}
export default ModalAlumnos