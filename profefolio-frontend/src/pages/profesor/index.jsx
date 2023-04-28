/* eslint-disable */
import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from "./components/LayoutAdmin.jsx";
import CreateModal from "./components/create/CreateModal.jsx";
import { useGeneralContext } from "../../context/GeneralContext";
import axios from 'axios';

import styles from  "./components/create/Index.module.css";

import APILINK from '../../components/link.js';
import { useNavigate } from 'react-router';
import ListDetallesProfesor from './list/ListDetallesProfesor.jsx';

import StyleComponentBreadcrumb from '../../components/StyleComponentBreadcrumb.jsx';

import Tabla from '../../components/Tabla.jsx';
import styled from 'styled-components';
import { AiOutlinePlus } from 'react-icons/ai';
import ModalProfesor from './components/ModalProfesor.jsx';
import Paginations from '../../components/Paginations.jsx';




function Profesores() {

  const [profesores, setProfesores] = useState([]);
  
  const [selected_data, setSelectedData] = useState(null);
  const { getToken, cancan, verifyToken } = useGeneralContext();

  const [fetch_data, setFetchData ] = useState([])

  const [page, setPage] = useState(0);

  const [totalPage, setTotalPage] = useState(1);
    
  const [next, setNext] = useState(false);
  const isLastPage = page === totalPage -1;
  const [currentPage, setCurrentPage] = useState(0);



  const nav = useNavigate()

  useEffect(() => {
    verifyToken()
    if(!cancan("Administrador de Colegio")){
      nav("/")
    }else{
      //axios.get(`https://miapi.com/products?page=${page}&size=${size}`, {
  
      axios.get(`${APILINK}/api/profesor/page/${page}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
  
        .then(response => {
          setProfesores(response.data.dataList);
          setTotalPage(response.data.totalPage);//Total de Paginas
          setCurrentPage(response.data.currentPage);
          setNext(response.data.next);
      
  

        })
        .catch(error => {
          console.error(error);
        });
    }
  }, [page, cancan, verifyToken, nav, getToken, fetch_data]);

 const doFetch =() =>{
    setFetchData((before)=>[before])
}
 


const btndetalles = (data) => {
  setSelectedData(data)
  setShow(true);
};


  const handlePrevClick = () => {
    setPage(page - 1);
  };


  const handleNextClick = () => {

    if (!isLastPage) {
      setPage(page + 1);
    }
  };


  const [show, setShow] = useState(false);

  const handleHide = () => {
    setShow(false)
    doFetch()
    setSelectedData(null)
  }


  return (
    <>

      <div>
      <StyleComponentBreadcrumb nombre="Profesor" />

        <PanelContainerBG>


          <div>

          <Tabla
              datosTabla={{
                tituloTabla: 'Lista de profesores',
                titulos: [
                  { titulo: 'CI' },
                  { titulo: 'Nombre' },
                  { titulo: 'Apellido' },
                  { titulo: 'Fecha de nacimiento' },
                  { titulo: 'Dirección' },
                  { titulo: 'Teléfono' },
                ],
                clickable: { action: btndetalles },
                colorHeader: '',
                tableWidth: '',

            

                filas: profesores.map((profe) => ({
                  fila: profe,
                  datos: [
                    { dato: profe.documento },
                    { dato: profe.nombre },
                    { dato: profe.apellido },
                    {
                      dato: new Date(profe.nacimiento).toLocaleDateString(),
                    },
                    { dato: profe.direccion },
                    { dato: profe.telefono },
                  ],
                })),
              }}
            />

           <Paginations totalPage={totalPage} currentPage={currentPage} setCurrentPage={setCurrentPage} next={next} />
            {/* <ListDetallesProfesor showModal={showModal} setShowModal={setShowModal} id={id}  triggerState={(profesor)=>{setProfesores(profesor)}} page={page} /> */}

            <AddButton onClick={()=>setShow(true)}>
              <AiOutlinePlus size={"35px"} />
            </AddButton>
  
            <div >

           
        
      </div>
 
          </div>
          
          {/* <CreateModal title="My Modal" onHide={() => setShow(false)}  show={show}
             triggerState={(profesor)=>{doFetch(profesor)}}/> */}

          <ModalProfesor onHide={handleHide} selected_data={selected_data} show={show}  />
        </PanelContainerBG>
        

      </div>

      <style jsx='true'>{`

        footer {
          position: fixed;
          background-color: hwb(0 99% 0%);
          color: rgb(245, 249, 249);
          bottom: 0;
          left: 0;
          right: 0;
          padding: 20px;
          text-align: right;
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

export default Profesores