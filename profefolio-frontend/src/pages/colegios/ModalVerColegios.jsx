import React, { useState, useEffect } from 'react'
import { Modal, Button, Form } from 'react-bootstrap'
import styles from "./ModalVerColegios.module.css"
import axios from "axios";
import { useGeneralContext } from "../../context/GeneralContext";

import APILINK from '../../components/link';
import { useNavigate } from "react-router-dom";
import { toast } from 'react-hot-toast';

function ModalVerColegios(props) {

  const { datoIdColegio, setShow, show, disabled, setDisabled, triggerState, page } = props;

  const handleClose = () => {
    setNombre("");
    setNombreAdministrador("");
    setApellidoAdministrador("");
    setShow(false);

  }
  const [colegio, setColegio] = useState([""]);
  const { getToken, verifyToken, cancan } = useGeneralContext();
  const nav = useNavigate();
  const [administradores, setAdministradores] = useState([]);
  // eslint-disable-next-line no-unused-vars
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
          url: `${APILINK}/api/ColegiosFull/${datoIdColegio}`,
          headers: {
            'Authorization': `Bearer ${getToken()}`
          }
        };
        axios(config)
          .then(function (response) {
            setColegio(response.data); //Guarda los datos
            console.log(colegio);
            // eslint-disable-next-line no-unused-vars
            const { apellido, direccion, documento, documentoTipo, genero, id, nacimiento, nombre, nombreAdministrador, telefono } = response.data;
            setNombre(nombre);
            setApellidoAdministrador(apellido);
            setNombreAdministrador(nombreAdministrador);
            setDisabled(true);



          })
          .catch(function (error) {
            console.log(error);
          });
      }
      handleEdit();
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cancan, verifyToken, nav, getToken, datoIdColegio])
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

    console.log(typeof (idAdmin));

    if (nombre === "" || idAdmin === "")
      toast.error("revisa los datos, los campos deben ser completados")
    else {
      axios.put(`${APILINK}/api/Colegios/${datoIdColegio}`, {
        nombre,
        idAdmin

      }, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          //triggerState(response.data)



          setColegio("");
          toast.success("Editado exitoso");

          handleClose(false);
          setNombre("");
          setIdAdmin("");

        })
        .catch(error => {
          if (typeof (error.response.data) === "string" ? true : false) {
            toast.error(error.response.data)
          } else {
            toast.error(error.response.data?.errors.Email[0])
          }
        });

    }

  }


  //Guardar el id del admin
  // eslint-disable-next-line no-unused-vars

  const handleIDAdmin = (event) => {
    setIdAdmin(event.target.value)
  }
  const handleUpdate = () => {
    axios.get(`${APILINK}/api/ColegiosFull/page/${page}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    })

      .then(response => {
        triggerState(response.data.dataList);

    


      })
      .catch(error => {
        console.error(error);
      });
  }

  //Eliminar colegio
  const handleDelete = (id) => {
   
    axios.delete(`${APILINK}/api/Colegios/${datoIdColegio}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    })

      .then(response => {
        handleUpdate();
        setColegio(response.data)
        toast.success("Eliminado exitoso");
        handleClose(false);
        handleCloseModal2(false);



      })
      .catch(error => {
        console.error(error);
      });

  }
  const [nombre, setNombre] = useState(colegio.nombre || "");
  const [nombreAdministrador, setNombreAdministrador] = useState(colegio.nombreAdministrador || "");
  const [apellido, setApellidoAdministrador] = useState(colegio.apellido || "");
  const [showModal2, setShowModal2] = useState(false);

  const handleCloseModal2 = () => setShowModal2(false);
  const handleShowModal2 = () => setShowModal2(true);
  return (

    <>
      <div
        className="modal show"
        style={{ display: 'block', position: 'initial' }}
      >
        <Modal show={showModal2} onHide={handleCloseModal2}>
          <Modal.Header id={styles.modalContenido} closeButton>
            <Modal.Title>Eliminar Colegio</Modal.Title>
          </Modal.Header>

          <Modal.Body id={styles.modalContenido}>
            <p>Desea eliminar el Colegio {nombre}</p>
          </Modal.Body>

          <Modal.Footer>
            <Button className={styles.btnCancelar} onClick={handleDelete}>Si</Button>
           
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
                  ? <Form.Control
                    type="text"
                    defaultValue={nombre}
                    disabled
                  />
                  : <Form.Control

                    type="text"
                    defaultValue={nombre}
                    onChange={event => setNombre(event.target.value)}
                  //placeholder={profesor.nombre} 
                  />
                }


                <label htmlFor="administrador"><strong> Administrador</strong></label><br />
                {disabled
                  ? <Form.Control
                    type="text"
                    defaultValue={colegio.nombreAdministrador + " " + colegio.apellido || ''}
                    disabled
                  />

                  : <div>
                    <Form.Select aria-label="Default select example" defaultValue={idAdmin} onChange={event => handleIDAdmin(event)}>
                      <option disabled >{nombreAdministrador} {apellido}</option>
                      {administradores.map((administrador) =>
                        <option key={administrador.id} value={administrador.id}>{administrador.nombre} {administrador.apellido}</option>
                      )}
                    </Form.Select>


                  </div>
                }
              </form>
            </div>
          </Modal.Body>
          <Modal.Footer >
            {disabled
              ? <div>
                <Button className={styles.btnCancelar} onClick={handleShowModal2} >Eliminar</Button>
                <Button className={styles.btnGuardar} onClick={handleEdit}>Editar</Button>
              </div>
              : <div>
               
                <Button className={styles.btnGuardar} onClick={handleSubmit}>Guardar</Button>
              </div>
            }

          </Modal.Footer>
        </Modal>
      </div>

    </>)
}
export default ModalVerColegios