import React, { useEffect, useState } from "react";
import styles from './ListarColegios.module.css'
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import { Table } from "../../components/Table";
import ModalAgregarColegios from './AgregarColegios'
import axios from "axios";

import { useGeneralContext } from "../../context/GeneralContext";
import APILINK from "../../components/link";
import ModalVerColegios from './ModalVerColegios'
import { AiOutlineEye } from "react-icons/ai";
import Paginations from "../../components/Paginations"
function ListarColegios() {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()
  const [totalPage, setTotalPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [colegios, setColegios] = useState([]);
  const [datoIdColegio, setDatoIdColegio] = useState('');
  const [next, setNext]=useState(false);
  useEffect(() => {

    verifyToken()
    if (!cancan("Master")) {
      nav("/")
    } else {
      axios.get(`${APILINK}/api/ColegiosFull/page/${currentPage}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })

        .then(response => {
          setColegios(response.data.dataList); //Guarda los datos
          setTotalPage(response.data.totalPage);//Total de Paginas
          setCurrentPage(response.data.currentPage);//Actualiza la pagina en donde estan los datos
          setNext(response.data.next);
        })
        .catch(error => {
          console.error(error);
        });
    }
     // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cancan, verifyToken, nav, currentPage, getToken]);

 
  const doFetch = (colegio) => {
    setColegios([...colegios, colegio])
  }
  

  
  const [show, setShow] = useState(false);
  const [disabled, setDisabled] = useState(true);
  const handleShow = (id) => {
    setDatoIdColegio(id);
    console.log(datoIdColegio);
    setShow(true);
  }


  return (
    <>
      <div>

        <div className={styles.nombrePagina}>
          <div className={styles.divNombrePagina}>
            <button className={styles.buttonBack} onClick={() => { navigate('/') }}><BiArrowBack className={styles.arrowButton} /></button>
            <span className={styles.tituloPagina}>Colegios</span>
          </div>
        </div>
        <div className={styles.tablePrincipal} >
          <Table
            headers={["Numero", "Nombre", "Administrador", "Acciones"]}
            datas={colegios}
            parseToRow={(col, index) => {
              return (
                <tr key={index} >
                  <td>{index + 1}</td>
                  <td>{col?.nombre}</td>
                  <td>{col?.nombreAdministrador} {col?.apellido}</td>
                  <td><button className={styles.iconButton} onClick={() => handleShow(col?.id)}><AiOutlineEye /></button></td>

                </tr>
              )
            }}
          />
        
          <Paginations totalPage={totalPage} currentPage={currentPage} setCurrentPage={setCurrentPage} next={next} ></Paginations>
        </div>

        <ModalVerColegios idColegio={datoIdColegio} show={show} setShow={setShow} disabled={disabled} setDisabled={setDisabled}></ModalVerColegios>

        <ModalAgregarColegios triggerState={(colegio) => { doFetch(colegio) }}></ModalAgregarColegios>
      </div>
    </>)
}
export default ListarColegios