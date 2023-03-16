import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from "./components/LayoutAdmin.jsx";
import NavAdmin from "./components/NavAdmin.jsx";
import CreateModal from "./components/create/CreateModal.jsx";

import { useGeneralContext } from "../../context/GeneralContext";
import axios from 'axios';

import "./components/create/Index.module.css";


import { BsTrash, BsPencilFill, BsInfoCircle } from 'react-icons/bs';
import APILINK from '../../components/link.js';
import { useNavigate } from 'react-router';


function Profesores() {
  const [profesores, setProfesores] = useState([]);
  const [page, setPage] = useState(0);


  const { getToken, cancan, verifyToken } = useGeneralContext();

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
  
        })
        .catch(error => {
          console.error(error);
        });
    }
  }, [page, cancan, verifyToken, nav, getToken]);





  const handlePrevClick = () => {
    setPage(page - 1);
  };

  const handleNextClick = () => {
    setPage(page + 1);
  };



  const [show, setShow] = useState(false);


  return (
    <>

      <div className="page">
        <NavAdmin />

        <PanelContainerBG>


          <div>
            <table className="CustomTable">
              <thead>
                <tr>
                  <th>CI</th>
                  <th>Nombre</th>
                  <th>Apellido</th>
                  <th>Fecha de nacimiento</th>
                  <th>Direccion</th>
                  <th>Telefono</th>
                  <th>Acciones</th>
                </tr>
              </thead>
              <tbody>

                {profesores.map(profe => (
                  <tr key={profe.id}>
                    <td>{profe.documento}</td>
                    <td>{profe.nombre}</td>
                    <td>{profe.apellido}</td>
                    <td>{profe.nacimiento}</td>
                    <td>{profe.direccion}</td>
                    <td>{profe.telefono}</td>
                    <td> <BsTrash />  <BsPencilFill /> <BsInfoCircle /></td>

                  </tr>
                ))}
              </tbody>
            </table>


            <nav aria-label="Page navigation example">
              <ul className="pagination justify-content-end">
                <li className="page-item disabled">
                  <button className="pag" onClick={handlePrevClick} disabled={page === 0}>Anterior</button>
                </li>
                <li className="page-item">
                  <button className="pag" href="#" onClick={handleNextClick}>Siguiente</button>
                </li>
              </ul>
            </nav>



          </div>







        </PanelContainerBG>
        <footer>
          <div className="NButtonForSideA ">
            <CreateModal title="My Modal" onClose={() => setShow(false)} onSubmit={(value)=>{setProfesores([...profesores, value])}} show={show}>
            </CreateModal>
            
          </div>

        </footer>












      </div>

      <style jsx='true'>{`
            .page{
                display: grid;
                grid-template-rows: 5% 95%;
                width: 100%;
                height: 100vh;
            }
            .content{
                width: 100%;
                height: 100%;
            }
            
            .NavbarA{
                width: 100%;
                height: 100%;
                background-color:  #F0544F;
                display: flex;
                background-color: #F0544F;
            }
            .NButtonForSideA{
               width: 25%;
               position: absolute;
               bottom: 5px;
               right : 5px;
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
            .pag{
              outline: none;
              border: none;
              background-color: #FFFFFF;
              font-size: 10px;
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

export default Profesores