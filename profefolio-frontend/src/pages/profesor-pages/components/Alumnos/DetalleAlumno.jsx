import React, { useEffect, useState } from 'react'
import { useModularContext } from '../../context';
import { useGeneralContext } from '../../../../context/GeneralContext';
import { toast } from 'react-hot-toast';
import APILINK from '../../../../components/link';
import axios from "axios";
import styles from "./DetalleAlumno.module.css"
import styled from "styled-components";
import { Row, Col } from 'react-bootstrap';
import TextButton from '../../../../components/TextButton';
const DetalleAlumno = () => {
    const { dataSet, stateController } = useModularContext()
    const { claseId } = stateController;
    const { alumnoId } = dataSet;
    const [datosAlumno, setDatosAlumno] = useState([]);
    const { getToken } = useGeneralContext();
    const [titulo, setTitulo] = useState("")
    const [contenido, setContenido] = useState("");
    const AnotacionShowDiv = styled.div`
    margin: 5px;
    padding: 5%;
    border: 1px solid #DDDDDD;
    border-radius: 15px;
    height: 50vh;
    width:80%;
    display: flex;
    flex-direction: column;
    gap: 2vh;
`

    const InputTitulo = styled.input`
    border: 1px solid #DDDDDD;
    outline: none;
    border-radius: 10px;
    padding: 10px;
    font-size: 16px;
    &:hover{
        filter: brightness(0.8);
    }
`

    const InputContenido = styled.textarea`
    border: 1px solid #DDDDDD;
    outline: none;
    border-radius: 10px;
    padding: 10px;
    resize: none;
    height: 50vh;
    font-size: 16px;
    &:hover{
        filter: brightness(0.8);
    }
`

    const DeselectDiv = styled.div`
    width: 40px;
    height: 40px;
    text-align: center;
    padding-top: 10px;
    border-radius: 50%;
    background-color: white;
    &:hover{
        filter:brightness(0.85);
    }
    &:active{
        filter:brightness(0.75);
    }
`;
    useEffect(() => {
        console.log(claseId);
        axios.post(`${APILINK}/api/AnotacionAlumno/getWithInfo`, {
            alumnoId: alumnoId,
            claseId: claseId

        }, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })
            .then(response => {
                setDatosAlumno(response.data);
                console.log(response.data)

            })
            .catch(error => {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                    toast.error(error.response.data?.errors.Email[0])
                }
            });

    }, [alumnoId, getToken, claseId])
   /*  const handleCreate = (titulo, contenido) => {
        if (titulo === "") return toast.error("Revise los Campos")
        if (contenido === "") return toast.error("Revise los Campos")
        let body = {
            "titulo": titulo,
            "contenido": contenido,
            "materiaListaId": materiaId
        }
        axios.post(`${APILINK}/api/AnotacionAlumno/getWithInfo`, {
            alumnoId: alumnoId,
            claseId: claseId

        }, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })
            .then(response => {
                setDatosAlumno(response.data);
                console.log(response.data)

            })
            .catch(error => {
                if (typeof (error.response.data) === "string" ? true : false) {
                    toast.error(error.response.data)
                } else {
                    toast.error(error.response.data?.errors.Email[0])
                }
            });
    } */

    return <>
        <div>
            <Row>
                <Col md={4}>
                    <div className={styles.information_div} >
                        <h5>Nombre: {datosAlumno.nombre} {datosAlumno.apellido} </h5>
                        <h5>Grado: {datosAlumno.clase}</h5>
                        <h5>Ciclo: {datosAlumno.ciclo}</h5>
                        <div>
                            <h3>Materias:</h3><ul>
                                {datosAlumno?.materias?.map(materia => {
                                    return <div><h5><li>{materia}</li></h5></div>
                                }
                                )
                                }</ul>

                        </div>
                    </div>
                </Col>
                <Col md={4}>

                </Col>
                
                    <Col md={4}>
                        <AnotacionShowDiv>
                            <div className="d-flex align-items-center justify-content-between">
                                <h5 className="m-0">
                                    Anotacion
                                </h5>

                            </div>
                            <InputTitulo value={titulo} placeholder="Titulo"
                                onChange={(event) => {
                                    setTitulo(event?.target?.value)
                                }} />
                            <InputContenido value={contenido} placeholder="Contenido"
                                onChange={(event) => {
                                    setContenido(event?.target?.value)
                                }} />
                                <TextButton enabled={true} buttonType='save' onClick={()=>console.log('Guardando')} />
                        </AnotacionShowDiv>
                    </Col>
                
            </Row>
        </div>
    </>

}
export default DetalleAlumno