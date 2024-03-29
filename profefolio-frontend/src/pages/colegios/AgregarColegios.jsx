import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import styles from "./ModalAgregarColegios.module.css"
import { BsFillPlusCircleFill } from "react-icons/bs"
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";
import { toast } from 'react-hot-toast';
import APILINK from '../../components/link';
function ModalAgregarColegios(props) {
    const { onSubmit = () => { }, triggerState = () => { }, currentPage}=props;
    const { getToken } = useGeneralContext()
    const [nombreColegio, setNombreColegio] = useState("");
    const [idAdmin, setIdAdmin] = useState(0);
    const [administradores, setAdministradores] = useState([]);
   
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
                toast.error(error);
            });
    }, [getToken])
    const handleAdmin = (idAdmin) => {
        setIdAdmin(idAdmin);
    }

    const handleSubmit = () => {

        if (nombreColegio === "") {
            toast.error("Ingrese todos los datos");
            return
        }
        if (idAdmin === 0) {
            toast.error("Ingrese todos los datos");
            return
        }
        else {
            let data = JSON.stringify({
                "nombre": nombreColegio,
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
                    if (response.status >= 400) {
                        toast.error("Hubo un error")
                    }
                    else if (response.status >= 200) {
                        
                       handleUpdate();
                       
                        setNombreColegio("");
                        setIdAdmin("");

                        toast.success("Guardado correctamente");
                    }
                })
                .catch(function (error) {
                    if (typeof (error.response.data) === "string" ? true : false) {
                        toast.error(error.response.data)
                    } else {
                    }
                });
            handleClose();
        }

    }
    const handleNombreColegio = (event) => {

        setNombreColegio(event.target.value)

    }
    const handleIDAdmin = (event) => {

        handleAdmin(event.target.value)
    }
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const handleUpdate = () => {
        axios.get(`${APILINK}/api/ColegiosFull/page/${currentPage}`, {
          headers: {
            Authorization: `Bearer ${getToken()}`,
          }
        })
    
          .then(response => {
            triggerState(response.data.dataList);
            onSubmit(response.data)
        
    
    
          })
          .catch(error => {
            toast.error(error);
          });
      }
    
    
    return (
        <>
            <button className={styles.buttonAgregar} onClick={handleShow}><BsFillPlusCircleFill className={styles.iconoAgregar} /></button>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header id={styles.modalContenido} closeButton onClick={handleClose}>
                    <h5 className={styles.tituloForm} >Agregar Colegio</h5>
                </Modal.Header>
                <Modal.Body id={styles.modalContenido}>
                    <div>
                        <form onSubmit={handleSubmit}>
                            <label htmlFor="colegio-nombre" className={styles.labelForm}>Nombre</label><br />
                            <input required type="text" id={styles.inputColegio} name="colegio-nombre" onChange={event => handleNombreColegio(event)}></input><br />
                            
                            <label htmlFor="administrador"><strong> Administrador</strong></label><br />

                            <select required name="admin" value={idAdmin || ''} onChange={event => handleIDAdmin(event)} className={styles.selectAdmin}>
                                <option disabled value={0 || ''}>Seleccione Administrador</option>
                                {administradores.map((administrador) =>
                                    <option key={administrador.id} value={administrador.id || ''}>{administrador.nombre} {administrador.apellido}</option>
                                )}
                            </select>
                            


                        </form>
                    </div>
                </Modal.Body>
                <Modal.Footer >
                    <Button className={styles.btnCancelar} onClick={handleClose} >Cancelar</Button>
                    <Button className={styles.btnGuardar} onClick={() => handleSubmit()}>Guardar</Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}
export default ModalAgregarColegios