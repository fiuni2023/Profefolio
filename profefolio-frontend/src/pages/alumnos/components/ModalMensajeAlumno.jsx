import React from 'react';
import styled from 'styled-components';
import TextButton from '../../../components/TextButton';
import { useClaseContext } from '../../clases/context/ClaseContext';
import { useGeneralContext } from '../../../context/GeneralContext';
import StudentHelper from '../helpers/StudentHelper'
import { toast } from 'react-hot-toast';

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
  z-index: 1100;
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
  const { colegio, getToken } = useGeneralContext()
  console.log(student)
  const addStudent = () => {
    let newStudent = { "colegioId": colegio, "alumnoId": student.id }
    console.log(newStudent)
    toast.promise(StudentHelper.addStudentToSchool(newStudent, getToken()), {
      loading: "Grardando Alumno...",
      success: "Alumno guardado",
      error: "Hubo un error al guardar el alumno, intente nuevamente.",
    })
      .then((res) => {
        console.log(res);
        onAdd()
        onCancel()
      }).catch((error) => {
        console.error(error);
        return error.message;
      })


    // toast.promise(SaveStudentInMySchool, {
    //   loading: "Grardando Alumno...",
    //   success: (data) =>{
    //     console.log(data);
    //       onAdd()
    //       onCancel()
    //     if (data.status === 200) {
    //       console.log("Entró");
    //     }
    //     return "Alumno guardado"},
    //   error: "Hubo un error al guardar el alumno, intente nuevamente.",
    // });

  }
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
              <TextButton buttonType={'accept'} enabled={true} onClick={addStudent} />
            </ButtonContainer>
          </ModalContent>
        </ModalContainer>
      )}
    </>
  );
};

export default ModalMensajeAlumno;