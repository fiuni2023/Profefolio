import React from "react";
import { useGeneralContext } from "../../../../context/GeneralContext";
import styles from "./index.module.css";
import { Row, Col } from "react-bootstrap";
import CategoryPanel from "./components/categoryPanel";
import { useNavigate } from "react-router";

const AdminHome = () => {

    const {getUserName} = useGeneralContext()
    const nav = useNavigate()

    return(
        <div className={styles.HomeDiv}>
            <Row className="mb-3">
                <Col>
                    <h3>Bienvenido,</h3>  
                    <h3 className={`${styles.RubyText} mt-2 `}>{getUserName()}</h3>
                </Col>
            </Row>
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
        </div>
    )
}

export default AdminHome