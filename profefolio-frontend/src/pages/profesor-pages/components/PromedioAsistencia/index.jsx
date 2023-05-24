import React, { useEffect, useState } from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { Bar } from 'react-chartjs-2';
import { useModularContext } from '../../context';

const PromedioAsistencia = (
) => {

    const { dataSet } = useModularContext()
    const { asistencias } = dataSet

    const [labels, setLabels] = useState([])
    const [presentes, setPresentes] = useState([])
    const [ausentes, setAusentes] = useState([])
    const [justificados, setJustificados] = useState([])


    useEffect(() => {
        if (asistencias.length > 0) {
            setLabels(asistencias.map(a => { return a.mes }))
            setPresentes(asistencias.map(a=>{return a.presentes}))
            setAusentes(asistencias.map(a => { return a.ausentes }))
            setJustificados(asistencias.map(a=>{return a.justificados}))
        }
    }, [asistencias])


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
                data: presentes,
                backgroundColor: '#24B787',
            },
            {
                label: 'Ausente',
                data: ausentes,
                backgroundColor: '#D93D79',
            },
            {
                label: 'Justificado',
                data: justificados,
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