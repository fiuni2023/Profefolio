import React from "react";
import { Modal } from "react-bootstrap";
import TextButton from "../../../../../../../components/TextButton";
import { usePageContext } from "../../../context/PageContext";

const DeleteEventConfirmationModal = ({
    onAccept = () => {}
}) => {

    const { dataSet, stateHandlers } = usePageContext()
    const { modalDeleteFunction, showDeleteModal } = dataSet
    const { setShowDeleteModal } = stateHandlers

    return<>
        <Modal show={showDeleteModal} onHide={()=>{setShowDeleteModal(false)}}>
            <Modal.Header closeButton>
                <h3>Borrar la tarea / evento?</h3>
            </Modal.Header>
            <Modal.Body>
                <p>Desea borrar la tarea? <b>este acto es irreversible</b></p>
            </Modal.Body>
            <Modal.Footer>
                <TextButton buttonType={"accept"} enabled={true} onClick={()=>{
                        modalDeleteFunction.func()
                        setShowDeleteModal(false)
                    }} />
            </Modal.Footer>
        </Modal>
    </>
}

export default DeleteEventConfirmationModal