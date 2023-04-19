/**
 * Aqui se contendran componentes para caso de usos especiales como un input que bajo cierta entrada mostrara un otro fomulario 
 */

import React from 'react'

import { map } from "lodash";
import { Col } from 'react-bootstrap';
import { SGroup, SLabel, SSelect } from './componentsStyles/StyledForm';

const SpecialSelect = ({ name = "", specialOption="", className = "", isSend = false, inCreation = false, setInCreation = () => { }, col = 12, values = null, /* validateSelect = () => { },*/
    handleChange = () => { }, handleBlur = () => { }, errors = null, touched = null, data = [], alternativeForm }) => {

    const CREATE = "___option____create____select";

    const validateSelect = (e) => {
        if (CREATE === e.target.value) {
            setInCreation();
            e.target.value = "";
        }
    }
    return <>
        {
            !inCreation
                ?
                <SGroup as={Col} md={col} className={className} >
                    <SLabel>Ciclo</SLabel>

                    <SSelect aria-label="Default select" disabled={isSend}
                        name={name}
                        value={values}
                        onChange={(e) => {
                            validateSelect(e);
                            return handleChange(e);
                        }}
                        onBlur={handleBlur}
                        isInvalid={!!errors && touched}
                    >
                        <option value={""} disabled>Seleccione una opción</option>
                        <option className="option-create" value={CREATE}>{specialOption}</option>
                        {data && map(data, (c) => <option key={c.id} value={c.id}>{c.nombre}</option>)}
                    </SSelect>

                </SGroup>
                :
                <>{alternativeForm}</>
        }
        <style jsx="true">
            {`
                .option-create{
                    background: #e59c68;
                    color: white;
                }
            `}
        </style>
    </>
}

export { SpecialSelect }
