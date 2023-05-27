import React from "react";
import styled from "styled-components";
import BackButton from "../../components/BackButton";
import { Row } from "react-bootstrap";
import EvaluationTable from "./components/EvaluationTable";

const FlexDiv = styled.div`
    display: flex;
    align-items: center;
    width: 100%;
    gap: 10px;
`

const GapDiv = styled.div`
    height: 30px;
`

const Evaluaciones = () => {

    return <>
        <Row>
            <FlexDiv>
                <BackButton to="materiashow"/>
                <h5 className="m-0">
                    Evaluaciones
                </h5>
            </FlexDiv>
        </Row>
        <Row>
            <EvaluationTable />
        </Row>
        <GapDiv />
    </>
}

export default Evaluaciones