import React, { useEffect, useState } from "react";
import { toast } from "react-hot-toast";
import AdminService from "../../../sevices/administrador";
import { useAdminContext } from "../context/AdminContext";
import { useGeneralContext } from '../../../context/GeneralContext';
import IconButton from '../../../components/IconButton';
import styles from './EditPanel.module.css'
import { SInput, SSelect, SLabel, SRow, SOption, SCol, SCol2, SClose, H1, SErase } from "./StyledEditPanel";
import TextButton from "../../../components/TextButton";

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
                    <SRow>
                        <SClose >
                            {!editing && !erasing && 
                                <><H1>
                                    Datos del administrador
                                </H1></>}
                            {editing && !erasing && 
                                <><H1>
                                    Editar administrador
                                </H1></>}
                            {!editing && erasing && 
                                <><H1>
                                    Eliminar administrador
                                </H1></>}        
                            <IconButton
                                buttonType={"close"}
                                onClick={()=>{handleClose()}}
                                enabled={true}
                            ></IconButton>
                        </SClose>
                    </SRow>
                    <SRow>
                        <SCol className="col-sm-12 col-md-6 col-lg-3">
                                <SLabel>Nombres:</SLabel>
                                <SInput disabled={!editing} className = {''} value={selectedAdmin.nombre} onChange={(event) => { handleChange("nombre", event.target.value) }} />
                        </SCol>
                        <SCol className="col-sm-12 col-md-6 col-lg-3">
                                <SLabel>Apellidos:</SLabel>
                                <SInput disabled={!editing} className = {''} placeholder={"Agregar Apellido"}  value={selectedAdmin.apellido} onChange={(event) => { handleChange("apellido", event.target.value) }} />
                        </SCol>
                        <SCol className="col-sm-12 col-md-12 col-lg-6">
                                    <SLabel>E-mail:</SLabel>
                                    <SInput disabled={!editing} className = {''} placeholder={"Agregar Email"} value={selectedAdmin.email ?? ""} onChange={(event) => { handleChange("email", event.target.value) }} />
                        </SCol>
                    </SRow>
                    <SRow className="mt-2">
                        <SCol className="col-sm-12 col-md-6 col-lg-2">
                                <SLabel>CIN:</SLabel>
                                <SInput disabled={!editing} className = {''} placeholder={"Agregar CIN"} style={{width: 'auto'}} value={selectedAdmin.documento ?? ""} onChange={(event) => { handleChange("documento", event.target.value) }} />
                        </SCol>
                        <SCol className="col-sm-12 col-md-6 col-lg-2">
                                <SLabel>Tel:</SLabel>
                                <SInput disabled={!editing} className = {''} placeholder={"Agregar Telefono"} style={{width: 'auto'}} value={selectedAdmin.telefono ?? ""} onChange={(event) => { handleChange("telefono", event.target.value) }} />
                        </SCol>
                        <SCol className="col-sm-12 col-sm-6 col-lg-2">
                                <SLabel>Fecha Nac.:</SLabel>
                                <SInput disabled={!editing} type={"date"} className = {''} style={{width: 'auto' }} value={selectedAdmin.nacimiento.split('T')[0] ?? ""} onChange={(event) => { handleChange("nacimiento", event.target.value) }} /> 
                        </SCol>
                            
                        <SCol className="col-sm-12 col-md-6 col-lg-6">
                                <SLabel>Direccion:</SLabel>
                                <SInput disabled={!editing} className = {''} placeholder={"Agregar Direccion"} value={selectedAdmin.direccion ?? ""} onChange={(event) => { handleChange("direccion", event.target.value) }} />
                        </SCol>
                    </SRow>
                    <SRow className="my-2">
                        <SCol className="col-sm-9 col-md-6 col-lg-4">
                            <SLabel>Tipo de Documento:</SLabel>
                            <SInput disabled={!editing} className = {''} placeholder={"Tipo de documento"} style={{width: "auto"}} value={selectedAdmin.documentoTipo ?? ""} onChange={(event) => { handleChange("documentoTipo", event.target.value) }} />
                        </SCol>
                        <SCol className="col-sm-12 col-md-6 col-lg-4">
                            <SLabel>Genero:</SLabel>
                            <SSelect disabled={!editing} className = {""} style={{width: "auto"}} value={selectedAdmin.genero ?? ""} onChange={(event) => { handleChange("genero", event.target.value) }} >
                                <SOption value={"M"}>Masculino</SOption>
                                <SOption value={"F"}>Femenino</SOption>
                            </SSelect>
                        </SCol>
                    </SRow>
                    <SRow className="mt-2">
                        <SCol2 >
                                {erasing && !editing && <>
                                    <SErase >
                                            <SLabel className="align-self-center" >¿Desea eliminar? </SLabel>
                                            <SLabel className="text-danger">ESTA ACCION ES IRREVERSIBLE</SLabel>
                                            <div className="d-flex gap-2">
                                                <TextButton
                                                    buttonType={"no"}
                                                    onClick={()=>{
                                                        setErasing(false)
                                                        setEditing(false)
                                                    }}
                                                    enabled={true}></TextButton>
                                                <TextButton
                                                    buttonType={"yes"}
                                                    onClick={()=>{
                                                        handleDelete()
                                                    }}
                                                    enabled={true}></TextButton>
                                            </div>
                                        </SErase>
                                </>}
                                {!erasing && editing && <>
                                    <TextButton
                                            buttonType={"cancel"}
                                            onClick={()=>{
                                                toast.success("Cambios revertidos")
                                                setSelectedAdmin(before)
                                                setEditing(false)
                                                setErasing(false)
                                            }}
                                            enabled={true}>
                                    </TextButton>
                                    <TextButton
                                            buttonType={"save-changes"}
                                            onClick={()=>{handleUpdate()}}
                                            enabled={true}>
                                    </TextButton>
                                </>}
                                {!erasing && !editing && <>
                                
                                    <IconButton 
                                        buttonType={"my-delete"} 
                                        onClick={()=>{
                                            setErasing(true)
                                            setEditing(false)
                                        }}
                                        enabled={true}
                                        >
                                    </IconButton>

                                    <IconButton 
                                        buttonType={"edit"} 
                                        onClick={()=>{
                                            setBefore(selectedAdmin)
                                            setEditing(true)
                                            setErasing(false)
                                        }}
                                        enabled={true}>
                                        </IconButton> 
                                </>}
                
                        </SCol2>
                    </SRow>
                    
            </div>
        </>
    )
}

export default LAEditPanel