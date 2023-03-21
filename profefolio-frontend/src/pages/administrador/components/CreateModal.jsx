import React from "react";
import { Col, Modal, Row } from "react-bootstrap";
import { RxCross2 } from 'react-icons/rx'
import { useFormik } from "formik";

import { ButtonInput, DateInput, TextInput } from "../../../components/Inputs"
import ModalContainer from "../../../components/Modals";
import { useGeneralContext } from '../../../context/GeneralContext'

import styles from './CreateModal.module.css'
import AdminService from "../servicios/Administradores";
import { toast } from "react-hot-toast";

const LACreateModal = ({
    show = false,
    handleClose = () => { },
    triggerState = () => { }
}) => {

    const { getToken } = useGeneralContext()

    const parseToDate = (d = new Date()) => {
        return `${d.getFullYear()}-${d.getMonth() > 10 ? d.getMonth() : `0${d.getMonth()}`}-${d.getDate() > 10 ? d.getDate() : `0${d.getDate()}`}`
    }

    const formik = useFormik({
        initialValues: {
            cin: "",
            name: "",
            surname: "",
            nacimiento: parseToDate(new Date()),
            telefono: "",
            direccion: "",
            genero: "F",
            pass: "",
            passConf: "",
            correo: ""
        }
    })

    const resetFormik = () => {
        formik.resetForm()
    }

    const toDate = () => {
        let returnDate = formik.values.nacimiento + "T00:00:00.000Z"
        return returnDate
    }

    const validarPass = (data = "", data2 = "") => {
        if (data.length < 8) return true
        if (data !== data2) return true
    }

    const validarDate = (date) => {
        if (new Date(date) > new Date()) return true
        return false
    }

    const handleSubmit = () => {
        if (formik.values.cin === "" || formik.values.name === "" || formik.values.surname === "" || formik.values.documento === "" || formik.values.correo === "" || formik.values.telefono === "") toast.error("Ingrese todos los datos importantes")
        else if (validarPass(formik.values.pass, formik.values.passConf)) toast.error("la contraseña es invalida")
        else if (validarDate(formik.values.date)) toast.error("ingresa una fecha valida")
        else {
            const body = {
                "nombre": formik.values.name,
                "apellido": formik.values.surname,
                "nacimiento": toDate(),
                "documento": formik.values.cin,
                "documentoTipo": "CIN",
                "genero": formik.values.genero,
                "direccion": formik.values.direccion,
                "telefono": formik.values.telefono,
                "password": formik.values.pass,
                "confirmPassword": formik.values.passConf,
                "email": formik.values.correo
            }
            AdminService.createAdmin(body, getToken())
                .then(r => {
                    triggerState()
                    toast.success("creado con exito!!")
                    resetFormik()
                    handleClose()
                })
                .catch(error => {
                    if (typeof (error.response.data) === "string" ? true : false) {
                        toast.error(error.response.data)
                    } else {
                        toast.error(error.response.data?.Password ? error.response.data?.Password[0] : error.response.data?.Email[0])
                    }
                })
        }
    }

    return (
        <>
            <ModalContainer show={show} size={"lg"} handleClose={handleClose} >
                <Modal.Body>
                    <div className={styles.Header}>
                        <div className={styles.Htext}>
                            <h5>agregar administrador</h5>
                        </div>
                        <div className={styles.ExitContainer} onClick={handleClose}>
                            <RxCross2 size={20} />
                        </div>
                    </div>
                    <div className={styles.Divisor}></div>
                    <div className={styles.ModalBody}>
                        <Row>
                            <Col>
                                <label>CI: <RedText>*</RedText></label>
                                <TextInput name="cin" placeholder="" value={formik.values.cin} handleChange={formik.handleChange} width={"100%"} />
                                <label>Fecha de Nacimiento: <RedText>*</RedText></label>
                                <DateInput name="nacimiento" value={formik.values.nacimiento} handleChange={formik.handleChange} width={"100%"} />
                            </Col>
                            <Col>
                                <label>Nombres: <RedText>*</RedText></label>
                                <TextInput name="name" placeholder="" value={formik.values.name} handleChange={formik.handleChange} width={"100%"} />
                                <label>Apellidos: <RedText>*</RedText></label>
                                <TextInput name="surname" placeholder="" value={formik.values.surname} handleChange={formik.handleChange} width={"100%"} />
                            </Col>
                        </Row>

                        <div className={styles.Divisor}></div>

                        <Row>
                            <Col>
                                <label>Teléfono: <RedText>*</RedText></label>
                                <TextInput name="telefono" placeholder="" value={formik.values.telefono} handleChange={formik.handleChange} width={"100%"} />
                            </Col>
                            <Col>
                                <label>Correo Electrónico: <RedText>*</RedText></label>
                                <TextInput name="correo" placeholder="" value={formik.values.correo} handleChange={formik.handleChange} width={"100%"} />
                            </Col>
                            <Col>
                                <label>Género: <RedText>*</RedText></label>
                                <select name="genero" className={styles.Select} placeholder="" value={formik.values.genero} onChange={formik.handleChange} width={"100%"}>
                                    <option value={"F"}>Mujer</option>
                                    <option value={"M"}>Hombre</option>
                                </select>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <label>Dirección:</label>
                                <TextInput name="direccion" placeholder="" value={formik.values.direccion} handleChange={formik.handleChange} width={"100%"} />
                            </Col>
                        </Row>

                        <div className={styles.Divisor}></div>
                        <Row>
                            <Col>
                                <label>Contraseña: <RedText>*</RedText></label>
                                <input type={"password"} name="pass" className={styles.PassInput} placeholder="" value={formik.values.pass} onChange={formik.handleChange} width={"100%"} />

                            </Col>
                            <Col>
                                <label>Confirmar Contraseña: <RedText>*</RedText></label>
                                <input type={"password"} name="passConf" className={styles.PassInput} value={formik.values.passConf} onChange={formik.handleChange} width={"100%"} />
                            </Col>
                        </Row>
                    </div>
                    <div className={`${styles.Divisor} ${styles.MarginedDivisor}`}></div>
                    <div className={styles.ButtonGroup}>
                        <ButtonInput variant="primary" text="Guardar" handleClick={() => handleSubmit()} />
                    </div>
                </Modal.Body>
            </ModalContainer>
        </>
    )
}

const RedText = ({ children }) => {
    return (
        <label style={{ color: "red" }}>
            {children}
        </label>
    )
}

export default LACreateModal