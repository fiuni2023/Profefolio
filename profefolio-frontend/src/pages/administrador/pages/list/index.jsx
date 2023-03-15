import React, { useState } from "react";
import styles from "./index.module.css"
import { HiArrowLeft } from 'react-icons/hi'
import { AiOutlinePlus } from 'react-icons/ai'
import { Table } from "../../../../components/Table";
import LACreateModal from "../../components/CreateModal";

import NavAdmin from "../../../profesor/components/NavAdmin";

const ListAdministrador = () => {

    const [showCreateModal, setShowCreateModal] = useState(false)

    return (
        <>

        <NavAdmin/>
            <LACreateModal show={showCreateModal} handleClose={()=>{setShowCreateModal(!showCreateModal)}} />
            <div className={styles.GridContainer}>
                <div className={styles.LANavbar}> 
                    <HiArrowLeft size={"20px"}/>
                    <h5 className={styles.LANText}>Administradores</h5>
                </div>
                <div className={styles.TableContainer}>
                    <Table 
                        headers={["CI", "Nombre", "Fecha de Nacimiento", "Direccion", "Telefono", "Acciones"]}
                        datas={[{id: 1, cin: "0000001", nombre: "Nombre1", nacimiento: new Date(), direccion: "direcciones bla bla bla bla", telefono: "+595985111111"}]}
                        parseToRow = {(d, index) =>{
                            return(
                                <tr key={index}>
                                    <td>{d.cin}</td>
                                    <td>{d.nombre}</td>
                                    <td>{d.nacimiento.toDateString()}</td>
                                    <td>{d.direccion}</td>
                                    <td>{d.telefono}</td>
                                    <td></td>
                                </tr>
                            )
                        }}
                    />
                    <div className={styles.LAAddButton} onClick={()=>{setShowCreateModal(!showCreateModal)}}>
                        <AiOutlinePlus size={"35px"}/>
                    </div>
                </div>
            </div>
        </>
    )
}

export default ListAdministrador