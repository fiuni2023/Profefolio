import React from 'react';
import styled from 'styled-components';
import TextButton from '../../../components/TextButton';

const ModalContainer = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
`;

const ModalContent = styled.div`
  background-color: #fff;
  border-radius: 15px;
  padding: 10px;
  display: flex;
  max-width: 512px;
  flex-direction: column;
  align-items: center;
  font-size: 16px;
  text-align: left;
  > p{
    margin: 20px;
  }
`;

const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  width: 100%;
`;

const ModalMensajeAlumno = ({ student, isOpen, onAdd, onCancel }) => {
  return (
    <>
      {isOpen && (
        <ModalContainer>
          <ModalContent>
            <p>
              El alumno con documento: "{student.documento}" ya existe en la base de datos. <br />
              ¿Deseas traer sus datos y agregarlo a tu colegio?
            </p>
            <ButtonContainer>
              <TextButton buttonType={'cancel'} enabled={true} onClick={onCancel} />
              <TextButton buttonType={'accept'} enabled={true} onClick={() => onAdd(student)} />
            </ButtonContainer>
          </ModalContent>
        </ModalContainer>
      )}
    </>
  );
};

export default ModalMensajeAlumno;