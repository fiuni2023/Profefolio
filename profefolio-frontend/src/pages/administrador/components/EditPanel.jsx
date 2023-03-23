import React, { useState } from "react";
import { Col, Row } from "react-bootstrap";
import { ButtonInput } from "../../../components/Inputs";
import { useAdminContext } from "../context/AdminContext";

import styles from './EditPanel.module.css'

const LAEditPanel = () => {

    const { selectedAdmin, resetAdmin, changeAdminData, setShowAdmin } = useAdminContext()

    const [editing, setEditing] = useState(false)

    const handleChange = (area, value) => {
        changeAdminData(area, value)
    }

    const handleClose = () => {
        setShowAdmin(false)
        resetAdmin()
    }

    console.log(selectedAdmin)

    const widthOf = (data, ph) => {
        if(!(data)) return ( ph?.length * 14 ) + 10 < 400? ( ph?.length * 14 ) + 10 : 400
        if(data?.length === 0) return ( ph?.length * 14 ) + 10 < 400? ( ph?.length * 14 ) + 10 : 400
        return ( data?.length * 14 ) + 10 < 400? ( data?.length * 14 ) + 10 : 400
    }

    return (
        <>
            <div className={styles.PanelContainer} >
                <div className={styles.ContentBody}>
                    <Row>
                        <Col>
                            <div className="d-flex gap-2">
                                <input className = {styles.invisInputB} style={{width: widthOf(selectedAdmin.nombre)}} value={selectedAdmin.nombre} onChange={(event) => { handleChange("nombre", event.target.value) }} />
                                <input className = {styles.invisInputB} placeholder={"Agregar Apellido"} style={{width: widthOf(selectedAdmin.apellido, "Agregar Apellido")}} value={selectedAdmin.apellido} onChange={(event) => { handleChange("apellido", event.target.value) }} />
                            </div>    
                        </Col>
                        <Col>
                            <div className="d-flex justify-content-between">
                                <div className="d-flex gap-2 align-items-center">
                                    <label>E-mail:</label>
                                    <input className = {styles.invisInputB} placeholder={"Agregar Email"} style={{width: widthOf(selectedAdmin.email, "Agregar Email")}} value={selectedAdmin.email ?? ""} onChange={(event) => { handleChange("email", event.target.value) }} />
                                </div>
                                <button onClick={()=>handleClose()}>a</button>
                            </div>  
                        </Col>
                    </Row>
                    <Row className="mt-2">
                        <Col>
                            <div className="d-flex gap-2 align-items-center">
                                <div className="d-flex gap-2 align-items-center">
                                    <label>CIN:</label>
                                    <input disabled className = {styles.invisInput} placeholder={"Agregar CIN"} style={{width: widthOf(selectedAdmin.documento, "Agregar CIN")}} value={selectedAdmin.documento ?? ""} onChange={(event) => { handleChange("documento", event.target.value) }} />
                                </div>
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Tel:</label>
                                    <input className = {styles.invisInput} placeholder={"Agregar Telefono"} style={{width: widthOf(selectedAdmin.telefono, "Agregar CIN")}} value={selectedAdmin.telefono ?? ""} onChange={(event) => { handleChange("telefono", event.target.value) }} />
                                </div>
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Fecha Nacimiento:</label>
                                    <input type={"date"} className = {styles.invisInput} style={{width: 120 }} value={selectedAdmin.nacimiento.split('T')[0] ?? ""} onChange={(event) => { handleChange("nacimiento", `${event.target.value}T${selectedAdmin.nacimiento.split('T')[1]}`) }} /> 
                                </div>
                            </div>
                        </Col>
                        <Col>
                            <div className="d-flex gap-2 align-items-center">
                                <label>Direccion:</label>
                                <input className = {styles.invisInput} placeholder={"Agregar Direccion"} style={{width: widthOf(selectedAdmin.direccion, "Agregar Direccion")}} value={selectedAdmin.direccion ?? ""} onChange={(event) => { handleChange("direccion", event.target.value) }} />
                            </div>
                        </Col>
                    </Row>
                    <Row className="mt-2">
                        <Col className="d-flex gap-2 justify-content-end">
                            <ButtonInput variant="primary-inv" />
                        </Col>
                    </Row>
                </div>
            </div>
        </>
    )
}

export default LAEditPanel