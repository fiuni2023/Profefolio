import React, { useState } from 'react';
import StyledIconButton from './StyledIconButton';
import TextButton from './TextButton';

/*Este componente define el funcionamiento de los botones con iconos.
  Importa los estilos desde StyledIconButton.
  Recibe como props:
    buttonType [Tipo de botón: 'delete', 'edit', 'close', 'arrowL', 'arrowR']
    onClick [Función que realizara al hacer click (TIENE QUE SER DE FLECHA ()=>)]
    enabled [Permite hacer o no click]

  Para utilizarlo llamar al componente de la siguiente como los siguientes ejemplos:
        <IconButton enabled={true} buttonType='edit' onClick={miFuncion}/>
        <IconButton enabled={true} buttonType='delete' onClick={()=>console.log("Borrando")}/>
        <IconButton enabled={true} buttonType='close' onClick={()=>console.log("Saliendo")}/>
*/
function IconButton({ buttonType, onClick, enabled }) {
  const [confirmation, setConfirmation] = useState(false);
  let onConfirm;
  let icon;
  switch (buttonType) {
    case 'delete':
      icon = '/icons/trash.svg';
      onConfirm = onClick;
      onClick = () => setConfirmation(true);
      break;
    case 'edit':
      icon = '/icons/pen.svg';
      break;
    case 'close':
      icon = '/icons/cross.svg';
      break;
    case 'arrowL':
      icon = '/icons/arrowL.svg';
      break;
    case 'arrowR':
      icon = '/icons/arrowR.svg';
      break;
    default:
      icon = null;
  }

  return (
    <div style={{ display: 'inline-block', verticalAlign: 'middle' }}>
      {confirmation ?
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <TextButton buttonType='cancel' enabled={true} onClick={() => setConfirmation(false)} />
          <TextButton buttonType='confirm' enabled={true} onClick={onConfirm} />
          <p style={{ color: '#A32A26', margin: 0 }}>ESTA ACCIÓN ES IRREVERSIBLE POR LO QUE NECESITA CONFIRMAR.</p>
        </div> :
        <StyledIconButton buttonType={buttonType} enabled={enabled} onClick={onClick}>
          {icon && <img src={icon} alt={`${buttonType} icon`} />}
        </StyledIconButton>
      }
    </div>
  );
}
export default IconButton;