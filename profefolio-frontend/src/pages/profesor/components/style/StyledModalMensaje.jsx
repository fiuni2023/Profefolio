import styled from "styled-components";

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

export {ButtonContainer, ModalContent, ModalContainer};