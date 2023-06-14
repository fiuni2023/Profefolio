import React from 'react'
import TextButton from './TextButton'
import IconButton from './IconButton'
import {Form} from './Form'
import { FormExample } from './Example Data/ExampleForm'
import styled from 'styled-components'
import { Container} from 'react-bootstrap'
import { Separator } from './componentsStyles/StyledDashComponent'
import { SRow } from './componentsStyles/StyledForm'
import { colegios, clases, materias, infoClase, graficos } from './Example Data/ExampleCards'
import Card from './Card'
import ScrollListAlumnosExample from './Example Data/ScrollListAlumnosExample'

const Div = styled.div`
  margin-top: 50px; 
  width: 50%;
`;


const TestPage = () => {
  const miFuncion = () => {
    
  }
  return (
    <div>
      <div>
        <TextButton enabled={true} buttonType='accept' onClick={() => console.log('Aceptando')} />
        <IconButton enabled={true} buttonType='edit' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='delete' onClick={miFuncion} />
        <IconButton enabled={true} buttonType='arrowL' onClick={() => console.log("Izquierda")} />
        <IconButton enabled={true} buttonType='arrowR' onClick={() => console.log("Derecha")} />
        <IconButton enabled={true} buttonType='close' onClick={() => console.log("Saliendo")} />
        <TextButton enabled={true} buttonType='cancel' onClick={() => console.log('Cancelando')} />
        <TextButton enabled={true} buttonType='save' onClick={() => console.log('Guardando')} />
        <TextButton enabled={true} buttonType='confirm' onClick={() => console.log('Confirmando')} />
        <TextButton enabled={true} buttonType='save-changes' onClick={() => console.log('Confirmando')} />

      <Div>
        <Form form={FormExample}></Form>
      </Div>
      </div>


      <Container>
           
            <SRow>
              {colegios.map(element => {
                if(element?.goto) return <Card cardInfo={element}></Card>
                else return 0
              })}
            </SRow>
            <SRow>
              {clases.map(element => {
                if(element?.goto) return <Card cardInfo={element}></Card>
                else return 0
              })}
            </SRow>
            <SRow>
              {materias.map(element => {
                if(element?.goto) return <Card cardInfo={element}></Card>
                else return 0
              })}
            </SRow>
            <SRow>
              {infoClase.map(element => {
                if(element?.goto) return <Card cardInfo={element}></Card>
                else return 0
              })}
            </SRow>
            
                {graficos.map(element => {
                  return <SRow><Card cardInfo={element}></Card></SRow>
                })}
                <Separator></Separator>
            

        </Container>
      <ScrollListAlumnosExample />
    </div>
  )
}

export default TestPage