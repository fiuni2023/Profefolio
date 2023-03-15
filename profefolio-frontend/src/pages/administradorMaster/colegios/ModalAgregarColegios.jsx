import React, { useState, useEffect } from 'react'
import { Modal, Button } from 'react-bootstrap'
import "./ModalAgregarColegios.css"
import { BsFillPlusCircleFill } from "react-icons/bs"
import axios from "axios";
function ModalDialog() {
    const [nombreColegio, setNombreColegio] = useState(null);
    const [idAdmin, setIdAdmin] = useState(0);
    const [administradores, setAdministradores] = useState([]);
    const [cantidadId, setCantidadId] = useState(0);
    //Get administadores
    var config = {
        method: 'get',
        url: 'https://localhost:7063/api/administrador/page/0',
        headers: {
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQ2FybG9zLlRvcnJlczEyM0BtYWlsLmNvbSIsImp0aSI6IjY3NjdkMTZhLTQxNWItNGRhZC1iMTRjLTBjZjZkNDI2ZDRmYiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1hc3RlciIsImV4cCI6MTY3ODg1MDUwOSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.R_dHv3w5gutvZzBcO2x2px8RyqIdVbgrlL2eck-fXAM',
            'Content-Type': 'application/json'
        },


    };
    useEffect(() => {
        axios(config)
            .then(function (response) {
                console.log(JSON.stringify(response.data));
                setAdministradores(response.data.dataList);
            })
            .catch(function (error) {
                console.log(error);
            });
    }, [])
    const handleSubmit = (event) => {

        event.preventDefault();
    
        var axios = require('axios');
        var data = JSON.stringify({
          "nombre": JSON.stringify(nombreColegio),
          "estado":"true",
          "personaId": JSON.stringify(idAdmin)
        });
        
        var config = {
          method: 'post',
        maxBodyLength: Infinity,
          url: 'https://localhost:7063/api/Colegios',
          headers: { 
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQ2FybG9zLlRvcnJlczEyM0BtYWlsLmNvbSIsImp0aSI6IjFiOWRjZTExLWNlZjQtNGI0Ny05MTRhLThjOWM0OWM0Nzc3NiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1hc3RlciIsImV4cCI6MTY3ODc3MzgwOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.TBGSrTUNbwoZEWXR4yd4uWR3W6Y_bgr9F3XCOIycHUI', 
            'Content-Type': 'application/json'
          },
          data : data
        };
        
        axios(config)
        .then(function (response) {
          console.log(JSON.stringify(response.data));
        })
        .catch(function (error) {
          console.log(error);
        });
        
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

                            <select name="admin" value={idAdmin} onChange={e => setIdAdmin(e.target.value)}>
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
                    <Button className='btn-guardar btn-sm' type='submit'>
                        Guardar
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}
export default ModalDialog