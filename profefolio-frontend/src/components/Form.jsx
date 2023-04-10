import React, { useEffect, useState } from 'react';
import { Col, Container, Row, Form as RForm} from "react-bootstrap";
import {Info} from "./componentsStyles/StyledForm";
import IconButton from "./IconButton";
import TextButton from "./TextButton";

const types = ["text", "date", "password", "textarea"];
const checks = ["checkbox", "radio", "switch"];

/*  Formato del prop "inputs", es un arreglo de objetos, todas las opciones son opcionales, pero algunas son necesarias 
    para mostrar los inputs, por ejemplo, para mostrar un grupo de selects, se necesita que el input tenga un key value 

inputs = [
    {    
        xs: valor del 1 al 12 que representa en ancho del input en una pantalla pequeñita en una base de 12, por defecto es 12
        sm: valor del 1 al 12 que representa en ancho del input en una pantalla pequeña en una base de 12, por defecto es 12
        md: valor del 1 al 12 que representa en ancho del input en una pantalla mediana en una base de 12, por defecto es 12
        lg: valor del 1 al 12 que representa en ancho del input en una pantalla grande en una base de 12, por defecto es 12
        key: valor unico
        label: label del input
        type: tipo de input: text, date, password, textarea, checkbox, radio, select, switch
        placeholder: placeholder del input
        disabled: boolean que indica si esta desactivado, o se puede editar, por defecto es false
        onChange: { action: si es necesario, se puede mandar un callback para cuando el input cambia }
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
                xs: valor del 1 al 12 que representa en ancho del input en una pantalla pequeñita en una base de 12, por defecto es 12
                sm: valor del 1 al 12 que representa en ancho del input en una pantalla pequeña en una base de 12, por defecto es 12
                md: valor del 1 al 12 que representa en ancho del input en una pantalla mediana en una base de 12, por defecto es 12
                lg: valor del 1 al 12 que representa en ancho del input en una pantalla grande en una base de 12, por defecto es 12
                id: id de la opcion
                disabled: cuando la opcion esta desactivada, por defecto es false
                label: label de la opcion
            }
        ]

    }
}]

*/

/* Formato del prop buttons, es un arreglo de objetos
    buttons = [
        {
            style: tipo general de boton icon o text
            type: tipo especifico de boton: cancel, accept, etc
            onclick: { action: callback del boton } 
            enabled: cuando la opcion esta desactivada, por defecto es false
        }
    ]
*/

function Form (form){
    const [inputs, setInputs] = useState(form?.form?.inputs ?? null); 
    const [buttons, setButtons] = useState(form?.form?.buttons ?? null);
    const [info, setInfo] = useState(form?.form?.info ?? null); 
    
    useEffect(() => {
        setInputs(form?.form?.inputs ?? null);
        setButtons(form?.form?.buttons ?? null);
        setInfo(form?.form?.info ?? null);
    } , [form])

    return (
        <>
        <Container>    
            {info && <Info>{info}</Info>}
            {inputs && <Row>{inputs.map((input, i) => {
                return (
                    <Col key={input?.key ?? i} xs={input?.xs ?? 12} sm={input?.sm ?? 12} md={input?.md ?? 12} lg={input?.lg ?? 12} >
                        <RForm.Group key={input?.key ?? i} controlId={input?.key ?? input?.label ?? ""}>
                            {input?.label && <RForm.Label key={input.label}>{input.label}</RForm.Label>}
                            
                            {input?.type && types.includes(input.type) && 
                                <RForm.Control 
                                    key={input?.key ?? i}
                                    onChange={input?.onChange ?? null}
                                    type= {input.type}
                                    placeholder={input?.placeholder ?? ""} 
                                    disabled={input?.disabled ?? false} 
                                    readOnly={input?.disabled ?? false}>      
                                </RForm.Control>}
                            {input?.type === "select" && 
                                <RForm.Select 
                                    key={input?.key ?? ""}
                                    disabled={input?.disabled ?? false} 
                                    onChange={input?.onChange ?? null}>
                                        {input?.select?.default && <option key="default">{input?.select?.default}</option>}
                                        {input?.select?.options && input.select.options.map((option) => {
                                            return ( <option key={`${option?.value ?? i}-${option?.text ?? 'option'}`} value={option?.value}>{option?.text}</option> )
                                        })}
                                </RForm.Select>}
                            {input?.type && input?.key && checks.includes(input?.type) && input?.checks && 
                            <Row>
                                {input.checks.map((check) => {
                                    return (
                                        <Col key={check?.id ?? ""} xs={check?.xs ?? "auto"} sm={check?.sm ?? "auto"} md={check?.md ?? "auto"} lg={check?.lg ?? "auto"}><RForm.Check
                                            key={check?.id ?? ""}
                                            name={input.key}
                                            type={input.type}
                                            disabled={check?.disabled ?? false}
                                            id={check?.id}
                                            label={check?.label ?? ""}>
                                        </RForm.Check></Col>
                                    )
                                })}
                            </Row>
                            }

                            {input?.text && <RForm.Text key="additional" muted>{input.text}</RForm.Text>}
                        </RForm.Group>
                    </Col>
                )
            })}</Row>}
            <Row>
            {buttons && buttons.map((button, i) => {
                return (
                    <Col key={i}>
                        {button?.style === "icon" && 
                            <IconButton
                                key={i} 
                                enabled={button?.enabled ?? false}
                                buttonType={button.type}
                                onClick={button.onclick.action}
                            ></IconButton>}
                        {button?.style === "text" && 
                            <TextButton
                                key={i} 
                                enabled={button?.enabled ?? true}
                                buttonType={button.type}
                                onClick={button.onclick.action}
                            ></TextButton>
                        }
                    </Col>
                )
            })}
            </Row>
        </Container>
        </>
    )


}

export {Form};