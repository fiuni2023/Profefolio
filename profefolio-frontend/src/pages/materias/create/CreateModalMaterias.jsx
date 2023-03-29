import React, { useState } from 'react';
import axios from 'axios';
import { Modal, Form,Col,Row } from 'react-bootstrap';
import { BsFillPlusCircleFill } from 'react-icons/bs';
import { useGeneralContext } from '../../../context/GeneralContext';
import { toast } from 'react-hot-toast';

import styles from  './Modal.module.css';
import APILINK from '../../../components/link.js';


function CreateModalMaterias({onSubmit = ()=>{}, triggerState = () => {}}) {

  

  const [nombre_Materia, setNombreMateria] = useState('');

  const { getToken } = useGeneralContext();


  const handleSubmit = (event) => {

    event.preventDefault();

    if (nombre_Materia === "" ) toast.error("revisa los datos, los campos deben ser completados")
    else {
      axios.post(`${APILINK}/api/Materia`, {
        nombre_Materia,
    
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
          setNombreMateria("")
          

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
          setNombreMateria("")
        
    setShowModal(false);
  }

  const handleCloseModal = () => setShowModal(false);
  const handleShowModal = () => setShowModal(true);



  return (

    <>
      <div className={styles.NButtonForSideA}>
        <div className={styles.buttonNavBarAa}>
          <button className={styles.buttonNavBarA} onClick={handleShowModal}>
            <BsFillPlusCircleFill />
          </button>
        </div>
      </div>



      <Modal show={showModal} onHide={handleCloseModal} >




        <Modal.Header closeButton className={styles.contentModal}>
          <Modal.Title className="">Agregar Materia</Modal.Title>
        </Modal.Header>


        <Modal.Body className={styles.contentModal}>
          <form onSubmit={handleSubmit}>
            
            
            <Row>
              <Col>
              <Form.Label >Nombre de la materia:  </Form.Label>
              <div >
                <Form.Control
                  className={styles.option}
                  type="text"
                  value={nombre_Materia}
                  onChange={event => setNombreMateria(event.target.value)}
                  placeholder="Ingrese el nombre"
                />
              </div>
            
              </Col>

             
            </Row>
            
           
            <br/>
            
        
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


export default CreateModalMaterias;