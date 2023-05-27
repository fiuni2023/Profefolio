/* eslint-disable */
import React, { useState, useEffect, useFetch } from 'react';
import {PanelContainerBG} from '../../../../profesor/components/LayoutAdmin';
import { useGeneralContext } from "../../../../../context/GeneralContext";
import axios from 'axios';
import TextButton from '../../../../../components/TextButton';
import BackButton from '../../../components/BackButton';
import Card from '../../../../../components/Card';
import { SRow } from '../../../../../components/componentsStyles/StyledDashComponent';
import styled from "styled-components";
import StyleComponentBreadcrumb from '../../../../../components/StyleComponentBreadcrumb';
import { toast } from 'react-hot-toast';
import { AiOutlinePlus } from 'react-icons/ai';
import APILINK from '../../../../../components/link';
import { useNavigate } from 'react-router';

import CreateModalDocumento from '../create/CreateModalDocumento';

import Tabla from '../../../../../components/Tabla';

import ModalConfirmacion from '../modal/ModalConfirmarDelete.jsx';

import IconButton from '../../../../../components/IconButton';
import { Row, Col } from "react-bootstrap";

import ClassesService from '../Helper/DocumentoHelper';
import { useModularContext } from '../../../context';

const FlexDiv = styled.div`
    display: flex;
    align-items: center;
    width: 100%;
    gap: 10px;
`

const GridDiv = styled.div`
    margin-top: 1%;
    display: grid;
    grid-template-columns: 23% 23% 23% 23%;
    gap: 10px;
`

const GapDiv = styled.div`
    height: 30px;
    `
function ListarDocumentos() {

  const [materias, setMaterias] = useState([]);

  const [id, setId] = useState(null);
  const [selectData, setSelectedData] = useState({})
  const { getToken, cancan, verifyToken,getUserId } = useGeneralContext();
  const {stateController} = useModularContext()
  const {materiaId} = stateController

  const [page, setPage] = useState(1);
  const [nombre, setNombre] = useState('');
  const [enlace, setEnlace] = useState('');

  const [showModal, setShowModal] = useState(false);
  const [nombre_Materia_delete, setNombreMateriaDelete] = useState('');
  const [fetch_data, setFetchData ] = useState([])

  const nav = useNavigate()

  const [show, setShow] = useState(false);

  const handleHide = () => {
    setShow(false)
    doFetch()
    setSelectedData(null)
  }

  const doFetch =() =>{
    setFetchData((before)=>[before])
}
  const getMaterias = () => {
    verifyToken()
    if (!cancan("Profesor")) {
      nav("/")
    } else {
      //https://localhost:7063/api/Materia

      axios.get(`${APILINK}/api/Materia`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })

        .then(response => {
          setMaterias(response.data);
        })
        .catch(error => {
          toast.error(error);
        });
    }

  }

  useEffect(() => {

    //getMaterias();

  }, [page, cancan, verifyToken, nav, fetch_data]);

  /*
    "nombre": "string",
  "enlace": "string",
  "profesorId": "string",
  "materiaListaId": 0 */


  const handleDocumentos = async () => {

    const body = {
      "nombre": nombre,
      "enlace": enlace,
      "profesorId": getUserId(),
      "materiaListaId": materiaId,
    };

    ClassesService.createDocumento(body, getToken())
      .then(() => {
        toast.success("Los datos fueron enviados correctamente.");
        doFetch()
      })
      .catch(() => {
        toast.error("No se pudieron guardar los cambios. Intente de nuevo o recargue la página.");
      });
  };


  const handleNombre = (event) => {
    setNombre(event.target.value);
  }

  const handleEnlace = (event) => {
    setEnlace(event.target.value);
  }
 
  const handleShowModal = (event, id, nombre) => {
    setId(id);
    setNombreMateriaDelete(nombre);
    setShowModal(true);
    event.stopPropagation();
  };


  const handleDelete = async (event) => {
    try {
      // lógica para eliminar el elemento
      // `https://localhost:7063/api/Materias/${id}`
      await axios.delete(`${APILINK}/api/Materia/${id}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      });

      toast.success("Eliminado exitoso");
      getMaterias();
      setShowModal(false);
    } catch (error) {
      if (error.response && error.response.status === 400) {
        toast.error("No se puede eliminar una materia asignada a una clase");
      }
    }
    setShowModal(false); // ocultar el modal después de eliminar el elemento
  };
  return (
    <>
      <div>

      <StyleComponentBreadcrumb nombre={`Documentos`} />
        <PanelContainerBG>

          <div >
            <div >
              <div >
                <Tabla
                  datosTabla={{
                    tituloTabla: 'Lista de Documentos',
                    titulos: [
                      { titulo: 'Nombre' },
                      { titulo: 'Url' },
                      { titulo: 'Acciones' }
                    ],
                   // clickable: { action: btndetallesMateria },
                    
                    filas: materias.map((materia) => ({
                      fila: materia,
                      datos: [
                        { dato: materia.id },
                        { dato: materia.nombre_Materia },
                        {
                          dato: <IconButton enabled={true} buttonType='close' onClick={(event) => handleShowModal(event, materia.id, materia.nombre_Materia)}> X </IconButton>
                        },

                      ],
                    })),
                  }}
                  selected={id ?? '-'}
                />
              </div>

              <AddButton onClick={()=>setShow(true)}>
              <AiOutlinePlus size={"35px"} />
            </AddButton>
              <ModalConfirmacion
                show={showModal}
                onHide={() => setShowModal(false)}
                onConfirm={handleDelete}
               // materia={nombre_Materia_delete}
              />

              <div>  
             

              </div>
            </div>
          </div>

         
        </PanelContainerBG>
        <footer>
        <CreateModalDocumento onHide={handleHide} selectData={selectData} show={show}  />

        </footer>

      </div>

      <style jsx='true'>{`

footer {
  position: fixed;
  background-color: hwb(0 99% 0%);
  color: rgb(245, 249, 249);
  bottom: 0;
  left: 0;
  right: 0;
  padding: 0px;
  text-align: right;
}

        .btn-smaller {
          font-size: 0.8rem;
        }

           

            
            `}</style>
    </>
  )
}

const AddButton = styled.button`
    width: 50px;
    height: 50px;
    padding: 7px;
    color: white;
    background-color: #F0544F;
    border-radius: 50%;
    position: fixed;
    bottom: 1.5%;
    right: 1%;
    cursor: pointer;
    border: none;
&:hover {
    filter: brightness(0.95);
&:active {
    filter: brightness(0.8);
  }
`;
export default ListarDocumentos