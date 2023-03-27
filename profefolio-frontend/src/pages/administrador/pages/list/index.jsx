import React, { useEffect, useState } from "react";
import styles from "./index.module.css"
import { HiArrowLeft } from 'react-icons/hi'
import { AiOutlinePlus } from 'react-icons/ai'
import { Table } from "../../../../components/Table";
import LACreateModal from "../../components/CreateModal";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { Pagination } from "react-bootstrap";

import { toast } from "react-hot-toast";
import { useFetchEffect } from "../../../../components/utils/useFetchEffect";
import { useNavigate } from "react-router-dom";
import AdminService from "../../../../sevices/administrador";
import { useAdminContext } from "../../context/AdminContext";
import LAEditPanel from "../../components/EditPanel";

const ListAdministrador = () => {
    const {getToken, cancan, verifyToken} = useGeneralContext()
    const {selectedAdmin, setSelectedAdmin, showAdmin, setShowAdmin } = useAdminContext()

    const [showCreateModal, setShowCreateModal] = useState(false)
    const [admins, setAdmins] = useState([])
    const [currentPage, setCurrentPage] = useState(0)
    const [next, setNext] = useState(true)
    const [condFetch, setCondFetch] = useState(false)

    const parseToDate = (d=new Date()) => {
        return `${d.getFullYear()}-${d.getMonth()>10? d.getMonth()+1:`0${d.getMonth()+1}`}-${d.getDate()>10? d.getDate():`0${d.getDate()}`}`
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
    

    const { isLoading, error, doFetch } = useFetchEffect(
        ()=>{
            return AdminService.getList(currentPage, getToken())
        }, 
        [currentPage, getToken, condFetch],
        {
            condition: condFetch,
            handleSuccess: (r)=>{
                setAdmins(r.data.dataList)
                setNext(r.data.next)
            },
            handleError: ()=>{
                toast.error("No se pudieron cargar los administradores, intente de nuevo")
            }
        }
    )

    const getPages = () => {
        return (
            <>
                <Pagination.Prev disabled={currentPage<=0} onClick={()=>{
                    setCurrentPage(currentPage-1)
                }} />
                <Pagination.Item disabled >{currentPage + 1}</Pagination.Item>
                <Pagination.Next disabled={!next || isLoading || error} onClick={()=>{
                    setCurrentPage(currentPage+1)
                }}/>
            </>
        )
    }

    const doChangeAdmin = (data) => {
        setSelectedAdmin(data)
        setShowAdmin(true)
    }

    return (
        <>
            <LACreateModal show={showCreateModal} handleClose={()=>{setShowCreateModal(!showCreateModal) }} triggerState={()=>{doFetch(true)}} />
            <div className={styles.GridContainer}>
                <div className={styles.LANavbar}> 
                    <HiArrowLeft size={"20px"} onClick={()=>nav("/")}/>
                    <h5 className={styles.LANText}>Administradores</h5>
                </div>
                <div className={styles.TableContainer}>
                    { showAdmin && 
                        <LAEditPanel onUpdate={()=>{setCurrentPage(0);doFetch(true)}}/>
                    }
                    <Table 
                        headers={["CI", "Nombre", "Fecha de Nacimiento", "Direccion", "Telefono"]}
                        datas={admins}
                        parseToRow = {(d, index) =>{
                            return(
                                <tr key={index} className={`${d?.id === selectedAdmin?.id ? styles.SelectedTR : ""}`} onClick={()=>{doChangeAdmin(d)}}>
                                    <td>{d?.documento}</td>
                                    <td>{`${d?.nombre} ${d?.apellido}`}</td>
                                    <td>{parseToDate(new Date(d?.nacimiento))}</td>
                                    <td>{d?.direccion}</td>
                                    <td>{d?.telefono}</td>
                                </tr>
                            )
                        }}
                    />
                    <Pagination size="sm mt-3">
                        {getPages()}
                    </Pagination>
                    <div className={styles.LAAddButton} onClick={()=>{setShowCreateModal(!showCreateModal)}}>
                        <AiOutlinePlus size={"35px"}/>
                    </div>
                </div>
            </div>
        </>
    )
}

export default ListAdministrador