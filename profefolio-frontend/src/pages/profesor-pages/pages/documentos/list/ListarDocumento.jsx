/* eslint-disable */
import React, { useState, useEffect, useFetch } from 'react';
import {PanelContainerBG} from '../../../../profesor/components/LayoutAdmin';
import { useGeneralContext } from "../../../../../context/GeneralContext";
import axios from 'axios';
import TextButton from '../../../../../components/TextButton';
import BackButton from '../../../components/BackButton';

import styled from "styled-components";
import StyleComponentBreadcrumb from '../../../../../components/StyleComponentBreadcrumb';
import { toast } from 'react-hot-toast';
import styles from '../create/Index.module.css';

import APILINK from '../../../../../components/link';
import { useNavigate } from 'react-router';

import Tabla from '../../../../../components/Tabla';

import ModalConfirmacion from '../modal/ModalConfirmarDelete.jsx';

import IconButton from '../../../../../components/IconButton';
import { Row, Col } from "react-bootstrap";

const FlexDiv = styled.div`
    display: flex;
    align-items: center;
    width: 100%;
    gap: 10px;
`

const GridDiv = styled.div`
    margin-top: 1%;
    display: grid;
    grid-template-columns: 23% 23% 23% 23%;
    gap: 10px;
`

const GapDiv = styled.div`
    height: 30px;
    `
function ListarDocumentos() {

  const [materias, setMaterias] = useState([]);
  const [ciclos, setCiclos] = useState([]);
  const [id, setId] = useState(null);
  const [data, setData] = useState({})
  const [dataCiclo, setDataCiclo] = useState({})
  const { getToken, cancan, verifyToken } = useGeneralContext();
  const [page, setPage] = useState(1);
  const [nombreCiclo, setNombreCiclo] = useState(null);
  const [nombre_Materia, setNombreMateria] = useState('');
  const [detallesCiclo, setDetallesCiclo] = useState(false);
  const [nombreNuevoCiclo, setNombreNuevoCiclo] = useState(null);
  const [detallesMateria, setDetallesMateria] = useState(false);
  const [nombreNuevoMateria, setNombreNuevoMateria] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [nombre_Materia_delete, setNombreMateriaDelete] = useState('');
  const [showModalCiclo, setShowModalCiclo] = useState(false);
  const [nombre_Ciclo_delete, setNombreCicloDelete] = useState('');
  const nav = useNavigate()



  const getMaterias = () => {
    verifyToken()
    if (!cancan("Profesor")) {
      nav("/")
    } else {
      //https://localhost:7063/api/Materia

      axios.get(`${APILINK}/api/Materia`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })

        .then(response => {
          setMaterias(response.data);
        })
        .catch(error => {
          toast.error(error);
        });
    }

  }

  useEffect(() => {

    getMaterias();



  }, [page, cancan, verifyToken, nav, getToken]);

  const handleSubmitMateria = () => {

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
          setNombreMateria('')
          getMaterias();
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


  const btndetallesMateria = (data) => {
    setId(data.id);
    setData(data);
    setDetallesMateria(true);
  };

  const handleNombreMateria = (event) => {
    setNombreMateria(event.target.value);

  }
 

  const handleCancelMaterias = () => {

    setDetallesMateria(false);
  }

  const handleNombreNuevoMateria = (event) => {
    setNombreNuevoMateria(event.target.value);
  }
 
  const handleEditMateria = () => {

    if (nombreNuevoMateria === null || nombreNuevoCiclo === "") {
      toast.error("Favor rellenar el campo correctamente")
    }
    else {
      axios.put(`${APILINK}/api/Materia/${data.id}`, {
        "nombre_Materia": nombreNuevoMateria,

      }, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          toast.success("Editado");
          setNombreNuevoMateria("")
          setDetallesMateria(false);
          getMaterias();
        })
        .catch(error => {
          if (typeof (error.response.data) === "string" ? true : false) {
            toast.error(error.response.data)
          } else {
            toast.error(error.response.data?.errors.Email[0])
          }
        });

    }

  }

  //mi codigo

  const handleShowModal = (event, id, nombre) => {
    setId(id);
    setNombreMateriaDelete(nombre);
    setShowModal(true);
    event.stopPropagation();
  };


  const handleDelete = async (event) => {
    try {
      // lógica para eliminar el elemento
      // `https://localhost:7063/api/Materias/${id}`
      await axios.delete(`${APILINK}/api/Materia/${id}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      });

      toast.success("Eliminado exitoso");
      getMaterias();
      setShowModal(false);
    } catch (error) {
      if (error.response && error.response.status === 400) {
        toast.error("No se puede eliminar una materia asignada a una clase");
      }
    }
    setShowModal(false); // ocultar el modal después de eliminar el elemento
  };

  return (
    <>
      <div className="page">

      <StyleComponentBreadcrumb nombre={`Documentos`} />
        <PanelContainerBG>

          <div className={styles.tableContainer}>
            <div className={styles.container} id={styles.containerMaterias} >
              <div id={styles.materiasTable}>
                <Tabla
                  datosTabla={{
                    tituloTabla: 'Lista de Documentos',
                    titulos: [
                      { titulo: 'Documentos' },
                    ],
                   // clickable: { action: btndetallesMateria },
                    tableWidth: '100%',
                    filas: materias.map((materia) => ({
                      fila: materia,
                      datos: [
                        { dato: materia.id },
                        { dato: materia.nombre_Materia },
                        {
                          dato: <IconButton enabled={true} buttonType='close' onClick={(event) => handleShowModal(event, materia.id, materia.nombre_Materia)}> X </IconButton>
                        },

                      ],
                    })),
                  }}
                  selected={id ?? '-'}
                />
              </div>
              <ModalConfirmacion
                show={showModal}
                onHide={() => setShowModal(false)}
                onConfirm={handleDelete}
                materia={nombre_Materia_delete}
              />

              <div>
                {detallesMateria
                  ? <div className={styles.divEditMateria}>
                    <label className={styles.label}><strong>Editar Materia</strong></label>
                    <br />
                    <label className={styles.label}>Nombre Actual</label>
                    <br />
                    <div className={styles.inputAdd}>{data.nombre_Materia}</div>

                    <label className={styles.label}>Nombre Nuevo</label>
                    <br />
                    <input className={styles.inputAdd} placeholder='Nombre de la Materia' onChange={(event) => handleNombreNuevoMateria(event)} id='input-Materia' ></input>
                    <div className={styles.buttonsMateria}>
                      <TextButton enabled={true} buttonType='cancel' onClick={() => handleCancelMaterias()} />
                      <TextButton enabled={true} buttonType='save' onClick={() => handleEditMateria()} />
                    </div>
                  </div>
                  : <div className={styles.divAdd}>
                    <label className={styles.label}><strong>Agregar Materia </strong></label>
                    <br />
                    <input type='text' className={styles.inputAdd} placeholder='Nombre de la materia' onChange={(event) => handleNombreMateria(event)} value={nombre_Materia || ''} ></input>
                    <br />
                    <div className={styles.buttonGuardarMateria}>
                      <TextButton enabled={true} buttonType='save' onClick={() => handleSubmitMateria()} />

                    </div>
                  </div>
                }
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

export default ListarDocumentos