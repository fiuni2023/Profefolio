import React, { useEffect, useState } from "react";
import styles from "./index.module.css"
import { HiArrowLeft } from 'react-icons/hi'
import { AiOutlinePlus } from 'react-icons/ai'
import { Table } from "../../../../components/Table";
import LACreateModal from "../../components/CreateModal";
import { useGeneralContext } from "../../../../context/GeneralContext";
import AdminService from "../../servicios/Administradores";
import { Pagination } from "react-bootstrap";

const ListAdministrador = () => {

    const {getToken} = useGeneralContext()

    const [showCreateModal, setShowCreateModal] = useState(false)
    const [admins, setAdmins] = useState([])
    const [currentPage, setCurrentPage] = useState(0)
    const [next, setNext] = useState(true)
    const [loading, setLoading] = useState(false)

    const parseToDate = (d=new Date()) => {
        return `${d.getFullYear()}-${d.getMonth()>10? d.getMonth():`0${d.getMonth()}`}-${d.getDate()>10? d.getDate():`0${d.getDate()}`}`
    }

    useEffect(()=>{
        setLoading(true)
        AdminService.getList(currentPage, getToken())
        .then(r=>{
            setAdmins(r.data.dataList)
            setNext(r.next)
            setLoading(false)
        })
        .catch(e=>{
            setLoading(false)
        })
    }, [currentPage, getToken])

    const doFetch =(admin) =>{
        setAdmins([...admins, admin])
    }

    const getPages = () => {
        return (
            <>
                <Pagination.Prev disabled={currentPage<=0} onClick={()=>{
                    setCurrentPage(currentPage-1)
                }} />
                <Pagination.Item disabled >{currentPage + 1}</Pagination.Item>
                <Pagination.Next disabled={!next || loading} onClick={()=>{
                    setCurrentPage(currentPage+1)
                }}/>
            </>
        )
    }


    return (
        <>
            <LACreateModal show={showCreateModal} handleClose={()=>{setShowCreateModal(!showCreateModal)}} triggerState={(admin)=>{doFetch(admin)}} />
            <div className={styles.GridContainer}>
                <div className={styles.LANavbar}> 
                    <HiArrowLeft size={"20px"}/>
                    <h5 className={styles.LANText}>Administradores</h5>
                </div>
                <div className={styles.TableContainer}>
                    <Table 
                        headers={["CI", "Nombre", "Fecha de Nacimiento", "Direccion", "Telefono", "Acciones"]}
                        datas={admins}
                        parseToRow = {(d, index) =>{
                            return(
                                <tr key={index}>
                                    <td>{d?.documento}</td>
                                    <td>{d?.nombre + " " + d?.apellido}</td>
                                    <td>{parseToDate(new Date(d?.nacimiento))}</td>
                                    <td>{d?.direccion}</td>
                                    <td>{d?.telefono}</td>
                                    <td></td>
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