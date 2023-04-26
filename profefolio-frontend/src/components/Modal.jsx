import React from 'react';
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

function Modal({
    datosModal = {}, 
    show = false,
    onHide = ()=>{}
}){
    return (
        <>
        {datosModal && (
            <BModal show={show} onHide={onHide}>
                <SHeader closeButton onClick={onHide}>
                    {datosModal?.header && <H1>{datosModal.header}</H1>}
                </SHeader>
                <BModal.Body>
                    {datosModal?.text && <Text>{datosModal.text}</Text>}
                    {datosModal?.form && <Form 
                            form={datosModal?.form}
                        ></Form>}
                </BModal.Body>
            </BModal>
        )}
        
        </>
    )

}

export default Modal;