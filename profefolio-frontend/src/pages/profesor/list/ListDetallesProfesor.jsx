import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Modal, Form,Col,Row,Button } from 'react-bootstrap';
import { useGeneralContext } from '../../../context/GeneralContext';
import styles from  '../components/create/Modal.module.css';
import APILINK from '../../../components/link';
import { toast } from 'react-hot-toast';
import { BsTrash, BsPencilFill, BsInfoCircle } from 'react-icons/bs';

function ListDetallesProfesor(props) {
  const { showModal, setShowModal ,id,triggerState} = props;

  const [profesor, setProfesores] = useState([]);

  const { getToken } = useGeneralContext();

  const [readOnly, setReadOnly] = useState(true);

  const [eliminarVisible, setEliminarVisible] = useState(true);

  const [mostrarInput, setMostrarInput] = useState(false);



  const handleSubmit = (event) => {

    console.log('nombre',nombre);
    console.log('apellido',apellido);
    console.log('documento',documento);
    console.log('email',email);
    console.log('nacimiento',nacimiento);

    console.log('genero',genero);

    event.preventDefault();

    if (nombre === "" || apellido === "" || documento === "" || documentoTipo === "" || email === "" || nacimiento === "" || genero === "" || direccion === "" || telefono === "") toast.error("revisa los datos, los campos deben ser completados")
    else if( new Date()< new Date(nacimiento)) toast.error("Ingrese una fecha valida")
    
    

    else {
      axios.put(`${APILINK}/api/profesor/${id}`, {
        nombre,
        apellido,
        documento,
        documentoTipo,
        email,
        nacimiento,
        genero,
        direccion,
        telefono,

      }, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          triggerState(response.data)
          setProfesores(response.data)
          toast.success("Guardado exitoso");

          setShowModal(false);
          setNombre("")
          setApellido("")
          setDireccion("")
          setDocumentoTipo("")
          setDocumento("")
          setEmail("")
          setNacimiento("")
          setGenero("")
          setTelefono("")

        })
        .catch(error => {
          if(typeof(error.response.data) === "string"? true:false){
            toast.error(error.response.data)
          }else{
            toast.error(error.response.data?.errors.Email[0])
          }
        });

    }
    

  };

  const handleCloseModal = () => {
    setShowModal(false);
    setReadOnly(true);
  };

  function closeModal() {
    setShowModal(false);
  }
  

  const handleModificar = (event) => {
    setReadOnly(!readOnly);
    setNombre(profesor.nombre, () => {
      console.log('Nombre actualizado:', nombre);
    });

    setApellido(profesor.apellido, () => {
      console.log('Nombre actualizado:', nombre);
    });

    setTelefono(profesor.telefono, () => {
      console.log('Nombre actualizado:', nombre);
    });

    setDireccion(profesor.direccion, () => {
      console.log('Nombre actualizado:', nombre);
    });
  
    setNacimiento(profesor.nacimiento, () => {
      console.log('Nacimiento actualizado:', nacimiento.toLocaleDateString());
    });

    setEmail(profesor.email, () => {
      console.log('Nombre actualizado:', nombre);
    });

    setGenero(profesor.genero === "Femenino" ? "F" : "M", () => {
      console.log('Género actualizado:', genero);
    });

    setDocumento(profesor.documento, () => {
      console.log('Nombre actualizado:', nombre);
    });

    setDocumentoTipo(profesor.documentoTipo, () => {
      console.log('Nombre actualizado:', nombre);
    });



  
  
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


}, [ id, getToken ]);

const [nombre, setNombre] = useState(profesor.nombre);
const [apellido, setApellido] = useState(profesor.nombre);
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

      <form  profesor={handleSubmit}>
      
      <Modal.Body className={styles.contentModal} >

         
            
            <Row>
              <Col>
              <Form.Label >Nombre: </Form.Label>
             

                 {readOnly ? (
          <>
           
            {eliminarVisible && (
              <Form.Control
              type="text"
              defaultValue={profesor.nombre}
               readOnly={readOnly}
            /> 
            )}
          </>
        ) : (

          <Form.Control
          className={styles.option}
          type="text"
          defaultValue={profesor.nombre|| ""} 
          value={nombre}
          onChange={event => setNombre(event.target.value)}
          //placeholder={profesor.nombre} 
        />
          
        )}
    
              </Col>

              <Col>

              <Form.Label className="">Apellido:  </Form.Label>
              {readOnly ? (
          <>
           
            {eliminarVisible && (
                <Form.Control
                  type="text"
                  value={profesor.apellido}
                  readOnly={readOnly}
                />

                )}
                </>
              ) : (
              <Form.Control
                 className={styles.option}
                  type="text"
                  defaultValue={profesor.apellido|| ""} 
                  value={apellido}
                  onChange={event => setApellido(event.target.value)}
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
                  defaultValue={profesor.telefono|| ""} 
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
                  defaultValue={profesor.direccion|| ""} 
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
                  //value={profesor.nacimiento}
                  value={new Date(profesor.nacimiento).toLocaleDateString()}

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
                onChange={event => {
                  console.log(event.target.value);
                  setNacimiento(new Date(event.target.value));
                }}
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
                  defaultValue={profesor.email|| ""} 
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
                  <option value="" className={styles.option}> Seleccionar</option>
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
                  defaultValue={profesor.documento|| ""} 
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
                  <option value=""  className={styles.option}>Seleccionar</option>
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
          <button type="submit" className={styles.button} onClick={handleSubmit}>
            Guardar
          </button>
        )}
       
        <button variant="primary" onClick={closeModal} className={styles.buttonClose}>Cerrar</button>

        </Modal.Footer>
        </form>
    
     

    </Modal>
  );
}

export default ListDetallesProfesor;
