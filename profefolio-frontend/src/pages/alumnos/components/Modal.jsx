import React, { useState, useEffect } from 'react';
import { Text, SHeader } from '../../../components/componentsStyles/StyledModal';
import { Modal as BModal } from 'react-bootstrap';
import { Form } from '../../../components/Form';
import styled from 'styled-components';

const H1 = styled.h1`
    font-family: 'Poppins';
    font-style: normal;
    font-weight: 600;
    font-size: 1.8em;
    color: #282828;
`;

function Modal({ datosModal, isOpen, handleClose }) {
    const [form, setForm] = useState(datosModal?.form ?? null);
    useEffect(() => { setForm(datosModal?.form) }, [datosModal]);
    ////Debo quitar después de preguntar
    return (
        <>
            {datosModal && (
                <BModal show={isOpen} onHide={handleClose}>
                    <SHeader closeButton onClick={handleClose}>
                        {datosModal?.header && <H1>{datosModal.header}</H1>}
                    </SHeader>
                    <BModal.Body>
                        {datosModal?.text && <Text>{datosModal.text}</Text>}
                        {datosModal?.form && <Form form={form}></Form>}
                    </BModal.Body>
                </BModal>
            )}

        </>
    )

}

export default Modal;