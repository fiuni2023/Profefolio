import React from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { Line } from 'react-chartjs-2'

const PromedioPuntaje = () => {
    const labels = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Deciembre'];
    const data = {
    labels: labels,
    datasets: [{
        label: 'Promedio del Puntaje',
        data: [65.5, 59.765, 80, 81, 56, 55, 40],
        fill: false,
        borderColor: '#FF0000',
        tension: 0.1
    }]
    };
    const config = {
        type: 'line',
        data: data,
    };

    return <>
        <SCard>
            <SHeader>Promedio de Puntaje</SHeader>
            <SBody> 
                <Line options={config} data={data}/>
            </SBody>
        </SCard>
    </>
}

export default PromedioPuntaje