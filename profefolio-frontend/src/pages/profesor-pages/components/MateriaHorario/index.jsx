import React, { useState } from "react";
import { Button, ButtonGroup, Modal } from "react-bootstrap";
import { AiOutlinePlus } from "react-icons/ai";
import styled from "styled-components";
import HoraCatedraService from "../../services/HoraCatedraService";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { toast } from "react-hot-toast";
import { useModularContext } from "../../context";


const MateriaHorario = () => {

    const {getToken} = useGeneralContext()
    const {stateController} = useModularContext()
    const {materiaId} = stateController
    const token = getToken()

    const [show, setShow] = useState(false)
    const [fromHour, setFromHour]= useState("")
    const [toHour, setToHour]= useState("")
    const [L, setL] = useState(false)
    const [M, setM] = useState(false)
    const [X, setX] = useState(false)
    const [J, setJ] = useState(false)
    const [V, setV] = useState(false)

    const handleShow = () => {
        setShow(true)
    }
    
    const handleHide = () => {
        setShow(false)
    }
    
    
    const handleChangeDia = (dia) => {
        setL(dia === 1)
        setM(dia === 2)
        setX(dia === 3)
        setJ(dia === 4)
        setV(dia === 5)
    }

    const getDateString = () => {
        if(L) return "lunes"
        if(M) return "martes"
        if(X) return "miercoles"
        if(J) return "jueves"
        if(V) return "viernes"
        return false
    }
    
    const resetForm = () => {
        setL(false)
        setM(false)
        setX(false)
        setJ(false)
        setV(false)
        setFromHour("")
        setToHour("")
    }
    
    const handleCreate = async () => {
        if(getDateString() === false) return toast.error("Elija un dia")
        if(fromHour === "") return toast.error("Complete los campos")        
        if(toHour === "") return toast.error("Complete los campos")

        let created = {id:0}
        let body = {
            "inicio": fromHour,
            "fin": toHour
        }
        await HoraCatedraService.Post(body, token)
        .then(r=>{created = r.data})
        body = {
            "horaCatedraId": created.id,
            "materiaListaId": materiaId,
            "dia": getDateString()
        }
        await HoraCatedraService.Second(body, token)
        .then(r=>{
            toast.success("Creado con exito")
            setShow(false)
            resetForm()
        })
    }
    
    return <>
        <AddButton onClick={handleShow}>
            <AiOutlinePlus size={"25px"} />
        </AddButton>
        <Modal show={show} onHide={handleHide} centered>
            <Modal.Header closeButton>
                <Modal.Title>Crear Horario</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div className="d-flex flex-column align-items-start gap-3 w-100">
                    <Label>Dia:</Label>
                    <ButtonGroup className="me-2 w-100" >
                        <Button variant={`${L? `primary`:`secondary`}`} onClick={()=>{handleChangeDia(1)}} >L</Button> 
                        <Button variant={`${M? `primary`:`secondary`}`} onClick={()=>{handleChangeDia(2)}} >M</Button> 
                        <Button variant={`${X? `primary`:`secondary`}`} onClick={()=>{handleChangeDia(3)}} >X</Button>
                        <Button variant={`${J? `primary`:`secondary`}`} onClick={()=>{handleChangeDia(4)}} >J</Button>
                        <Button variant={`${V? `primary`:`secondary`}`} onClick={()=>{handleChangeDia(5)}} >V</Button>
                    </ButtonGroup>
                    <Label> Desde: </Label>
                    <div className="d-flex w-100 gap-2">
                        <TimeInput type="time" value={fromHour} onChange={(e)=>{setFromHour(e.target.value)}}/>
                    </div>
                    <Label> Hasta: </Label>
                    <div className="d-flex w-100 gap-2">
                        <TimeInput type="time" value={toHour} onChange={(e)=>{setToHour(e.target.value)}}/>
                    </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={handleCreate}>
                    Guardar
                </Button>
            </Modal.Footer>
        </Modal>
    </>
}

const AddButton = styled.button`
    width: 40px;
    height: 40px;
    padding: 0;
    color: white;
    background-color: #F0544F;
    border-radius: 10px;
    cursor: pointer;
    border: none;
&:hover {
    filter: brightness(0.95);
&:active {
    filter: brightness(0.8);
  }
`;

const Label = styled.label`
  font-size: 15px;
`;

const DateInput = styled.input`
    width: 100%;
    height: 35px;
    padding: 10px;
    border-radius: 10px;
    outline: none;
    border: 1px solid #DDDDDD;
`;

const TimeInput = styled.input`
    width: 100%;
    height: 35px;
    padding: 10px;
    border-radius: 10px;
    outline: none;
    border: 1px solid #DDDDDD;
`;

export default MateriaHorario