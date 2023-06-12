import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from '../../../../profesor/components/LayoutAdmin';
import { useGeneralContext } from "../../../../../context/GeneralContext";
import styled from "styled-components";
import { AiOutlinePlus, AiOutlineRight } from 'react-icons/ai';
import { useNavigate } from 'react-router';
import CreateModalDocumento from '../create/CreateModalDocumento';
import Tabla from '../../../../../components/Tabla';
import Spinner from '../../../../../components/componentsStyles/SyledSpinner';

import ClassesService from '../Helper/DocumentoHelper';
import { Row } from 'react-bootstrap';
import BackButton from '../../../components/BackButton';
import { useModularContext } from '../../../context';

const FlexDiv = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  gap: 10px;
`


function ListarDocumentos() {
  
  const {stateController, dataSet} = useModularContext()
  const {materiaId} = stateController
  const {loading} = dataSet
  const [selectData, setSelectedData] = useState(null)
  const { getToken, cancan, verifyToken } = useGeneralContext();
  const [documento, setDocumento] = useState([]);
  const token = getToken()

  const [fetch_data, setFetchData] = useState([])
  const { materiaName, currColegio, currClase } = dataSet

  const nav = useNavigate()

  const [show, setShow] = useState(false);

  const handleHide = () => {
    setShow(false)
    doFetch()
    setSelectedData(null)
  }

  const doFetch = () => {
    setFetchData((before) => [before])
  }


  useEffect(() => {
    const listaMateriasProfesores = async () => {
      try {
        const dataList = await ClassesService.getDocumento(materiaId, token);
        setDocumento(dataList.data ?? []);
      } catch (e) {
        setDocumento([]);
      }
    };
    listaMateriasProfesores();
  }, [cancan, verifyToken, nav, token, fetch_data, materiaId]);


  const btndetallesDocumento = (data) => {
    setSelectedData(data)
    setShow(true);
  };


  return (
    <>
      <div className='m-4'>
        
        <Row>
          <FlexDiv>
            <BackButton to="materiashow" />
            <h5 className="m-0">
            {currColegio} - {currClase} - {materiaName} - Documentos
            </h5>
          </FlexDiv>
        </Row>

        {loading ? <Spinner height={"calc(100vh - 90px)"}></Spinner>
        :  
        <PanelContainerBG>
          <div >
            <Tabla
              datosTabla={{
                tituloTabla: 'Lista de Documentos',
                titulos: [
                  { titulo: 'Nombre' },
                  { titulo: 'Enlace' },
                ],
                clickable: { action: btndetallesDocumento },
                
                filas: documento.map((documento) => ({
                  fila: documento,
                  datos: [
                    { dato: documento.nombre },
                    { dato: documento.enlace },
                    { componente: <InvisibleButton onClick={(e)=>{
                      e.stopPropagation()
                      window.open(documento.enlace, "_blank")
                    }}><AiOutlineRight size={20} /></InvisibleButton>}
                  ],
                })),
              }}
              />
          </div>

          <CreateModalDocumento onHide={handleHide} selectData={selectData} show={show} />

          <AddButton onClick={() => setShow(true)}>
            <AiOutlinePlus size={"35px"} />
          </AddButton>

        </PanelContainerBG>
        }

        <footer />
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

const InvisibleButton = styled.button`
  width: 30px;
  height: 30px;
  background-color: white;
  border: none;
  outline: none;
  border-radius: 50%;
  &:hover {
    filter: brightness(0.7);
  }
  &:active {
    filter: brightness(0.6);
  }
`;

export default ListarDocumentos