import React from "react";
import styled from "styled-components";
import { usePageContext } from "../../context/PageContext";
import { RxCross2, RxPlus } from 'react-icons/rx'
import AddButton from '../AddButton'
import InvisibleInput from "./components/Invisibleinput";

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

const ButtonDivStyle = styled.button`
    width: 20px;
    padding: 1px;
    border-radius: 50%;
    background-color: #C6D8D3;
    cursor: pointer;
    border: none;
&:hover {
    filter: brightness(0.95);
&:active {
    filter: brightness(0.8);
  }
`;


const EvaluationTable = () => {

    const { dataSet, functions } = usePageContext()
    const { evalAlumnos, etapas } = dataSet
    const { handleAddEtapa, handleDeleteEtapa, handleAddEvent, handleEditEventName } = functions

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

    const getTotal = (e = []) => {
        if(e.length === 0) return 0
        return e?.reduce((b,a)=>{return b+a})
    }

    return <>
        <ETable>
            <thead>
                <tr>
                    <ETH rowSpan={3}>Alumnos</ETH>
                    {
                        etapas.map((e, i) => {
                            return  <ETH colSpan={e.length + 2} key={`Etapas${i}`}> {getIndexTexto(i)} Etapa 
                                        <ButtonDivStyle onClick={()=>{handleAddEvent(i)}}><RxPlus/></ButtonDivStyle>
                                        <ButtonDivStyle onClick={()=>{handleDeleteEtapa(i)}}><RxCross2/></ButtonDivStyle>
                                </ETH>
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
                                        return <ETH key={`EEN${i}${ev.id}`}><InvisibleInput key={`EEN${i}${ev.id}Input`} value={ev.nombre} handleBlur={(text)=>{handleEditEventName(ev.id, text)}}/></ETH>
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
                    evalAlumnos.map((a) => {
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
        <AddButton onClick={handleAddEtapa} />
    </>
}

export default EvaluationTable