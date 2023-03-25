import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Modal, Form,Col,Row,Button } from 'react-bootstrap';
import { useGeneralContext } from '../../../context/GeneralContext';
import styles from  '../components/create/Modal.module.css';
import APILINK from '../../../components/link';
import { BsTrash, BsPencilFill, BsInfoCircle } from 'react-icons/bs';

function ListDetallesProfesor(props) {
  const { showModal, setShowModal ,id} = props;

  const [profesor, setProfesores] = useState([]);

  const { getToken } = useGeneralContext();

  const [readOnly, setReadOnly] = useState(true);

  const [eliminarVisible, setEliminarVisible] = useState(true);

  const [mostrarInput, setMostrarInput] = useState(false);


  const handleCloseModal = () => {
    setShowModal(false);
    setReadOnly(true);
  };

  function closeModal() {
    setShowModal(false);
  }
  const handleModificar = () => {
    setReadOnly(false);

    console.log('setReadOnly',readOnly);
    setReadOnly(!readOnly);
  };


  const handleGuardar = () => {
    setReadOnly(true);
    setEliminarVisible(true);
    // Aquí puedes agregar la lógica para guardar los cambios
  };

  useEffect(() => {
      
    axios.get(`${APILINK}/api/profesor/${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    })

      .then(response => {
        setProfesores(response.data);
        //triggerState(response.data)
    


      })
      .catch(error => {
        console.error(error);
      });

}, [ id, getToken]);

const [nombre, setNombre] = useState(profesor.nombre);
const [apellido, setApellido] = useState(profesor.apellido);
const [telefono, setTelefono] = useState(profesor.telefono);
const [direccion, setDireccion] = useState(profesor.direccion);
const [nacimiento, setNacimiento] = useState(profesor.nacimiento);
const [email, setEmail] = useState(profesor.email);
const [genero, setGenero] = useState(profesor.genero);
const [documento, setDocumento] = useState(profesor.documento);
const [documentoTipo, setDocumentoTipo] = useState(profesor.documentoTipo);

  return (
    <Modal show={showModal} onHide={handleCloseModal}>
      <Modal.Header className={styles.contentModal} closeButton>
      <Modal.Title className="">Detalles Profesor</Modal.Title>
      </Modal.Header>
      
      <Modal.Body className={styles.contentModal} >

          <form>
            
            <Row>
              <Col>
              <Form.Label >Nombre: </Form.Label>
             

                 {readOnly ? (
          <>
           
            {eliminarVisible && (
              <Form.Control
              type="text"
              value= {profesor.nombre}
              placeholder="Ingrese su nombre"
               readOnly={readOnly}
            /> 
            )}
          </>
        ) : (

          <Form.Control
          className={styles.option}
          type="text"
          value={nombre}
          onChange={event => setNombre(event.target.value)}
          placeholder="Ingrese su nombre"
        />
          
        )}
    
              </Col>

              <Col>

              <Form.Label className="">Apellido:  </Form.Label>
              {readOnly ? (
          <>
           
            {eliminarVisible && (
             
      
             
                <Form.Control
                 className={styles.option}
                  type="text"
                  value={profesor.apellido}
                  placeholder="Ingrese su apellido"
                  readOnly={readOnly}
                />

                )}
                </>
              ) : (
              <Form.Control
                 className={styles.option}
                  type="text"
                  value={apellido}
                  onChange={event => setApellido(event.target.value)}
                  placeholder="Ingrese su apellido"
                />
                
                )}
              </Col>
            </Row>
            <br/>

            <Row>
            <Col>
            <Form.Label >Telefono: </Form.Label>
            {readOnly ? (
          <>
           
            {eliminarVisible && (         
            <Form.Control
                 className={styles.option}
                  type="number"
                  name="telefono"
                  value= {profesor.telefono}
                  placeholder="09xxxxxxxxx"
                  readOnly={readOnly}
                />
                )}
                </>
              ) : (
            <Form.Control
                 className={styles.option}
                  type="number"
                  name="telefono"
                  value={telefono}
                  onChange={event => setTelefono(event.target.value)}
                  placeholder="09xxxxxxxxx"
                />
                )}

              </Col>

              <Col>
             
              <Form.Label className="">Direccion:  </Form.Label>
              {readOnly ? (
          <>
           
            {eliminarVisible && (         

              <Form.Control
                 className={styles.option}
                  type="text"
                  name="direccion"
                  value={profesor.direccion}
                  placeholder="Ingrese su direccion"
                  readOnly={readOnly}
                />
                )}
                </>
              ) : (

            <Form.Control
                 className={styles.option}
                  type="text"
                  name="direccion"
                  value={direccion}
                  onChange={event => setDireccion(event.target.value)}
                  placeholder="Ingrese su direccion"
                />
                )}
              </Col>

               

            </Row>
            <br/>
            <Row>
            <Col>
            <Form.Label className="">Fecha de nacimiento:  </Form.Label>

            {readOnly ? (
          <>
           
            {eliminarVisible && (         

// {new Date(fecha).toLocaleDateString()} profesor.nacimiento
            
              <Form.Control
                className={styles.option}
                  type="text"
                  name="nacimiento"
                  value={new Date(profesor.nacimiento).toLocaleDateString()}
                  placeholder="aaaa/mm/ddd"
                  readOnly={readOnly}

                />
                 )}
                </>
              ) : (

                <Form.Control
                className={styles.option}
                  type="date"
                  name="nacimiento"
                  value={nacimiento}
                  onChange={event => setNacimiento(event.target.value)}
                  placeholder="aaaa/mm/ddd"

                />
 )}
                </Col>

                <Col>
              <Form.Label className="">Correo Electronico:  </Form.Label>

              {readOnly ? (
          <>
           
            {eliminarVisible && (         

              <Form.Control
                 className={styles.option}
                 type="email"
                  name="email"
                  value={profesor.email}
                  placeholder="Ingrese su correo electronico"
                  readOnly={readOnly}
                />
   )}
                </>
              ) : (
              
              <Form.Control
                 className={styles.option}
                 type="email"
                  name="email"
                  value={email}
                  onChange={event => setEmail(event.target.value)}
                  placeholder="Ingrese su correo electronico"
                />
)}

                </Col>

            </Row>
            <br/>
            <Row>
              <Col>
            
              <Form.Label className="">Genero:  </Form.Label>
              
              {readOnly ? (
          <>
           
            {eliminarVisible && (         

              <Form.Control
                 className={styles.option}
                 type="text"
                  name="genero"
                  value={profesor.genero}
                  readOnly={readOnly}
                />
                 )}
                </>
              ) : (

          <Form.Control
                 className={styles.option}
                  as="select"
                  name="genero"
                  value={genero}
                  onChange={event => setGenero(event.target.value)}
                >
                  <option value="" className={styles.option}>Seleccione </option>
                  <option value="F" className={styles.option}>Femenino</option>
                  <option value="M"className={styles.option}>Masculino</option>
                </Form.Control>
                 
                )}
          

              </Col>

              <Col>
              <Form.Label className="">Documento: </Form.Label>
   
              {readOnly ? (
          <>
           
            {eliminarVisible && (         
              <Form.Control
                 className={styles.option}
                  type="text"
                  name="documento"
                  value={profesor.documento}
                  placeholder="Ingrese su documento"
                  readOnly={readOnly}
                />
                )}
                </>
              ) : (

              <Form.Control
                 className={styles.option}
                  type="text"
                  name="documento"
                  value={documento}
                  onChange={event => setDocumento(event.target.value)}
                  placeholder="Ingrese su documento"
                />
             )}  
            
              </Col>
            </Row>
            <br/>

            <Row>
              <Col>

              <Form.Label className="">Tipo de documento:</Form.Label>

              
              {readOnly ? (
          <>
           
            {eliminarVisible && (         
            
                <Form.Control
                 className={styles.option}
                 type="text"
                  name="documentoTipo"
                  value={profesor.documentoTipo}
                  readOnly={readOnly}
               />
               

                )}
                </>
              ) : (
                <Form.Control
                 className={styles.option}
                  as="select"
                  name="documentoTipo"
                  value={documentoTipo}
                  onChange={event => setDocumentoTipo(event.target.value)}
                >
                  <option value="" className={styles.option}>Seleccione un tipo</option>
                  <option value="cedula" className={styles.option}> Cédula</option>
                  <option value="dni" className={styles.option}>DNI</option>
                  <option value="pasaporte" className={styles.option}>Pasaporte</option>
                </Form.Control>
                )}  
             
              </Col>

              <Col>
</Col>
            </Row>
           
           
            <br/>
       
            

          </form>
        </Modal.Body> 

     
        <Modal.Footer className={styles.footerModal}>
        {readOnly ? (
          <>
            <button variant="primary" className={styles.buttonModify} onClick={handleModificar}>
            <BsPencilFill />
            </button>
            {eliminarVisible && (
              <button variant="danger" className={styles.buttonDelete}>   <BsTrash />  </button>
            )}
          </>
        ) : (
          <button variant="primary" className={styles.button} onClick={handleGuardar}>
            Guardar
          </button>
        )}
       
        <button variant="primary" onClick={closeModal} className={styles.buttonClose}>Cerrar</button>

        </Modal.Footer>
      
    
     

    </Modal>
  );
}

export default ListDetallesProfesor;
