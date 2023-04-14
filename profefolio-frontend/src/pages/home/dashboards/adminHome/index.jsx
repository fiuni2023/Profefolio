import React from "react";
import { useGeneralContext } from "../../../../context/GeneralContext";
import styles from "./index.module.css";
import { Row, Container } from "react-bootstrap";
import Card from "../../../../components/Card";
import { Colegios, Administradores } from "./cards";
import {DTitle, SRow} from "../../../../components/componentsStyles/StyledDashComponent"

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

    const {getUserName} = useGeneralContext()

    return(
        <Container className={styles.HomeDiv}>
             <DTitle>Bienvenido, {getUserName()}</DTitle>  
            <SRow>
                <Card cardInfo={Colegios}></Card>
                <Card cardInfo={Administradores}></Card>
            </SRow>
            
        </Container>
    )
}

export default AdminHome