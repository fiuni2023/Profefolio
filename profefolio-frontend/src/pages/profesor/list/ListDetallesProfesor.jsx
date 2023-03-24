import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Modal, Form,Col,Row } from 'react-bootstrap';
import { useGeneralContext } from '../../../context/GeneralContext';
import styles from  '../components/create/Modal.module.css';
import APILINK from '../../../components/link';


function ListDetallesProfesor({id, triggerState = () => {}}) {

    const [profesor, setProfesores] = useState([]);

    const { getToken } = useGeneralContext();

    const [readOnly, setReadOnly] = useState(true);

    

    useEffect(() => {
      
        axios.get(`${APILINK}/api/profesor/${id}`, {
          headers: {
            Authorization: `Bearer ${getToken()}`,
          }
        })
    
          .then(response => {
            setProfesores(response.data);
           // triggerState(response.data)
        
    
  
          })
          .catch(error => {
            console.error(error);
          });
    
    }, [ id, getToken]);


    // Define state variables for the input field values
const [nombre, setNombre] = useState(profesor.nombre);
const [apellido, setApellido] = useState(profesor.apellido);
const [telefono, setTelefono] = useState(profesor.telefono);
const [direccion, setDireccion] = useState(profesor.direccion);
const [nacimiento, setNacimiento] = useState(profesor.nacimiento);
const [email, setEmail] = useState(profesor.email);
const [genero, setGenero] = useState(profesor.genero);
const [documento, setDocumento] = useState(profesor.documento);
const [documentoTipo, setDocumentoTipo] = useState(profesor.documentoTipo);



  const [showModal, setShowModal] = useState(true);

  const [eliminarVisible, setEliminarVisible] = useState(true);

  function closeModal() {
    setShowModal(false);
  }


  const handleModificar = () => {
    setReadOnly(false);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setReadOnly(true);
  };

  const handleGuardar = () => {
    setReadOnly(true);
    setEliminarVisible(true);
    // Aquí puedes agregar la lógica para guardar los cambios
  };

  return (

    <>
    

   
      <Modal show={showModal} onHide={handleCloseModal}  >




        <Modal.Header className={styles.contentModal} closeButton>
          <Modal.Title className="">Detalles Profesor</Modal.Title>

        </Modal.Header>



      <Modal.Body className={styles.contentModal} >

          <form>
            
            
            <Row>
              <Col>
              <Form.Label >Nombre: </Form.Label>
              <div >
                <Form.Control
               
                  type="text"
                  value= {profesor.nombre}
                  placeholder="Ingrese su nombre"
                />
              </div>
             
            
              </Col>

              <Col>
              <Form.Label className="">Apellido:  </Form.Label>
                <Form.Control
                 className={styles.option}
                  type="text"
                  value={profesor.apellido}
                  placeholder="Ingrese su apellido"
                />
              </Col>
            </Row>
            <br/>

            <Row>
            <Col>
            <Form.Label >Telefono: </Form.Label>
            <Form.Control
                 className={styles.option}
                  type="number"
                  name="telefono"
                  value= {profesor.telefono}
                  placeholder="09xxxxxxxxx"
                  readOnly={readOnly}
                />
            
              </Col>

              <Col>
             
              <Form.Label className="">Direccion:  </Form.Label>
              <Form.Control
                 className={styles.option}
                  type="text"
                  name="direccion"
                  value={profesor.direccion}
                  placeholder="Ingrese su direccion"
                  readOnly={readOnly}
                />
              </Col>

               

            </Row>
            <br/>
            <Row>
            <Col>
              <Form.Label className="">Fecha de nacimiento:  </Form.Label>

              <Form.Control
                className={styles.option}
                  type="date"
                  name="nacimiento"
                  value={profesor.nacimiento}
                  placeholder="aaaa/mm/ddd"
                  readOnly={readOnly}

                />
                </Col>

                <Col>
              <Form.Label className="">Correo Electronico:  </Form.Label>
              <Form.Control
                 className={styles.option}
                 type="email"
                  name="email"
                  value={profesor.email}
                  placeholder="Ingrese su correo electronico"
                  readOnly={readOnly}
                />
                </Col>

            </Row>
            <br/>
            <Row>
              <Col>
              <Form.Group className="">
              <Form.Label className="">Genero:  </Form.Label>
              <Form.Control
                 className={styles.option}
                  as="select"
                  name="genero"
                  value={profesor.genero}
                  readOnly={readOnly}
                >
                  <option value="" className={styles.option} >Seleccione </option>
                  <option value="F" className={styles.option}>Femenino</option>
                  <option value="M"className={styles.option}>Masculino</option>
                  </Form.Control>
            </Form.Group>

              </Col>

              <Col>
              <Form.Label className="">Documento: </Form.Label>
              <Form.Control
                 className={styles.option}
                  type="text"
                  name="documento"
                  value={profesor.documento}
                  placeholder="Ingrese su documento"
                  readOnly={readOnly}
                />
              
            
              </Col>
            </Row>
            <br/>

            <Row>
              <Col>

              <Form.Label className="">Tipo de documento:</Form.Label>
              <div className="">
                <Form.Control
                 className={styles.option}
                  as="select"
                  name="documentoTipo"
                  value={profesor.documentoTipo}
                  readOnly={readOnly}
                >
                  <option value="" className={styles.option}>Seleccione un tipo</option>
                  <option value="cedula" className={styles.option}> Cédula</option>
                  <option value="dni" className={styles.option}>DNI</option>
                  <option value="pasaporte" className={styles.option}>Pasaporte</option>
                </Form.Control>
              </div>
              </Col>

              <Col>
</Col>
            </Row>
           
           
            <br/>
            
        
            {readOnly ? (
          <>
            <button variant="primary" onClick={handleModificar}>
              Modificar
            </button>
            {eliminarVisible && (
              <button variant="danger">Eliminar</button>
            )}
          </>
        ) : (
          <button variant="primary" onClick={handleGuardar}>
            Guardar
          </button>
        )}
            <div className="modal-footer">
              <button className={styles.buttonClose} onClick={closeModal}> Cerrar</button>

            </div>


          </form>
        </Modal.Body>  

      </Modal>




      <style jsx='true'>{`
          
            
            `}</style>
    </>

  )
}


export default ListDetallesProfesor;
