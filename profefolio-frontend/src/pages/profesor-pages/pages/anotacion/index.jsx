import React, { useState } from "react";
import styled from "styled-components";
import BackButton from "../../components/BackButton";
import { Row, Col } from "react-bootstrap";
import Paginations from "../../../../components/Paginations";
import AnotacionShow from "./componentes/AnotacionShow";
import AnotacionCard from "./componentes/AnotacionCard";

const FlexDiv = styled.div`
    display: flex;
    align-items: center;
    width: 100%;
    gap: 10px;
`

const GridDiv = styled.div`
    margin-top: 1%;
    display: grid;
    grid-template-columns: 23% 23% 23% 23%;
    gap: 10px;
`

const GapDiv = styled.div`
    height: 30px;
`

const Anotacion = () => {
    const lista = [1,1,1,1,1,1,1,1,1,1,1,1]
    const [selected, setSelected] = useState({})
    return <>
        <Row>
            <FlexDiv>
                <BackButton />
                <h5 className="m-0">
                Anotaciones 
                </h5>
            </FlexDiv>
        </Row>
        <Row className="my-2">
            <Col md={8}>
                <GridDiv>
                    {lista.map(l=>{return <AnotacionCard onClick={setSelected} />})}
                </GridDiv>
                <Paginations />
            </Col>
            <Col md={4}>
                <AnotacionShow selectedAnotation={selected}/>
            </Col>
        </Row>
        <GapDiv />
    </>
}

export default Anotacion