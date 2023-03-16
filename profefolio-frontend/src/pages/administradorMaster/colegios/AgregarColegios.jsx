import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import styles from "./ModalAgregarColegios.module.css"
import { BsFillPlusCircleFill } from "react-icons/bs"
import axios from "axios";
import { useGeneralContext } from "../../../context/GeneralContext";
function ModalAgregarColegios() {
    const { getToken } = useGeneralContext()
    const [nombreColegio, setNombreColegio] = useState("");
    const [idAdmin, setIdAdmin] = useState(0);
    const [administradores, setAdministradores] = useState([]);
    const [mensajeError, setMensajeError] = useState(null);
    //Get administadores
    var config = {
        method: 'get',
        url: 'https://localhost:7063/api/administrador/page/0',
        headers: {
            'Authorization': `Bearer ${getToken()}`,
            'Content-Type': 'application/json'
        },


    };
    useEffect(() => {
        axios(config)
            .then(function (response) {

                setAdministradores(response.data.dataList);
            })
            .catch(function (error) {
                console.log(error);
            });
    }, [])
    const handleAdmin = (idAdmin) => {
        setIdAdmin(idAdmin);
    }

    const handleSubmit = () => {

        if (nombreColegio == "") {
            setMensajeError("Rellenar todos los campos");
            return
        }
        if (idAdmin == 0) {
            setMensajeError("Rellenar todos los campos");
            return
        }
        else {
            var data = JSON.stringify({
                "nombre": nombreColegio,
                "personaId": idAdmin
            });

            var config = {
                method: 'post',
                maxBodyLength: Infinity,
                url: 'https://localhost:7063/api/Colegios',
                headers: {
                    'Authorization': `Bearer ${getToken()}`,
                    'Content-Type': 'application/json'
                },
                data: data
            };

            axios(config)
                .then(function (response) {
                    if (response.status >= 400) {
                        console.log("Hubo un error")
                    }
                    else if (response.status >= 200) {
                        console.log("Guardado correctamente");
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
            handleClose();
        }

    }
    const handleNombreColegio = (event) => {
        setMensajeError("");
        setNombreColegio(event.target.value)


    }
    const handleIDAdmin = (event) => {
        setMensajeError("")
        handleAdmin(event.target.value)
    }
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    return (
        <>
            <button className={styles.buttonAgregar} onClick={handleShow}><BsFillPlusCircleFill className={styles.iconoAgregar} /></button>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header id={styles.modalContenido} closeButton onClick={handleClose}>
                    <h5 >Agregar Colegio</h5>
                </Modal.Header>
                <Modal.Body id={styles.modalContenido}>
                    <div>
                        <form>
                            <label htmlFor="colegio-nombre"><strong> Colegio</strong></label><br />
                            <input required type="text" id={styles.inputColegio} name="colegio-nombre" onChange={event => handleNombreColegio(event)}></input><br />
                            <p className={styles.mensajeError}>{mensajeError}</p>
                            <label htmlFor="administrador"><strong> Administrador</strong></label><br />

                            <select required name="admin" value="Seleccionar Administrador" onChange={event => handleIDAdmin(event)} className={styles.selectAdmin}>
                                <option disabled value="Seleccionar Administrador">Seleccione Administrador</option>
                                {administradores.map((administrador) =>
                                    <option key={administrador.id} value={administrador.id}>{administrador.nombre} {administrador.apellido}</option>
                                )}
                            </select>
                            <p className={styles.mensajeError}>{mensajeError}</p>


                        </form>
                    </div>
                </Modal.Body>
                <Modal.Footer id={styles.modalContenido}>
                    <Button className={styles.btnCancelar} onClick={handleClose} >Cancelar</Button>
                    <Button className={styles.btnGuardar} onClick={() => handleSubmit()}>Guardar</Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}
export default ModalAgregarColegios