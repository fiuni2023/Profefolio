import React, { useState, useEffect } from 'react';
import {H1, Text, SHeader} from './componentsStyles/StyledModal';
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

    const handleClose = () => { setOpen(false); }
    
    return (
        <>

        {datosModal && (
            <BModal show={open} onHide={handleClose}>
                <SHeader closeButton onClick={handleClose}>
                    {datosModal?.header && <H1>{datosModal.header}</H1>}
                </SHeader>
                <BModal.Body>
                    {datosModal?.text && <Text>{datosModal.text}</Text>}
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