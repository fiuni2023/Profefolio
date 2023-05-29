import styled from 'styled-components';

/*Este componente define los estilos del botón utilizado en TextButton.
    Utiliza desde sus propiedades: buttonType para setear sus colores y enabled para setear si
    es editable o no.
*/
const StyledTextButton = styled.button`
  font-weight: 600;
  width: 124px;
  height: 40px;
  border: none;
  border-radius: 5px;
  font-size: 20px;
  margin:5px;
  cursor: ${props => props.enabled ? 'pointer' : 'not-allowed'};
  opacity: ${props => props.enabled ? 1 : 0.5};
  pointer-events: ${props => props.enabled ? 'auto' : 'none'};

  &:active {
    transform: scale(0.95);
  }
  ${props => {
    switch (props.buttonType) {
      case 'save':
        return `
          background-color: #8DACE1;
          color: #fff
          ;&:hover {
            background-color: #5181D1;
          }
        `;
      case 'create':
        return `
            background-color: #8DACE1;
            color: #fff
            ;&:hover {
              background-color: #5181D1;
            }
          `;
      case 'save-changes':
        return `
              background-color: #8DACE1;
              color: #fff;
              width: 200px;
              height: 35px;
              ;&:hover {
                background-color: #5181D1;
              }
            `;
      case 'cancel':
        return `
          background-color: #E57BA4;
          color: #fff;

          &:hover {
            background-color: #D93D79;
          }
        `;
      case 'accept':
        return `
          background-color: #59C8A4;
          color: #fff;
          &:hover {
            background-color: #24B787;
          }
        `;
      case 'confirm':
        return `
          background-color: #F0544F;
          color: #fff;
  
          &:hover {
            background-color: #A32A26;
          }
        `;
      case 'danger':
        return `
          background-color: #F01F18;
          color: #fff;
  
          &:hover {
            background-color: #EB231C;
          }
        `;
      case 'yes':
        return `
          background-color: #59C8A4;
          color: #fff;
          &:hover {
            background-color: #24B787;
          }
        `;
      case 'no':
        return `
          background-color: #E57BA4;
          color: #fff;

          &:hover {
            background-color: #D93D79;
          }
        `;    
      default:
        return `
          background-color: #ccc;
          color: #333;
          cursor: not-allowed;
          &:hover {
            background-color: #ddd;
          }
        `;
    }
  }}
`;

export default StyledTextButton;