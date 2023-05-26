import React, { useEffect, useState } from "react";
import styles from "./index.module.css"
import { HiArrowLeft } from 'react-icons/hi'
import { AiOutlinePlus } from 'react-icons/ai'
// import { Table } from "../../../../components/Table";
import  Tabla  from "../../../../components/Tabla";
// import LACreateModal from "../../components/CreateModal";
import { useGeneralContext } from "../../../../context/GeneralContext";
import Paginations from '../../../../components/Paginations';
import { toast } from "react-hot-toast";
import { useFetchEffect } from "../../../../components/utils/useFetchEffect";
import { useNavigate } from "react-router-dom";
import AdminService from "../../../../sevices/administrador";
import { useAdminContext } from "../../context/AdminContext";
import LAEditPanel from "../../components/EditPanel";
import ModalAdmin from "../../components/AdminModal";
import Spinner from "../../../../components/componentsStyles/SyledSpinner";
import Text from "../../../../components/componentsStyles/StyledText";

const ListAdministrador = () => {
    const {getToken, cancan, verifyToken} = useGeneralContext()
    const {selectedAdmin, setSelectedAdmin, showAdmin, setShowAdmin } = useAdminContext()

    const [showCreateModal, setShowCreateModal] = useState(false)
    //const [admins, setAdmins] = useState([])
    const [currentPage, setCurrentPage] = useState(0)
    const [next, setNext] = useState(true)
    const [totalPage, setTotalPage] = useState(0)
    const [condFetch, setCondFetch] = useState(false)
    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "adminsList", 
        titulos: [{titulo: "CI"}, {titulo: "Nombre"}, {titulo: "Fecha de nacimiento"}, {titulo: "Dirección"}, {titulo: "Teléfono"}]});

    const parseToDate = (d=new Date()) => {
        return `${d.getFullYear()}-${d.getMonth()>10? d.getMonth()+1:`0${d.getMonth()+1}`}-${d.getDate()>9? d.getDate():`0${d.getDate()}`}`
    }

    const nav = useNavigate()

    useEffect(()=>{
        verifyToken()
        if(!cancan("Master")){
            nav('/')
        }else{
            setCondFetch(true)
        }
    }, [cancan, verifyToken, nav])
    
    const doChangeAdmin = (data) => {
        setSelectedAdmin(data)
        setShowAdmin(true)
    }
    
    const { doFetch, loading, error } = useFetchEffect(
        ()=>{
            return AdminService.getList(currentPage, getToken())
        }, 
        [currentPage, getToken, condFetch],
        {
            condition: condFetch,
            handleSuccess: (r)=>{
                //setAdmins(r.data.dataList)
                setNext(r.data.next)
                setTotalPage(r.data.totalPage)
                setDatosTabla({...datosTabla,   clickable: {action: doChangeAdmin},
                                                filas: r.data.dataList.map((dato)=> {return {fila: dato, 
                                                    datos:[ 
                                                        {dato: dato?.documento ? dato.documento : ""},
                                                        {dato: dato?.nombre && dato.apellido ? dato.nombre + " " + dato.apellido : ""},
                                                        {dato: dato?.nacimiento ? parseToDate(new Date(dato.nacimiento)) : ""},
                                                        {dato: dato?.direccion ? dato.direccion : ""},
                                                        {dato: dato?.telefono ? dato.telefono : ""}]}})
                                                
                })
            },
            handleError: ()=>{
                if (!loading) toast.error("No se pudieron cargar los administradores, intente de nuevo")
            }
        }
    )

    const handleHide = () => {
        setShowCreateModal(false)
        doFetch(true)
    }

    return (
        <>
            {/* <LACreateModal show={showCreateModal} handleClose={()=>{setShowCreateModal(!showCreateModal) }} triggerState={()=>{doFetch(true)}} /> */}
            <ModalAdmin show={showCreateModal} onHide={handleHide}  />
            <div className={styles.GridContainer}>
                <div className={styles.LANavbar}> 
                    <HiArrowLeft size={"20px"} onClick={()=>nav("/")}/>
                    <h5 className={styles.LANText}>Administradores</h5>
                </div>
                {loading ? <Spinner height={'calc(100vh - 80px)'} />
                : error ? <Text>Lamentamos esto, ha ocurrido un error al obtener los datos.</Text>
                    :
                    <div className={styles.TableContainer}>
                        { showAdmin && 
                            <LAEditPanel onUpdate={()=>{setCurrentPage(0);doFetch(true)}}/>
                        }
                        <Tabla datosTabla = {datosTabla} selected = {selectedAdmin?.id}/>
                        <Paginations totalPage={totalPage} currentPage={currentPage} setCurrentPage={setCurrentPage} next={next} />
                        <div className={styles.LAAddButton} onClick={()=>{setShowCreateModal(!showCreateModal)}}>
                            <AiOutlinePlus size={"35px"}/>
                        </div>
                    </div>
}
            </div>
        </>
    )
}

export default ListAdministrador