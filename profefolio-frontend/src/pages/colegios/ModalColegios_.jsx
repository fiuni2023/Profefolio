import React, { useState, useEffect } from 'react'
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";
import APILINK from '../../components/link';
import Modal from '../../components/Modal';
import { toast } from 'react-hot-toast';

function ModalColegio({
    show = false,
    onHide = () => { },
    fetchFunc = () => { },
    selected_data,
    administrators = []
}) {
    const { getToken } = useGeneralContext()
    const disabled = false

    const [tempProf, setTempProf] = useState("")

    const handleCreateSubmit = () => {

        const nombre = document.getElementById("nombreColegio").value
        const idAdmin = document.getElementById("administradorColegio").value
        if (nombre === "" || idAdmin === "") return toast.error("Revise los datos, los campos deben ser completados")
        let data = JSON.stringify({
            "nombre": nombre,
            "personaId": idAdmin
        });
        let config = {
            method: 'post',
            maxBodyLength: Infinity,
            url: `${APILINK}/api/Colegios`,
            headers: {
                'Authorization': `Bearer ${getToken()}`,
                'Content-Type': 'application/json'
            },
            data: data
        };

        axios(config)
            .then(function (response) {
                if (response.status >= 200) {
                    toast.success("Guardado correctamente");
                    handleHide()
                    fetchFunc()
                }
            })
            .catch(function (error) {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                }
            });
    }

    const handleEditSubmit = () => {
        const nombre = document.getElementById("nombreColegio").value

        if (nombre === "" || tempProf === "" ) return toast.error("revisa los datos, los campos deben ser completados")
        let data = JSON.stringify({
            "id": selected_data.id,
            "nombre": nombre,
            "personaId": tempProf
        });
        let config = {
            method: 'put',
            maxBodyLength: Infinity,
            url: `${APILINK}/api/Colegios/${selected_data.id}`,
            headers: {
                'Authorization': `Bearer ${getToken()}`,
                'Content-Type': 'application/json'
            },
            data: data
        };

        axios(config)
            .then(response => {
                toast.success("Editado exitoso");
                handleHide()
                fetchFunc()
            })
            .catch(error => {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                    toast.error(error.response.data?.errors.Email[0])
                }
            });

    }

    const handleDelete = () => {

        axios.delete(`${APILINK}/api/Colegios/${selected_data.id}`, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })

            .then(response => {
                toast.success("Borrado exitoso");
                handleHide()
                fetchFunc()



            })
            .catch(error => {
                toast.error(error);
            });
    }
    useEffect(() => {
        if (selected_data) {
            document.getElementById("nombreColegio").value = selected_data.nombre
            setTempProf(selected_data.idAdmin)
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [selected_data])

    const [datosModal, setDatosModal] = useState(null);
    const [deleting, setDeleting] = useState(false)

    useEffect(() => {
        let adminNew=administrators;
        if(selected_data){
            let admin={
                id: selected_data.idAdmin,
                nombre: selected_data.nombreAdministrador,
                apellido: selected_data.apellido
            }
            let defaultValue={
                id: "",
                nombre: "Selecciona un",
                apellido: "administrador"
            }
            adminNew=[ defaultValue, ...adminNew, admin];
        }

        setDatosModal({
            header: selected_data ? deleting ? "ELIMINAR COLEGIO?" : "Editar Colegio" : "Agregar Colegio",
            form: {
                onSubmit: { action: () => { } },
                inputs: [
                    {
                        key: "nombreColegio", label: "Nombre del Colegio",
                        type: "text", placeholder: "Ingrese el nombre",
                        disabled: disabled, required: true,
                        invalidText: "Ingrese un nombre",
                    },
                    {
                        key: "administradorColegio", label: "Administrador",
                        type: "select",
                        disabled: disabled, required: true, value: tempProf,
                        invalidText: "Seleccione un Administrador",
                        onChange: {action: (e)=>{setTempProf(e.target.value)}},
                        select: {
                            options: adminNew.map((a) => {
                                return {
                                    value: a.id,
                                    text: a.nombre + " " + a.apellido
                                }
                            }),

                        }
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
                            onclick: { action: () => { handleCreateSubmit() } }
                        },
                    ]
            }
        })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [disabled, selected_data, administrators, deleting,tempProf]);

    const handleHide = () => {
        document.getElementById("nombreColegio").value = ""
        document.getElementById("administradorColegio").value = ""
        setDeleting(false)
        onHide()
    }

    return (
        <>
            <Modal show={show} onHide={handleHide} datosModal={datosModal} />
        </>
    )
}
export default ModalColegio