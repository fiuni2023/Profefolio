import { Button, Col, Form, InputGroup, Row, Modal } from "react-bootstrap";
import * as Yup from 'yup';
import { Formik } from "formik";
import { map } from "lodash";
import { useEffect, useState } from "react";
import useAxiosGet from "../../hooks/useAxiosGet";
import { useGeneralContext } from "../../../../context/GeneralContext";
import FormAddCiclo from "./FormAddCiclo";
import axios from "axios";
import APILINK from "../../../../components/link";
import { toast } from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import ModalContainer from "../../../../components/Modals";
import TextButton from "../../../../components/TextButton";
import { SControl, SGroup, SLabel, SSelect } from "../../../../components/componentsStyles/StyledForm";


const ModalCreateClase = ({ title = "My Modal", handleClose = () => { }, show = false, triggerState = () => { }, handlePage = () => { } }) => {
    const CREATE_CICLO = "___option____create____ciclo";

    // eslint-disable-next-line no-unused-vars
    const { getToken, cancan, verifyToken, getUserMail } = useGeneralContext();

    const [addCiclos, setAddCiclos] = useState(false);

    const [isSend, setIsSend] = useState(false);

    const onSetAddCiclos = () => {
        setAddCiclos(!addCiclos)
    }

    const validateSelect = (e) => {
        if (CREATE_CICLO === e.target.value) {
            onSetAddCiclos();
            e.target.value = "";
        }
    }

    // eslint-disable-next-line no-unused-vars
    const [data, loading, error, setData] = useAxiosGet(`api/ciclo`, getToken());
    // eslint-disable-next-line no-unused-vars
    const [colegio, loadingColegio, errorColegio, setColegio] = useAxiosGet(`api/administrador/${getUserMail()}`, getToken());



    useEffect(() => {
        verifyToken();
        setAddCiclos(false);

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [show])

    const addCicloList = (ciclo) => {
        setData([...data, ciclo])
    }

    const nav = useNavigate()

    const schema = Yup.object().shape({
        nombre: Yup.string().required("Campo requerido").max(128, "La longitud maxima es de 128 caracteres"),
        turno: Yup.string().required("Campo requerido").max(32, "La longitud es de 32 caracteres"),
        ciclo: Yup.string().required("Seleccione una opcion"),
        anho: Yup.number().required("Campo requerido").min(1951, "Año minimo valido es 1951").max(new Date().getFullYear() + 1, `Año maximo valido es ${new Date().getFullYear() + 1}`),
    });


    const actualizarClase = () => {
        verifyToken()
        const toastLoadig = toast.loading("Obteniendo Clase.");

        if (!cancan("Administrador de Colegio")) {
            nav("/")
        } else {
            //axios.get(`https://miapi.com/products?page=${page}&size=${size}`, {

            colegio && axios.get(`${APILINK}/api/clase/page/${colegio?.id}/${0}`, {
                headers: {
                    Authorization: `Bearer ${getToken()}`,
                }
            })
                .then(response => {
                    triggerState(response.data.dataList);
                    handlePage(response.data.next);
                })
                .catch(error => {
                    //console.error(error);
                    toast.dismiss(toastLoadig);
                    toast.error(`Error recargue la pagina`);
                });
            toast.dismiss(toastLoadig);

        }
    }

    const handleSubmit = async (e) => {
        setIsSend(true);
        const toastLoadig = toast.loading("Guardando Clase.");
        const obj = {
            "colegioId": colegio?.id,
            "cicloId": parseInt(e.ciclo),
            "nombre": e.nombre,
            "turno": e.turno,
            "anho": e.anho
        }

        const result = await axios.post(`${APILINK}/api/clase`, obj, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })

        if (result.status === 200) {
            //console.log("Result: ", result);
            setIsSend(false);
            handleClose(false);



            //actualizar lista de la tabla aqui
            actualizarClase();




            toast.success("Guardado exitoso.");
        } else {
            //console.log("Error: ", result.data)
            setIsSend(false);
            toast.error(`Error: ${result.data.error}`);
        }
        toast.dismiss(toastLoadig);
    }

    return <>
        <ModalContainer
            show={show}
            handleClose={handleClose}
            backdrop="static"
            keyboard={false}

        >
            <Modal.Header closeButton className="modal-crear-clase">
                <Modal.Title>{title}</Modal.Title>
            </Modal.Header>
            <Modal.Body className="modal-crear-clase modal-body-create-clase">



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
                        return <Form noValidate onSubmit={handleSubmit} as={`${addCiclos ? "div" : "form"}`} >

                            <Row className="mb-3">
                                <Form.Group as={Col} md="12" controlId="validationFormikNombre">
                                    <Form.Label>Nombre</Form.Label>
                                    <InputGroup hasValidation >
                                        <Form.Control disabled={addCiclos || isSend}
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
                                        <Form.Control disabled={addCiclos || isSend}
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
                                    <SGroup as={Col} md="12" controlId="validationFormikCiclo" >
                                        <SLabel>Ciclo</SLabel>

                                        <SSelect aria-label="Default select" disabled={isSend}
                                            name="ciclo"
                                            value={values.ciclo}
                                            onChange={(e) => {
                                                validateSelect(e);
                                                return handleChange(e);
                                            }}
                                            onBlur={handleBlur}
                                            isInvalid={!!errors.ciclo && touched.ciclo}
                                        >
                                            <option value={""} disabled>Seleccione un Ciclo</option>
                                            <option className="option-create-ciclo" value={CREATE_CICLO}>Crear Ciclo</option>
                                            {map(data, (c) => <option key={c.id} value={c.id}>{c.nombre}</option>)}
                                        </SSelect>

                                    </SGroup>

                                    :
                                    <FormAddCiclo handleClose={onSetAddCiclos} handleChangeListCiclos={addCicloList} />
                                }

                            </Row>

                            <Row className="mb-3">
                                {/* <Form.Group
                                    as={Col}
                                    md="12"
                                    controlId="validationFormik103"
                                    className="position-relative"
                                > */}
                                <SGroup
                                    as={Col}
                                    md="12"
                                    controlId="validationFormik103"
                                    className="position-relative"
                                >
                                    {/* <Form.Label>Año</Form.Label> */}
                                    <SLabel>Año</SLabel>
                                    {/* <Form.Control disabled={addCiclos || isSend}
                                        type="number"
                                        placeholder="Año"
                                        name="anho"
                                        value={values.anho}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        min={1950}
                                        max={new Date().getFullYear() + 1}
                                        isInvalid={!addCiclos && (!!errors.anho && touched.anho)}
                                    /> */}
                                    <SControl
                                        disabled={addCiclos || isSend}
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
                                    {/* </Form.Group> */}
                                </SGroup>
                            </Row>

                            <Row className="btn-save-contanier">
                                <Col className="btn-save-subcontanier" md={4}>
                                    {/* <Button className="btn-save" type="submit" disabled={addCiclos || isSend}>Guardar</Button>*/}
                                    <TextButton enabled={!(addCiclos || isSend)} buttonType='save' onClick={() => console.log('Guardando')} />
                                </Col>
                            </Row>
                        </Form>
                    }}
                </Formik>





            </Modal.Body>

        </ModalContainer>

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

                    .btn-save:disabled {
                        background: gray;
                        border: 1px solid #F0544F;
                    }

                    .btn-save:hover {
                        background: #F05418;
                        border: 1px solid #F0544F;
                    }

                    .btn-cancel-icon {
                        color: #FDF0D5;
                    }
                    .btn-save-contanier, .btn-save-subcontanier{
                        display: flex;
                        justify-content: center;
                    }
                    .option-create-ciclo{
                        background: #e59c68;
                        color: white;
                    }
                    .modal-crear-clase{
                        background: #C6D8D3;
                    }
                    .modal-body-create-clase{
                        border-radius: 0 0 10px 10px;
                    }
                `
            }
        </style>
    </>
}

export default ModalCreateClase