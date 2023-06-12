/* eslint-disable */
import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from "./components/LayoutAdmin.jsx";
import CreateModal from "./components/create/CreateModal.jsx";
import { useGeneralContext } from "../../context/GeneralContext";
import axios from 'axios';

import styles from "./components/create/Index.module.css";

import APILINK from '../../components/link.js';
import { useNavigate } from 'react-router';
import ListDetallesProfesor from './list/ListDetallesProfesor.jsx';

import StyleComponentBreadcrumb from '../../components/StyleComponentBreadcrumb.jsx';

import Tabla from '../../components/Tabla.jsx';
import styled from 'styled-components';
import { AiOutlinePlus } from 'react-icons/ai';
import ModalProfesor from './components/ModalProfesor.jsx';
import Paginations from '../../components/Paginations.jsx';
import Spinner from '../../components/componentsStyles/SyledSpinner.jsx';
import Text from '../../components/componentsStyles/StyledText.jsx';




function Profesores() {

  const [profesores, setProfesores] = useState([]);

  const [selected_data, setSelectedData] = useState(null);
  const { getToken, cancan, verifyToken, getColegioId } = useGeneralContext();
  const idColegio=getColegioId();
  const [fetch_data, setFetchData] = useState([])

  const [page, setPage] = useState(0);

  const [totalPage, setTotalPage] = useState(1);

  const [next, setNext] = useState(false);
  const isLastPage = page === totalPage - 1;
  const [currentPage, setCurrentPage] = useState(0);



  const nav = useNavigate()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)

  useEffect(() => {
    verifyToken()
    if (!cancan("Administrador de Colegio")) {
      nav("/")
    } else {
      setLoading(true);
      let data = JSON.stringify({
        "pagina": currentPage, //numero de pagina
        "colegioId": idColegio //id de colegio del administrador de colegio
      });
      axios.post(`${APILINK}/api/Profesor/ByColegio/page`, data, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
          "Content-Type": "application/json"
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
          setError(true)
        })
        .finally(() => {
          setLoading(false);
        });


      /*
      axios.post(`${APILINK}/api/profesor/page/${currentPage}`, {
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
      */

    }
  }, [page, cancan, verifyToken, nav, getToken, fetch_data, currentPage]);

  const doFetch = () => {
    setFetchData((before) => [before])
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
        <StyleComponentBreadcrumb nombre="Profesores" />

        {loading ? <Spinner height={'calc(100vh - 80px)'} />
          : error ? <Text>Lamentamos esto, ha ocurrido un error al obtener los datos.</Text>
            :
            <>
              <PanelContainerBG>


                <div>

                  <Tabla
                    datosTabla={{
                      tituloTabla: 'Lista_de_profesores',
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



                  <div >



                  </div>

                </div>

                {/* <CreateModal title="My Modal" onHide={() => setShow(false)}  show={show}
             triggerState={(profesor)=>{doFetch(profesor)}}/> */}
              </PanelContainerBG>
            </>}

            <ModalProfesor onHide={handleHide} selected_data={selected_data} show={show} />
            <AddButton onClick={() => setShow(true)}>
                  <AiOutlinePlus size={"35px"} />
                </AddButton>

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