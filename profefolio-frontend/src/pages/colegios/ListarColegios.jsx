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
const ListarColegios = (triggerState = () => {}) => {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()
  const [totalPage, setTotalPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [colegios, setColegios] = useState([]);
  const [trigger, setTrigger]=useState([]);

  useEffect(() => {

    verifyToken()
    if (!cancan("Master")) {
      nav("/")
    } else {
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
  }, [cancan, verifyToken, nav, currentPage, getToken, trigger])
  function handleAction(event) {
    setTrigger(colegios);
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

  return (
    <>
      <div>
        <div className={styles.nombrePagina}>
          <button className={styles.buttonBack} onClick={() => { navigate('/') }}><BiArrowBack /></button>
          <span>Colegios</span>
        </div>
        <div className={styles.tablePrincipal} >
          <Table
            headers={["Numero", "Nombre", "Administrador","Acciones"]}
            datas={colegios}
            parseToRow={(col, index) => {
              return (
                <tr key={index}>
                  <td>{index + 1}</td>
                  <td>{col?.nombre}</td>
                  <td>{col?.nombreAdministrador} {col?.apellido}</td>

                  <td></td>
                </tr>
              )
            }}
          />
          <Pagination onClick={e => handleCurrentPage(e.target.text)} size="sm">{items} </Pagination>
        </div>



        <ModalAgregarColegios onAction={handleAction} ></ModalAgregarColegios>
      </div>
    </>)
}
export default ListarColegios