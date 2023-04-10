import React, { useState, useEffect } from 'react';
import {H1} from './componentsStyles/StyledModal';
import { Modal as BModal } from 'react-bootstrap';
import {Form} from './Form';

/*Estructura de datosModal

    datosModal = {
        header : titulo del modal
        text : si queremos agregar algun texto
        form : {
            inputs: inputs del form 
            buttons: botones del form
            info: info del form
        }
    }

*/

function Modal({datosModal, isOpen}){

    const [open, setOpen] = useState(isOpen ? isOpen : false);
    const [form, setForm] = useState(datosModal?.form ?? null); 
    useEffect(() => { setOpen(isOpen) }, [isOpen]);
    useEffect(() => { setForm(datosModal?.form) }, [datosModal]);

    const handleClose = () => {
        setOpen(false); 
    }

    return (
        <>

        {datosModal && (
            <BModal show={open} onHide={handleClose}>
                <BModal.Header closeButton onClick={handleClose}>
                    {datosModal?.header && <H1>{datosModal.header}</H1>}
                </BModal.Header>
                <BModal.Body>
                    {datosModal?.text && <div>{datosModal.text}</div>}
                    {datosModal?.form && <Form 
                            form={form}
                        ></Form>}
                </BModal.Body>
            </BModal>
        )}
        
        </>
    )

}

export default Modal;