import React, { useState } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';

import {BsFillPlusCircleFill} from 'react-icons/bs';

function CreateModal() {

  

  // Función para abrir el modal
 

  // Función para cerrar el modal
 
  const [showModal, setShowModal] = useState(false);
  const [formValues, setFormValues] = useState({
    nombre: '',
    apellido: '',
    documento: '',
    documentoTipo: '',
    password: '',
    confirmPassword: '',
    email: '',

    nacimiento: '',
    genero: '',
    direccion: '',
    telefono: '',
  
  });

  function closeModal() {
    setShowModal(false);
  }

  const handleCloseModal = () => setShowModal(false);
  const handleShowModal = () => setShowModal(true);
  const handleFormChange = (event) => {
    const { name, value } = event.target;
    setFormValues((prevValues) => ({
      ...prevValues,
      [name]: value,
    }));
  };
  const handleFormSubmit = (event) => {
    event.preventDefault();
    console.log('Formulario enviado:', formValues);
    handleCloseModal();
  };

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

      <Modal show={showModal} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>Formulario</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <Form onSubmit={handleFormSubmit}>
            <Form.Group className="row">
              <Form.Label className="col-sm-3">Nombre:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="nombre"
                  value={formValues.nombre}
                  onChange={handleFormChange}
                  placeholder="Ingrese su nombre"
                />
              </div>
            </Form.Group>
            

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Apellido:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="apellido"
                  value={formValues.apellido}
                  onChange={handleFormChange}
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
                  value={formValues.nacimiento}
                  onChange={handleFormChange}
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Genero:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="genero"
                  value={formValues.genero}
                  onChange={handleFormChange}
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Documento:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="documento"
                  value={formValues.documento}
                  onChange={handleFormChange}
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
                  value={formValues.documentoTipo}
                  onChange={handleFormChange}
                >
                  <option value="">Seleccione un tipo</option>
                  <option value="dni">DNI</option>
                  <option value="cedula">Cédula</option>
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
                  value={formValues.telefono}
                  onChange={handleFormChange}
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Direccion:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="text"
                  name="direccion"
                  value={formValues.direccion}
                  onChange={handleFormChange}
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Contraseña:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="password"
                  name="password"
                  value={formValues.password}
                  onChange={handleFormChange}
                  placeholder="Ingrese su contraseña"
                />
              </div>
            </Form.Group>

            <Form.Group className="row">
              <Form.Label className="col-sm-3">Confirmar contraseña:</Form.Label>
              <div className="col-sm-9">
                <Form.Control
                  type="password"
                  name="confirmPassword"
                  value={formValues.confirmPassword}
                  onChange={handleFormChange}
                  placeholder="Confirme su contraseña"
                />

                </div>



              </Form.Group>

              
              <div class="modal-footer">

       <button type="button" class="btn btn-primary">Guardar cambios</button>
        <Button type="button" class="btn btn-secondary"  className="button" onClick={closeModal}> Cerrar</Button>
       
      </div>

              </Form>
              </Modal.Body>
              </Modal>

      )}

<style jsx='true'>{`
            .page{
                display: grid;
                grid-template-rows: 5% 95%;
                width: 100%;
                height: 100vh;
            }
            .content{
                width: 100%;
                height: 100%;
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