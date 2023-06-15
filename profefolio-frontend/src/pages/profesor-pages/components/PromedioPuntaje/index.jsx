import React, { useEffect, useState } from 'react'
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent'
import { Line } from 'react-chartjs-2'
import { useModularContext } from '../../context'

const PromedioPuntaje = () => {

    const { dataSet } = useModularContext()
    const { puntajes } = dataSet

    const [labels, setLabels] = useState([])
    const [promedios, setPromedios] = useState([])

    useEffect(() => {
        if (puntajes.length > 0) {
            setLabels(puntajes.map(p => { return p.nombreEvaluacion }))
            setPromedios(puntajes.map(p=>{return p.promedio}))
        }
    }, [puntajes])

    const data = {
    labels: labels,
    datasets: [{
        label: 'Promedio del Puntaje',
        data: promedios,
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