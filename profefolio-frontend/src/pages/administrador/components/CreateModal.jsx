import React from "react";
import { Modal } from "react-bootstrap";
import { RxCross2 } from 'react-icons/rx'
import { useFormik } from "formik";

import { ButtonInput, DateInput, TextInput } from "../../../components/Inputs"
import ModalContainer from "../../../components/Modals";

import styles from './CreateModal.module.css'

const LACreateModal = ({ 
    show = false, 
    handleClose = () => {}, 
}) => {

    const formik = useFormik({
        initialValues:{
            cin: "",
            name: "",
            nacimiento: null,
            telefono: "",
            direccion: "",
            correo: ""
        },
        onSubmit: values =>{
            console.log(values)
        },
    })
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
                        <label>Nombre y Apellido:</label>
                        <TextInput name="name" placeholder="" value={formik.values.name} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Fecha de Nacimiento:</label>
                        <DateInput name="nacimiento" value={formik.values.nacimiento} handleChange={formik.handleChange} width={"100%"} />
                        <label>Teléfono:</label>
                        <TextInput name="telefono" placeholder="" value={formik.values.telefono} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Dirección:</label>
                        <TextInput name="direccion" placeholder="" value={formik.values.direccion} handleChange={formik.handleChange} width={"100%"}/>
                        <label>Correo Electrónico:</label>
                        <TextInput name="correo" placeholder="" value={formik.values.correo} handleChange={formik.handleChange} width={"100%"}/>
                    </div>
                    <div className={`${styles.Divisor} ${styles.MarginedDivisor}`}></div>
                    <div className={styles.ButtonGroup}>
                        <ButtonInput variant="secondary" text="Cancelar" onClick={handleClose}/>
                        <ButtonInput variant="primary" text="Guardar"/>
                    </div>
                </Modal.Body>
            </ModalContainer>
        </>
    )
}

export default LACreateModal