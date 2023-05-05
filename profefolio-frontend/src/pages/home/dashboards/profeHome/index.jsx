import React from "react";
//import { useGeneralContext } from "../../../../context/GeneralContext";
import styles from "./index.module.css";
//import { Row, Col } from "react-bootstrap";
import ProfesorPage from "../../../profesor-pages";

const ProfeHome = () => {

    //const {getUserName} = useGeneralContext()

    return(
        <div className={styles.HomeDiv}>
            <ProfesorPage/>
            {/* <Row className="mb-3">
                <Col>
                    <h3>Bienvenido,</h3>  
                    <h3 className={`${styles.RubyText} mt-2 `}>{getUserName()}</h3>
                </Col>
            </Row> */}
            
        </div>
    )
}

export default ProfeHome