import React from "react";
import styled from "styled-components";
import BackButton from "../../components/BackButton";
import { Row } from "react-bootstrap";
import EvaluationTable from "./components/EvaluationTable";
import { PageProvider } from "./context/PageContext";
import { useModularContext } from "../../context";

const FlexDiv = styled.div`
    display: flex;
    align-items: center;
    width: 100%;
    gap: 10px;
`

const Evaluaciones = () => {

    const {dataSet} = useModularContext()
    const { materiaName, currColegio, currClase } = dataSet

    return <>
        <PageProvider>
            <div className="m-4">
                <Row>
                    <FlexDiv>
                        <BackButton to="materiashow"/>
                        <h5 className="m-0">
                        {currColegio} - {currClase} - {materiaName} - Calificaciones
                        </h5>
                    </FlexDiv>
                </Row>
                <Row>
                    <EvaluationTable />
                </Row>
            </div>
        </PageProvider>
    </>
}

export default Evaluaciones