import React, { useState } from 'react';
import axios from 'axios';
import { Button, Modal, Form,Col,Row ,Container} from 'react-bootstrap';

import { TextInput } from '../../../../components/Inputs';

import { BsFillPlusCircleFill } from 'react-icons/bs';
import { useGeneralContext } from "../../../../context/GeneralContext";
import { toast } from 'react-hot-toast';

import styles from  './Modal.module.css';
import APILINK from '../../../../components/link';


function CreateModal({onSubmit = ()=>{}, triggerState = () => {}}) {

  

  const [nombre, setNombre] = useState('');
  const [apellido, setApellido] = useState('');
  const [documento, setDocumento] = useState('');
  const [documentoTipo, setDocumentoTipo] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [email, setEmail] = useState('');
  const [nacimiento, setNacimiento] = useState('');
  const [genero, setGenero] = useState('');
  const [direccion, setDireccion] = useState('');
  const [telefono, setTelefono] = useState('');

  const { getToken } = useGeneralContext();


  const handleSubmit = (event) => {

    event.preventDefault();

    if (nombre === "" || apellido === "" || documento === "" || documentoTipo === "" || password === "" || confirmPassword === "" || email === "" || nacimiento === "" || genero === "" || direccion === "" || telefono === "") toast.error("revisa los datos, los campos deben ser completados")
    else if (password.length < 8) toast.error("Contraseña no suficientemente larga")
    else if (password !== confirmPassword) toast.error("las contraseñas no son iguales")
    else if( new Date()< new Date(nacimiento)) toast.error("Ingrese una fecha valida")
    else {
      axios.post(`${APILINK}/api/profesor`, {
        nombre,
        apellido,
        documento,
        documentoTipo,
        password,
        confirmPassword,
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
          onSubmit(response.data)
          toast.success("Guardado exitoso");

          setShowModal(false);
          setNombre("")
          setApellido("")
          setDireccion("")
          setDocumentoTipo("")
          setDocumento("")
          setPassword("")
          setConfirmPassword("")
          setEmail("")
          setNacimiento("")
          setGenero("")
          setTelefono("")

        })
        .catch(error => {
          if(typeof(error.response.data) === "string"? true:false){
            toast.error(error.response.data)
          }else{
            toast.error(error.response.data?.errors.Password? error.response.data?.errors.Password[0] : error.response.data?.errors.Email[0])
          }
        });

    }
    

  };

  const [showModal, setShowModal] = useState(false);

  function closeModal() {
    setShowModal(false);
          setNombre("")
          setApellido("")
          setDireccion("")
          setDocumentoTipo("")
          setDocumento("")
          setPassword("")
          setConfirmPassword("")
          setEmail("")
          setNacimiento("")
          setGenero("")
          setTelefono("")
    setShowModal(false);
  }

  const handleCloseModal = () => setShowModal(false);
  const handleShowModal = () => setShowModal(true);



  return (

    <>
      <div className='NButtonForSideA'>
        <div className="buttonNavBarAa">
          <button className="buttonNavBarA" onClick={handleShowModal}>
            <BsFillPlusCircleFill />
          </button>
        </div>
      </div>



      <Modal show={showModal} onHide={handleCloseModal} >




        <Modal.Header closeButton className={styles.contentModal}>
          <Modal.Title className="">Agregar Profesor</Modal.Title>
        </Modal.Header>


        <Modal.Body className={styles.contentModal}>
          <form onSubmit={handleSubmit}>
            
            
            <Row>
              <Col>
              <Form.Label column sm="2">Nombre:  </Form.Label>
              <div >
                <Form.Control
                  className={styles.option}
                  type="text"
                  value={nombre}
                  onChange={event => setNombre(event.target.value)}
                  placeholder="Ingrese su nombre"
                />
              </div>
            
              </Col>

              <Col>
              
              <Form.Label >Telefono:</Form.Label>
              <div >
                <Form.Control
                 className={styles.option}
                  type="number"
                  name="telefono"
                  value={telefono}
                  onChange={event => setTelefono(event.target.value)}
                  placeholder="09xxxxxxxxx"
                />
                  
              </div>
           
              </Col>

             
            </Row>
            <br/>

            <Row>
            <Col>
              <Form.Label className="">Apellido:
              <div >
                <Form.Control
                 className={styles.option}
                  type="text"
                  value={apellido}
                  onChange={event => setApellido(event.target.value)}
                  placeholder="Ingrese su apellido"
                />
              </div>
              </Form.Label>
              
              </Col>

              <Col>
             
              <Form.Label className="">Direccion:</Form.Label>
              <div className="">
                <Form.Control
                 className={styles.option}
                  type="text"
                  name="direccion"
                  value={direccion}
                  onChange={event => setDireccion(event.target.value)}
                  placeholder="Ingrese su direccion"
                />
              </div>
          
              </Col>

               

            </Row>
            <br/>
            <Row>
            <Col>
              <Form.Label className="">Fecha de nacimiento:</Form.Label>
              <div className={styles.option}>
                <Form.Control
                className={styles.option}
                  type="date"
                  name="nacimiento"
                  value={nacimiento}
                  onChange={event => setNacimiento(event.target.value)}
                  placeholder="aaaa/mm/ddd"

                />
              </div>
               
                </Col>

                <Col>
                
              <Form.Label className="">Correo Electronico:</Form.Label>
              <div className="">
                <Form.Control
                 className={styles.option}
                 type="email"
                  name="email"
                  value={email}
                  onChange={event => setEmail(event.target.value)}
                  placeholder="Ingrese su correo electronico"
                />
              </div>
          
                </Col>

            </Row>
            <br/>
            <Row>
              <Col>
              <Form.Group className="">
              <Form.Label className="">Genero:</Form.Label>
              <div className="">
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
              </div>
            </Form.Group>
              </Col>

              <Col>
             
              <Form.Label className="">Contraseña:</Form.Label>
              <div className="">
                <Form.Control
                 className={styles.option}
                  type="password"
                  pattern="^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$"
                  name="password"
                  value={password}
                  //onChange={handleConfirmPasswordChange}
                  onChange={event => setPassword(event.target.value)}
                  placeholder="Utilizar minuscula, mayuscula y caracter especial"
                />
              </div>
           
              </Col>
            </Row>
            <br/>

            <Row>
              <Col>
             
              <Form.Label className="">Documento:</Form.Label>
              <div className="">
                <Form.Control
                 className={styles.option}
                  type="text"
                  name="documento"
                  value={documento}
                  onChange={event => setDocumento(event.target.value)}
                  placeholder="Ingrese su documento"
                />
              </div>
         
              
              </Col>
              <Col>
             
              <Form.Label className="">Confirmar contraseña:</Form.Label>
              <div className="">
                <Form.Control
                 className={styles.option}
                  type="password"
                  name="confirmPassword"
                  value={confirmPassword}
                  onChange={event => setConfirmPassword(event.target.value)}
                  placeholder="Confirme su contraseña"
                />

              </div>
             
              </Col>
            </Row>
           
            <br/>
            
            
           <Row>
            <Col>
            <Form.Group className="">
              <Form.Label className="">Tipo de documento:</Form.Label>
              <div className="">
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
              </div>
            </Form.Group>
            </Col>

            <Col>
            </Col>
           </Row>




            <div className="modal-footer">
              <button type="submit" className={styles.button}   >Guardar</button>
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


export default CreateModal;