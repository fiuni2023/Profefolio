import React, { useEffect, useState } from "react";
import styles from './ListarColegios.module.css';
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import axios from "axios";
import Paginations from "../../components/Paginations"
import { useGeneralContext } from '../../context/GeneralContext'

import APILINK from "../../components/link";
import { toast } from "react-hot-toast";
import Tabla from "../../components/Tabla";
import ModalColegio from "./ModalColegios_";
import { AddButton } from "../alumnos/styles/Styles";
import { AiOutlinePlus } from "react-icons/ai";

function ListarColegios() {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()

  const [currentPage, setCurrentPage] = useState(0);
  const [datoColegio, setDatoColegio] = useState('');
  const [next, setNext] = useState(false);
  const [totalPage, setTotalPage] = useState(0);

  const [fetch_data, setFetchData] = useState([]);

  const [administrators, setAdministrators] = useState([])

  const [datosTabla, setDatosTabla] = useState({
    tituloTabla: "studentsList",
    titulos: [{ titulo: "Numero" }, { titulo: "Nombre" }, { titulo: "Administrador" }]
  });
  const [selectedId, setSelectedId] = useState(null)



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
          setDatosTabla({
            ...datosTabla, clickable: { action: handleShow },
            filas: response.data.dataList.map((dato) => {
              return {
                fila: dato,
                datos: [
                  { dato: dato?.id ?? "" },
                  { dato: dato?.nombre ?? "" },
                  { dato: dato?.idAdmin ? `${dato?.nombreAdministrador} ${dato?.apellido}` : "Sin Administrador" }]
              }
            })

          })

          setCurrentPage(response.data.currentPage);//Actualiza la pagina en donde estan los datos
          setNext(response.data.next);
          setTotalPage(response.data.totalPage)

        })
        .catch(error => {
          toast.error(error);
          console.error(error);
        });


      let config = {
        method: 'get',
        url: `${APILINK}/api/administrador`,
        headers: {
          'Authorization': `Bearer ${getToken()}`
        }
      };
      axios(config)
        .then(function (response) {
          setAdministrators(response.data);

        })
        .catch(function (error) {
          console.log(error);
        });

    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentPage, fetch_data]);


  const [show, setShow] = useState(false);

  const handleShow = (dato) => {
    setDatoColegio(dato)
    setSelectedId(dato?.id)
    setShow(true);
  }

  const handleHide = () => {
    setDatoColegio(null)
    setShow(false)
  }

  const doFetch = () => { setFetchData((before) => [before]) }


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
          <Tabla datosTabla={datosTabla} selected={selectedId ?? "-"} />
          <Paginations totalPage={totalPage} currentPage={currentPage} setCurrentPage={setCurrentPage} next={next} ></Paginations>

        </div>
        <AddButton onClick={() => { setShow(true) }}>
          <AiOutlinePlus size={"35px"} />
        </AddButton>
        {/* <ModalVerColegios datoColegio={datoColegio} onClose={()=>{setDatoColegio(null)}} show={show} setShow={setShow} disabled={disabled} setDisabled={setDisabled} triggerState={() => { setFetchData((before)=>[before]) }} page={currentPage}></ModalVerColegios>
        <ModalAgregarColegios triggerState={() => { setFetchData((before)=>[before]) }}  ></ModalAgregarColegios> */}

        <ModalColegio show={show} onHide={handleHide} administrators={administrators} selected_data={datoColegio} fetchFunc={doFetch} />
      </div>
    </>)
}
export default ListarColegios