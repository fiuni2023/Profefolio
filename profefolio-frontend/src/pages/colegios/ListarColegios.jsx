import React, { useEffect, useState } from "react";
import styles from './ListarColegios.module.css';
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import { Table } from "../../components/Table";
import ModalAgregarColegios from './AgregarColegios'
import axios from "axios";
import Pagination from 'react-bootstrap/Pagination';
import { useGeneralContext } from '../../context/GeneralContext'
import APILINK from "../../components/link";
import ModalVerColegios from './ModalVerColegios'

import { BsFillPlusCircleFill } from "react-icons/bs"


function ListarColegios() {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()

  const [currentPage, setCurrentPage] = useState(0);
  const [colegios, setColegios] = useState([]);
  const [datoIdColegio, setDatoIdColegio] = useState(null);
  const [next, setNext] = useState(true)


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
          setCurrentPage(response.data.currentPage);//Actualiza la pagina en donde estan los datos
          setNext(response.data.next);
          console.log(response.data);
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
  const getPages = () => {
    return (
      <>
        <Pagination.Prev disabled={currentPage <= 0} onClick={() => {
          setCurrentPage(currentPage - 1)
        }} />
        <Pagination.Item disabled >{currentPage + 1}</Pagination.Item>
        <Pagination.Next disabled={!next} onClick={() => {
          setCurrentPage(currentPage + 1)
        }} />
      </>
    )
  }


  const [show, setShow] = useState(false);
  const [disabled, setDisabled] = useState(false);
  const handleShow = (id) => {
    setDatoIdColegio(id);

    setShow(true);
  }


  const openNew = () => {
    setShow(!show);
  }


  return (
    <>
      <button className={styles.buttonAgregar} onClick={openNew}><BsFillPlusCircleFill className={styles.iconoAgregar} /></button>
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
          <Pagination size="sm mt-3">
            {getPages()}
          </Pagination>
        </div>

        <ModalVerColegios datoIdColegio={datoIdColegio} show={show} setShow={setShow} disabled={disabled} setDisabled={setDisabled} triggerState={(colegio) => { setColegios(colegio) }} page={currentPage}></ModalVerColegios>

        <ModalAgregarColegios triggerState={(colegio) => { doFetch(colegio) }}  ></ModalAgregarColegios>
      </div>
    </>)
}
export default ListarColegios