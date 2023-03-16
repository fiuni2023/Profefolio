import React, { useState } from 'react';
import axios from 'axios';
import { Button, Modal, Form } from 'react-bootstrap';

import { BsFillPlusCircleFill } from 'react-icons/bs';
import { useGeneralContext } from "../../../../context/GeneralContext";
import { toast } from 'react-hot-toast';
import APILINK from '../../../../components/link';


function CreateModal({onSubmit = ()=>{}}) {

  const [nombre, setNombre] = useState('');
  const [apellido, setApellido] = useState('');
  const [documento, setDocumento] = useState('');
  const [documentoTipo, setDocumentoTipo] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [email, setEmail] = useState('');
  const [nacimiento, setNacimiento] = useState('');
  const [genero, setGenero] = useState('');
  const [direccion, setDireccion] = useState('');
  const [telefono, setTelefono] = useState('');

  const { getToken } = useGeneralContext();


  const handleSubmit = (event) => {

    event.preventDefault();
    if (nombre === "" || apellido === "" || documento === "" || documentoTipo === "" || password === "" || confirmPassword === "" || email === "" || nacimiento === "" || genero === "" || direccion === "" || telefono === "") toast.error("revisa los datos, los campos deben ser completados")
    else if (password.length < 8) toast.error("Contraseña no suficientemente larga")
    else if (password !== confirmPassword) toast.error("las contraseñas no son iguales")
    else if( new Date()< new Date(nacimiento)) toast.error("Ingrese una fecha valida")
    else {
      axios.post(`${APILINK}/api/profesor`, {
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
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          onSubmit(response.data)
          toast.success("Guardado exitoso");

          setShowModal(false);
          setNombre("")
          setApellido("")
          setDireccion("")
          setDocumentoTipo("")
          setDocumento("")
          setPassword("")
          setConfirmPassword("")
          setEmail("")
          setNacimiento("")
          setGenero("")
          setTelefono("")

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
  }

  const handleCloseModal = () => setShowModal(false);
  const handleShowModal = () => setShowModal(true);



  return (

    <>
      <div className='NButtonForSideA'>
        <div className="buttonNavBarAa">
          <Button className="buttonNavBarA" onClick={handleShowModal}>
            <BsFillPlusCircleFill />
          </Button>
        </div>
      </div>



      <Modal show={showModal} onHide={handleCloseModal} >




        <Modal.Header closeButton className="contentModal text-center">
          <Modal.Title className="">Agregar Profesor</Modal.Title>
        </Modal.Header>


        <Modal.Body className="contentModal">
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
                  type="date"
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
                  pattern="^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$"
                  name="password"
                  value={password}
                  //onChange={handleConfirmPasswordChange}
                  onChange={event => setPassword(event.target.value)}
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


            <div className="modal-footer">
              <Button type="submit" className="button"  >Guardar</Button>
              <Button className="btn button" onClick={closeModal}> Cerrar</Button>

            </div>


          </Form>
        </Modal.Body>

      </Modal>




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

  )
}


export default CreateModal;