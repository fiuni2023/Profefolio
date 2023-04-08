import React, { useEffect, useState } from "react";
import styles from './ListarColegios.module.css'
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import { Table } from "../../components/Table";
import ModalAgregarColegios from './AgregarColegios'
import axios from "axios";
import Pagination from 'react-bootstrap/Pagination';
import { useGeneralContext } from "../../context/GeneralContext";
import APILINK from "../../components/link";
import ModalVerColegios from './ModalVerColegios'
import ModalPrueba from "./ModalPrueba";

function ListarColegios() {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()
  const [totalPage, setTotalPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [colegios, setColegios] = useState([]);
  const [datoIdColegio, setDatoIdColegio] = useState(null);

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
  let items = [];

  for (let number = 0; number < totalPage; number++) {
    items.push(
      <Pagination.Item key={number} >
        {number}
      </Pagination.Item>,
    );
  }

  const handleCurrentPage = (idPage) => {
    setCurrentPage(idPage);


  }
  const [show, setShow] = useState(false);
  const [disabled, setDisabled] = useState(true);
  const handleShow = (id) => {
    setShow(true);
    setDatoIdColegio(id); 
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
            headers={["Numero", "Nombre", "Administrador"]}
            datas={colegios}
            parseToRow={(col, index) => {
              return (
                <tr key={index} onClick={() => handleShow(col.id)}>
                  <td>{index + 1}</td>
                  <td>{col?.nombre}</td>
                  <td>{col?.nombreAdministrador} {col?.apellido}</td>
                 
                </tr>
              )
            }}
          />
          <Pagination onClick={e => handleCurrentPage(e.target.text)} size="sm">{items} </Pagination>
        </div>

        <ModalVerColegios datoIdColegio={datoIdColegio} show={show} setShow={setShow} disabled={disabled} setDisabled={setDisabled} triggerState={(colegio)=>{setColegios(colegio)}}></ModalVerColegios>

        <ModalAgregarColegios triggerState={(colegio) => { doFetch(colegio) }}></ModalAgregarColegios>
      </div>
    </>)
}
export default ListarColegios