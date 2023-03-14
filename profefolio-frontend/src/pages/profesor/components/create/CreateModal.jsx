import React, { useState } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';

function CreateModal() {
  const [showModal, setShowModal] = useState(false);
  const [formValues, setFormValues] = useState({
    nombre: '',
    apellido: '',
    documento: '',
    documentoTipo: '',
    password: '',
    confirmPassword: '',
    email: '',
  });

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
      <Button variant="primary" onClick={handleShowModal}>
        Abrir modal
      </Button>

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

              </Form>
              </Modal.Body>
              </Modal>
            </>

  )}


  export default CreateModal;