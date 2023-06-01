import React, { useState } from "react";
import { Modal } from "react-bootstrap";
import styled from "styled-components";
import TextButton from "../../../../../../components/TextButton";
import { useModularContext } from "../../../../context";
import DocumentosService from "../../service/DocumentosService";
import { useGeneralContext } from "../../../../../../context/GeneralContext";
import { toast } from "react-hot-toast";

const TextInput = styled.input`
    height: 40px;
    width: 100%;
    border-radius: 15px;
    border: 1px solid #DDDDDD;
    padding: 10px;
`;

const Flex = styled.div`
    display: flex;
    width: 100%;
    flex-direction: column;
    gap: 5px;
`;

const CreateDocumentoModal = ({
    show = false,
    onHide = () => {},
    doFetch = () => {}
}) => {
    
    const {stateController} = useModularContext()
    const {materiaId} = stateController
    const {getToken} = useGeneralContext()
    const token = getToken()

    const [nombre, setNombre] = useState("")
    const [link, setLink] = useState("")

    const handleCreateDocumento = async () => {
        let body = {
            "nombre": nombre,
            "enlace": link,
            "materiaListaId": materiaId
        }
        await DocumentosService.createDocumento(body, token)
        .then((r)=>{
            toast.success("Se ha creado con exito")
            doFetch()
            onHide()
        })
    }

    return <>
        <Modal show={show} onHide={onHide} centered>
            <Modal.Header closeButton>
                <h2 className="text-muted">CREAR DOCUMENTO</h2>
            </Modal.Header>
            <Modal.Body>
                <Flex>
                    <h5>Nombre</h5>
                    <TextInput type="text" value={nombre} onChange={(e)=>{setNombre(e.target.value)}} />
                    <h5>Link</h5>
                    <TextInput type="text" value={link} onChange={(e)=>{setLink(e.target.value)}}/>
                </Flex>
            </Modal.Body>
            <Modal.Footer>
                <TextButton buttonType={'save'} enabled={true} onClick={handleCreateDocumento}/>
            </Modal.Footer>
        </Modal>
    </>
}

export default CreateDocumentoModal