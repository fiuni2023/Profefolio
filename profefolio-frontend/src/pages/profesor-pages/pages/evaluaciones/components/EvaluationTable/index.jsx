import React from "react";
import styled from "styled-components";
import { usePageContext } from "../../context/PageContext";
import { RxCross2, RxPlus } from 'react-icons/rx'
//import AddButton from '../AddButton'
import InvisibleInput from "./components/Invisibleinput";
import CreateEventModal from "./components/createEventmodal";
import DeleteEventConfirmationModal from "./components/deleteEventConfirmationModal";

const ETable = styled.table`
    border: 1px solid #DDDDDD;
    margin-top: 2vh;
    width: 100%;
`;

const ETH = styled.th`
    border: 1px solid #ababab;
    background-color: #DDDDDD;
    min-width: 150px;
`;

const ETD = styled.td`
    border: 1px solid #ababab;
    min-width: ${props => props.minWidth};
`;

const ButtonDivStyle = styled.button`
    width: 20px;
    padding: 1px;
    border-radius: 50%;
    background-color: #DDDDDD;
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
    const { handleDeleteCalification, handleEditCalification, handleEditCalificationNombre, handleEditCalificationPT } = functions
    const { setShowModal, setEtapaName, setModalDeleteFunction, setShowDeleteModal } = stateHandlers

    const getCalif = (e) => {
        if(e.length === 0) return 0
        let valor = e.map((ev) => { return ev.porcentaje_logrado }).reduce((b, a) => { return b + a })
        valor = valor / e.length
        return valor.toFixed(2)
    }

    const getCalifFinal = (etapas) => {
        let sumatoria = 0
        let valor = 0
        etapas.forEach(e => {
            if(e.length > 0){
                valor = e.map((ev) => { return ev.porcentaje_logrado }).reduce((b, a) => { return b + a })
                valor = valor / e.length
            }else{
                valor = 0
            }
            sumatoria = sumatoria + valor
            return e
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
                                    <ButtonDivStyle onClick={() => {handleDelegateCreateModal(e.etapa)}}><RxPlus /></ButtonDivStyle>
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
                                        e.etapas.map((ev,x) => {
                                            return <ETH key={`EEN${i}${ev.id}${x}`}>
                                                <InvisibleInput width="80%" value={ev.nombre} handleBlur={(text) => { handleEditCalificationNombre(ev.id, ev.puntaje_total, 0, text) }} />
                                                <ButtonDivStyle onClick={()=>{
                                                    setModalDeleteFunction({func: ()=>{handleDeleteCalification(ev.id, ev.puntaje_total, 0, ev.nombre)}})
                                                    setShowDeleteModal(true)
                                                }}>
                                                    <RxCross2 />
                                                </ButtonDivStyle>
                                            </ETH>
                                                
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
                                        e.etapas.map((ev,x) => {
                                            return <ETH key={`EEP${i}${ev.id}${x}`}>P.T: <InvisibleInput width="20%" value={ev.puntaje_total} handleBlur={(text) => { handleEditCalificationPT(ev.id, parseInt(text), 0, ev.nombre) }} /></ETH>
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
                                                    return <ETD key={`LEP${p.id}`}><InvisibleInput type="number" value={p.puntaje} max={p.puntaje_total} back={"white"} handleBlur={(text) => { handleEditCalification(p.id, p.puntaje_total, parseInt(text), p.nombre) }} /></ETD>
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
        <CreateEventModal />
        <DeleteEventConfirmationModal />
    </>
}

export default EvaluationTable