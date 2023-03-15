import React, { useEffect } from "react";
import ReactDOM from "react-dom";
import { CSSTransition } from "react-transition-group";
import "./Modal.css";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';


const CreateModal = props => {

   
  const closeOnEscapeKeyDown = e => {
    if ((e.charCode || e.keyCode) === 27) {
      props.onClose();
    }
  };

  useEffect(() => {
    document.body.addEventListener("keydown", closeOnEscapeKeyDown);
    return function cleanup() {
      document.body.removeEventListener("keydown", closeOnEscapeKeyDown);
    };
  }, []);

  return ReactDOM.createPortal(
    <CSSTransition
      in={props.show}
      unmountOnExit
      timeout={{ enter: 0, exit: 300 }}
    >
      <div className="modal" onClick={props.onClose}>
        <div className="modal-content" onClick={e => e.stopPropagation()}>
          <div className="modal-header">
            <h4 className="modal-title">{props.title}</h4>
          </div>
          <div className="modal-body">

          <Form>
      <Form.Group className="mb-3" controlId="formBasicEmail">
        <Form.Label>Nombre y Apellido</Form.Label>
        <Form.Control type="name" placeholder="Nombre y apellido" />

        <Form.Label>Ci</Form.Label>
        <Form.Control type="name" placeholder="Ci" />

        <Form.Label>Correo</Form.Label>
        <Form.Control type="Correo" placeholder="ingresar correo" />

        <Form.Label>Telefono</Form.Label>
        <Form.Control type="Correo" placeholder="Telefono" />


        <Form.Label>Correo electronico</Form.Label>
        <Form.Control type="Correo" placeholder="Telefono" />

        <Form.Label>Fecha de nacimiento</Form.Label>
        <Form.Control type="Correo" placeholder="Telefono" />

        <Form.Label>Genero</Form.Label>
        <Form.Control type="Correo" placeholder="Telefono" />


        <Form.Text className="text-muted">
          We'll never share your email with anyone else.
        </Form.Text>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicPassword">
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Password" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formBasicCheckbox">
        <Form.Check type="checkbox" label="Check me out" />
      </Form.Group>
      <Button variant="primary" type="submit">
        Submit
      </Button>
    </Form>


          </div>
         
         
          <div className="modal-footer">
            <button onClick={props.onClose} className="button">
              Close
            </button>
          </div>
        </div>
      </div>
    </CSSTransition>,
    document.getElementById("root")
  );
};


export default CreateModal;
