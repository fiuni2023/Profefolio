import React from "react";
import { Modal } from "react-bootstrap";
import { RxCross2 } from 'react-icons/rx'
import { useFormik } from "formik";

import { ButtonInput, DateInput, TextInput } from "../../../components/Inputs"
import ModalContainer from "../../../components/Modals";
import {useGeneralContext} from '../../../context/GeneralContext'

import styles from './CreateModal.module.css'
import AdminService from "../servicios/Administradores";

const LACreateModal = ({ 
    show = false, 
    handleClose = () => {}, 
    triggerState = () => {}
}) => {

    const {getToken} = useGeneralContext()

    const parseToDate = (d=new Date()) => {
        return `${d.getFullYear()}-${d.getMonth()>10? d.getMonth():`0${d.getMonth()}`}-${d.getDate()>10? d.getDate():`0${d.getDate()}`}`
    }

    const formik = useFormik({
        initialValues:{
            cin: "",
            name: "",
            surname: "",
            nacimiento: parseToDate(new Date()),
            telefono: "",
            direccion: "",
            genero: "F",
            correo: ""
        }
    })

    const resetFormik = () => {
        formik.resetForm()
    }

    const toDate = () =>{
        let returnDate = formik.values.nacimiento + "T00:00:00.000Z"
        return returnDate
    }

    const handleSubmit=()=>{
        const body = {
            "nombre": formik.values.name,
            "apellido": formik.values.surname,
            "nacimiento": toDate(),
            "documento": formik.values.cin,
            "documentoTipo": "CIN",
            "genero": formik.values.genero,
            "direccion": formik.values.direccion,
            "telefono": formik.values.telefono,
            "password": "P@ssw0rd",
            "confirmPassword": "P@ssw0rd",
            "email": formik.values.correo
          }
        AdminService.createAdmin(body, getToken())
        .then(r => {
        })
        resetFormik()
        triggerState()
        handleClose()
    }

    return(
        <>
            <ModalContainer show={show} handleClose={handleClose} >
                <Modal.Body>
                    <div className={styles.Header}>
                        <div className={styles.Htext}>
                            <h5>agregar administrador</h5>
                        </div>
                        <div className={styles.ExitContainer} onClick = {handleClose}>
                            <RxCross2 size={20}/>
                        </div>
                    </div>
                    <div className={styles.Divisor}></div>
                    <div className={styles.ModalBody}>
                        <label>CI:</label>
                        <TextInput name="cin" placeholder="" value={formik.values.cin} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Nombres:</label>
                        <TextInput name="name" placeholder="" value={formik.values.name} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Apellidos:</label>
                        <TextInput name="surname" placeholder="" value={formik.values.surname} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Fecha de Nacimiento:</label>
                        <DateInput name="nacimiento" value={formik.values.nacimiento} handleChange={formik.handleChange} width={"100%"} />
                        <label>Teléfono:</label>
                        <TextInput name="telefono" placeholder="" value={formik.values.telefono} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Dirección:</label>
                        <TextInput name="direccion" placeholder="" value={formik.values.direccion} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Correo Electrónico:</label>
                        <TextInput name="correo" placeholder="" value={formik.values.correo} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Género:</label>
                        <select name="genero" className={styles.Select} placeholder="" value={formik.values.genero} onChange={formik.handleChange} width={"100%"}>
                            <option value={"F"}>Mujer</option>
                            <option value={"M"}>Hombre</option>
                            <option value={"X"}>No Especificar</option>
                        </select>
                    </div>
                    <div className={`${styles.Divisor} ${styles.MarginedDivisor}`}></div>
                    <div className={styles.ButtonGroup}>
                        <ButtonInput variant="primary" text="Guardar" handleClick={()=>handleSubmit()}/>
                    </div>
                </Modal.Body>
            </ModalContainer>
        </>
    )
}

export default LACreateModal