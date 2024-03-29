/* eslint-disable */
import React, { useState, useEffect } from 'react'
import { useGeneralContext } from '../../../../../context/GeneralContext';
import Modal from '../../../../../components/Modal';
import { toast } from 'react-hot-toast';
import axios from 'axios';
import APILINK from '../../../../../components/link';
import ClassesService from '../Helper/DocumentoHelper';
import { useModularContext } from '../../../context';




function CreateModalDocumento({
    show = false,
    onHide = () => { },
    fetchFunc = () => { },
    selectData}) {


    const { getToken } = useGeneralContext();
    const token = getToken()
    const {stateController} = useModularContext()
    const {materiaId} = stateController


    const disabled = false

    const handleEditSubmit = () => {
        const nombre = document.getElementById("nombre").value;
        const enlace = document.getElementById("enlace").value;
      
        let data = {
            "nombre": nombre,
            "enlace": enlace,
            "materiaListaId": materiaId
        }

        ClassesService.putDocumento(selectData.id, data, token )
            .then(() => {
                toast.success("Guardado exitoso");
                handleHide()
            })
            .catch(error => {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                //  toast.error(error.response.data?.errors.Password ? error.response.data?.errors.Password[0] : error.response.data?.errors.Email[0])
                }
            });
    }

    useEffect(() => {
      
        if (selectData) {
            document.getElementById("nombre").value = selectData.nombre;
            document.getElementById("enlace").value = selectData.enlace;
        }
    }, [selectData])


    const handleDelete = () => {
        axios.delete(`${APILINK}/api/Documento/${selectData.id}`, {
            headers: {
              Authorization: `Bearer ${token}`,
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
                toast.error(error.response.data)
              }
            });
    }

    const handleCreateSubmit = async () => {
      const nombre = document.getElementById("nombre").value;
      const enlace = document.getElementById("enlace").value;

      const body = {
        "nombre":nombre,
        "enlace":enlace,
        "materiaListaId":materiaId,
      };
  
      ClassesService.createDocumento(body, token)

        .then(() => {
          toast.success("Los datos fueron enviados correctamente.");
          onHide()
        })
        .catch(() => {
          toast.error("No se pudieron guardar los cambios. Intente de nuevo o recargue la página.");
        });
    };
  

    useEffect(() => {
        if (selectData) {
            document.getElementById("nombre").value = selectData.nombre;
            document.getElementById("enlace").value = selectData.enlace;
        }
    }, [selectData])

    const [datosModal, setDatosModal] = useState(null);
    const [deleting, setDeleting] = useState(false);

    const getInputs = () => {
        if (selectData) return [
            {
                key: "nombre", label: "Nombre del documento",
                type: "text", placeholder: "Ingrese el nombre",
                disabled: disabled, required: true,
                invalidText: "Ingrese un nombre",
            },
           
           
            {

                key: "enlace", label: "Dirección",
                type: "text", placeholder: "Ingrese el nombre del document",

                disabled: disabled,
            },

        ]
        return [
            {
                key: "nombre", label: "Nombre del documento",
                type: "text", placeholder: "Ingrese el nombre del documento",
                disabled: disabled, required: true,
                invalidText: "Ingrese el nombre del documento",
            },
            {
                key: "enlace", label: "Enlace del documento",
                type: "text", placeholder: "Ingrese el enlace del documento",
                disabled: disabled, required: true,
                invalidText: "Ingrese el enlace del documento",
            },
           
                    

                
            
        ]
    }

    useEffect(() => {
        setDatosModal({
            header: selectData ? deleting ? "ELIMINAR Documento?" : "Editar Documento" : "Agregar Documento",
            form: {
                onSubmit: { action: () => { } },
                inputs: getInputs(),
                buttons: selectData ?
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
                                onclick: { action: (event) => {
                                    event.preventDefault()
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
    }, [disabled, selectData, deleting]);

    const handleHide = () => {
        document.getElementById("nombre").value = "";
        document.getElementById("enlace").value = "";
      

        setDeleting(false)
        fetchFunc()
        onHide()
    }

    return (
        <>
            <Modal show={show} onHide={handleHide} datosModal={datosModal} />
        </>
    )
}
export default CreateModalDocumento