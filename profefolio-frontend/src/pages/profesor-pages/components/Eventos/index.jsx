import React from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { Col, Row } from 'react-bootstrap'
import styled from 'styled-components';
import EventosTabla from '../EventosTabla';

const EventTagDiv = styled.div`
    display: flex;
    align-items: center;
    gap: 5px;
    margin-top: 15%;
    justify-content: center;
`;

const ColorDot = styled.div`
    display: flex;
    align-items: center;
    background-color: ${props => props.color ?? "red"};
    width: 20px;
    height: 20px;
    border-radius: 50%;
`;

const EventText = styled.div`
    margin: 0;
    font-weight: 600;

`;

const GridDiv = styled.div`
    display: grid;
    grid-template-columns: 25% 25% 25% 25%;
    gap: 1%;
    font-size: 10px;
`;


const Eventos  = ({ tablaEventos }) => {
    const tipos_eventos = [
        {
            id: 1,
            texto: "Eventos",
            color: "#C8BFD9"
        },
        {
            id: 2,
            texto: "Parcial",
            color: "#C1E1FA"
        },
        {
            id: 3,
            texto: "Prueba Sumativa",
            color: "#FCC6AC"
        },
        {
            id: 4,
            texto: "Examen",
            color: "#F6E7A7"
        }
    ]
    return <>
        <SCard>
            <SHeader>Eventos</SHeader>
            <SBody>
                    <Row>
                    <Col>
                            {tablaEventos} {/* Renderiza el componente EventosTabla aquí */}
                        </Col>
                    </Row>
                    <Row>
                        <GridDiv>
                            {tipos_eventos.map((tipo,i)=>{
                                return (<EventTag key={i} tipo={tipo} />)
                            })}
                        </GridDiv>
                    </Row>
            </SBody>
        </SCard>
    </>
}



const EventTag = ({
    tipo = {id:0, color: "", texto: ""}
}) => {

    return(
        <EventTagDiv>
            <ColorDot color={tipo?.color} />
            <EventText>{tipo?.texto}</EventText>
        </EventTagDiv>
    )
}



export default Eventos