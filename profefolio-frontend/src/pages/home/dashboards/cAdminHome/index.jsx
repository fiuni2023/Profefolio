import React, { useEffect, useState } from "react";
import { useGeneralContext } from "../../../../context/GeneralContext";
import styles from "./index.module.css";
import { Row, Container } from "react-bootstrap";
import {DTitle, SRow, Separator} from "../../../../components/componentsStyles/StyledDashComponent"
import Card from "../../../../components/Card";
import { Stats} from "./cards"
import AlumnoService from "../../../../sevices/alumno";
import ProfesorService from "../../../../sevices/profesor";
import MateriaService from "../../../../sevices/materia";
import CicloService from "../../../../sevices/ciclo";
import ClaseService from "../../../../sevices/clase";

const CAdminHome = () => {

    const {getUserName, getToken, getColegioId } = useGeneralContext()
    const token = getToken()
    const colegioId = getColegioId()

    const [alumnos, setAlumnos] = useState({
        xs: 12, sm:12, md: 6, lg:3,
        background: "yellow",
        hover: true,
        goto: '/alumnos',
        header: {
            title: "Alumnos",
        },
        body: {
            title: "Ultimos Alumnos",
            table: {
                small: true, 
                titulos: [ {titulo: "Nombre y Apellido"} ], 
                filas: []
            }
        }
    })
    const [profesores, setProfesores] = useState({
        xs: 12, sm:12, md: 6, lg:3,
        background: "yellow",
        hover: true,
        goto: '/profesor',
        header: {
            title: "Profesores",
        },
        body: {
            title: "Ultimos Profesores",
            table: {
                small: true, 
                titulos: [ {titulo: "Nombre y Apellido"} ], 
                filas: []
            }
        }
    })
    const [materias, setMaterias] = useState({
        xs: 12, sm:12, md: 6, lg:3,
        background: "orange",
        hover: true,
        goto: '/materias',
        header: {
            title: "Materias/Ciclos",
        },
        body: {
            title: "Ultimas Materias/Ciclos",
            table: {
                small: true, 
                titulos: [ {titulo: "Nombre"} ], 
                filas: []
            },
            table2: {
                small: true, 
                titulos: [ {titulo: "Ciclo"}], 
                filas: []
            }
        }
    })
    const [clases, setClases] = useState({
        xs: 12, sm:12, md: 6, lg:3,
        background: "purple",
        hover: true,
        goto: '/clases',
        header: {
            title: "Clases",
        },
        body: {
            title: "Ultimas Clases",
            table: {
                small: true, 
                titulos: [ {titulo: "Titulo"},  {titulo: "Ciclo"}  ], 
                filas: []
            }
        }
    })

    useEffect(()=>{
        AlumnoService.getFirstPage(token)
        .then(r=>{
            if(r.data){
                let listaAlumno = r.data.dataList.length > 5? r.data.dataList.slice(0,5):r.data.dataList
                setAlumnos({
                    xs: 12, sm:12, md: 6, lg:3,
                    background: "yellow",
                    hover: true,
                    goto: '/alumnos',
                    header: {
                        title: "Alumnos",
                    },
                    body: {
                        title: "Ultimos Alumnos",
                        table: {
                            small: true, 
                            titulos: [ {titulo: "Nombre y Apellido"}], 
                            filas: listaAlumno.length === 0? [
                                {datos:[{dato: "No hay datos nuevos"}]}]
                                :listaAlumno.map(a=>{return {datos:[{dato: `${a.nombre} ${a.apellido}`}]}})
                        }
                    }
                
                })
            }
        })
        ProfesorService.getFirstPage(token)
        .then(r=>{
            if(r.data){
                let listaProfesor =  r.data.length > 5? r.data.slice(0,5):r.data
                setProfesores({
                    xs: 12, sm:12, md: 6, lg:3,
                    background: "yellow",
                    hover: true,
                    goto: '/profesor',
                    header: {
                        title: "Profesores",
                    },
                    body: {
                        title: "Ultimos Profesores",
                        table: {
                            small: true, 
                            titulos: [ {titulo: "Nombre y Apellido"} ], 
                            filas: listaProfesor.length === 0? [{datos:[{dato: "No hay datos nuevos"}]}] : listaProfesor.map(a=>{return {datos:[{dato: a.nombre}]}})
                        }
                    }
                })
            }
        })
        MateriaService.getFirstPage(token)
        .then(r=>{
            if(r.data){
                let ciclos
                let materias
                materias = r.data.dataList.length>4? r.data.dataList.slice(0,5) : r.data.dataList
                CicloService.getCiclos(token)
                .then(a=>{
                    if(a.data){
                        ciclos = a.data.length>4? a.data.slice(0,5): a.data
                        setMaterias({
                            xs: 12, sm:12, md: 6, lg:3,
                            background: "orange",
                            hover: true,
                            goto: '/materias',
                            header: {
                                title: "Materias/Ciclos",
                            },
                            body: {
                                title: "Ultimas Materias/Ciclos",
                                table: {
                                    small: true, 
                                    titulos: [ {titulo: "Nombre"} ], 
                                    filas: materias.length === 0? [{datos:[{dato: "No hay datos nuevos"}]}]: materias.map(d=>{return {datos:[{dato: d.nombre_Materia}]}})
                                },
                                table2: {
                                    small: true, 
                                    titulos: [ {titulo: "Ciclo"}], 
                                    filas: ciclos.length === 0? [{datos:[{dato: "No hay datos nuevos"}]}]:ciclos.map(b=>{return {datos:[{dato: b.nombre}]}})
                                }
                            }
                        })
                    }
                })
                
            }
        })
        ClaseService.getFirstPage(colegioId, token)
        .then(r=>{
            if(r.data){
                let clases =  r.data.length>4? r.data.slice(0,5) : r.data
                setClases({
                    xs: 12, sm:12, md: 6, lg:3,
                    background: "purple",
                    hover: true,
                    goto: '/clases',
                    header: {
                        title: "Clases",
                    },
                    body: {
                        title: "Ultimas Clases",
                        table: {
                            small: true, 
                            titulos: [ {titulo: "Titulo"},  {titulo: "Ciclo"}  ], 
                            filas: clases.length === 0? [{datos:[{dato: "No hay datos nuevos"},{dato: ""}]}]:clases.map(b=>{return {datos:[{dato: b.nombre}, {dato: b.ciclo}]}})
                        }
                    }
                })
            }
        })
        
    },[token, colegioId])

    return(
        <Container className={styles.HomeDiv}>
            <Row className="mb-3">
                <DTitle>Bienvenido, {getUserName()}</DTitle>  
            </Row>
            <SRow>
                <Card cardInfo={alumnos}></Card>
                <Card cardInfo={profesores}></Card>
                <Card cardInfo={materias}></Card>
                <Card cardInfo={clases}></Card>
            </SRow>
            <SRow>
                <Separator></Separator>
                <Card cardInfo={Stats}></Card>
            </SRow>
        </Container>
    )
}

export default CAdminHome