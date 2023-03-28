import React, { useState } from 'react';
import * as Yup from 'yup';
import { Formik } from "formik";
import { Button, Col, Form, InputGroup, Row } from 'react-bootstrap';
import axios from 'axios';
import APILINK from '../../../../components/link';
import { useGeneralContext } from '../../../../context/GeneralContext';
import { toast } from 'react-hot-toast';


const FormAddCiclo = ({ handleClose, handleChangeListCiclos }) => {
    const { getToken } = useGeneralContext();

    const [isSend, setIsSend] = useState(false);    


    const schema = Yup.object().shape({
        nombre: Yup.string().required().max(32)
    });

    const onClose = () => {
        handleClose()
    }
    const handleSubmitCiclo = async (ciclo) => {
        
        const obj = {
            "nombre": ciclo.nombre
        }
        
        console.log(obj)

        const result = await axios.post(`${APILINK}/api/ciclo`, 
        obj, 
        {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })

        if(result.status === 200){
            setIsSend(false);
            onClose();
            handleChangeListCiclos(result.data)
            toast.success("Guardado exitoso.");
        } else{
            console.log("Error: ", result.data)
            setIsSend(false);
            toast.error(`Error: ${result.data.error}`);
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
                return <Form  onSubmit={handleSubmit}>
                    <Row className="mb-3">
                        <Form.Group as={Col} md="7" controlId="validationFormikNombreCicloNuevo">
                            <Form.Label>Ciclo Nuevo</Form.Label>
                            <InputGroup hasValidation>
                                <Form.Control disabled={isSend}
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
                            </InputGroup>
                        </Form.Group>
                        <Form.Group as={Col} md="5" className="btn-gestion-ciclo">
                            <Button className="btn-save" type="submit" disabled={isSend}>Guardar</Button>
                            <Button className="btn-cancel" onClick={onClose} disabled={isSend}>Cancelar</Button>
                        </Form.Group>
                    </Row>
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