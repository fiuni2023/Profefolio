/* eslint-disable */
import React, { useState, useEffect, useFetch } from 'react';
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
import ModalConfirmacion from '../modal/ModalConfirmarDelete.jsx';
import IconButton from '../../../components/IconButton';
import Spinner from '../../../components/componentsStyles/SyledSpinner';

function ListarMaTerias() {

  const [materias, setMaterias] = useState([]);
  const [ciclos, setCiclos] = useState([]);
  const [id, setId] = useState(null);
  const [data, setData] = useState({})
  const [dataCiclo, setDataCiclo] = useState({})
  const { getToken, cancan, verifyToken } = useGeneralContext();
  const [page, setPage] = useState(0);
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
  const nav = useNavigate();
  const [loadingMaterias, setLoadingMaterias] = useState(true); 
  const [loadingCiclos, setLoadingCiclos] = useState(true); 

  const getCiclos = () => {
    if (!cancan("Administrador de Colegio")) {
      nav("/")
    } else {
      let config = {
        method: 'get',
        maxBodyLength: Infinity,
        url: `${APILINK}/api/Ciclo`,
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${getToken()}`,
        },

      };

      axios.request(config)
        .then((response) => {

          setCiclos(response.data);
          setLoadingCiclos(false);

        })
        .catch((error) => {
          setLoadingCiclos(false);
          toast.error(error)
        });
    }


  }

  const getMaterias = () => {
    verifyToken()
    if (!cancan("Administrador de Colegio")) {
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
          setLoadingMaterias(false);
        })
        .catch(error => {
          setLoadingMaterias(false);
          toast.error(error);
        });
    }

  }

  useEffect(() => {

    getMaterias();
    getCiclos();


  }, [page, cancan, verifyToken, nav, getToken]);

  const handleSubmitMateria = () => {

    if (nombre_Materia === "" || nombre_Materia === null) toast.error("revisa los datos, los campos deben ser completados")
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

  const handleSubmitCiclo = () => {
    if (nombreCiclo === "" || nombreCiclo === null ) toast.error("revisa los datos, los campos deben ser completados")
    else {
      axios.post(`${APILINK}/api/Ciclo`, {
        "nombre": nombreCiclo,

      }, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          toast.success("Guardado exitoso");
          setNombreCiclo('')
          getCiclos();


        })
        .catch(error => {

          toast.error(error.response.data)


        });

    }
  }

  const btndetallesCiclo = (data) => {
    setId(dataCiclo.id);
    setDataCiclo(data);
    setDetallesCiclo(true);
  };
  const btndetallesMateria = (data) => {
    setId(data.id);
    setData(data);
    setDetallesMateria(true);
  };

  const handleNombreMateria = (event) => {
    setNombreMateria(event.target.value);

  }
  const handleNombreCiclo = (event) => {
    setNombreCiclo(event.target.value);

  }

  const handleCancel = () => {
    setDetallesCiclo(false);

  }
  const handleCancelMaterias = () => {

    setDetallesMateria(false);
  }
  const handleNombreNuevoCiclo = (event) => {
    setNombreNuevoCiclo(event.target.value);

  }
  const handleNombreNuevoMateria = (event) => {
    setNombreNuevoMateria(event.target.value);
  }
  const handleEditCiclo = () => {
    if (nombreNuevoCiclo === null || nombreNuevoCiclo === "") {
      toast.error("Favor rellenar el campo correctamente")
    }
    else {
      axios.put(`${APILINK}/api/Ciclo/${dataCiclo.id}`, {
        "nombre": nombreNuevoCiclo,

      }, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })
        .then(response => {
          toast.success("Editado");
          setNombreNuevoCiclo("")
          setDetallesCiclo(false);
          getCiclos();
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

  const handleShowModalCiclo = (event, id, nombre) => {
    setId(id);
    setNombreCicloDelete(nombre);
    setShowModalCiclo(true);
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

  const handleDeleteCiclo = async (event) => {
    try {
      // lógica para eliminar el elemento
      await axios.delete(`${APILINK}/api/Ciclo/${id}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      });
      toast.success("Eliminado exitoso");
      getCiclos();
      setShowModalCiclo(false);
    } catch (error) {
      if (error.response && error.response.status === 400) {
        toast.error("No se puede eliminar el ciclo");
      }
    }
    setShowModalCiclo(false); // ocultar el modal después de eliminar el elemento
  };


  //end
  return (
    <>

      <div className="page">
        <StyleComponentBreadcrumb nombre="Materias / Ciclos" />


        <PanelContainerBG>

          <div className={styles.tableContainer}>
            <div className={styles.container} id={styles.containerMaterias} >
              <div id={styles.materiasTable}>
              {loadingMaterias ? <Spinner height={'calc(100vh - 80px)'} /> :
                <Tabla
                  datosTabla={{
                    tituloTabla: 'Lista_de_Materias',
                    titulos: [
                      { titulo: 'Materias' },
                    ],
                    clickable: { action: btndetallesMateria },
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
              }
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

            <div className={styles.container} id={styles.containerCiclos} >
              <div id={styles.ciclosTable}>
              {loadingCiclos ? <Spinner height={'calc(100vh - 80px)'} /> :
                <Tabla
                  datosTabla={{
                    tituloTabla: 'Lista_de_ciclos',
                    titulos: [
                      { titulo: 'Ciclos' },
                    ],
                    clickable: { action: btndetallesCiclo },
                    tableWidth: '100%',
                    filas: ciclos.map((materia) => ({
                      fila: materia,
                      datos: [
                        { dato: materia.id },
                        { dato: materia.nombre },
                        { dato: <IconButton enabled={true} buttonType='close' onClick={(event) => handleShowModalCiclo(event, materia.id, materia.nombre)}>  </IconButton> },
                      ],
                    })),
                  }}
                  selected={id ?? '-'}
                />
              }
                <ModalConfirmacion
                  show={showModalCiclo}
                  onHide={() => setShowModalCiclo(false)}
                  onConfirm={handleDeleteCiclo}
                  materia={nombre_Ciclo_delete}
                />
              </div>
              <div>
                {detallesCiclo
                  ? <div className={styles.divEditCiclos}>
                    <label className={styles.label}><strong>Editar Ciclo</strong></label>
                    <br />
                    <label className={styles.label}>Nombre Actual</label>
                    <br />
                    <div className={styles.inputAdd}> {dataCiclo.nombre} </div>

                    <label className={styles.label}>Nombre Nuevo</label>
                    <br />
                    <input className={styles.inputAdd} placeholder='Nombre del Ciclo' onChange={(event) => handleNombreNuevoCiclo(event)} id='input-Ciclo' ></input>
                    <div className={styles.buttonsCiclo}>
                      <TextButton enabled={true} buttonType='cancel' onClick={() => handleCancel()} />
                      <TextButton enabled={true} buttonType='save' onClick={() => handleEditCiclo()} />

                    </div>
                  </div>
                  : <div className={styles.divAddCiclos}>
                    <label className={styles.label}> <strong>Agregar Ciclo</strong></label>
                    <br />
                    <input type='text' className={styles.inputAdd} placeholder='Nombre del Ciclo' onChange={(event) => handleNombreCiclo(event)} value={nombreCiclo || ''} ></input>
                    <div className={styles.buttonGuardar}>

                      <TextButton enabled={true} buttonType='save' onClick={() => handleSubmitCiclo()} />

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

export default ListarMaTerias