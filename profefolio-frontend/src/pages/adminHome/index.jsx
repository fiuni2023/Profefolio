import React from "react";
import { useGeneralContext } from "../../context/GeneralContext";
import styles from "./index.module.css";
import { Row, Col } from "react-bootstrap";
import CategoryPanel from "./components/categoryPanel";
import { useNavigate } from "react-router";

const Home = () => {

    const {getUserName} = useGeneralContext()
    const nav = useNavigate()

    return(
        <div className={styles.HomeDiv}>
            <Row>
                <Col>
                    <h3>Bienvenido,</h3>  
                    <h3 className={`${styles.RubyText} mt-2 `}>{getUserName()}</h3>
                </Col>
            </Row>
            <Row>
                <Col>
                    <CategoryPanel text="Administradores" handleClick={()=>{nav('/administrador/list')}}>
                        <div className={styles.Panel}>
                            
                        </div>
                    </CategoryPanel>
                </Col>
                <Col>
                    <CategoryPanel text="Colegios" handleClick={()=>{nav('/colegio')}}></CategoryPanel>
                </Col>
            </Row>
        </div>
    )
}

export default Home