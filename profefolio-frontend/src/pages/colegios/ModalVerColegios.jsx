import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import styles from "./ModalVerColegios.module.css"
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";
import { toast } from 'react-hot-toast';
import APILINK from '../../components/link';
import { useNavigate } from "react-router-dom";

function ModalVerColegios({ idColegio, setShow, show, disabled, setDisabled, onSubmit = () => { }, triggerState = () => { } }) {
  const handleClose = () => setShow(false);
  const [colegio, setColegio] = useState([""]);
  const { getToken, verifyToken, cancan } = useGeneralContext();
  const nav = useNavigate();
  const [administradores, setAdministradores] = useState([]);
  const [nombreColegio, setNombreColegio] = useState("");
  const [nombreNuevoCol, setNombreNuevoCol] = useState("")
  const [idAdmin, setIdAdmin] = useState("");
  
  //Lamada de los datos del colegio
  useEffect(() => {
    if (show) {
      console.log(idColegio)
      verifyToken()
      if (!cancan("Master")) {
        nav("/")
      } else {
        let config = {
          method: 'get',
          url: `${APILINK}/api/ColegiosFull/${idColegio}`,
          headers: {
            'Authorization': `Bearer ${getToken()}`
          }
        };
        axios(config)
          .then(function (response) {
            setColegio(response.data); //Guarda los datos
            setNombreColegio(response.data.nombre);
            setDisabled(true);
            console.log(colegio);

          })
          .catch(function (error) {
            toast.error(error);
          });
      }
      handleEdit();
    }
  }, [cancan, verifyToken, nav, getToken, show, idColegio])
  //Llamada para obtener los datos de admninistradores
  const handleGetAdmin = () => {
    verifyToken()
    if (!cancan("Master")) {
      nav("/")
    } else {
      let config = {
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
          toast.error(error.resonse.data);
        });
    }
  }

  //Trae los administradores y habilita la edicion
  const handleEdit = () => {
    handleGetAdmin();
    setDisabled(false);
  }

  const handleSubmit = () => {
    console.log(idAdmin);

    axios.put(`${APILINK}/api/Colegios/${idColegio}`, {
      "nombre": nombreNuevoCol,
      "personaId": idAdmin

    }, {
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    })
      .then(response => {
        triggerState(response.data)
        onSubmit(response.data)
        toast.success("Guardado exitoso");
        handleClose();
        setNombreNuevoCol("");
        setIdAdmin("");

      })
      .catch(error => {

        toast.error(error.response.data)

      });

  }


  //Guardar el id del admin
  const handleIDAdmin = (event) => {
    setIdAdmin(event.target.value)
    console.log(typeof event.target.value);
  }

  const handleNombre = (nombre) => {
    setNombreNuevoCol(nombre);
  }
  const handleInputColegio = (event) => {
    handleNombre(event.target.value);
  }
  //Eliminar colegio
  const handleDelete = (id) => {
    verifyToken()
    if (!cancan("Master")) {
      nav("/")
    } else {
      let config = {
        method: 'delete',
        url: `${APILINK}/api/Colegios/${idColegio}`,
        headers: {
          'Authorization': `Bearer ${getToken()}`
        }
      };
      axios(config)
        .then(function (response) {
          if (response.status >= 400) {
            toast.error("Hubo un error")
          }
          else if (response.status >= 200) {
            triggerState(response.data)
            onSubmit(response.data)
            toast.success("Colegio Eliminado");
          }
        })
        .catch(function (error) {
          if (typeof (error.response.data) === "string" ? true : false) {
            toast.error(error.response.data)
          } else {
          }
        });
      handleCloseDelete();
      handleClose();
    }


  }
  const [showDelete, setShowDelete] = useState(false);

  const handleCloseDelete = () => setShowDelete(false);
  const handleShowDelete = () => setShowDelete(true);


  return (

    <>
      <div
        className="modal show"
        style={{ display: 'block', position: 'initial' }}
      >
        <Modal show={showDelete} onHide={handleCloseDelete}>
          <Modal.Header closeButton id={styles.modalContenido}>
            <Modal.Title>Eliminar</Modal.Title>
          </Modal.Header>
          <Modal.Body>Desea eliminar el colegio {nombreColegio}?</Modal.Body>
          <Modal.Footer id={styles.modalContenido}>
            <Button variant="secondary" onClick={handleCloseDelete}>
              No
            </Button>
            <Button variant="primary" onClick={handleDelete}>
              Si
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
      <div>

        <Modal show={show} onHide={handleClose}>
          <Modal.Header id={styles.modalContenido} closeButton onClick={handleClose}>
            <h5 className={styles.tituloForm} >Informacion Colegio</h5>
          </Modal.Header>
          <Modal.Body id={styles.modalContenido}>
            <div>
              <form>
                <label htmlFor="colegio-nombre" className={styles.labelForm}>Nombre Colegio</label><br />
                {disabled
                  ? <div> <input type="text" id={styles.inputColegio} defaultValue={nombreColegio || ''} disabled></input><br />
                    <br /></div>

                  : <div> <input type="text" id={styles.inputColegio} defaultValue={nombreColegio || ''} name="colegio-nombre" onChange={event => handleInputColegio(event)}></input><br />
                    <br /></div>
                }


                <label htmlFor="administrador"><strong> Administrador</strong></label><br />
                {disabled
                  ? <div> < input required type="text" id={styles.inputColegio} name="colegio-admin" defaultValue={colegio.nombreAdministrador + " " + colegio.apellido || ''} disabled>
                  </input>
                    <br /></div>

                  : <div>  <select required name="admin" defaultValue={idAdmin || ''} onChange={event => handleIDAdmin(event)} className={styles.selectAdmin}>
                    <option disabled value={0 || ''}>Seleccione Administrador</option>
                    {administradores.map((administrador) =>
                      <option key={administrador.id} value={administrador.id || ''}>{administrador.nombre} {administrador.apellido}</option>
                    )}
                  </select>
                  </div>
                }
              </form>
            </div>
          </Modal.Body>
          <Modal.Footer >
            {disabled
              ? <div>
                <Button className={styles.btnCancelar} onClick={handleShowDelete} >Eliminar</Button>
                <Button className={styles.btnGuardar} onClick={handleEdit}>Editar</Button>
              </div>
              : <div>
                <Button className={styles.btnCancelar} onClick={handleClose} >Cancelar</Button>
                <Button className={styles.btnGuardar} onClick={handleSubmit}>Guardar</Button>
              </div>
            }


          </Modal.Footer>
        </Modal>
      </div>

    </>)
}
export default ModalVerColegios