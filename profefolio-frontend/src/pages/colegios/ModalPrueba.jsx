import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import styles from "./ModalVerColegios.module.css"
import APILINK from '../../components/link';
import { useNavigate } from "react-router-dom";
import { toast } from 'react-hot-toast';
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";

function ModalPrueba(props) {

    const { datoIdColegio, setShow, show } = props;
    const [colegio, setColegio] = useState([""]);
    const { getToken, verifyToken, cancan } = useGeneralContext();
    const nav = useNavigate();
    const handleClose = () => setShow(false);

    useEffect(() => {
        if (show) {

            verifyToken()
            if (!cancan("Master")) {
                nav("/")
            } else {
                let config = {
                    method: 'get',
                    url: `${APILINK}/api/ColegiosFull/${datoIdColegio}`,
                    headers: {
                        'Authorization': `Bearer ${getToken()}`
                    }
                };
                axios(config)
                    .then(function (response) {
                        setColegio(response.data); //Guarda los datos
                        console.log(colegio);


                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }

        }
    }, [cancan, verifyToken, nav, getToken, datoIdColegio])



    return (

        <>
            <div>

                <Modal show={show} onHide={handleClose}>
                    <Modal.Header id={styles.modalContenido} closeButton onClick={handleClose}>
                        <h5 className={styles.tituloForm} >Informacion Colegio</h5>
                    </Modal.Header>
                    <Modal.Body id={styles.modalContenido}>
                        <div>
                            <form>
                                <label htmlFor="colegio-nombre" className={styles.labelForm}>Nombre Colegio</label><br />

                                <div> <input type="text" id={styles.inputColegio} value={colegio.nombre} disabled></input><br />
                                    <br /></div>



                                <label htmlFor="administrador"><strong> Administrador</strong></label><br />

                                <div> < input required type="text" id={styles.inputColegio} name="colegio-admin" defaultValue={colegio.nombreAdministrador + " " + colegio.apellido || ''} disabled>
                                </input>
                                    <br /></div>



                            </form>

                        </div>
                    </Modal.Body>
                    <Modal.Footer >


                    </Modal.Footer>
                </Modal>
            </div>

        </>)
}
export default ModalPrueba