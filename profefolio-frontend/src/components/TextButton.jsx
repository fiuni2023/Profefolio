import StyledTextButton from './componentsStyles/StyledTextButton';

/*Este componente define el funcionamiento de los botones con iconos.
  Importa los estilos desde StyledIconButton.
  Recibe como props:
    buttonType [Tipo de botón: 'cancel', 'accept', 'save', 'confirm']
    onClick [Función que realizara al hacer click (TIENE QUE SER DE FLECHA ()=>)]
    enabled [Permite hacer o no click]

  Para utilizarlo llamar al componente de la siguiente como los siguientes ejemplos:
        <TextButton enabled={true} buttonType='cancel' onClick={()=>console.log('Cancelando')} />
        <TextButton enabled={true} buttonType='save' onClick={()=>console.log('Guardando')} />
        <TextButton enabled={true} buttonType='confirm' onClick={()=>console.log('Confirmando')} />
*/
function TextButton({ buttonType, onClick, enabled }) {
  let text;
  let type = "button";
  let isEnabled = enabled;
  switch (buttonType) {
    case 'cancel':
      text = 'Cancelar';
      break;
    case 'accept':
      text = 'Aceptar';
      break;
    case 'create':
      text = 'Crear';
      type = 'submit';
      break;
    case 'create2':
      text = 'Crear';
      break;
    case 'save-changes':
      text = 'Guardar Cambios';
      type = "submit";
      break;
    case 'save':
      text = 'Guardar';
      type = 'submit'
      break;
    case 'save2':
      text = 'Guardar';
      break;
    case 'confirm':
      text = 'Confirmar';
      break;
    case "danger":
      text = 'Borrar'
      break;
    default:
      text = null;
      isEnabled = false;
  }

  return (
    <div style={{ display: 'inline-block', verticalAlign: 'middle' }}>
      <StyledTextButton type={type} buttonType={buttonType} enabled={isEnabled} onClick={onClick}>
        {text}
      </StyledTextButton>
    </div>
  );
}
export default TextButton;