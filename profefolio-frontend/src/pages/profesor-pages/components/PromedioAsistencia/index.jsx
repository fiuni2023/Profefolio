import React from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { Bar } from 'react-chartjs-2';

const PromedioAsistencia = () => {
    const labels = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Deciembre'];

    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Promedio de Asistencias',
            },
        },
    };

    const data = {
        labels,
        datasets: [
            {
                label: 'Presente',
                data: labels.map(() => parseInt(Math.random()*100)),
                backgroundColor: '#24B787',
            },
            {
                label: 'Ausente',
                data: labels.map(() => parseInt(Math.random()*100)),
                backgroundColor: '#D93D79',
            },
            {
                label: 'Justificado',
                data: labels.map(() => parseInt(Math.random()*100)),
                backgroundColor: '#5181D1',
            },
        ],
    };

    return <>
        <SCard>
            <SHeader>Promedio de Asistencia</SHeader>
            <SBody>
                <Bar options={options} data={data} />
            </SBody>
        </SCard>
    </>
}

export default PromedioAsistencia