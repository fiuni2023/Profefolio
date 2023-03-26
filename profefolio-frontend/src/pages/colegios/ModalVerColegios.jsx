import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import styles from "./ModalVerColegios.module.css"
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";
import { toast } from 'react-hot-toast';
import APILINK from '../../components/link';
import { useNavigate } from "react-router-dom";

function ModalVerColegios({ idColegio, setShow, show }) {
  const handleClose = () => setShow(false);
  const [colegio, setColegio] = useState([]);
  const { getToken, verifyToken, cancan } = useGeneralContext();
  const nav = useNavigate();
  const navigate = useNavigate();
  const [disabled, setDisabled] = useState(true);
  const [nombreCompletoAdmin, setNombreCompletoAdmin]=useState("");
  useEffect(() => {
    console.log(idColegio);
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
          setColegio(response.data); //Guarda los datos
          console.log(colegio);

        })
        .catch(function (error) {
          console.log(error);
        });
    }
  }, [cancan, verifyToken, nav, getToken, idColegio])

  const handleEdit=()=>{
    setDisabled(false);
  }
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
                <input required type="text" id={styles.inputColegio} name="colegio-nombre" value={colegio.nombre || ''} disabled={disabled}></input><br />
                
                <label htmlFor="administrador"><strong> Administrador</strong></label><br />
                <input required type="text" id={styles.inputColegio} name="colegio-admin" value={colegio.nombreAdministrador+" "+colegio.apellido || ''} disabled={disabled}></input><br />
               
               


              </form>
            </div>
          </Modal.Body>
          <Modal.Footer id={styles.modalContenido}>
            <Button className={styles.btnCancelar} onClick={handleClose} >Eliminar</Button>
            <Button className={styles.btnGuardar} onClick={handleEdit}>Editar</Button>
          </Modal.Footer>
        </Modal>
      </div>



    </>)
}
export default ModalVerColegios