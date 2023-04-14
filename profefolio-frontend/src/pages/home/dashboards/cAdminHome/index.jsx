import React from "react";
import { useGeneralContext } from "../../../../context/GeneralContext";
import styles from "./index.module.css";
import { Row, Container } from "react-bootstrap";
import {DTitle, SRow, Separator} from "../../../../components/componentsStyles/StyledDashComponent"
import Card from "../../../../components/Card";
import {Alumnos, Profesores, Materias, Clases, Stats} from "./cards"

const CAdminHome = () => {

    const {getUserName} = useGeneralContext()

    return(
        <Container className={styles.HomeDiv}>
            <Row className="mb-3">
                <DTitle>Bienvenido, {getUserName()}</DTitle>  
            </Row>
            <SRow>
                <Card cardInfo={Alumnos}></Card>
                <Card cardInfo={Profesores}></Card>
                <Card cardInfo={Materias}></Card>
                <Card cardInfo={Clases}></Card>
            </SRow>
            <SRow>
                <Separator></Separator>
                <Card cardInfo={Stats}></Card>
            </SRow>
        </Container>
    )
}

export default CAdminHome