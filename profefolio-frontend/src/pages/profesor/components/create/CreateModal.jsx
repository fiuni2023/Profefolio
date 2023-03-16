import React, { useState } from 'react';
import axios from 'axios';
import { Button, Modal, Form } from 'react-bootstrap';

import {BsFillPlusCircleFill} from 'react-icons/bs';
import { useGeneralContext } from "../../../../context/GeneralContext";


function CreateModal() {

  const [mostrarMensaje, setMostrarMensaje] = useState(false);
  const [nombre, setNombre] = useState('');
  const [apellido, setApellido] = useState('');
  const [documento, setDocumento] = useState('');
  const [documentoTipo, setDocumentoTipo] = useState('');
  const [password, setPasswordo] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [email, setEmail] = useState('');
  const [nacimiento, setNacimiento] = useState('');
  const [genero, setGenero] = useState('');
  const [direccion, setDireccion] = useState('');
  const [telefono, setTelefono] = useState('');

  const [success, setSuccess] = useState(false);
  const [showConfirmation, setShowConfirmation] = useState(false);
  const [error, setError] = useState('');

  const { getToken } = useGeneralContext();

  const mostrar = () => {
    setMostrarMensaje(true);
  }

  const handleSubmit = (event) => {

    event.preventDefault();

    axios.post('https://localhost:7063/api/profesor', {
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
        Authorization:  `Bearer ${getToken()}`,
      }
    })
    .then(response => {
      console.log(response.data);
      
      
      setShowConfirmation(true);
      alert("Guardado exitoso");
      

      setNombre(""); 

    })
    .catch(error => {
      console.error(error);
      alert("Error al guardar");
    });

    
   setShowModal(false);

  };
 
  const [showModal, setShowModal] = useState(false);

  function closeModal() {
    setShowModal(false);
  }

  const handleCloseModal = () => setShowModal(false);
  const handleShowModal = () => setShowModal(true);
  
 

  return (

    <>  
    <div className='NButtonForSideA'>
    <div className="buttonNavBarAa">
      <Button className="buttonNavBarA" onClick={handleShowModal}>
      <BsFillPlusCircleFill/>
      </Button>
      </div>
      </div>

    
      {showModal && (

      <Modal show={showModal} onHide={handleCloseModal} >

      


        <Modal.Header closeButton className="contentModal text-center">
          <Modal.Title className="">Agregar Profesor</Modal.Title>
        </Modal.Header>


        <Modal.Body className="contentModal">
        {success && <p>Producto guardado exitosamente.</p>}
          <Form onSubmit={handleSubmit}>
            <Form.Group className="row">
              <Form.Label className="col-sm-3">Nombre:
                     
              </Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  value={nombre}
                  onChange={event => setNombre(event.target.value)}
                  placeholder="Ingrese su nombre"
                />
              </div>
            </Form.Group>
            

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Apellido:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  value={apellido}
                  onChange={event => setApellido(event.target.value)}
                  placeholder="Ingrese su apellido"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Fecha de nacimiento:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="datetime"
                  name="nacimiento"
                  value={nacimiento}
                  onChange={event => setNacimiento(event.target.value)}
                  placeholder="aaaa/mm/ddd"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Genero:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  as="select"
                  name="genero"
                  value={genero}
                  onChange={event => setGenero(event.target.value)}
                >
                  <option value="">Seleccione </option>
                  <option value="F">Femenino</option>
                  <option value="M">Masculino</option>
                  
                
                </Form.Control>
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Documento:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="documento"
                  value={documento}
                  onChange={event => setDocumento(event.target.value)}
                  placeholder="Ingrese su documento"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Tipo de documento:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  as="select"
                  name="documentoTipo"
                  value={documentoTipo}
                  onChange={event => setDocumentoTipo(event.target.value)}
                >
                  <option value="">Seleccione un tipo</option>
                  <option value="cedula">Cédula</option>
                  <option value="dni">DNI</option>
                  <option value="pasaporte">Pasaporte</option>
                </Form.Control>
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Telefono:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="number"
                  name="telefono"
                  value={telefono}
                  onChange={event => setTelefono(event.target.value)}
                  placeholder="09xxxxxxxxx"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Direccion:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="direccion"
                  value={direccion}
                  onChange={event => setDireccion(event.target.value)}
                  placeholder="Ingrese su direccion"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Correo Electronico:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="email"
                  value={email}
                  onChange={event => setEmail(event.target.value)}
                  placeholder="Ingrese su correo electronico"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Contraseña:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="password"
                  pattern= "^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$"
                  name="password"
                  value={password}
                  //onChange={handleConfirmPasswordChange}
                  onChange={event => setPasswordo(event.target.value)}
                  placeholder="Utilizar minuscula, mayuscula y caracter especial"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Confirmar contraseña:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="password"
                  name="confirmPassword"
                  value={confirmPassword}
                  onChange={event => setConfirmPassword(event.target.value)}
                  placeholder="Confirme su contraseña"
                />

                </div>



              </Form.Group>

              
              <div class="modal-footer">

             
        <Button type="submit" className="button"  >Guardar</Button>

  
        <Button type="button" class="btn button"  className="button" onClick={closeModal}> Cerrar</Button>
       
      </div>

     
              </Form>
              </Modal.Body>
              
              </Modal>
              

      )}

<style jsx='true'>{`
            .contentModal{
              text-align: center;
                background-color:  #C6D8D3;
            }
            .content{
                width: 100%;
                height: 100%;
            }
            
            .button{
              background-color:  #F0544F;
              outline: none;
              border: none;
              
            }
            .NavbarA{
                width: 100%;
                height: 100%;
                background-color:  #F0544F;
                display: flex;
                background-color: #F0544F;
            }
            .NButtonForSideA{
               
            }
            .buttonNavBarA{
                width: 100%;
                height: 100%;
                outline: none;
                border: none;
                background-color: #FFFFFF;
                font-size: 50px;
                color: #F0544F;
            }

            .buttonNavBarAa{
                outline: none;
                border: none;
                background-color: #FFFFFF;
                font-size: 20px;
                color: black;
            }
            .navbarmainAd{
                width: 97.5%;
                display: flex;
                justify-content: space-between;
            }

            .CustomTable{
                width: 100%;
                border-spacing: 0px;
            }
            .CustomTable>thead>tr>th{
                border: 1px solid black;
                padding-left: 5px;
            }
            .CustomTable>tbody>tr>td{
                text-align: center;
                border: 1px solid black;
            }

            
            `}</style>
            </>

  )}

          
  export default CreateModal;