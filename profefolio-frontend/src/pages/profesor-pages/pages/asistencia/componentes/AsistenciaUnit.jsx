import React, { useState } from "react";
import { Modal } from "react-bootstrap";
import styled from "styled-components";
import TextButton from "../../../../../components/TextButton";
import AsistenciaService from "../services/AsistenciaService";
import { useModularContext } from "../../../context";
import { useGeneralContext } from "../../../../../context/GeneralContext";
import { AiOutlineInfoCircle } from "react-icons/ai";

const InvisibleSelect = styled.select`
    width: 40px;
    outline: none;
    border: none;
    appearance: none;
    background-color: #FFFFFF;
    text-align:center;
    &:hover{
        filter: brightness(0.8);
    }
`;


const AsistenciaUnit = ({
    fecha,
    fetchFunc = () => {},
    disabled = false
}) => {

    const {getToken} = useGeneralContext()
    const token = getToken()
    const {stateController} = useModularContext()
    const {materiaId} = stateController

    const [textoJustificativo, setTextoJustificativo] = useState("")
    const [showModal, setShowModal] = useState(false)

    const evaluateChange = (value = "A") => {
        let body = fecha
        switch (value) {
            case "A":
                body = {
                    "id": fecha.id,
                    "fecha": fecha.fecha,
                    "estado": "A",
                    "accion": "U",
                    "observacion": ""
                }
                handleEdit([body])
                break;
            case "P":
                body = {
                    "id": fecha.id,
                    "fecha": fecha.fecha,
                    "estado": "P",
                    "accion": "U",
                    "observacion": ""
                }
                handleEdit([body])
                break;
            case "J":
                setShowModal(true)
                break;
            default:
                break;
        }
    }

    const doEditForJ = () => {
        let body = {
            "id": fecha.id,
            "fecha": fecha.fecha,
            "estado": "J",
            "accion": "U",
            "observacion": textoJustificativo
        }
        setTextoJustificativo("")
        handleEdit([body])
        setShowModal(false)
    }

    const handleEdit = (body) => {
        AsistenciaService.updateAsistencia(materiaId, body, token)
        .then((r)=>{fetchFunc()})
    }

    return <>
        <div className="d-flex align-items-center gap-2">
            <InvisibleSelect disabled={disabled} className="HideDefault" defaultValue={fecha.estado} onChange={(event) => evaluateChange(event.target.value)}>
                <option value="A">A</option>
                <option value="P">P</option>
                <option value="J">J</option>
            </InvisibleSelect>
            { fecha.observacion !== "" &&
                <span className="d-inline-block" tabindex="0" data-bs-toggle="tooltip" title={`${fecha.observacion}`}>
                    <AiOutlineInfoCircle size={15} />
                </span>
            }
        </div>
        <Modal show={showModal} onHide={()=>{setShowModal(false)}} centered>
            <Modal.Header closeButton>
                Agregar Justificativo
            </Modal.Header>
            <Modal.Body>
                <div className="d-flex flex-column w-100 ">
                    <label>Justificativo:</label>
                    <textarea value={textoJustificativo} onChange={(e)=>{setTextoJustificativo(e.target?.value)}}></textarea>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <TextButton buttonType={"accept"} enabled={textoJustificativo !== ""} onClick={doEditForJ} />
            </Modal.Footer>
        </Modal>
    </>
}

export default AsistenciaUnit