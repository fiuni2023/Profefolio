import React from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { Col, Row } from 'react-bootstrap'

const EventTagDiv = styled.div`
    display: flex;
    align-items: center;

`;

const ColorDot = styled.div`
    display: flex;
    align-items: center;
    background-color: ${props => props.color ?? "red"};
`;

const EventText = styled.div`
    margin: 0;
    font-weight: 600;

`;

const GridDiv = styled.div`
    display: grid;
    grid-template-columns: 23% 23% 23% 23%;
    gap: 1%;
`;


const Eventos = ({
    lista_de_eventos = [],
    key_colors = []
}) => {
    return <>
        <SCard>
            <SHeader>Eventos</SHeader>
            <SBody>
                    <Row>
                        <Col>
                        
                        </Col>
                    </Row>
                    <Row>
                        <GridDiv>
                            {lista_de_eventos.map((evento,i)=>{
                                return (<EventTag />)
                            })}
                        </GridDiv>
                    </Row>
            </SBody>
        </SCard>
    </>
}



const EventTag = ({
    color = "",
    evento = {id: 1, texto: "algo"}
}) => {
    console.log(evento)

    return(
        <EventTagDiv>
            <ColorDot color={color} />
            <EventText>{evento.texto}</EventText>
        </EventTagDiv>
    )
}



export default Eventos