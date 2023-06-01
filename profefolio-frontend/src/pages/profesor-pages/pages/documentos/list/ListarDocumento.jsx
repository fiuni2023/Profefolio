import React, { useState, useEffect } from 'react';
import styled from "styled-components";
import BackButton from '../../../components/BackButton';
import DocumentoCard from '../componentes/DocumentoCard';
import AddButton from '../../evaluaciones/components/AddButton';
import CreateDocumentoModal from '../componentes/CreateModal';
import { useGeneralContext } from '../../../../../context/GeneralContext';
import DocumentosService from '../service/DocumentosService';
import { useModularContext } from '../../../context';

const FlexDiv = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  gap: 10px;
`;

const GridView = styled.div`
  margin-top: 15px;
  display: grid;
  grid-template-columns: 23% 23% 23% 23%;
  gap: 1%;
`;

const ListarDocumentos = ({
  List = [1,1,1,1]
}) => {

  const { getToken } = useGeneralContext()
  const token = getToken()
  const {stateController} = useModularContext()
  const {materiaId} = stateController

  const [ showModal, setShowModal ] = useState(false)
  const [ fetch_data, setFetchData ] = useState([])

  useEffect(()=>{
    DocumentosService.getDocumento(materiaId, token)
    .then((r)=>{
      console.log(r)
    })
  },[token, materiaId, fetch_data])

  const doFetch = () => {
    setFetchData((before)=>{return [before]})
  }

  return <>
    <FlexDiv>
      <BackButton to="materiashow" />
      <h5 className="m-0">
        Documentos
      </h5>
    </FlexDiv>
    <GridView>
      {
        List.map((l)=>{
          return <DocumentoCard onClick={(d)=>{console.log(d)}} />
        })
      }
    </GridView>
    <CreateDocumentoModal show={showModal} onHide={()=>{setShowModal(!showModal)}} doFetch={doFetch}/>
    <AddButton onClick={()=>{setShowModal(!showModal)}} />
  </>
}

export default ListarDocumentos