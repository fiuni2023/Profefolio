import { Button, Col, Form, FormGroup, InputGroup, Modal, Row } from "react-bootstrap";
import * as Yup from 'yup';
import { Formik } from "formik";
import { map } from "lodash";
import { useEffect, useRef, useState } from "react";
import useAxiosGet from "../../hooks/useAxiosGet";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { BsPlusCircleFill } from "react-icons/bs";
import { MdCancel } from "react-icons/md";
import { IoSaveSharp } from "react-icons/io5";
import FormAddCiclo from "./FormAddCiclo";

const ModalCreateClase = ({ title = "My Modal", handleClose = () => { }, show = false, triggerState = () => { } }) => {
    const CREATE_CICLO = "___option____create____ciclo";

    const { getToken, cancan, verifyToken } = useGeneralContext();

    const [addCiclos, setAddCiclos] = useState(false);


    const onSetAddCiclos = () => {
        setAddCiclos(!addCiclos)
    }

    const validateSelect = (e) => {
        if (CREATE_CICLO === e.target.value) {
            onSetAddCiclos();
            e.target.value = "";
        }
    }

    const [data, loading, error, setData] = useAxiosGet(`api/ciclo`, getToken());



    console.log(data);

    useEffect(() => {
        verifyToken();
    }, [show])

    const addCicloList = (ciclo) => {
        setData([...data, ciclo])
    }

    const schema = Yup.object().shape({
        nombre: Yup.string().required(),
        turno: Yup.string().required(),
        ciclo: Yup.string().required(),
        anho: Yup.number().required().min(1951).max(new Date().getFullYear() + 1),
    });

    const handleSubmit = (e) => {
        //e.preventDefault();
        console.log(e)
    }

    return <>
        <Modal
            show={show}
            onHide={handleClose}
            backdrop="static"
            keyboard={false}
        >
            <Modal.Header closeButton>
                <Modal.Title>{title}</Modal.Title>
            </Modal.Header>
            <Modal.Body>



                <Formik
                    validationSchema={schema}
                    onSubmit={handleSubmit}
                    validateOnBlur
                    initialValues={{
                        nombre: '',
                        turno: '',
                        ciclo: '',
                        anho: 2023,
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
                        return <Form noValidate onSubmit={handleSubmit} as={`${addCiclos ? "div" : "form"}`}>

                            <Row className="mb-3">
                                <Form.Group as={Col} md="12" controlId="validationFormikNombre">
                                    <Form.Label>Nombre</Form.Label>
                                    <InputGroup hasValidation >
                                        <Form.Control disabled={addCiclos}
                                            type="text"
                                            placeholder="Nombre"
                                            aria-describedby="inputGroupPrepend"
                                            name="nombre"
                                            value={values.nombre}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            isInvalid={!addCiclos && !!errors.nombre && touched.nombre}

                                        />
                                        {errors.nombre && touched.nombre && <Form.Control.Feedback type="invalid" tooltip>
                                            {errors.nombre}
                                        </Form.Control.Feedback>}
                                    </InputGroup>
                                </Form.Group>

                                <Form.Group as={Col} md="12" controlId="validationFormikTurno">
                                    <Form.Label>Turno</Form.Label>
                                    <InputGroup hasValidation >
                                        <Form.Control disabled={addCiclos}
                                            type="text"
                                            placeholder="Turno"
                                            aria-describedby="inputGroupPrepend"
                                            name="turno"
                                            value={values.turno}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            isInvalid={!addCiclos && (!!errors.turno && touched.turno)}
                                        />
                                        {errors.turno && touched.turno && <Form.Control.Feedback type="invalid" tooltip>
                                            {errors.turno}
                                        </Form.Control.Feedback>}
                                    </InputGroup>
                                </Form.Group>

                                {!addCiclos
                                    ?
                                    <Form.Group as={Col} md="12" controlId="validationFormikCiclo" >
                                        <Form.Label>Ciclo</Form.Label>

                                        <Form.Select aria-label="Default select"
                                            name="ciclo"
                                            value={ values.ciclo }
                                            onChange={(e) => {
                                                validateSelect(e);
                                                return handleChange(e);
                                            }}
                                            onBlur={handleBlur}
                                            isInvalid={!!errors.ciclo && touched.ciclo}
                                        >
                                            <option value={""} disabled>Seleccione un Ciclo</option>
                                            <option value={CREATE_CICLO}>Crear Ciclo</option>
                                            {map(data, (c) => <option key={c.id} value={c.id}>{c.nombre}</option>)}
                                        </Form.Select>

                                    </Form.Group>

                                    :
                                    <FormAddCiclo handleClose={onSetAddCiclos} handleChangeListCiclos={addCicloList} />
                                }

                            </Row>

                            <Row className="mb-3">
                                <Form.Group
                                    as={Col}
                                    md="12"
                                    controlId="validationFormik103"
                                    className="position-relative"
                                >
                                    <Form.Label>Año</Form.Label>
                                    <Form.Control disabled={addCiclos}
                                        type="number"
                                        placeholder="Año"
                                        name="anho"
                                        value={values.anho}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        min={1950}
                                        max={new Date().getFullYear() + 1}
                                        isInvalid={!addCiclos && (!!errors.anho && touched.anho)}
                                    />
                                    {touched.anho && errors.anho && <Form.Control.Feedback type="invalid" tooltip>
                                        {errors.anho}
                                    </Form.Control.Feedback>}
                                </Form.Group>
                            </Row>

                            <Button className="btn-save" type="submit" disabled={addCiclos}>Guardar</Button>
                        </Form>
                    }}
                </Formik>





            </Modal.Body>
            
        </Modal>

        <style jsx="true">
            {
                `
                    .btn-gestion-ciclo{
                        display:flex;
                        align-items: flex-end;
                        justify-content: flex-end;
                        column-gap: 8px;
                    }
                    .btn-icon-ciclo{
                        font-size: 39px;
                    }

                    .btn-save-icon {
                        color: #F0544F;
                    }

                    .btn-save {
                        background: #F0544F;
                        border: 1px solid #F0544F;
                    }

                    .btn-save:hover {
                        background: #F05418;
                        border: 1px solid #F0544F;
                    }

                    .btn-cancel-icon {
                        color: #FDF0D5;
                    }

                `
            }
        </style>
    </>
}

export default ModalCreateClase