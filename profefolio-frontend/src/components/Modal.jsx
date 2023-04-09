import React, { useState } from 'react';
import styled from 'styled-components';
import { Modal as BModal } from 'react-bootstrap'

const H1 = styled.h1`
    font-family: 'Poppins';
    font-style: normal;
    font-weight: 600;
    font-size: 1.8em;`;


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