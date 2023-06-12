import React from "react";
import styled from "styled-components";
import { usePageContext } from "../../context/PageContext";
import { RxCross2, RxPlus } from 'react-icons/rx'
import AddButton from '../AddButton'
import InvisibleInput from "./components/Invisibleinput";
import CreateEventModal from "./components/createEventmodal";

const ETable = styled.table`
    border: 1px solid black;
    margin-top: 2vh;
    width: 100%;
`;

const ETH = styled.th`
    border: 1px solid black;
    background-color: #C6D8D3;
    min-width: 150px;
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

const SideScrillingDiv = styled.div`
    max-width: 1460px;
    overflow: auto;
    max-height: 80%;

`;


const EvaluationTable = () => {

    const { dataSet, functions, stateHandlers } = usePageContext()
    const { evalAlumnos, etapas } = dataSet
    // eslint-disable-next-line no-unused-vars
    const { handleAddEtapa, handleDeleteEtapa, handleEditEventName, handleEditCalification } = functions
    const { setShowModal, setEtapaName } = stateHandlers

    const getCalif = (e) => {
        let valor = e.map((ev) => { return ev.porcentaje_logrado }).reduce((b, a) => { return b + a })
        valor = valor / e.length
        return valor.toFixed(2)
    }

    const getCalifFinal = (etapas) => {
        let sumatoria = 0
        let valor = 0
        etapas.forEach(e => {
            valor = e.map((ev) => { return ev.porcentaje_logrado }).reduce((b, a) => { return b + a })
            valor = valor / e.length
            sumatoria += valor
        });
        sumatoria = sumatoria / etapas.length
        return sumatoria.toFixed(2)
    }

    const getTotal = (e = []) => {
        if (e.length === 0) return 0
        return e?.map((ev) => { return ev.puntaje }).reduce((b, a) => { return b + a })
    }

    const handleDelegateCreateModal = (name) => {
        setEtapaName(name)
        setShowModal(true)
    }

    return <>
        <SideScrillingDiv >
            <ETable>
                <thead>
                    <tr>
                        <ETH rowSpan={3}>Alumnos</ETH>
                        {
                            etapas.map((e, i) => {
                                return <ETH colSpan={e.etapas.length + 2} key={`Etapas${i}`}> {e.etapa}
                                    <ButtonDivStyle onClick={() => { handleDelegateCreateModal(e.etapa) }}><RxPlus /></ButtonDivStyle>
                                    <ButtonDivStyle onClick={() => { handleDeleteEtapa(i) }}><RxCross2 /></ButtonDivStyle>
                                </ETH>
                            })
                        }
                        <ETH rowSpan={3}>Porcentaje Final</ETH>
                    </tr>
                    <tr>
                        {
                            etapas.map((e, i) => {
                                return <>
                                    {
                                        e.etapas.map((ev, x) => {
                                            return <ETH key={`EEN${i}${ev.id}${x}`}><InvisibleInput value={ev.nombre} handleBlur={(text) => { /*handleEditEventName(ev.id, text)*/ }} /></ETH>
                                        })
                                    }
                                    <ETH rowSpan={2} key={`ETP${i}`}>Total</ETH>
                                    <ETH rowSpan={2} key={`EC${i}`}>Porcentaje</ETH>
                                </>
                            })
                        }
                    </tr>
                    <tr>
                        {
                            etapas.map((e, i) => {
                                return <>
                                    {
                                        e.etapas.map((ev, x) => {
                                            return <ETH key={`EEP${i}${ev.id}${x}`}>{`P.T:  ${ev.puntaje_total}`}</ETH>
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
                                <ETD minWidth={`250px`} key={`AN${a.id}${a}`}>{`${a.nombreAlumno}`}</ETD>
                                {
                                    a.etapas.map((e, i) => {
                                        return <>
                                            {
                                                e.map((p) => {
                                                    return <ETD key={`LEP${p.id}`}><InvisibleInput type="number" value={p.puntaje} max={p.puntaje_total} back={"white"} handleBlur={(text) => { handleEditCalification(p.id, p.puntaje_total, parseInt(text)) }} /></ETD>
                                                })
                                            }
                                            <ETD key={`AETP${i},${a.id}`}>{`${getTotal(e)}`}</ETD>
                                            <ETD key={`AEC${i},${a.id}`}>{`${getCalif(e)}`}</ETD>
                                        </>
                                    })
                                }
                                <ETD key={`ACF${a.id}`}>{`${getCalifFinal(a.etapas)}`}</ETD>
                            </tr>
                        })
                    }
                </tbody>
            </ETable>
        </SideScrillingDiv>
        <AddButton onClick={handleAddEtapa} />
        <CreateEventModal />
    </>
}

export default EvaluationTable