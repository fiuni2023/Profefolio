import React, { useState } from 'react';
import {H1} from "./componentsStyles/StyledModal";
import { Modal as BModal } from 'react-bootstrap';

function Modal({datosModal, isOpen}){
    const [open, setOpen] = useState(isOpen ? isOpen : false);
    
    React.useEffect(() => {
        setOpen(isOpen)
      }, [isOpen]);

    const handleClose = () => {
        setOpen(false); 
    }

    return (
        <>

        {datosModal && (
            <BModal show={open} onHide={handleClose}>
                <BModal.Header closeButton onClick={handleClose}>
                    {datosModal.header && <H1>{datosModal.header}</H1>}
                </BModal.Header>
                <BModal.Body>Here Lies The Modal Body</BModal.Body>
            </BModal>
        )}
        
        </>
    )

}

export default Modal;