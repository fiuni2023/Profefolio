import React, { useState, useEffect } from 'react'
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";
import APILINK from '../../components/link';
import Modal from '../../components/Modal';
import { toast } from 'react-hot-toast';

function ModalColegios({tituloModal, isOpen, disabled}) {
    const { getToken } = useGeneralContext()
    const [administradores, setAdministradores] = useState([]);
    const [open, setOpen] = useState(isOpen ? isOpen : false);
    const [ModalTitle, setModalTitle] = useState(tituloModal ? tituloModal : "");
    const [isDisabled, setDisabled] = useState(disabled ? disabled : false); 
    
    useEffect(() => {setModalTitle(ModalTitle)}, [ModalTitle]);
    useEffect(() => {setOpen(isOpen) }, [isOpen]);
    useEffect(() => {setDisabled(disabled)}, [disabled]);
    //Get administadores
    useEffect(() => {
        let config = {
            method: 'get',
            url: `${APILINK}/api/administrador`,
            headers: {
                'Authorization': `Bearer ${getToken()}`,
                'Content-Type': 'application/json'
            },
        };
        axios(config)
            .then(function (response) {
                setAdministradores(response.data);  
            })
            .catch(function (error) {
                console.log(error);
            });
    }, [getToken])

    const handleSubmit = () => {
        const nombre = document.getElementById("nombre").value;
        const id = document.getElementById("administrador").value;

            let data = JSON.stringify({
                "nombre": nombre,
                "personaId": id
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
                    setOpen(false);
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

      useEffect (() => {
        setDatosModal({
            header: ModalTitle,
            form: {
              onSubmit: {action: handleSubmit},
              inputs: [
                {
                  key: "nombre", label: "Nombre del colegio",
                  type: "text", placeholder: "Ingrese el nombre",
                  disabled: isDisabled, required: true,
                  invalidText: "Ingrese un nombre",
                },
                {
                  key: "administrador", label: "Administrador",
                  type: "select", 
                  disabled: isDisabled, required: true,
                  invalidText: "Seleccione un administrador",
                  select: {
                    default: "Seleccione el administrador",
                    options: administradores,
                  }
                }
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
      }, [administradores]);

    return (
        <>
            <Modal isOpen={open} datosModal={datosModal}></Modal>
        </>
    )
}
export default ModalColegios;