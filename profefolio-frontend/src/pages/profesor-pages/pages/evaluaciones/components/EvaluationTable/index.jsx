import React from "react";
import styled from "styled-components";

const ETable = styled.table`
    border: 1px solid black;
    margin-top: 2vh;
    width: 100%;
`;

const ETH = styled.th`
    border: 1px solid black;
    background-color: #C6D8D3;
`;

const ETD = styled.td`
    border: 1px solid black;
    min-width: ${props => props.minWidth};
`;


const EvaluationTable = ({
    etapas = [
        [
            {
                id: 1,
                nombre: "Tarea 1",
                puntaje_total: 10
            },
            {
                id: 2,
                nombre: "Tarea 2",
                puntaje_total: 10
            }
        ],
        [
            {
                id: 3,
                nombre: "Tarea 3",
                puntaje_total: 15
            }
        ]
    ],
    alumnos = [
        {
            id: 1,
            nombreAlumno: "Alumno 1",
            etapas: [[8, 5], [10]]
        }
    ]
}) => {

    const getIndexTexto = (index) => {
        if (index === 0) return "Primera"
        if (index === 1) return "Segunda"
        if (index === 2) return "Tercera"
        return "No implementado"
    }

    const getCalif = (e, etapaIndex) => {
        return 0
    }

    const getCalifFinal = (etapas)=>{
        return 0
    }

    const getTotal = (e) => {
        return e?.reduce((b,a)=>{return b+a})
    }

    return <>
        <ETable>
            <thead>
                <tr>
                    <ETH rowSpan={3}>Alumnos</ETH>
                    {
                        etapas.map((e, i) => {
                            return <ETH colSpan={e.length + 2} key={`Etapas${i}`}> {getIndexTexto(i)} Etapa </ETH>
                        })
                    }
                    <ETH rowSpan={3}>Calificacion Final</ETH>
                </tr>
                <tr>

                    {
                        etapas.map((e, i) => {
                            return <>
                                {
                                    e.map((ev) => {
                                        return <ETH key={`EEN${i}${ev.id}`}>{ev.nombre}</ETH>
                                    })
                                }
                                <ETH rowSpan={2} key={`ETP${i}`}>Total</ETH>
                                <ETH rowSpan={2} key={`EC${i}`}>Calificacion</ETH>
                            </>
                        })
                    }
                </tr>
                <tr>
                    {
                        etapas.map((e, i) => {
                            return <>
                                {
                                    e.map((ev) => {
                                        return <ETH key={`EEP${i}${ev.id}`}>{`P.T:  ${ev.puntaje_total}`}</ETH>
                                    })
                                }
                            </>
                        })
                    }
                </tr>
            </thead>
            <tbody>
                {
                    alumnos.map((a) => {
                        return <tr key={`AR${a.id}`}>
                            <ETD minWidth={`150px`} key={`AN${a.id}${a}`}>{`${a.nombreAlumno}`}</ETD>
                            {
                                a.etapas.map((e,i)=>{
                                    return <>
                                        {
                                            e.map((p,pi)=>{
                                                return <ETD key={`LEP${i},${pi}=${p}`}>{`${p}`}</ETD>
                                            })
                                        }
                                        <ETD key={`AETP${i},${a.id}`}>{`${getTotal(e)}`}</ETD>
                                        <ETD key={`AEC${i},${a.id}`}>{`${getCalif(e,i)}`}</ETD>
                                    </>
                                })
                            }
                            <ETD key={`ACF${a.id}`}>{`${getCalifFinal(a.etapas)}`}</ETD>
                        </tr>
                    })
                }
            </tbody>
        </ETable>
    </>
}

export default EvaluationTable