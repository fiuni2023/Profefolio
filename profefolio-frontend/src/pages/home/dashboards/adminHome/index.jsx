import React, { useEffect, useState } from "react";
import { useGeneralContext } from "../../../../context/GeneralContext";
import styles from "./index.module.css";
import { Container } from "react-bootstrap";
import Card from "../../../../components/Card";
import {DTitle, SRow} from "../../../../components/componentsStyles/StyledDashComponent"
import AdminService from "../../../../sevices/administrador";
import ColegioService from "../../../../sevices/colegio";
import Spinner from "../../../../components/componentsStyles/SyledSpinner";

/* 
<Row>
                <Col>
                    <CategoryPanel text="Administradores" handleClick={()=>{nav('/administrador/list')}}>
                        <div className ="d-flex flex-column"> 
                            <p>Aca podras:</p>
                            <ul>
                                <li><p>Agregar Administradores.</p></li>
                                <li><p>Editar / Ver Administradores ya existentes.</p></li>
                                <li><p>Borrar Administradores.</p></li>
                            </ul>
                        </div>
                    </CategoryPanel>
                </Col>
                <Col>
                    <CategoryPanel text="Colegios" handleClick={()=>{nav('/colegio')}}>
                        <div className ="d-flex flex-column"> 
                            <p>Aca podras:</p>
                            <ul>
                                <li><p>Agregar Colegios.</p></li>
                                <li><p>Editar / Ver Colegios ya existentes.</p></li>
                                <li><p>Borrar Colegios.</p></li>
                            </ul>
                        </div>
                    </CategoryPanel>
                </Col>
            </Row>
*/

const AdminHome = () => {

    const {getUserName, getToken} = useGeneralContext()
    const token = getToken()
    const [loading, setLoading] = useState(true); 

    const [colegios, setColegios] = useState({
        background: "orange",
        hover: true,
        goto: '/colegios/list',
        header: {
            title: "Colegios",
        },
        body: {
            title: "Ultimos Colegios",
            table: {
                small: true, 
                titulos: [ {titulo: "Nombre"} ], 
                filas: []
            }
        }
    })

    const [administradores, setAdministradores] = useState({
        background: "orange",
        hover: true,
        goto: '/administrador/list',
        header: {
            title: "Administradores",
        },
        body: {
            title: "Ultimos Administradores",
            table: {
                small: true, 
                titulos: [ {titulo: "Nombre y Apellido"}], 
                filas: []
            }
        }
    })
    


    useEffect(()=>{
        let loadingColegios = true;
        let loadingAdmins = true;  
        AdminService.getAll(token)
        .then((r)=>{
            if(r.data){
                let listaAdmin = r.data.length > 5? r.data.slice(0,5):r.data
                setAdministradores({
                    background: "orange",
                    hover: true,
                    goto: '/administrador/list',
                    header: {
                        title: "Administradores",
                    },
                    body: {
                        title: "Ultimos Administradores",
                        table: {
                            small: true, 
                            titulos: [ {titulo: "Nombre y Apellido"} ], 
                            filas: listaAdmin.map(a=>{return{datos: [{dato: a.nombre}]}})
                        }
                    }
                })
            }
            loadingAdmins = false; 
            if (!loadingColegios){
                setLoading(false); 
            }
        }).catch((e) => {
            loadingAdmins = false; 
            if (!loadingColegios){
                setLoading(false); 
            }
        })
        ColegioService.getFirstPage(token)
        .then((r)=>{
            if(r.data){
                let listaColegio = r.data.dataList.length > 5? r.data.dataList.slice(0,5):r.data.dataList
                setColegios({
                    background: "orange",
                    hover: true,
                    goto: '/colegios/list',
                    header: {
                        title: "Colegios",
                    },
                    body: {
                        title: "Ultimos Colegios",
                        table: {
                            small: true, 
                            titulos: [ {titulo: "Nombre"}], 
                            filas: listaColegio.map(c=>{return {datos:[{dato: c.nombre}]} })
                        }
                    }
                })
            }
            loadingColegios = false; 
            if (!loadingAdmins){
                setLoading(false); 
            }
        }).catch((e) => {
            loadingColegios = false; 
            if (!loadingAdmins){
                setLoading(false); 
            }
        })
        
    },[token])

    return(
        <Container className={styles.HomeDiv}>
             <DTitle>Bienvenido, {getUserName()}</DTitle>  
                {loading ? <Spinner height={"calc(50vh - 80px)"} /> :
                    <SRow>
                        <>
                            <Card cardInfo={colegios}></Card>
                            <Card cardInfo={administradores}></Card>
                        </>
                    </SRow>
                }
            
        </Container>
    )
}

export default AdminHome