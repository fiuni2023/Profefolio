/* eslint-disable */
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Modal, Form,Col,Row } from 'react-bootstrap';
import { useGeneralContext } from '../../../context/GeneralContext';
import styles from  '../create/Modal.module.css';
import APILINK from '../../../components/link';
import { toast } from 'react-hot-toast';
import { BsTrash, BsPencilFill } from 'react-icons/bs';
import ModalConfirmacion from '../../profesor/components/create/ModalConfirmacon';


function ListDetallesMateria(props) {
  const { showModal, setShowModal ,id,triggerState , page} = props;

 const [materia, setMaterias] = useState([]);

  const { getToken } = useGeneralContext();

  const [readOnly, setReadOnly] = useState(true);

  const [eliminarVisible, setEliminarVisible] = useState(true);

  const [showConfirmDialog, setShowConfirmDialog] = useState(false);

  const handleDeleteClick = () => {
    setShowConfirmDialog(true);
  };

  const handleConfirmDelete = () => {
    axios.delete(`${APILINK}/api/Materia/${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    })

      .then(response => {
        handleUpdate(); 
        setMaterias(response.data)
        toast.success("Eliminado exitoso");
        setShowModal(false);
    


      })
      .catch(error => {
        console.error(error);
      });
    setShowConfirmDialog(false);
  };

  const handleClose = () => {
    setShowConfirmDialog(false);
  };
 


  const handleUpdate = () => {
    axios.get(`${APILINK}/api/Materia/page/${page}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    })

      .then(response => {
        triggerState(response.data.dataList);

    


      })
      .catch(error => {
        console.error(error);
      });
  }




  const handleSubmit = (event) => {

    event.preventDefault();

    if (nombre_Materia === "" ) toast.error("revisa los datos, los campos deben ser completados")
   

    else {
      axios.put(`${APILINK}/api/Materia/${id}`, {
        nombre_Materia,
       

      }, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          //triggerState(response.data)
          handleUpdate();

          
          setProfesores(response.data)
          toast.success("Editado exitoso");

          setShowModal(false);
          setNombreMateria("")
         
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
    setEliminarVisible(true);
  };

  function closeModal() {
    setShowModal(false);
    setReadOnly(true);
    setEliminarVisible(true);
  }
  
  const handleCancelar = () => {

    setReadOnly(true);

  }

  const handleModificar = () => {
    setReadOnly(!readOnly);
   
  
  
  };

 
  useEffect(() => {

    if(showModal){
      
    axios.get(`${APILINK}/api/Materia/${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    })

      .then(response => {

        setMaterias(response.data);
        const { nombre_Materia} = response.data;
        setNombreMateria(nombre_Materia);
       


      })
      .catch(error => {
       // console.error(error);
      });


}
}, [ id, getToken ]);

const [nombre_Materia, setNombreMateria] = useState(materia.nombre_Materia || '');


  return (

    <>
    <Modal show={showModal} onHide={handleCloseModal}>
      <Modal.Header className={styles.contentModal}>
      <Modal.Title className="">Detalles Materia</Modal.Title>
      </Modal.Header>

    
      <Modal.Body className={styles.contentModal} >

         
   
      
            <Row>
              <Col>
              <Form.Label >Nombre Materia: </Form.Label>
             

                 {readOnly ? (
          <>
           
            {eliminarVisible && (
              <Form.Control
              type="text"
              defaultValue={materia.nombre_Materia  || ''}
              readOnly={readOnly}
            /> 
            )}
          </>
        ) : (

          <Form.Control
          className={styles.option}
          type="text"
          value={nombre_Materia}
          onChange={event => setNombreMateria(event.target.value)}
          //placeholder={profesor.nombre} 
        />
          
        )}
    
              </Col>

             
            </Row>
           
           
            <br/>
       
            
               
        
        </Modal.Body> 

     
        <Modal.Footer className={styles.footerModal}>
         

       
        <button variant="primary" onClick={closeModal} className={styles.buttonClose}>Cerrar</button>


      <ModalConfirmacion
        show={showConfirmDialog}
        onHide={handleClose}
        onConfirm={handleConfirmDelete}
        message="¿Está seguro de que desea eliminar ?"
      />

        </Modal.Footer>
       
  
    </Modal>


    
    </>
  );
}

export default ListDetallesMateria;