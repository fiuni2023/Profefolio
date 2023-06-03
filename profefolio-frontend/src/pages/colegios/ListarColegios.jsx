/* eslint-disable */
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
import Spinner from "../../components/componentsStyles/SyledSpinner";
import Text from "../../components/componentsStyles/StyledText";

function ListarColegios() {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()
  const navigate = useNavigate()

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
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
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        verifyToken();
        if (!cancan("Master")) {
          nav("/");
        } else {
          const response = await axios.get(`${APILINK}/api/ColegiosFull/page/${currentPage}`, {
            headers: {
              Authorization: `Bearer ${getToken()}`,
            }
          })

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

          setCurrentPage(response.data.currentPage);
          setNext(response.data.next);
          setTotalPage(response.data.totalPage);

        }

        const config = {
          method: 'get',
          url: `${APILINK}/api/administrador`,
          headers: {
            Authorization: `Bearer ${getToken()}`,
          },
        };

        const adminResponse = await axios(config);
        setAdministrators(adminResponse.data);

        setLoading(false);
      } catch (error) {
        setLoading(false);
        setError(error);
        toast.error(error);
        console.error(error);
      }
    };

    fetchData();
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
    <div>
      <div className={styles.nombrePagina}>
        <div className={styles.divNombrePagina}>
          <button className={styles.buttonBack} onClick={() => { navigate('/') }}><BiArrowBack className={styles.arrowButton} /></button>
          <span className={styles.tituloPagina}>Colegios</span>
        </div>
      </div>
      {loading ? <Spinner height={'calc(100vh - 80px)'} />
        : error ? <Text>Lamentamos esto, ha ocurrido un error al obtener los datos.</Text>
          :
          <>
            <div className={styles.tablePrincipal} >
              <Tabla datosTabla={datosTabla} selected={selectedId ?? "-"} />
              <Paginations totalPage={totalPage} currentPage={currentPage} setCurrentPage={setCurrentPage} next={next} ></Paginations>
            </div>
            <AddButton onClick={() => { setShow(true) }}>
              <AiOutlinePlus size={"35px"} />
            </AddButton>
            <ModalColegio show={show} onHide={handleHide} administrators={administrators} selected_data={datoColegio} fetchFunc={doFetch} />
          </>}
    </div>
  )
}
export default ListarColegios