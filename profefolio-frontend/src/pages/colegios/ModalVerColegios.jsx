import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import styles from "./ModalVerColegios.module.css"
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";

import APILINK from '../../components/link';
import { useNavigate } from "react-router-dom";
import { toast } from 'react-hot-toast';

function ModalVerColegios({ idColegio, setShow, show, disabled, setDisabled }) {
  const handleClose = () => setShow(false);
  const [colegio, setColegio] = useState([""]);
  const { getToken, verifyToken, cancan } = useGeneralContext();
  const nav = useNavigate();
  const [administradores, setAdministradores] = useState([]);
  // eslint-disable-next-line no-unused-vars
  const [nombreColegio, setNombreColegio] = useState("");
  const [nombreNuevoCol, setNombreNuevoCol] = useState("")
  const [idAdmin, setIdAdmin] = useState(0);

  //Lamada de los datos del colegio


  useEffect(() => {
    if (show) {
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
            setDisabled(true);
            

          })
          .catch(function (error) {
            console.log(error);
          });
      }
      handleEdit();
    }
    
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cancan, verifyToken, nav, getToken, idColegio])
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
          console.log(error);
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
    // eslint-disable-next-line no-mixed-operators, eqeqeq
    if (nombreNuevoCol == nombreColegio && idAdmin != 0 || nombreNuevoCol != "" && idAdmin != 0) {
      let data = JSON.stringify({
        "nombre": nombreNuevoCol,
        "personaId": idAdmin
      });
      console.log(data);
      verifyToken()
      let config = {
          method: 'put',
          url: `${APILINK}/api/Colegios/${idColegio}`,
          headers: {
            'Authorization': `Bearer ${getToken()}`
          },
          data: data
        };
        axios.request(config)
          .then((response) => {
            console.log(JSON.stringify(response.data));
            toast.success("Cambios Guardados");
          })
          .catch((error) => {
            console.log(error);
            toast.error("Hubo un error al guardar los cambios", error);
          });

      
    }
    else {
      toast.error("Rellene todos los campos");

    }
  }


  //Guardar el id del admin
  // eslint-disable-next-line no-unused-vars

  const handleIDAdmin = (event) => {
    setIdAdmin(event.target.value)
  }

  const handleNombre = (nombre) => {
    setNombreNuevoCol(nombre);
  }
  const handleInputColegio = (event) => {
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
                {disabled
                  ? <div> <input type="text" id={styles.inputColegio} defaultValue={colegio.nombre || ''} disabled></input><br />
                    <br /></div>

                  : <div> <input type="text" id={styles.inputColegio} defaultValue={colegio.nombre || colegio.nombre} name="colegio-nombre" onChange={event => handleInputColegio(event)}></input><br />
                    <br /></div>
                }


                <label htmlFor="administrador"><strong> Administrador</strong></label><br />
                {disabled
                  ? <div> < input required type="text" id={styles.inputColegio} name="colegio-admin" defaultValue={colegio.nombreAdministrador + " " + colegio.apellido || ''} disabled>
                  </input>
                    <br /></div>

                  : <div>  <select required name="admin" defaultValue={idAdmin || idAdmin} onChange={event => handleIDAdmin(event)} className={styles.selectAdmin}>
                    <option disabled value={0 || ''}>Seleccione Administrador</option>
                    {administradores.map((administrador) =>
                      <option key={administrador.id} value={administrador.id || administrador.id}>{administrador.nombre} {administrador.apellido}</option>
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
                <Button className={styles.btnCancelar} onClick={handleDelete} >Eliminar</Button>
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