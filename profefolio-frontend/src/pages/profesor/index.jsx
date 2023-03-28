/* eslint-disable */
import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from "./components/LayoutAdmin.jsx";
import NavAdmin from "./components/NavAdmin.jsx";
import CreateModal from "./components/create/CreateModal.jsx";
import { useGeneralContext } from "../../context/GeneralContext";
import axios from 'axios';

import styles from  "./components/create/Index.module.css";

import APILINK from '../../components/link.js';
import { useNavigate } from 'react-router';
import ListDetallesProfesor from './list/ListDetallesProfesor.jsx';




function Profesores() {

  const [profesores, setProfesores] = useState([]);
  
  const [showModal, setShowModal] = useState(false);
  const [id, setId] = useState(null);
  const { getToken, cancan, verifyToken } = useGeneralContext();

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
  }, [page, cancan, verifyToken, nav, getToken]);

  const doFetch =(profesor) =>{
    setProfesores([...profesores, profesor])
}


const btndetalles = (id) => {
  setShowModal(true);
  setId(id);
};

const handleCloseModal = () => {
  setShowModal(false);
  setProfesores([]);
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


  return (
    <>

      <div className="page">
        <NavAdmin />

        <PanelContainerBG>


          <div>
            <table className={styles.CustomTable}>
              <thead>
                <tr>
                  <th>CI</th>
                  <th>Nombre</th>
                  <th>Apellido</th>
                  <th>Fecha de nacimiento</th>
                  <th>Direccion</th>
                  <th>Telefono</th>
                 
                </tr>
              </thead>
              <tbody > 

              {profesores && profesores.length > 0 && profesores.map(profe => (
            <tr  className={styles.tableRow} key={profe.id} onClick={() => btndetalles(profe.id)}>
                    <td>{profe.documento}</td>
                    <td>{profe.nombre}</td>
                    <td>{profe.apellido}</td>
                    <td>{(new Date(profe.nacimiento)).toLocaleDateString()}</td>
                 
                    <td>{profe.direccion}</td>
                    <td>{profe.telefono}</td>
                  

                  </tr>
                 
                ))}
              </tbody>
            </table>
           
            <ListDetallesProfesor showModal={showModal} setShowModal={setShowModal} id={id}  triggerState={(profesor)=>{setProfesores(profesor)}} page={page} />

  
            <div >

           
        
      </div>

             
     
             
                        
            <nav aria-label="Page navigation example">
              <ul className="pagination justify-content-end">
                <li className="page-item disabled">

              
                  <button className="btn page-item btn-sm" onClick={handlePrevClick} disabled={page === 0}>Anterior</button>
                </li>
                <li className="page-item">
                 
                <button className="btn page-item btn-sm" onClick={handleNextClick}  disabled={isLastPage} >
                          Siguiente
                        </button>
                </li>
              </ul>
            </nav>
 
          </div>

        </PanelContainerBG>
        <footer>
          <div className={styles.NButtonForSideA}>
            <CreateModal title="My Modal" onClose={() => setShow(false)}  show={show}
             triggerState={(profesor)=>{doFetch(profesor)}}>
            </CreateModal>
            
          </div>

     

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
          padding: 20px;
          text-align: right;
        }

           

            
            `}</style>
    </>
  )
}

export default Profesores