import React, { useState } from 'react';
import styled from 'styled-components';
import { Col, Container, Row, Form } from "react-bootstrap";

const types = ["text", "date", "password", "textarea"];
const check = ["checkbox", "radio"];

const Info = styled.div``;

function Form(inputs, buttons, info, onSubmit){

    /*  Formato del prop "inputs", es un arreglo de objetos, todas las opciones son opcionales, pero algunas son necesarias 
        para mostrar los inputs, por ejemplo, para mostrar un grupo de selects, se necesita que el input tenga un key value 

    inputs = [
        {    
            xs: valor del 1 al 12 que representa en ancho del input en una pantalla pequeñita en una base de 12
            sm: valor del 1 al 12 que representa en ancho del input en una pantalla pequeña en una base de 12
            md: valor del 1 al 12 que representa en ancho del input en una pantalla mediana en una base de 12
            lg: valor del 1 al 12 que representa en ancho del input en una pantalla grande en una base de 12
            key: valor unico
            label: label del input
            type: tipo de input: text, date, password, textarea, checkbox, radio, select
            placeholder: placeholder del input
            disabled: boolean que indica si esta desactivado, o se puede editar
            onChange: si es necesario, se puede mandar un callback para cuando el input cambia
            select: {
                default: valor por defecto, que se usa solo como placeholder
                options: [
                    {
                        value: valor del select, suele ser el id del elemento
                        text: lo que lee el usuario
                    }
                ]
            }
            checks: [
                {
                    id: id de la opcion
                    disabled: cuando la opcion esta desactivada
                    label: label de la opcion
                }
            ]

        }
    }]

    */

    return (
        <>
        <Container>    
            {info && <Info>{info}</Info>}
            {inputs && <Row>{inputs.map((input) => {
                <Col xs={input?.xs ?? 12} sm={input?.sm ?? 12} md={input?.md ?? 12} lg={input?.lg ?? 12} >
                    <Form.Group controlId={input?.key ?? input?.label ?? ""}>
                        {input?.label && <Form.Label>{input.label}</Form.Label>}
                        
                        {input?.type && types.includes(input.type) && 
                            <Form.Control 
                                onChange={input?.onChange ?? null}
                                type= {input.type}
                                placeholder={input?.placeholder ?? ""} 
                                disabled={input?.disabled ?? false} 
                                readOnly={input?.disabled ?? false}>      
                            </Form.Control>}
                        {input?.type == "select" && 
                            <Form.Select 
                                disabled={input?.disabled ?? false} 
                                onChange={input?.onChange ?? null}>
                                    {input?.select?.default && <option>input?.select?.default</option>}
                                    {input?.select?.options && input.select.options.map((option) => {
                                        <option value={option?.value}>{option?.text}</option>
                                    })}
                            </Form.Select>}
                        {input?.type && input?.key && checks.includes(input?.type) && input?.checks && input.checks.map((ckeck) => {
                            <Form.Check
                                name={input.key}
                                type={input.type}
                                disabled={check?.disabled ?? false}
                                id={check?.id}
                                label={check?.label ?? ""}>
                            </Form.Check>
                        })}

                        {input?.text && <Form.Text muted>{input.text}</Form.Text>}
                    </Form.Group>
                </Col>
            })}</Row>}
            {buttons && <div></div>}
        </Container>
        </>
    )


}

export default Form;