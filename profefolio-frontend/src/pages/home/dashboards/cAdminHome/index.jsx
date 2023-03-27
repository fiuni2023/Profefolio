import React from "react";
import { useGeneralContext } from "../../../../context/GeneralContext";
import styles from "./index.module.css";
import { Row, Col } from "react-bootstrap";

const CAdminHome = () => {

    const {getUserName} = useGeneralContext()

    return(
        <div className={styles.HomeDiv}>
            <Row className="mb-3">
                <Col>
                    <h3>Bienvenido,</h3>  
                    <h3 className={`${styles.RubyText} mt-2 `}>{getUserName()}</h3>
                </Col>
            </Row>
            
        </div>
    )
}

export default CAdminHome