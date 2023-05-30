/* eslint-disable */
import React, { useState, useEffect } from 'react'
import { useGeneralContext } from '../../../../../context/GeneralContext';
import Modal from '../../../../../components/Modal';
import { toast } from 'react-hot-toast';
import axios from 'axios';
import APILINK from '../../../../../components/link';
import ClassesService from '../Helper/DocumentoHelper';




function CreateModalDocumento({
    show = false,
    onHide = () => { },
    fetchFunc = () => { },
    selected_data
}) {

    const { getToken, cancan, verifyToken,getMateriaId,getUserId } = useGeneralContext();

    
    const [nombre, setNombre] = useState('');
    const [enlace, setEnlace] = useState(''); 
    const disabled = false

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

    const handleCreateSubmit = async () => {
      const nombre = document.getElementById("nombre").value;
      const enlace = document.getElementById("enlace").value;

      const body = {
        "nombre":nombre,
        "enlace":enlace,
        "materiaListaId":1,
      };
  
      console.log('body',body);
      ClassesService.createDocumento(body, getToken())

        .then(() => {
          toast.success("Los datos fueron enviados correctamente.");
          window.location.reload();
        })
        .catch(() => {
          toast.error("No se pudieron guardar los cambios. Intente de nuevo o recargue la página.");
        });
    };
  

    useEffect(() => {
        if (selected_data) {
            document.getElementById("nombre").value = selected_data.nombre;
            document.getElementById("apellido").value = selected_data.apellido;
        }
    }, [selected_data])

    const [datosModal, setDatosModal] = useState(null);
    const [deleting, setDeleting] = useState(false);

    const getInputs = () => {
        if (selected_data) return [
            {
                key: "nombre", label: "Nombre del Profesor",
                type: "text", placeholder: "Ingrese el nombre",
                disabled: disabled, required: true,
                invalidText: "Ingrese un nombre",
            },
           
           
            {
                key: "direccion", label: "Dirrección",
                type: "text", placeholder: "Ingrese la dirrección",
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
            header: selected_data ? deleting ? "ELIMINAR Documento?" : "Editar Profesor" : "Agregar Documento",
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
                                onclick: { action: () => {  } }
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
    }, [disabled, selected_data, deleting]);

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