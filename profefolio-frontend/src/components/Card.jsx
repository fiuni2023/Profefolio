import React from 'react';
import {SCard, SBody, SHeader, STitle, SCol} from "./componentsStyles/StyledDashComponent";
import Tabla from './Tabla';
import { useNavigate } from 'react-router';



function Card({cardInfo}){
    const nav = useNavigate();
    const handleClick = (goto) => {
        nav(goto);
    }
    return (
        <SCol xs={cardInfo?.xs ?? 12} sm={cardInfo?.sm ?? 12} md={cardInfo?.md ?? 6} lg={cardInfo?.lg ?? 4}>
            <SCard onClick={cardInfo?.goto ? () => handleClick(cardInfo.goto) : null} hover={cardInfo?.hover ? "true" : "false" }>
                {cardInfo?.header && 
                    <SHeader background={cardInfo?.background ?? "gray"}>
                        {cardInfo?.header?.title}</SHeader>}
                {cardInfo?.body && 
                    <SBody background={cardInfo?.background ?? "gray"}>
                        {cardInfo?.body?.title && <STitle>{cardInfo.body.title}</STitle>}
                        {cardInfo?.body?.table && <Tabla datosTabla={cardInfo?.body?.table}></Tabla>}
                    </SBody>}
            </SCard>
        </SCol>
    )
}

export default Card; 