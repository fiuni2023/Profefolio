/* eslint-disable */
import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from '../../profesor/components/LayoutAdmin';
import NavMaterias from '../componentes/NavMaterias.jsx';
import CreateModal from '../create/CreateModalMaterias.jsx';
import { useGeneralContext } from "../../../context/GeneralContext.jsx";
import axios from 'axios';

import styles from  '../create/Index.module.css';
import APILINK from '../../../components/link.js';
import { useNavigate } from 'react-router';

import Pagination from 'react-bootstrap/Pagination';





function ListarMaTerias() {

  const [materias, setMaterias] = useState([]);
  
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
  
      axios.get(`${APILINK}/api/Materia/page/${page}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
  
        .then(response => {
          setMaterias(response.data.dataList);
          setTotalPage(response.data.totalPage);//Total de Paginas
          setCurrentPage(response.data.currentPage);
          setNext(response.data.next);
      
  

        })
        .catch(error => {
          console.error(error);
        });
    }
  }, [page, cancan, verifyToken, nav, getToken]);

  const doFetch =(materia) =>{
    setMaterias([...materias, materia])
}


const btndetalles = (id) => {
  setShowModal(true);
  setId(id);
};

const handleCloseModal = () => {
  setShowModal(false);
  setMaterias([]);
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
        <NavMaterias />

        <PanelContainerBG>


          <div className={styles.container}>
            <table className={styles.CustomTable}>
              <thead>
                <tr>
                  <th>Nombre de la Materia</th>
                 
                </tr>
              </thead>
              <tbody > 

          {materias && materias.length > 0 && materias.map(materia => (
            <tr  className={styles.tableRow} key={materia.id}>
                    <td>{materia.nombre_Materia}</td>
                      </tr>

                        ))}
                  

                
                 
              
              </tbody>
            </table>
        
  
           
            <div >

            <div>
  </div>

           
 
      </div>

             
     
             
          </div>

          
          <div className={styles.container}>

          <Pagination className="justify-content-end">
      <Pagination.Prev onClick={handlePrevClick} disabled={page === 0} />
      <Pagination.Next onClick={handleNextClick} disabled={isLastPage} />
    </Pagination>

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

        .btn-smaller {
          font-size: 0.8rem;
        }

           

            
            `}</style>
    </>
  )
}

export default ListarMaTerias