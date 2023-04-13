import React from 'react';
import { Col } from 'react-bootstrap';
import {SCard, SBody, SHeader, STitle} from "../components/componentsStyles/StyledCard";
import Tabla from './Tabla';

function Card(cardInfo){
    return (
        <Col xs={cardInfo?.cardInfo?.xs ?? 12} sm={cardInfo?.cardInfo?.sm ?? 12} md={cardInfo?.cardInfo?.md ?? 6} lg={cardInfo?.cardInfo?.lg ?? 4}>
            <SCard>
                {cardInfo?.cardInfo?.header && 
                    <SHeader background={cardInfo?.cardInfo?.background ?? "gray"}>
                        {cardInfo?.cardInfo?.header?.title}</SHeader>}
                {cardInfo?.cardInfo?.body && 
                    <SBody background={cardInfo?.cardInfo?.background ?? "gray"}>
                        {cardInfo?.cardInfo?.body?.title && <STitle>{cardInfo.cardInfo.body.title}</STitle>}
                        {cardInfo?.cardInfo?.body?.table && <Tabla datosTabla={cardInfo?.cardInfo?.body?.table}></Tabla>}
                    </SBody>}
            </SCard>
        </Col>
    )
}

export default Card; 