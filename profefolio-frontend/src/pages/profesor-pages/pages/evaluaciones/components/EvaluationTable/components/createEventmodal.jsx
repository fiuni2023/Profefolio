import React from "react";
import { Modal } from "react-bootstrap";
import TextButton from "../../../../../../../components/TextButton";
import styled from "styled-components";
import { usePageContext } from "../../../context/PageContext";

const StyledInput = styled.input`
    height: 40px;
    border-radius: 10px;
    outline: none;
    border: 1px solid #DDDDDD;
    padding: 5px;
    &:hover{
        filter: brightness(0.9);
    }
`

const StyledSelect = styled.select`
    appearance: none;
    height: 40px;
    border-radius: 10px;
    outline: none;
    border: 1px solid #DDDDDD;
    padding: 5px;
    &:hover{
        filter: brightness(0.9);
    }
`

const CreateEventModal = () => {

    const { dataSet, functions, stateHandlers } = usePageContext()
    const { newEvalName, newEvalPoint, newEvalType, showModal } = dataSet
    const { handleAddEvent, doFetch } = functions
    const { setShowModal, setNewEvalName, setNewEvalPoint, setNewEvalType } = stateHandlers

    const handleHide = () => {
        setShowModal(false)
        setNewEvalName("")
        setNewEvalPoint("")
        setNewEvalType("Examen")
        doFetch()
    }

    return <>
        <Modal centered show={showModal} onHide={handleHide} >
            <Modal.Header closeButton>
                <h3>Crear Nueva Evaluación</h3>
            </Modal.Header>
            <Modal.Body>
                <div className="w-100 d-flex flex-column gap-2">
                    <label>Nombre:</label>
                    <StyledInput type="text" value={newEvalName} onChange={(e)=>{setNewEvalName(e.target.value)}}></StyledInput>
                    <label>Puntaje:</label>
                    <StyledInput type="number" value={newEvalPoint} onChange={(e)=>{setNewEvalPoint(e.target.value)}}></StyledInput>
                    <label>Tipo:</label>
                    <StyledSelect value={newEvalType} onChange={(e)=>{setNewEvalType(e.target?.value)}} >
                        <option value={"Examen"}> Exámen </option>
                        <option value={"Parcial"}> Parcial </option>
                        <option value={"Prueba sumatoria"}> Prueba Sumatoria</option>
                        <option value={"Evento"}> Otros </option>
                    </StyledSelect>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <TextButton buttonType={"accept"} enabled={true} onClick={()=>{
                    handleAddEvent()
                    handleHide()
                }} />
            </Modal.Footer>
        </Modal>
    </>
}

export default CreateEventModal