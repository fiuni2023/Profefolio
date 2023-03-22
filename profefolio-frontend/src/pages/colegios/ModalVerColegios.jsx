import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import styles from "./ModalVerColegios.module.css"
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";
import { toast } from 'react-hot-toast';
import APILINK from '../../components/link';
import { AiOutlineEye } from "react-icons/ai";
import { useNavigate } from "react-router-dom";
function ModalVerColegios({idColegio}) {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [colegio, setColegio]=useState([]);
    const { getToken, verifyToken, cancan } = useGeneralContext()

    const nav = useNavigate()
  
    const navigate = useNavigate()

useEffect(() => {
console.log(idColegio)
    verifyToken()
    if (!cancan("Master")) {
      nav("/")
    } else {
      var config = {
        method: 'get',
        url: `${APILINK}/api/ColegiosFull/${idColegio}`,
        headers: {
          'Authorization': `Bearer ${getToken()}`
        }
      };
      axios(config)
        .then(function (response) {
          setColegio(response.data.dataList); //Guarda los datos
          console.log(colegio);
        })
        .catch(function (error) {
          console.log(error);
        });
    }
  }, [cancan, verifyToken, nav, getToken])

    return (
        <>
        <div>
        <button className={styles.iconButton}onClick={handleShow}><AiOutlineEye/></button>
        <Modal show={show} onHide={handleClose}>
                <Modal.Header id={styles.modalContenido} closeButton onClick={handleClose}>
                    <h5 className={styles.tituloForm} >Informacion Colegio</h5>
                </Modal.Header>
                <Modal.Body id={styles.modalContenido}>
                    <div>
                        <form>
                            

                        </form>
                    </div>
                </Modal.Body>
                <Modal.Footer id={styles.modalContenido}>
                    <Button className={styles.btnCancelar} onClick={handleClose} >Eliminar</Button>
                    <Button className={styles.btnGuardar} onClick={handleClose}>Editar</Button>
                </Modal.Footer>
            </Modal>
        </div>



        </>)
}
export default ModalVerColegios