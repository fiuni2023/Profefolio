import React from 'react'
import IconButton from './Buttons/IconButton'
import TextButton from './Buttons/TextButton'

const TestPage = () => {
    const miFuncion=()=>{
        console.log("Editando")
    }
  return (
    <div>
        <div>

        <TextButton enabled={true} buttonType='accept' onClick={()=>console.log('Aceptando')} />
        <IconButton enabled={true} buttonType='edit' onClick={miFuncion}/>
        <IconButton enabled={true} buttonType='edit' onClick={miFuncion}/>
        <IconButton enabled={true} buttonType='arrowL' onClick={()=>console.log("Izquierda")}/>
        <IconButton enabled={true} buttonType='arrowR' onClick={()=>console.log("Derecha")}/>
        <IconButton enabled={true} buttonType='close' onClick={()=>console.log("Saliendo")}/>
        <IconButton enabled={true} buttonType='x' onClick={()=>console.log("Saliendo")}/>
        <TextButton enabled={true} buttonType='cancel' onClick={()=>console.log('Cancelando')} />
        <TextButton enabled={true} buttonType='save' onClick={()=>console.log('Guardando')} />
        <TextButton enabled={true} buttonType='confirm' onClick={()=>console.log('Confirmando')} />
        </div>
    </div>
  )
}

export default TestPage