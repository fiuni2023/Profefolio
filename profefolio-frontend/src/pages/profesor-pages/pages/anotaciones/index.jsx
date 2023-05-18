import React from "react";
//import { useModularContext } from "../../context";
import BackButton from "../../components/BackButton";
import { Col, Row } from "react-bootstrap";
import AnotationCard from "../../components/AnotationCard";
import styled from "styled-components";
import AnotationShow from "../../components/AnotationShow";

const GridLayout = styled.div`
    display: grid;
    grid-template-columns: 23.5% 23.5% 23.5% 23.5%;
    gap: 1%;
`

const Anotaciones = () => {
    //const {setPage} = useModularContext()

    const lista = [1,1,1,1,1,1,1,1,1]

    return (
        <>  
            <Row>
                <div className="d-flex gap-2 align-items-center">
                    <BackButton to="materiashow" />
                    <h5 className="m-0">Anotaciones</h5>
                </div>
            </Row>
            <Row className="mt-4">
                <Col sm={8}>
                    <Row>
                        <GridLayout>
                            {lista.map((l)=>{return <AnotationCard />})}
                        </GridLayout>
                    </Row>
                </Col>
                <Col sm={4}>
                    <AnotationShow />
                </Col>
            </Row>
            <Row>
                <div style={{height: "20px"}}></div>
            </Row>
        </>
    )
}

export default Anotaciones