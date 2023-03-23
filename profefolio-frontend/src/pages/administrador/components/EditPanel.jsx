import React, { useState } from "react";
import { Col, Row } from "react-bootstrap";
import { toast } from "react-hot-toast";
import { ButtonInput } from "../../../components/Inputs";
import AdminService from "../../../sevices/administrador";
import { useAdminContext } from "../context/AdminContext";
import { useGeneralContext } from '../../../context/GeneralContext'

import styles from './EditPanel.module.css'

const LAEditPanel = ({
    onUpdate = () => {}
}) => {

    const { getToken } = useGeneralContext()

    const { selectedAdmin, setSelectedAdmin, resetAdmin, changeAdminData, setShowAdmin } = useAdminContext()

    const [editing, setEditing] = useState(false)
    const [before, setBefore] = useState({})

    const handleChange = (area, value) => {
        changeAdminData(area, value)
    }

    const handleClose = () => {
        setShowAdmin(false)
        resetAdmin()
    }

    const handleUpdate = () => {
        const {direccion, documento, documentoTipo, email, genero, nacimiento, nombre, apellido, telefono} = selectedAdmin
        const body = {
            nombre: nombre,
            apellido: apellido,
            nacimiento: nacimiento,
            documento: documento,
            documentoTipo: documentoTipo,
            genero: genero==="Masculino"? "M" : "F",
            direccion: direccion,
            telefono: telefono, 
            email: email,
        }
        AdminService.updateAdmin(selectedAdmin.id, body, getToken())
            .then((r)=>{
                onUpdate()
                toast.success("Editado con éxito")
                setEditing(false)
                setSelectedAdmin(r.data)
            })
            .catch((error)=>{
                toast.error("No se pudieron editar los administradores, verifique sus datos e intente de nuevo")
            })
    }

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
                                <input disabled={!editing} className = {styles.invisInputB} style={{width: widthOf(selectedAdmin.nombre)}} value={selectedAdmin.nombre} onChange={(event) => { handleChange("nombre", event.target.value) }} />
                                <input disabled={!editing} className = {styles.invisInputB} placeholder={"Agregar Apellido"} style={{width: widthOf(selectedAdmin.apellido, "Agregar Apellido")}} value={selectedAdmin.apellido} onChange={(event) => { handleChange("apellido", event.target.value) }} />
                            </div>    
                        </Col>
                        <Col>
                            <div className="d-flex justify-content-between">
                                <div className="d-flex gap-2 align-items-center">
                                    <label>E-mail:</label>
                                    <input disabled={!editing} className = {styles.invisInputB} placeholder={"Agregar Email"} style={{width: widthOf(selectedAdmin.email, "Agregar Email")}} value={selectedAdmin.email ?? ""} onChange={(event) => { handleChange("email", event.target.value) }} />
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
                                    <input disabled={!editing} className = {styles.invisInput} placeholder={"Agregar CIN"} style={{width: widthOf(selectedAdmin.documento, "Agregar CIN")}} value={selectedAdmin.documento ?? ""} onChange={(event) => { handleChange("documento", event.target.value) }} />
                                </div>
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Tel:</label>
                                    <input disabled={!editing} className = {styles.invisInput} placeholder={"Agregar Telefono"} style={{width: widthOf(selectedAdmin.telefono, "Agregar CIN")}} value={selectedAdmin.telefono ?? ""} onChange={(event) => { handleChange("telefono", event.target.value) }} />
                                </div>
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Fecha Nacimiento:</label>
                                    <input disabled={!editing} type={"date"} className = {styles.invisInput} style={{width: 120 }} value={selectedAdmin.nacimiento.split('T')[0] ?? ""} onChange={(event) => { handleChange("nacimiento", event.target.value) }} /> 
                                </div>
                            </div>
                        </Col>
                        <Col>
                            <div className="d-flex gap-2 align-items-center">
                                <label>Direccion:</label>
                                <input disabled={!editing} className = {styles.invisInput} placeholder={"Agregar Direccion"} style={{width: widthOf(selectedAdmin.direccion, "Agregar Direccion")}} value={selectedAdmin.direccion ?? ""} onChange={(event) => { handleChange("direccion", event.target.value) }} />
                            </div>
                        </Col>
                    </Row>
                    <Row className="mt-2">
                        <Col className="d-flex gap-2 justify-content-end">
                            { !editing &&
                                <ButtonInput variant="primary-inv" text="EDITAR" handleClick={()=>{
                                    setBefore(selectedAdmin)
                                    setEditing(true)
                                }} />
                            }
                            { editing &&
                                <>
                                    <ButtonInput variant="primary-inv" text="CANCELAR" handleClick={()=>{
                                            toast.success("Cambios revertidos")
                                            setSelectedAdmin(before)
                                            setEditing(false)
                                        }} />
                                    <ButtonInput variant="primary-inv" text="CONFIRMAR" handleClick={()=>{handleUpdate()}} />
                                </>
                            }
                        </Col>
                    </Row>
                </div>
            </div>
        </>
    )
}

export default LAEditPanel