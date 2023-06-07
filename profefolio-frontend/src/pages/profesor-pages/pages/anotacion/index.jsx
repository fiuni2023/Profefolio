import React, { useEffect, useState } from "react";
import styled from "styled-components";
import BackButton from "../../components/BackButton";
import { Row, Col } from "react-bootstrap";
import AnotacionShow from "./componentes/AnotacionShow";
import AnotacionCard from "./componentes/AnotacionCard";
import { useGeneralContext } from "../../../../context/GeneralContext";
import AnotationsService from "../../services/AnotationsService";

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

    
    const {getToken} = useGeneralContext()
    const token = getToken()

    const [lista, setLista] = useState([])
    const [selected, setSelected] = useState(null)
    const [fetchdata, setFetchData] = useState([])

    useEffect(()=>{
        AnotationsService.Get(token)
        .then(r=>{
            setLista(r.data)
        })
    }, [token, fetchdata])

    const doFetch = () => {
        setFetchData((before)=>{return [before]})
    }

    return <>
        <Row>
            <FlexDiv>
                <BackButton to="materiashow"/>
                <h5 className="m-0">
                Anotaciones 
                </h5>
            </FlexDiv>
        </Row>
        <Row className="my-2">
            <Col md={8}>
                <GridDiv>
                    {lista.map(l=>{return <AnotacionCard onClick={setSelected} observacion={l} />})}
                </GridDiv>
                {/* <Paginations /> */}
            </Col>
            <Col md={4}>
                <AnotacionShow doFetch={doFetch} setSelectedAnotation={setSelected} selectedAnotation={selected}/>
            </Col>
        </Row>
        <GapDiv />
    </>
}

export default Anotacion