/* eslint-disable */
import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from '../../profesor/components/LayoutAdmin';
import CreateModal from '../create/CreateModalMaterias.jsx';
import { useGeneralContext } from "../../../context/GeneralContext.jsx";
import axios from 'axios';

import StyleComponentBreadcrumb from '../../../components/StyleComponentBreadcrumb';

import styles from '../create/Index.module.css';
import APILINK from '../../../components/link.js';
import { useNavigate } from 'react-router';

import Pagination from 'react-bootstrap/Pagination';

import Tabla from '../../../components/Tabla';

import CreateModalMaterias from '../create/CreateModalMaterias.jsx';

import ListDetallesMateria from './ListDetallesMateria';





function ListarMaTerias() {

  const [materias, setMaterias] = useState([]);
  const [ciclos, setCiclos] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [id, setId] = useState(null);
  const [data, setData] = useState({})
  const { getToken, cancan, verifyToken } = useGeneralContext();

  const [page, setPage] = useState(0);





  const nav = useNavigate()

  useEffect(() => {
    verifyToken()
    if (!cancan("Administrador de Colegio")) {
      nav("/")
    } else {
      //axios.get(`https://miapi.com/products?page=${page}&size=${size}`, {

      axios.get(`${APILINK}/api/Ciclo`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })

        .then(response => {
          setCiclos(response.data.dataList);


        })
        .catch(error => {
          console.error(error);
        });
    }
  }, [cancan, verifyToken, nav, getToken]);


  useEffect(() => {
    verifyToken()
    if (!cancan("Administrador de Colegio")) {
      nav("/")
    } else {
      //axios.get(`https://miapi.com/products?page=${page}&size=${size}`, {

      axios.get(`${APILINK}/api/Materia/page/${page}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })

        .then(response => {
          setMaterias(response.data.dataList);


        })
        .catch(error => {
          console.error(error);
        });
    }
  }, [page, cancan, verifyToken, nav, getToken]);



  const doFetch = (materia) => {
    setMaterias([...materias, materia])
  }


  const btndetalles = (data) => {
    setShowModal(true);
    setId(data.id);
    setData(data)
  };





  const modalOnHide = (bool) => {
    setShowModal(bool)
    setData({})
    setId(null)
  }


  const [show, setShow] = useState(false);


  return (
    <>

      <div className="page">
        <StyleComponentBreadcrumb nombre="Materias y Ciclos" />


        <PanelContainerBG>

          <div className={styles.tableContainer}>
            <div className={styles.container}>

              <Tabla
                datosTabla={{
                  tituloTabla: 'Lista de materias',
                  titulos: [
                    { titulo: 'Materias' },
                  ],
                  clickable: { action: btndetalles },
                  tableWidth: '50%',
                  filas: materias.map((materia) => ({
                    fila: materia,
                    datos: [
                      { dato: materia.nombre_Materia },
                    ],
                  })),
                }}
                selected={id ?? '-'}
              />

            </div>

            <div className={styles.container}>

              <Tabla
                datosTabla={{
                  tituloTabla: 'Lista de Ciclos',
                  titulos: [
                    { titulo: 'Ciclos' },
                  ],
                  clickable: { action: btndetalles },
                  tableWidth: '50%',
                  filas: materias.map((materia) => ({
                    fila: materia,
                    datos: [
                      { dato: materia.nombre_Materia },
                    ],
                  })),
                }}
                selected={id ?? '-'}
              />
            </div>
          </div>
        </PanelContainerBG>
        <footer>
          <div className={styles.NButtonForSideA}>
            <CreateModalMaterias title="My Modal" onClose={() => setShow(false)} show={show}
              triggerState={(materia) => { doFetch(materia) }}>
            </CreateModalMaterias>

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