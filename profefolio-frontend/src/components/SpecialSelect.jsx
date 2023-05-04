/**
 * Aqui se contendran componentes para caso de usos especiales como un input que bajo cierta entrada mostrara un otro fomulario,
 * Este select esta hecho de modo que este integrado con Formik ya que hay popiedades y funciones que son proveidas por los formularios hechos por formik
 */

import React from 'react'

import { map } from "lodash";
import { Col } from 'react-bootstrap';
import { SGroup, SLabel, SSelect } from './componentsStyles/StyledForm';

/**
 * 
 * @param {*} name el nombre que el compoente tiene a nivel de formulario
 * @param {*} nameLabel el nombre del label del select
 * @param {*} specialOption el texto que contendra el option que va a mostrar el otro componete
 * @param {*} className el className del grupo de label-select 
 * @param {*} isSend //valida si se esta realizando el envio de los datos del formulario, si es asi se bloquea el select
 * @param {*} inCreation //valida si el ciclo se esta creando, si es asi se bloquean los inputs
 * @param {*} setInCreation //cambia el valor del estado que verifica que un ciclos se esta creando, se usa cuando se selecciona la opcion especial 
 * @param {*} col // cantidad de columnas que ocupara el grupo label-select
 * @param {*} values // el estado que contendra el valor que se esta seleccionando en el select
 * @param {*} handleChange // funcion que se disparara cuando suceda un cambio en el select
 * @param {*} handleBlur // funcion que se disparara cuando el select puerda el foco
 * @param {*} errors // erores del input de select (estos errores viene dados ya en Formik)
 * @param {*} touched // estado que verifica que el select este seleccionado (esto tambien esta proveido por Formik)
 * @param {*} data // los datos que se van a cargar en los options
 * @param {*} alternativeForm // el Formulario o componente alternativo o secundario que se va a mostrar cuando se seleccione la opcion especial en el select
 * @returns 
 */
const SpecialSelect = ({ name = "", textLabel="", specialOption = "", className = "", isSend = false, inCreation = false, setInCreation = () => { }, col = 12, values = null, /* validateSelect = () => { },*/
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
                    <SLabel>{textLabel}</SLabel>

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
