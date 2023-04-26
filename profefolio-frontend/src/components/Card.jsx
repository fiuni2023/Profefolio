import React from 'react';
import {SCard, SBody, SHeader, STitle, SCol, TwoCol, FirstCol, SecondCol, SingleCol, ThreeCol, MainCol, SecondaryCol, ClockContainer} from "./componentsStyles/StyledDashComponent";
import Tabla from './Tabla';
import { useNavigate } from 'react-router';
import { HiClock } from 'react-icons/hi';
import Chart from 'chart.js/auto';



function Card({cardInfo}){
    const nav = useNavigate();
    const handleClick = (goto) => {
        nav(goto);
    }
    const generateChart = (info) => {
        setTimeout(()=>{
            new Chart(
                document.getElementById(info.id),
                info.config);
        },100)
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
                        <TwoCol> 
                            {cardInfo?.body?.table && <Tabla datosTabla={cardInfo?.body?.table}></Tabla>}
                            {cardInfo?.body?.table2 && <Tabla datosTabla={cardInfo?.body?.table2}></Tabla>}
                        </TwoCol>
                        {cardInfo?.body?.first && cardInfo?.body?.first?.title && cardInfo?.body?.first?.subtitle &&
                            <TwoCol>
                                <FirstCol>{cardInfo.body.first.title}</FirstCol>
                                <SecondCol>{cardInfo.body.first.subtitle}</SecondCol>
                            </TwoCol>
                        }
                        {cardInfo?.body?.first && cardInfo?.body?.first?.title && !cardInfo?.body?.first?.subtitle &&
                            <SingleCol>{cardInfo.body.first.title}</SingleCol>
                        }
                        {cardInfo?.body?.second && cardInfo.body?.second?.title &&
                            <SingleCol>{cardInfo.body.second.title}</SingleCol>
                        }
                        {cardInfo?.body?.third && cardInfo.body?.third?.title &&
                            <SingleCol>{cardInfo.body.third.title}</SingleCol>
                        }
                        {cardInfo?.body?.schedule &&
                        <ThreeCol>
                            <ClockContainer><HiClock/></ClockContainer>
                            {cardInfo?.body?.schedule?.main && <MainCol>{cardInfo?.body?.schedule?.main}</MainCol>}
                            {cardInfo?.body?.schedule?.secondary && <SecondaryCol>{cardInfo?.body?.schedule?.secondary}</SecondaryCol>}
                        </ThreeCol> 
                        }
                        {cardInfo?.body?.graph && cardInfo?.body?.graph?.id && <div><canvas id={cardInfo.body.graph.id}>{generateChart(cardInfo.body.graph)}</canvas></div>}
                    </SBody>}
            </SCard>
        </SCol>
    )
}

export default Card; 