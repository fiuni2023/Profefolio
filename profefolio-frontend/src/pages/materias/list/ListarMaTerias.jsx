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
import { useFetchEffect } from '../../../components/utils/useFetchEffect';
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
  const [deleteMateria, setDeleteMateria]=useState(false);
  const nav = useNavigate()

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

          setCiclos(response.data)

        })
        .catch((error) => {
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

      axios.get(`${APILINK}/api/Materia/page/${page}`, {
        headers: {
          Authorization: `Bearer ${getToken()}`,
        }
      })

        .then(response => {
          setMaterias(response.data.dataList);
        })
        .catch(error => {
          toast.error(error);
        });
    }

  }

  useEffect(() => {

    getMaterias();
    getCiclos();


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

  const handleSubmitCiclo = () => {

    if (nombreCiclo === "") toast.error("revisa los datos, los campos deben ser completados")
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
    console.log(data);
    setId(dataCiclo.id);
    setDataCiclo(data);
    setDetallesCiclo(true);
  };
  const btndetallesMateria = (data) => {
    setId(data.id);
    setData(data);
    if(!deleteMateria){
      setDetallesMateria(true);}
    else{
      setDeleteMateria(false)

    }
    
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
  const handleDeleteMateria = (id) => {
    setDetallesCiclo(false);
    setDetallesMateria(false);
    setDetallesMateria(false);
    console.log(id)

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
                    clickable: { action: btndetallesMateria },
                    tableWidth: '100%',
                    filas: materias.map((materia) => ({
                      fila: materia,
                      datos: [
                        { dato: materia.id },
                        { dato: materia.nombre_Materia },
                        { dato: <button onClick={() =>setDeleteMateria(true)}>H</button> },
                      ],
                    })),
                  }}
                  selected={id ?? '-'}
                />
              </div>
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
                <Tabla
                  datosTabla={{
                    tituloTabla: 'Lista de Ciclos',
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
                        { dato: "X" },
                      ],
                    })),
                  }}
                  selected={id ?? '-'}
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
                    <input value={nombreCiclo || ''} className={styles.inputAdd} placeholder='Nombre del Ciclo' onChange={(event) => handleNombreCiclo(event)} ></input>
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