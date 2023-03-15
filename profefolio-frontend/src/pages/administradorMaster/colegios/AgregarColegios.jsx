import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import "./ModalAgregarColegios.css"
import { BsFillPlusCircleFill } from "react-icons/bs"
import axios from "axios";
import { useGeneralContext } from "../../../context/GeneralContext";
function ModalAgregarColegios() {
    const { getToken } = useGeneralContext()
    const [nombreColegio, setNombreColegio] = useState(null);
    const [idAdmin, setIdAdmin] = useState(0);
    const [administradores, setAdministradores] = useState([]);
    const [cantidadId, setCantidadId] = useState(0);
    //Get administadores
    var config = {
        method: 'get',
        url: 'https://localhost:7063/api/administrador/page/0',
        headers: {
            'Authorization': `Bearer ${getToken()}`,
            'Content-Type': 'application/json'
        },


    };
    useEffect(() => {
        axios(config)
            .then(function (response) {
                
                setAdministradores(response.data.dataList);
            })
            .catch(function (error) {
                console.log(error);
            });
    }, [])
    const handleAdmin=(idAdmin)=>{
        console.log(idAdmin);
        setIdAdmin(idAdmin);
    }
    const handleSubmit = (event) => {
        //event.preventDefault();
        
        var data = JSON.stringify({
          "nombre": nombreColegio,
          "personaId": idAdmin
        });
        
        var config = {
          method: 'post',
        maxBodyLength: Infinity,
          url: 'https://localhost:7063/api/Colegios',
          headers: { 
            'Authorization': `Bearer ${getToken()}`,
            'Content-Type': 'application/json'
          },
          data : data
        };
        
        axios(config)
        .then(function (response) {
            if(response.status>=400){
                console.log("Hubo un error")
            }
          else if(response.status>=200){
            console.log("Guardado correctamente");
          }
        })
        .catch(function (error) {
          console.log(error);
        });
        closeModal();
        
}

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
                <Modal.Header id='modal-contenido' closeButton onClick={closeModal}>
                    <h5 >Agregar Colegio</h5>
                </Modal.Header>
                <Modal.Body id='modal-contenido'>
                    <div>
                        <form>
                            <label for="colegio-nombre"><strong> Colegio</strong></label><br />
                            <input required type="text" id="input-colegio" name="colegio-nombre" onChange={event => setNombreColegio(event.target.value)}></input><br />
                            <label for="administrador"><strong> Administrador</strong></label><br />

                            <select name="admin" value={idAdmin} onChange={e => handleAdmin(e.target.value)} className="select_admin">
                                {administradores.map((administrador) =>
                                    <option key={administrador.id} value={administrador.id}>{administrador.nombre} {administrador.apellido}</option>
                                )}
                            </select>
                        </form>
                    </div>

                </Modal.Body>
                <Modal.Footer id='modal-contenido'>
                    <Button className="btn btn-light btn-sm" onClick={closeModal}>
                        Cancelar
                    </Button>
                    <Button className='btn-guardar btn-sm' onClick={()=>handleSubmit()}>
                        Guardar
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}
export default ModalAgregarColegios