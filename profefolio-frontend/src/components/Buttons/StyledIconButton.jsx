import styled from 'styled-components';
/*Este componente define los estilos del botón utilizado en IconButton.
    Utiliza desde sus propiedades: buttonType para setear sus colores y enabled para setear si
    es editable o no.
*/
const StyledIconButton = styled.button`
  margin:5px;
  width: 40px;
  height: 40px;
  background-color: ${props =>
        props.buttonType === 'delete'
            ? '#E57BA4'
            : props.buttonType === 'arrowL' || props.buttonType === 'arrowR'
                ? '#F0544F'
                : props.buttonType === 'edit'
                    ? '#8DACE1'
                    : '#fff'};
  border: none;
  border-radius: 10px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: ${props => (props.enabled ? 'pointer' : 'not-allowed')};
  opacity: ${props => props.enabled ? 1 : 0.5};
  pointer-events: ${props => props.enabled ? 'auto' : 'none'};

  &:focus {
    outline: none;
  }
  &:hover {
    background-color: ${props =>
        props.buttonType === 'delete'
            ? '#D93D79'
            : props.buttonType === 'edit'
                ? '#5181D1'
                : props.buttonType === 'arrowL' || props.buttonType === 'arrowR'
                    ? '#A32A26'
                    : props.buttonType === 'close'
                        ? '#EEEEEE'
                        : '#fff'};
  }
  &:active {
    /*background-color:#fff;
    box-shadow: 0px 0px 3px rgba(0, 0, 0, 0.5);*/
    transform: scale(0.95);
  }
  img {
    width: 24px;
    height: 24px;
  }
`;

export default StyledIconButton;