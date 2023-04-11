import React, { useEffect, useState } from 'react';
import { Col, Row, Form as RForm} from "react-bootstrap";
import {SContainer, SRow, Info, SControl, SLabel, SSelect, SCheck, SGroup} from "./componentsStyles/StyledForm";
import IconButton from "./IconButton";
import TextButton from "./TextButton";

const types = ["text", "date", "password", "textarea"];
const checks = ["checkbox", "radio", "switch"];
const inputs = ["text", "date", "password", "textarea", "select"];

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
        required: boolean si el input es requerido
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

function Form (form, onSubmit){
    const [inputs, setInputs] = useState(form?.form?.inputs ?? null); 
    const [buttons, setButtons] = useState(form?.form?.buttons ?? null);
    const [info, setInfo] = useState(form?.form?.info ?? null); 
    const [validated, setValidated] = useState(false);
    
    useEffect(() => {
        setInputs(form?.form?.inputs ?? null);
        setButtons(form?.form?.buttons ?? null);
        setInfo(form?.form?.info ?? null);
    } , [form])

    const handleSubmit = (event) => {
        const cform = event.currentTarget;
        event.preventDefault();
        console.log(event.target);
        if (cform.checkValidity() === false) {
          event.stopPropagation();
        }
    
        setValidated(true);
      };

    return (
        <>
        <SContainer>
            <RForm noValidate validated={validated}>     
                {info && <Info>{info}</Info>}
                {inputs && <Row>{inputs.map((input, i) => {
                    return (
                        <Col key={input?.key ?? i} xs={input?.xs ?? 12} sm={input?.sm ?? 12} md={input?.md ?? 12} lg={input?.lg ?? 12} >
                            <SGroup key={input?.key ?? i} controlId={input?.key ?? input?.label ?? ""}>
                                {input?.label && <SLabel key={input.label}>{input.label}{input?.required ? " *" : ""}</SLabel>}
                                
                                {input?.type && types.includes(input.type) && 
                                    <SControl 
                                        key={input?.key ?? i}
                                        onChange={input?.onChange ?? null}
                                        type= {input.type}
                                        placeholder={input?.placeholder ?? ""} 
                                        disabled={input?.disabled ?? false} 
                                        readOnly={input?.disabled ?? false}
                                        required={input?.required ?? false}
                                        >      
                                    </SControl>}
                                {input?.type === "select" && 
                                    <SSelect 
                                        required={input?.required ?? false}
                                        key={input?.key ?? ""}
                                        disabled={input?.disabled ?? false} 
                                        onChange={input?.onChange ?? null}>
                                            {input?.select?.default && <option key="default">{input?.select?.default}</option>}
                                            {input?.select?.options && input.select.options.map((option) => {
                                                return ( <option key={`${option?.value ?? i}-${option?.text ?? 'option'}`} value={option?.value}>{option?.text}</option> )
                                            })}
                                    </SSelect>}
                                {input?.type && inputs.includes(input.type) && input?.required && 
                                    <RForm.Control.Feedback type="invalid" tooltip> 
                                        {input?.invalidText ?? "Este campo es necesario"} 
                                    </RForm.Control.Feedback>
                                }
                                {input?.type && input?.key && checks.includes(input?.type) && input?.checks && 
                                    <Row>
                                        {input.checks.map((check) => {
                                            return (
                                                <Col key={check?.id ?? ""} xs={check?.xs ?? "auto"} sm={check?.sm ?? "auto"} md={check?.md ?? "auto"} lg={check?.lg ?? "auto"}>
                                                    <SCheck
                                                        key={check?.id ?? ""}
                                                        name={input.key}
                                                        type={input.type}
                                                        disabled={check?.disabled ?? false}
                                                        id={check?.id}
                                                        label={check?.label ?? ""}
                                                        value={check?.value ?? 0}
                                                        isInvalid={false}>
                                                    </SCheck>
                                                </Col>
                                            )
                                        })}
                                    </Row>
                                }

                                {input?.text && <RForm.Text key="additional" muted>{input.text}</RForm.Text>}
                            </SGroup>
                        </Col>
                    )
                })}</Row>}
                <SRow>
                {buttons && buttons.map((button, i) => {
                    return (
                        <div key={i}>
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
                                    onClick={button?.submit ? handleSubmit : button.onclick.action}
                                ></TextButton>
                            }
                        </div>
                    )
                })}
                </SRow>
            </RForm> 
        </SContainer>
        </>
    )


}

export {Form};