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
  const [administradores, setAdministradores] = useState([]);
  const [nombreNuevoCol, setNombreNuevoCol]=useState("")
  const [idAdmin, setIdAdmin] = useState(0);

  //Lamada de los datos del colegio
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
//Llamada para obtener los datos de admninistradores
  const handleGetAdmin = () => {
    verifyToken()
    if (!cancan("Master")) {
      nav("/")
    } else {
      var config = {
        method: 'get',
        url: `${APILINK}/api/administrador`,
        headers: {
          'Authorization': `Bearer ${getToken()}`
        }
      };
      axios(config)
        .then(function (response) {
          setAdministradores(response.data);

        })
        .catch(function (error) {
          console.log(error);
        });
    }
  }

  //Trae los administradores y habilita la edicion
  const handleEdit = () => {
    handleGetAdmin();
    setDisabled(false);
    

  }
//Guardar el id del admin
  const handleAdmin = (idAdmin) => {
    setIdAdmin(idAdmin);
  }
  const handleIDAdmin = (event) => {
    handleAdmin(event.target.value)
  }

  const handleNombre=(nombre)=>{
    setNombreNuevoCol(nombre);
  }
  const handleInputColegio=(event)=>{
    handleNombre(event.target.value);
  }
  //Eliminar colegio
  const handleDelete = (id) => {
    //https://localhost:7063/api/Colegios/5

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
                <input required type="text" id={styles.inputColegio} name="colegio-nombre" value={colegio.nombre || ''} onChange={event=>handleInputColegio(event)} disabled={disabled}></input><br />

                <label htmlFor="administrador"><strong> Administrador</strong></label><br />
                {disabled
                  ? <div> < input required type="text" id={styles.inputColegio} name="colegio-admin" value={colegio.nombreAdministrador + " " + colegio.apellido || ''} disabled={disabled}>
                  </input>
                    <br /></div>

                  : <select required name="admin" onChange={event => handleIDAdmin(event)} className={styles.selectAdmin}>
                    
                    {administradores.map((administrador) =>
                      <option key={administrador.id} value={administrador.id || ''}>{administrador.nombre} {administrador.apellido}</option>
                    )}
                  </select>
                }
              </form>
            </div>
          </Modal.Body>
          <Modal.Footer id={styles.modalContenido}>
            {disabled
              ? <div>
                <Button className={styles.btnCancelar} onClick={handleDelete} >Eliminar</Button>
                <Button className={styles.btnGuardar} onClick={handleEdit}>Editar</Button>
              </div>
              :<div>
              <Button className={styles.btnCancelar} onClick={handleClose} >Cancelar</Button>
              <Button className={styles.btnGuardar} onClick={handleEdit}>Guardar</Button>
            </div>
          }

          </Modal.Footer>
        </Modal>
      </div>

    </>)
}
export default ModalVerColegios