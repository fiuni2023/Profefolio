import React, { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import { toast } from "react-hot-toast";
import { ButtonInput } from "../../../components/Inputs";
import AdminService from "../../../sevices/administrador";
import { useAdminContext } from "../context/AdminContext";
import { useGeneralContext } from '../../../context/GeneralContext'

import styles from './EditPanel.module.css'
import { RxCross2 } from "react-icons/rx";
import { BsPencilFill } from 'react-icons/bs'
import { GoTrashcan } from 'react-icons/go'
import { SInput } from "./StyledEditPanel";

const LAEditPanel = ({
    onUpdate = () => {}
}) => {

    const { getToken } = useGeneralContext()

    const { selectedAdmin, setSelectedAdmin, resetAdmin, changeAdminData, setShowAdmin } = useAdminContext()

    
    const [editing, setEditing] = useState(false)
    const [erasing, setErasing] = useState(false)
    const [before, setBefore] = useState({})

    useEffect(()=>{
        if (before?.id !== selectedAdmin.id && editing){
            setEditing(false)
        }
    
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [setSelectedAdmin])
    
    const handleChange = (area, value) => {
        changeAdminData(area, value)
    }

    const handleClose = () => {
        setShowAdmin(false)
        resetAdmin()
    }

    const handleDelete = () => {
        AdminService.deleteAdmin(selectedAdmin.id, getToken())
        .then((r)=>{
            toast.success("Eliminado con exito")
            onUpdate()
            handleClose()
        })
        .catch((error)=>{
            toast.error("Error en Eliminar, Intente de nuevo")
        })
    }

    const handleUpdate = () => {
        const {direccion, documento, documentoTipo, email, genero, nacimiento, nombre, apellido, telefono} = selectedAdmin
        const body = {
            nombre: nombre,
            apellido: apellido,
            nacimiento: nacimiento,
            documento: documento,
            documentoTipo: documentoTipo?? "CIN",
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
                toast.error(error.response.data)
            })
    }

    // const widthOf = (data, ph) => {
    //     if(!(data)) return ( ph?.length * 12 ) + 8 < 400? ( ph?.length * 12 ) + 8 : 400
    //     if(data?.length === 0) return ( ph?.length * 12 ) + 8 < 400? ( ph?.length * 12 ) + 8 : 400
    //     return ( data?.length * 12 ) + 8 < 400? ( data?.length * 12 ) + 8 : 400
    // }

    return (
        <>
            <div className={styles.PanelContainer} >
                <div className={styles.ContentBody}>
                    <Row>
                        <Col>
                            <div className="d-flex gap-2">
                                <SInput disabled={!editing} className = {''} style={{width: '49%'}} value={selectedAdmin.nombre} onChange={(event) => { handleChange("nombre", event.target.value) }} />
                                <SInput disabled={!editing} className = {''} placeholder={"Agregar Apellido"} style={{width: '49%'}} value={selectedAdmin.apellido} onChange={(event) => { handleChange("apellido", event.target.value) }} />
                            </div>    
                        </Col>
                        <Col>
                            <div className="d-flex justify-content-between">
                                <div className="d-flex gap-2 align-items-center w-75">
                                    <label>E-mail:</label>
                                    <SInput disabled={!editing} className = {''} placeholder={"Agregar Email"} style={{width: '85%'}} value={selectedAdmin.email ?? ""} onChange={(event) => { handleChange("email", event.target.value) }} />
                                </div>
                                <div className={styles.ExitContainer} onClick={()=>{handleClose()}}>
                                    <RxCross2 size={18} />
                                </div>
                            </div>  
                        </Col>
                    </Row>
                    <Row className="mt-2">
                        <Col>
                            <div className="d-flex gap-2 align-items-center">
                                <div className="d-flex gap-2 align-items-center">
                                    <label>CIN:</label>
                                    <SInput disabled={!editing} className = {''} placeholder={"Agregar CIN"} style={{width: 'auto'}} value={selectedAdmin.documento ?? ""} onChange={(event) => { handleChange("documento", event.target.value) }} />
                                </div>
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Tel:</label>
                                    <SInput disabled={!editing} className = {''} placeholder={"Agregar Telefono"} style={{width: 'auto'}} value={selectedAdmin.telefono ?? ""} onChange={(event) => { handleChange("telefono", event.target.value) }} />
                                </div>
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Fecha Nac.:</label>
                                    <SInput disabled={!editing} type={"date"} className = {''} style={{width: 'auto' }} value={selectedAdmin.nacimiento.split('T')[0] ?? ""} onChange={(event) => { handleChange("nacimiento", event.target.value) }} /> 
                                </div>
                            </div>
                        </Col>
                        <Col>
                            <div className="d-flex gap-2 align-items-center">
                                <label>Direccion:</label>
                                <SInput disabled={!editing} className = {''} placeholder={"Agregar Direccion"} style={{width: "90%"}} value={selectedAdmin.direccion ?? ""} onChange={(event) => { handleChange("direccion", event.target.value) }} />
                            </div>
                        </Col>
                    </Row>
                    <Row className="my-2">
                        <Col>
                            <div className="d-flex gap-2">
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Tipo de Documento:</label>
                                    <SInput disabled={!editing} className = {''} placeholder={"Tipo de documento"} style={{width: "auto"}} value={selectedAdmin.documentoTipo ?? ""} onChange={(event) => { handleChange("documentoTipo", event.target.value) }} />
                                </div>
                                <div className="d-flex gap-2 align-items-center">
                                    <label>Genero:</label>
                                    <select disabled={!editing} className = {""} style={{width: "auto"}} value={selectedAdmin.genero ?? ""} onChange={(event) => { handleChange("genero", event.target.value) }} >
                                        <option value={"M"}>Masculino</option>
                                        <option value={"F"}>Femenino</option>
                                    </select>
                                </div>
                            </div>
                        </Col>
                        <Col>
                        </Col>
                    </Row>
                    <Row className="mt-2">
                        <Col className="d-flex gap-2 justify-content-between">
                            <div className="d-flex gap-2">
                                { erasing?
                                    <>
                                        <div className="d-flex flex-column ">
                                            <label className="align-self-center" >¿Desea eliminar? <label className="text-danger">ESTA ACCION ES IRREVERSIBLE</label></label>
                                            <div className="d-flex gap-2">
                                                <ButtonInput variant="danger-inv" width="100%" height="100%" fontSize="12px" text="CANCELAR" handleClick={()=>{
                                                        setErasing(false)
                                                    }} />
                                                <ButtonInput variant="danger" width="100%" height="100%" fontSize="12px" text="CONFIRMAR" handleClick={()=>{
                                                        handleDelete()
                                                    }} />
                                            </div>
                                        </div>
                                    </>
                                    :
                                    <ButtonInput variant="danger" width="100%" height="100%" text={
                                        <GoTrashcan size={12} />
                                    } handleClick={()=>{
                                        setErasing(true)
                                    }} />
                                }
                            </div>
                            <div className="d-flex gap-2">
                                { editing?
                                    <>
                                        <ButtonInput variant="primary-inv" width="100%" height="100%" fontSize="12px" text="CANCELAR" handleClick={()=>{
                                                toast.success("Cambios revertidos")
                                                setSelectedAdmin(before)
                                                setEditing(false)
                                            }} />
                                        <ButtonInput variant="primary-inv" width="100%" height="100%" fontSize="12px" text="CONFIRMAR" handleClick={()=>{handleUpdate()}} />
                                    </>
                                    :
                                    <ButtonInput variant="primary-inv" width="100%" height="100%" fontSize="12px" text={
                                        <BsPencilFill />
                                    } handleClick={()=>{
                                        setBefore(selectedAdmin)
                                        setEditing(true)
                                    }} />
                                }
                            </div>
                        </Col>
                    </Row>
                    
                </div>
            </div>
        </>
    )
}

export default LAEditPanel