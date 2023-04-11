import React from 'react';
import { Col } from 'react-bootstrap';
import {SCard, SBody, SHeader} from "../components/componentsStyles/StyledCard";

function Card(cardInfo){
    return (
        <Col xs={cardInfo?.xs ?? 12} sm={cardInfo?.sm ?? 12} md={cardInfo?.md ?? 6} lg={cardInfo?.lg ?? 4}>
            <SCard>
                {cardInfo?.header && <SHeader>{cardInfo?.header?.title}</SHeader>}
                {cardInfo?.body && <SBody></SBody>}
            </SCard>
        </Col>
    )
}

export default Card; 