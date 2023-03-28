import React from "react";
import { useNavigate } from "react-router-dom";
import { useGeneralContext } from "../context/GeneralContext";
import styles from './Sidebar.module.css'
import {RxCross2} from 'react-icons/rx'

const SideBar = () => {
    
    const navigate = useNavigate()
    const { showSB, isLogged, setIsLogged, cancan } = useGeneralContext()

    const handleLogOut = () => {
        localStorage.removeItem('loginData')
        sessionStorage.removeItem('loginData')
        navigate("/")
        setIsLogged(!isLogged)
        
    }

    const style = {
        height: showSB? "100%" : "0%",
        paddingTop: showSB? "20px": "0px"
    }

    return <>
        <div className={styles.container} style={style}>
            {showSB && <>
                <SideBarClose>
                    <div className="d-flex justify-content-end h-100 w-100">
                        <div className={styles.ExitContainer}>
                            <RxCross2 size={12} />
                        </div>
                    </div>
                </SideBarClose>
                <SideBarTab page={"home"} handleClick={handleLogOut} > Cerrar Sesión </SideBarTab>
                <SideBarTab page={"home"} handleClick={()=>{navigate("/")}} > - Home </SideBarTab>
                {   
                    cancan("Master") &&
                    <>
                        <SideBarTab page={"administrador"}  handleClick={()=>{navigate("/administrador/list")}} > - Administrador </SideBarTab>
                        <SideBarTab page={"colegio"}  handleClick={()=>{navigate("/colegio")}} > - Colegios </SideBarTab>                    
                    </>
                }
                {
                    cancan("Administrador de Colegio") &&
                    <>
                        <SideBarTab page={"profesor"}  handleClick={()=>{navigate("/profesor")}}>- Profesor</SideBarTab>

                        <SideBarTab page={"materia"}  handleClick={()=>{navigate("/materias")}}>- Materias</SideBarTab>
                    </>
                }

            </>}
        </div>
    </>
}

const SideBarTab = ({ children, page="", current="", handleClick = () => {} }) => {
    const selected = current.includes(page)
    const {showSB, setShowSB} = useGeneralContext()

    return <>
        <div className={styles.tabContainer} onClick={()=>{ handleClick(); setShowSB(!showSB)}}>
            <span className={`${styles.sbt} ${selected ? styles.selected: ""}`} >
                {children}
            </span>
        </div>
    </>
}

const SideBarClose = ({ children }) => {
    const { setShowSB } = useGeneralContext()

    return <>
        <div className={styles.tabContainer} onClick={() => { setShowSB(false) }}>
                {children}
        </div>
    </>
}

export default SideBar