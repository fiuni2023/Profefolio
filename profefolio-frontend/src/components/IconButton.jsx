import React, { useState } from 'react';
import StyledIconButton from './componentsStyles/StyledIconButton';
import TextButton from './TextButton';
import { HiOutlinePencil, HiOutlineTrash } from 'react-icons/hi';
import { IoIosArrowBack, IoIosArrowForward, IoIosClose } from 'react-icons/io';

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
  const [pressed, setPressed] = useState(false);
  const [confirmation, setConfirmation] = useState(false);
  let onConfirm;
  let iconMainColor= 'white';
  let iconColor= 'white';
  let icon;
  switch (buttonType) {
    case 'delete':
      onConfirm = onClick;
      onClick = () => setConfirmation(true);
      iconColor= '#D93D79';
      icon = <HiOutlineTrash  size={24} color={iconColor}/>;
      break;
    case 'edit':
      iconColor= '#5181D1';
      icon = <HiOutlinePencil size={24} color={iconColor}/>;
      break;
    case 'close':
      iconColor= 'black';
      icon = <IoIosClose size={26} color={iconColor}/>;
      iconMainColor= 'black';
      break;
    case 'arrowL':
      iconColor= '#A32A26';
      icon = <IoIosArrowBack size={26} color={iconColor}/>;
      break;
    case 'arrowR':
      iconColor= '#A32A26';
      icon = <IoIosArrowForward size={26} color={iconColor}/>;
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
        <StyledIconButton
          buttonType={buttonType}
          enabled={enabled}
          onClick={onClick}
          onMouseDown={() => setPressed(true)}
          onMouseUp={() => setPressed(false)}
        >
          {icon && React.cloneElement(icon, { color: pressed ? iconColor : iconMainColor })}
        </StyledIconButton>
      }
    </div>
  );
}
export default IconButton;