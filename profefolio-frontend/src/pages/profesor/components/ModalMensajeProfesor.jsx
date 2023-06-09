import React from 'react';
import TextButton from '../../../components/TextButton';
import { useGeneralContext } from '../../../context/GeneralContext';
import ProfeHelper from '../helpers/ProfeHelper'
import { toast } from 'react-hot-toast';
import {ButtonContainer, ModalContent, ModalContainer} from "../components/style/StyledModalMensaje";


const ModalMensajeProfesor= ({ profesor, isOpen, onAdd, onCancel, onSuccess}) => {
  const { getColegioId, getToken } = useGeneralContext()
  const colegioId = getColegioId()

  const addProfesor = () => {
    let newProfesor = { "colegioId": colegioId, "profesorId": profesor.id }
    toast.promise(ProfeHelper.addProfesorToSchool(newProfesor, getToken()), {
      loading: "Grardando Profesor...",
      success: "Profesor guardado",
      error: "Hubo un error al guardar el profesor, intente nuevamente.",
    })
      .then(() => {
        onAdd()
        onCancel()
        onSuccess()
      }).catch((error) => {
        return error.message;
      })

  }
  return (
    <>
      {isOpen && (
        <ModalContainer>
          <ModalContent>
            <p>
              El profesor con documento: "{profesor.documento}" ya existe en la base de datos. <br />
              ¿Deseas traer sus datos y agregarlo a tu colegio?
            </p>
            <ButtonContainer>
              <TextButton buttonType={'cancel'} enabled={true} onClick={onCancel} />
              <TextButton buttonType={'accept'} enabled={true} onClick={addProfesor} />
            </ButtonContainer>
          </ModalContent>
        </ModalContainer>
      )}
    </>
  );
};

export default ModalMensajeProfesor;