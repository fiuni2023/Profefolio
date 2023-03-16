import React, { useEffect, useState } from "react";
import Table from 'react-bootstrap/Table';
import styles from './ListarColegios.module.css'
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import { BiPencil } from "react-icons/bi"
import { BiTrash } from "react-icons/bi"
import { BiInfoCircle } from "react-icons/bi"
import ModalAgregarColegios from './AgregarColegios'
import axios from "axios";
import Pagination from 'react-bootstrap/Pagination';
import { useGeneralContext } from "../../context/GeneralContext";
import APILINK from "../../components/link";
const ListarColegios = () => {

  const { getToken, verifyToken, cancan} = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()
  const [totalPage, setTotalPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [colegios, setColegios] = useState([]);
  
  useEffect(() => {

      verifyToken()
      if(!cancan("Master")){
        nav("/")
      }else{
        var config = {
          method: 'get',
          url: `${APILINK}/api/ColegiosFull/page/${currentPage}`,
          headers: {
            'Authorization': `Bearer ${getToken()}`
          }
        };
      axios(config)
      .then(function (response) {
        setColegios(response.data.dataList); //Guarda los datos
        setTotalPage(response.data.totalPage);//Total de Paginas
        setCurrentPage(response.data.currentPage)//Actualiza la pagina en donde estan los datos

      })
      .catch(function (error) {
        console.log(error);
      });
    }
  }, [cancan, verifyToken, nav, currentPage, getToken])

const recargaDatos=(id)=>{
 var config = {
    method: 'get',
    url: `${APILINK}/api/ColegiosFull/page/${id}`,
    headers: {
      'Authorization': `Bearer ${getToken()}`
    }
  };
    axios(config)
      .then(function (response) {
       
        setColegios(response.data.dataList); //Guarda los datos
        setTotalPage(response.data.totalPage);//Total de Paginas
        setCurrentPage(response.data.currentPage)//Actualiza la pagina en donde estan los datos

      })
      .catch(function (error) {
        console.log(error);
      });
     
}
  let items = [];

   for (let number = 0; number < totalPage; number++) {
    items.push(
      <Pagination.Item key={number} >
        {number}
      </Pagination.Item>,
    );
  }

  const handleCurrentPage=(idPage)=>{
    setCurrentPage(idPage);
    recargaDatos(idPage);
    
}
  //hola
  
  return (
    <>
      <div>
        <div className={styles.nombrePagina}>
          <button className={styles.buttonBack} onClick={() => { navigate('/administrador') }}><BiArrowBack /></button>
          <span>Colegios</span>
        </div>
        <div className={styles.tablePrincipal} >
          <Table bordered >
            <thead>
              <tr>
              <th id={styles.tableBorder}>Numero</th>
                <th id={styles.tableBorder}>Nombre</th>
                <th id={styles.tableBorder}>Administrador</th>
                
                <th className={styles.actionsTh} id={styles.tableBorder}>Acciones</th>
              </tr>
            </thead>
            <tbody >
              {colegios.map((colegio, index) => (
                <tr key={colegio.id}>
                  <td id={styles.tableBorder} className="numero-td" >{index+1}</td>
                  <td id={styles.tableBorder} >{colegio.nombre}</td>
                  <td id={styles.tableBorder}>{colegio.nombreAdministrador} {colegio.apellido}</td>
                  <td className={styles.actionsTd} id={styles.tableBorder}><button className={styles.informationButtons}><BiTrash /></button> <button className={styles.informationButtons}><BiPencil /> </button> <button className={styles.informationButtons}><BiInfoCircle /></button></td>
                </tr>
                
              ))}
              
               
            </tbody>
          </Table>
        </div>
        <div className={styles.paginacion}>
          
            <Pagination onClick={e => handleCurrentPage(e.target.text)}  size="sm">{items} </Pagination>
          
        </div>
        <ModalAgregarColegios setColegios={()=>{recargaDatos(currentPage)}}></ModalAgregarColegios>
      </div>
    </>)
}
export default ListarColegios