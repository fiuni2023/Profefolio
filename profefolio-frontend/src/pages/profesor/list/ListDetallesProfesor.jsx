import React, { useState } from 'react';
import axios from 'axios';
import { Modal, Form,Col,Row } from 'react-bootstrap';

import { useGeneralContext } from '../../../context/GeneralContext';


import styles from  '../components/create/Modal.module.css';
import APILINK from '../../../components/link';


function ListDetallesProfesor({onSubmit = ()=>{}}) {

    const [profesores, setProfesores] = useState([]);

    const { getToken } = useGeneralContext();


  const handleRowClick = (id) => {
    axios.get(`${APILINK}/api/profesor/${id}`,{
    headers: {
      Authorization: `Bearer ${getToken()}`,
    }})
      .then(response => {
        setProfesores(response.data.dataList);
        alert(`Detalles de   ${id}: `);
      })
      .catch(error => {
        console.error(error);
      });
  };


  const handleSubmit = () => {

    
   

  };

  const [showModal, setShowModal] = useState(false);

  function closeModal() {
    setShowModal(false);
     
    setShowModal(false);
  }

  const handleCloseModal = () => setShowModal(false);
  const handleShowModal = () => setShowModal(true);



  return (

    <>
    

   
      <Modal show={showModal} onHide={handleCloseModal} handleRowClick={handleRowClick} setProfesores={setProfesores} >




        <Modal.Header closeButton className={styles.contentModal}>
          <Modal.Title className="">Detalles Profesor</Modal.Title>
        </Modal.Header>



 {/*     <Modal.Body className={styles.contentModal}>
          <form onSubmit={handleSubmit}>
            
            
            <Row>
              <Col>
              <Form.Label >Nombre:  </Form.Label>
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
              <Form.Label className="">Apellido:  </Form.Label>
              <div >
                <Form.Control
                 className={styles.option}
                  type="text"
                  value={apellido}
                  onChange={event => setApellido(event.target.value)}
                  placeholder="Ingrese su apellido"
                />
              </div>
        
              </Col>

             
            </Row>
            <br/>

            <Row>
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
                  value={documentoTipo}
                  onChange={event => setDocumentoTipo(event.target.value)}
                >
                  <option value="" className={styles.option}>Seleccione un tipo</option>
                  <option value="cedula" className={styles.option}> Cédula</option>
                  <option value="dni" className={styles.option}>DNI</option>
                  <option value="pasaporte" className={styles.option}>Pasaporte</option>
                </Form.Control>
              </div>

            
              
         
              
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

            <Col>
            </Col>
           </Row>




            <div className="modal-footer">
              <button type="submit" className={styles.button}   >Guardar</button>
              <button className={styles.buttonClose} onClick={closeModal}> Cerrar</button>

            </div>


          </form>
        </Modal.Body>*/}   

      </Modal>




      <style jsx='true'>{`
          
            
            `}</style>
    </>

  )
}


export default ListDetallesProfesor;
