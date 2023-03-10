import React from 'react'
import { Modal, Button } from 'react-bootstrap'
import "./ModalAgregarColegios.css"
import { BsFillPlusCircleFill } from "react-icons/bs"
function ModalDialog() {
    const [isShow, invokeModal] = React.useState(false)
    const initModal = () => {
      return invokeModal(!false)
    }
    const closeModal = () => {
        return invokeModal(false)
      }
    return (
      <>
        <button className="button-agregar" onClick={initModal}><BsFillPlusCircleFill className="icono-agregar" /></button>
        <Modal show={isShow} className="modal-principal">
          <Modal.Header  id='modal-contenido' closeButton onClick={closeModal}>
            <h5 >Agregar Colegio</h5>
          </Modal.Header>
          <Modal.Body id='modal-contenido'>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit.
          </Modal.Body>
          <Modal.Footer id='modal-contenido'>
            <Button className="btn btn-light btn-sm" onClick={closeModal}>
              Cancelar
            </Button>
            <Button className='btn-guardar btn-sm' onClick={closeModal}>
              Guardar
            </Button>
          </Modal.Footer>
        </Modal>
      </>
    )
  }
  export default ModalDialog