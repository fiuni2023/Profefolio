/* eslint-disable */
import React, { useState, useEffect, useFetch } from 'react';
import {PanelContainerBG} from '../../../../profesor/components/LayoutAdmin';
import { useGeneralContext } from "../../../../../context/GeneralContext";
import styled from "styled-components";
import StyleComponentBreadcrumb from '../../../../../components/StyleComponentBreadcrumb';
import { toast } from 'react-hot-toast';
import { AiOutlinePlus } from 'react-icons/ai';
import { useNavigate } from 'react-router';
import CreateModalDocumento from '../create/CreateModalDocumento';
import Tabla from '../../../../../components/Tabla';


import ClassesService from '../Helper/DocumentoHelper';
import { useModularContext } from '../../../context';


function ListarDocumentos() {

  const [id, setId] = useState(null);
  const [selectData, setSelectedData] = useState(null)
  const { getToken, cancan, verifyToken,getUserId } = useGeneralContext();
  const {stateController} = useModularContext()
  const {materiaId} = stateController
  const [page, setPage] = useState(1);
  const [nombre, setNombre] = useState('');
  const [enlace, setEnlace] = useState('');

  const [documento, setDocumento] = useState([]);

  const [showModal, setShowModal] = useState(false);
  const [nombre_delete, setNombreDelete] = useState('');
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


  const handleCreateDocumentos = async () => {

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


  const btndetallesDocumento = (data) => {
    setSelectedData(data)
    setShow(true);
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
                      { titulo: 'Enlace' },
                    
                    ],
                    clickable: { action: btndetallesDocumento},
                    
                    filas: documento.map((documento) => ({
                      fila: documento,
                      datos: [
                        { dato: documento.nombre },
                        { dato: documento.enlace },
                       
                      ],
                    })),
                  }}
                />
              </div>

              <CreateModalDocumento onHide={handleHide} selectData={selectData} show={show}  />

              <AddButton onClick={()=>setShow(true)}>
              <AiOutlinePlus size={"35px"} />
            </AddButton>
              <div>  
             

              </div>
            </div>
          </div>

         
        </PanelContainerBG>
        <footer>

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