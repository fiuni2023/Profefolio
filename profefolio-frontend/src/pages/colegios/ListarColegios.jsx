import React, { useEffect, useState } from "react";
import styles from './ListarColegios.module.css';
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import { Table } from "../../components/Table";
import ModalAgregarColegios from './AgregarColegios'
import axios from "axios";
import Pagination from 'react-bootstrap/Pagination';
import { useGeneralContext } from "../../context/GeneralContext";
import APILINK from "../../components/link";
import ModalVerColegios from './ModalVerColegios'
import { AiOutlineEye } from "react-icons/ai";
import ModalColegios from "./ModalColegios";
import { BsFillPlusCircleFill } from "react-icons/bs"
import Modal from "../../components/Modal";

function ListarColegios() {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()
  const [totalPage, setTotalPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [colegios, setColegios] = useState([]);
  const [datoIdColegio, setDatoIdColegio] = useState('');

  const [enabled, setEnabled] = useState(true);


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
  const [disabled, setDisabled] = useState(false);
  const [tituloModal, setTituloModal] = useState("AgregarColegio")
  const handleShow = (id) => {
    setDatoIdColegio(id);
    setShow(true);
  }

  const handleChangeTituloModal = (titulo) => {
    setTituloModal(titulo); 
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
          <Pagination onClick={e => handleCurrentPage(e.target.text)} size="sm">{items} </Pagination>
        </div>

       
        <ModalColegios tituloModal={tituloModal} isOpen={show} disabled={disabled}></ModalColegios>  

      </div>
    </>)
}
export default ListarColegios