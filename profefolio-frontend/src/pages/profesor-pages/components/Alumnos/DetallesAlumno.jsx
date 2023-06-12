import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import APILINK from '../../../../components/link.js';
import { useGeneralContext } from "../../../../context/GeneralContext.jsx";
import axios from 'axios';
import { toast } from 'react-hot-toast';
import { useNavigate } from 'react-router';
const DetallesAlumno = () => {
    const { id } = useParams();
    const { getToken, cancan, verifyToken, getMateriaId } = useGeneralContext();
    const [datos, setDatos] = useState([]);
    const nav = useNavigate()
    const idMat = getMateriaId();
    const getDatosAlumno = () => {
        if (!cancan("Profesor")) {
            nav("/")
        } else {
            axios.post(`${APILINK}/api/AnotacionAlumno/getWithInfo `, {
                "alumnoId": id, //id del alumno dentro de una clase (Tabla ClaseAlumnoColegio)
                "materiaListaId": 1

            }, {
                headers: {
                    Authorization: `Bearer ${getToken()}`,
                }
            })
                .then(response => {


                    setDatos(response.data);
                    console.log(datos);


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
    
     useEffect(() => {
 
         console.log(idMat);
        
     
     
       }, [cancan, verifyToken, nav, getToken]);
 
    return <>
        <div>
            <h1>{id}</h1>
        </div>
    </>
}

export default DetallesAlumno