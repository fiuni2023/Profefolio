import React, { useEffect, useState } from "react";
import styles from './ListarColegios.module.css';
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import ModalAgregarColegios from './AgregarColegios'
import axios from "axios";
import Paginations from "../../components/Paginations"
import { useGeneralContext} from '../../context/GeneralContext'

import APILINK from "../../components/link";
import ModalVerColegios from './ModalVerColegios'
import { toast } from "react-hot-toast";
import Tabla from "../../components/Tabla";

function ListarColegios() {

  const { getToken, verifyToken, cancan } = useGeneralContext()

  const nav = useNavigate()

  const navigate = useNavigate()

  const [currentPage, setCurrentPage] = useState(0);
  const [colegios, setColegios] = useState([]);
  const [datoIdColegio, setDatoIdColegio] = useState('');
  const [next, setNext]=useState(false);
  const [totalPage, setTotalPage]=useState(0);

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
          console.log(response)
          setColegios(response.data.dataList); //Guarda los datos
          setDatosTabla({
            ...datosTabla, clickable: { action: handleShow },
            filas: response.data.dataList.map((dato) => {
                return {
                    fila: dato,
                    datos: [
                        { dato: dato?.id ?? "" },
                        { dato: dato?.nombre ?? "" },
                        { dato: dato?.idAdmin? `${dato?.nombreAdministrador} ${dato?.apellido}` : "Sin Administrador" }]
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
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cancan, verifyToken, nav, currentPage, getToken]);
 
  const doFetch = (colegio) => {
    setColegios([...colegios, colegio])
  }

  const [show, setShow] = useState(false);
  const [disabled, setDisabled] = useState(false);

  const handleShow = (dato) => {
    console.log(dato)
    setDatoIdColegio(dato?.id);
    setSelectedId(dato?.id)
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
          <Tabla datosTabla={datosTabla} selected={selectedId ?? "-"} />
          <Paginations totalPage={totalPage} currentPage={currentPage} setCurrentPage={setCurrentPage} next={next} ></Paginations>

        </div>

        <ModalVerColegios datoIdColegio={datoIdColegio} onClose={()=>{setDatoIdColegio(null)}} show={show} setShow={setShow} disabled={disabled} setDisabled={setDisabled} triggerState={(colegio) => { setColegios(colegio) }} page={currentPage}></ModalVerColegios>

        <ModalAgregarColegios triggerState={(colegio) => { doFetch(colegio) }}  ></ModalAgregarColegios>
      </div>
    </>)
}
export default ListarColegios