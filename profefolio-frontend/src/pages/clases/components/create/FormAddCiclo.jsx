import React, { useState } from 'react';
import * as Yup from 'yup';
import { Formik } from "formik";
import { Col, Form } from 'react-bootstrap';
import axios from 'axios';
import APILINK from '../../../../components/link';
import { useGeneralContext } from '../../../../context/GeneralContext';
import { toast } from 'react-hot-toast';
import { SControl, SGroup, SLabel, SRow } from '../../../../components/componentsStyles/StyledForm';
import TextButton from '../../../../components/TextButton';


const FormAddCiclo = ({ handleClose, handleChangeListCiclos }) => {
    const { getToken } = useGeneralContext();

    const [isSend, setIsSend] = useState(false);


    const schema = Yup.object().shape({
        nombre: Yup.string().required("Campo requerido").max(32, "La longitud maxima es de 32 caracteres")
    });

    const onClose = () => {
        handleClose()
    }
    const handleSubmitCiclo = async (ciclo) => {

        const obj = {
            "nombre": ciclo.nombre
        }

        //console.log(obj)

        const result = await axios.post(`${APILINK}/api/ciclo`,
            obj,
            {
                headers: {
                    Authorization: `Bearer ${getToken()}`,
                }
            })

        if (result.status === 200) {
            setIsSend(false);
            onClose();
            handleChangeListCiclos(result.data)
            toast.success("Guardado exitoso.");
        } else {
            //console.log("Error: ", result.data)
            setIsSend(false);
            toast.error(`Error durante el guardado, verifique que el nombre no exista.`);
        }
    }

    return <>
        <Formik
            validationSchema={schema}
            onSubmit={handleSubmitCiclo}
            validateOnBlur
            initialValues={{
                nombre: ''
            }}

        >
            {({
                handleSubmit,
                handleChange,
                handleBlur,
                values,
                touched,
                isValid,
                errors,
                blur
            }) => {
                return <Form onSubmit={handleSubmit}>
                    <SRow className="mb-3">
                        <SGroup as={Col} md="7" className="position-relative" style={{ "margin": "4px" }}>
                            <SLabel>Ciclo Nuevo</SLabel>
                            <SControl disabled={isSend}
                                type="text"
                                placeholder="Ciclo Nuevo"
                                aria-describedby="inputGroupPrepend"
                                name="nombre"
                                value={values.nombre}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                isInvalid={!!errors.nombre && touched.nombre}

                            />
                            {errors.nombre && touched.nombre && <Form.Control.Feedback type="invalid" tooltip>
                                {errors.nombre}
                            </Form.Control.Feedback>}
                        </SGroup>
                        <SGroup as={Col} md="5" className="btn-gestion-ciclo">
                            <TextButton type="submit" enabled={!isSend} buttonType='save'>Guardar</TextButton>
                            <TextButton onClick={onClose} enabled={!isSend} buttonType='cancel'>Cancelar</TextButton>
                        </SGroup>
                    </SRow>
                </Form>
            }}


        </Formik>
        <style jsx="true">
            {
                `
                    .btn-gestion-ciclo{
                        display:flex;
                        align-items: flex-end;
                        justify-content: flex-end;
                        column-gap: 8px;
                    }

                    .btn-save {
                        background: #F0544F;
                        border: 1px solid #F0544F;
                    }

                    .btn-cancel {
                        background: #FDF0AA;
                        border: 1px solid #000;
                        color: black;
                    }

                    .btn-cancel:hover {
                        background: #FDF0D5;
                        border: 1px solid #AAA;
                        color: black;
                    }

                    .btn-save:hover {
                        background: #F05418;
                        border: 1px solid #F0544F;
                    }
                
                `
            }
        </style>

    </>

}

export default FormAddCiclo