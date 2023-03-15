import React, { useState } from 'react'
import { Modal, Button } from 'react-bootstrap'
import "./ModalAgregarColegios.css"
import { BsFillPlusCircleFill } from "react-icons/bs"
import axios from "axios";
function ModalDialog() {
    const [nombreColegio, setNombreColegio] = useState(null);

    /*var config = {
        method: 'post',
        url: 'https://localhost:7063/api/Colegios',
        headers: {
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQ2FybG9zLlRvcnJlczEyM0BtYWlsLmNvbSIsImp0aSI6IjFiOWRjZTExLWNlZjQtNGI0Ny05MTRhLThjOWM0OWM0Nzc3NiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1hc3RlciIsImV4cCI6MTY3ODc3MzgwOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.TBGSrTUNbwoZEWXR4yd4uWR3W6Y_bgr9F3XCOIycHUI',
            'Content-Type': 'application/json'
        },
        //data: data
    };
    function guardarDatos(){
        setNombreColegio()

    }*/
    /*
    var axios = require('axios');
    var data = JSON.stringify({
        "id": 0,
        "nombre": "Marcelina Bogado",
        "estado": true,
        "personaId": "7971f2c0-bf27-44a3-b3cb-239ed21eab4a"
    });

    

    axios(config)
        .then(function (response) {
            console.log(JSON.stringify(response.data));
        })
        .catch(function (error) {
            console.log(error);
        });
        */
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
                            <input required type="text" id="input-colegio" name="colegio-nombre"></input><br />
                            <label for="administrador"><strong> Administrador</strong></label><br />
                            <input required placeholder='Nombre' type="text" id="input-admin" name="nombre-administrador" ></input>
                            <input required type="text" id="input-apellido" placeholder='Apellido' name="apellido-administrador"></input>
                        </form>
                    </div>

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