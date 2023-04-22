import React from 'react'
import TextButton from './TextButton'
import IconButton from './IconButton'
import {Form} from './Form'
import { FormExample } from './Example Data/ExampleForm'
import styled from 'styled-components'

const Div = styled.div`
  margin-top: 50px; 
  width: 50%;
`;

const TestPage = () => {
  const miFuncion = () => {
    console.log("Editando")
  }



  return (
    <div>
      <div>

        <TextButton enabled={true} buttonType='accept' onClick={() => console.log('Aceptando')} />
        <IconButton enabled={true} buttonType='edit' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='delete' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='edit2' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='delete2' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='arrowL' onClick={() => console.log("Izquierda")} />
        <IconButton enabled={true} buttonType='arrowR' onClick={() => console.log("Derecha")} />
        <IconButton enabled={true} buttonType='close' onClick={() => console.log("Saliendo")} />
        <TextButton enabled={true} buttonType='cancel' onClick={() => console.log('Cancelando')} />
        <TextButton enabled={true} buttonType='save' onClick={() => console.log('Guardando')} />
        <TextButton enabled={true} buttonType='confirm' onClick={() => console.log('Confirmando')} />

      </div>
      <Div>
        <Form form={FormExample}></Form>
      </Div>
    </div>
  )
}

export default TestPage