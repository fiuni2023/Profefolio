/* eslint-disable */
import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from '../../profesor/components/LayoutAdmin';
import { useGeneralContext } from "../../../context/GeneralContext.jsx";
import axios from 'axios';
import TextButton from '../../../components/TextButton';
import StyleComponentBreadcrumb from '../../../components/StyleComponentBreadcrumb';
import { toast } from 'react-hot-toast';
import styles from '../create/Index.module.css';
import APILINK from '../../../components/link.js';
import { useNavigate } from 'react-router';
import Tabla from '../../../components/Tabla';

function ListarMaTerias() {

  const [materias, setMaterias] = useState([]);
  const [ciclos, setCiclos] = useState([]);
  const [id, setId] = useState(null);
  const [data, setData] = useState({})
  const { getToken, cancan, verifyToken } = useGeneralContext();
  const [page, setPage] = useState(0);
  const [nombreCiclo, setNombreCiclo] = useState(null);
  const [nombre_Materia, setNombreMateria] = useState('');



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

  const handleSubmitMateria = () => {
    console.log(nombre_Materia)
    if (nombre_Materia === "") toast.error("revisa los datos, los campos deben ser completados")
    else {
      axios.post(`${APILINK}/api/Materia`, {
        nombre_Materia,

      }, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          toast.success("Guardado exitoso");
          setNombreMateria("")
         
          console.log(materias);

        })
        .catch(error => {
          if (typeof (error.response.data) === "string" ? true : false) {
            toast.error(error.response.data)
          } else {
            toast.error(error.response.data?.errors.Password ? error.response.data?.errors.Password[0] : error.response.data?.errors.Email[0])
          }
        });

    }
    const handleSubmitCiclo = () => {
      console.log(nombreCiclo)
      if (nombreCiclo === "") toast.error("revisa los datos, los campos deben ser completados")
      else {
        axios.post(`${APILINK}/api/Clase`, {
          nombreCiclo,

        }, {
          headers: {
            Authorization: `Bearer ${getToken()}`,
          }
        })
          .then(response => {
            toast.success("Guardado exitoso");
            setNombreCiclo("")
            console.log(ciclos)

          })
          .catch(error => {
            if (typeof (error.response.data) === "string" ? true : false) {
              toast.error(error.response.data)
            } else {
              toast.error(error.response.data?.errors.Password ? error.response.data?.errors.Password[0] : error.response.data?.errors.Email[0])
            }
          });

      }
    }

  };

  const btndetalles = (data) => {
    setShowModal(true);
    setId(data.id);
    setData(data)
  };

  const handleNombreMateria = (event) => {
    setNombreMateria(event.target.value);

  }
  const handleNombreCiclo = (event) => {
    setNombreCiclo(event.target.value);

  }
  const doFetch = (materia) => {
    setColegios([...materias, materia])
  }

  return (
    <>

      <div className="page">
        <StyleComponentBreadcrumb nombre="Materias" />


        <PanelContainerBG>

          <div className={styles.tableContainer}>
            <div className={styles.container} id={styles.containerMaterias} >
              <div id={styles.materiasTable}>
                <Tabla
                  datosTabla={{
                    tituloTabla: 'Lista de Materias',
                    titulos: [
                      { titulo: 'Materias' },
                    ],
                    clickable: { action: btndetalles },
                    tableWidth: '100%',
                    filas: materias.map((materia) => ({
                      fila: materia,
                      datos: [
                        {dato: materia.id},
                        { dato: materia.nombre_Materia },
                      ],
                    })),
                  }}
                  selected={id ?? '-'}
                />
              </div>
              <div className={styles.divAdd}>
                <label>Agregar Materia</label>
                <br />
                <input className={styles.inputAdd} placeholder='Nombre de la materia' onChange={(event) => handleNombreMateria(event)} ></input>
                <br />
                <TextButton enabled={true} buttonType='save' onClick={() => handleSubmitMateria()} />
              </div>
            </div>

            <div className={styles.container} id={styles.containerCiclos} >
              <div id={styles.ciclosTable}>
                <Tabla
                  datosTabla={{
                    tituloTabla: 'Lista de Ciclos',
                    titulos: [
                      { titulo: 'Ciclos' },
                    ],
                    clickable: { action: btndetalles },
                    tableWidth: '100%',
                    filas: materias.map((ciclo) => ({
                      fila: ciclo,
                      datos: [
                        {dato: ciclo.id},
                        { dato: ciclo.nombre_Materia },
                      ],
                    })),
                  }}
                  selected={id ?? '-'}
                />
              </div>
              <div>
                <div className={styles.divAddCiclos}>
                  <label>Agregar Ciclo</label>
                  <br />
                  <input className={styles.inputAdd} placeholder='Nombre del Ciclo' onChange={(event) => handleNombreCiclo(event)} ></input>
                  <TextButton enabled={true} buttonType='save' onClick={() => handleSubmitCiclo()} className={styles.buttonGuardar} />
                </div>
              </div>
            </div>

          </div>
        </PanelContainerBG>
        <footer>

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