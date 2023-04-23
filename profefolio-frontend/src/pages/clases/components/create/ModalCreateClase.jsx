import { Col, Form, Modal } from "react-bootstrap";
import * as Yup from 'yup';
import { Formik } from "formik";
import { useEffect, useState } from "react";
import useAxiosGet from "../../hooks/useAxiosGet";
import { useGeneralContext } from "../../../../context/GeneralContext";
import FormAddCiclo from "./FormAddCiclo";
import axios from "axios";
import APILINK from "../../../../components/link";
import { toast } from "react-hot-toast";
import ModalContainer from "../../../../components/Modals";
import TextButton from "../../../../components/TextButton";
import { SContainer, SControl, SGroup, SLabel, SRow } from "../../../../components/componentsStyles/StyledForm";
import { SHeader } from "../../../../components/componentsStyles/StyledModal";
import { SpecialSelect } from "../../../../components/SpecialSelect.jsx";



const ModalCreateClase = ({ title = "My Modal", handleClose = () => { }, show = false, triggerState = () => { }, handlePage = () => { } }) => {

    // eslint-disable-next-line no-unused-vars
    const { getToken, cancan, verifyToken, getUserMail } = useGeneralContext();

    const [addCiclos, setAddCiclos] = useState(false);

    const [isSend, setIsSend] = useState(false);

    const onSetAddCiclos = () => {
        setAddCiclos(!addCiclos)
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


    const schema = Yup.object().shape({
        nombre: Yup.string().required("Campo requerido").max(128, "La longitud maxima es de 128 caracteres"),
        turno: Yup.string().required("Campo requerido").max(32, "La longitud es de 32 caracteres"),
        ciclo: Yup.string().required("Seleccione una opcion"),
        anho: Yup.number().required("Campo requerido").min(1951, "Año minimo valido es 1951").max(new Date().getFullYear() + 1, `Año maximo valido es ${new Date().getFullYear() + 1}`),
    });



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
            setIsSend(false);
            handleClose(false);

            //actualizar lista de la tabla aqui
            triggerState();

            toast.success("Guardado exitoso.");
        } else {
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
            className="modal-crear-clases"
            size={"lg"}
        >
            <SHeader closeButton >
                <Modal.Title>{title}</Modal.Title>
            </SHeader>
            <Modal.Body >



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
                        return <SContainer>
                            <Form noValidate onSubmit={handleSubmit} as={`${addCiclos ? "div" : "form"}`} >

                                <SGroup as={Col} md={12} className="position-relative">
                                    <SLabel>Nombre</SLabel>
                                    <SControl disabled={addCiclos || isSend}
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

                                </SGroup>


                                <SGroup as={Col} md={12} className="position-relative">

                                    <SLabel>Turno</SLabel>
                                    <SControl disabled={addCiclos || isSend}
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
                                </SGroup>



                                <SpecialSelect
                                    name={"ciclo"}
                                    textLabel={"Ciclo"}
                                    className={""}
                                    isSend={isSend} // estado para bloquear los input si es que se esta enviando datos del formulario principal 
                                    inCreation={addCiclos} // estado para poder saber si el otro formulario esta activo
                                    setInCreation={onSetAddCiclos}  // setea el estado de actividad del otro formulario
                                    col={12}
                                    values={values.ciclo}
                                    handleChange={handleChange}
                                    handleBlur={handleBlur}
                                    errors={errors.ciclo}
                                    touched={touched.ciclo}
                                    data={data} // valores del select
                                    specialOption="Crear Ciclo" //texto de la opcion que mostrara el otro formulario
                                    alternativeForm={<FormAddCiclo handleClose={onSetAddCiclos} handleChangeListCiclos={addCicloList} />}
                                />

                                <SGroup
                                    as={Col}
                                    md={12}
                                    className="position-relative"
                                >
                                    <SLabel>Año</SLabel>
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

                                </SGroup>
                                {/* </SRow> */}

                                <SRow>
                                    <Col className="btn-container pe-2" md={10}>
                                        <TextButton enabled={!(addCiclos || isSend)} buttonType='accept' />
                                    </Col>
                                    <Col className="btn-container" md={2}>
                                        <TextButton enabled={!(addCiclos || isSend)} buttonType='cancel' onClick={handleClose} />
                                    </Col>
                                </SRow>
                            </Form>
                        </SContainer>
                    }}
                </Formik>

            </Modal.Body>

        </ModalContainer>
        
        <style jsx="true">
            {
                `
                    .btn-container{
                        display: flex;
                        justify-content: flex-end;
                    }
                ` 
            }
        </style>
        
    </>
}

export default ModalCreateClase