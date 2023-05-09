import React from "react";
import { useNavigate } from "react-router-dom";
import { useGeneralContext } from "../context/GeneralContext";
import styles from './Sidebar.module.css'
import {RxCross2} from 'react-icons/rx'

const SideBar = ({ showSB = false, setShowSB = ()=>{}}) => {
    
    const navigate = useNavigate()
    const { cancan} = useGeneralContext()

    const style = {
        height: showSB? "100%" : "0%",
        paddingTop: showSB? "20px": "0px"
    }

    return <>
        <div className={styles.container} style={{
            height: showSB? "100%" : "0%"
        }}  onClick={(e)=>{
            e.stopPropagation()
            setShowSB(!showSB)
        }}>
            <div className={styles.background} style={style} onClick={(e)=>{e.stopPropagation()}}>
                {showSB && <>
                    <SideBarClose setShowSB={setShowSB}>
                        <div className="d-flex justify-content-end h-100 w-100">
                            <div className={styles.ExitContainer}>
                                <RxCross2 size={12} />
                            </div>
                        </div>
                    </SideBarClose>
                    <SideBarTab showSB={showSB} setShowSB={setShowSB} page={"home"} handleClick={()=>{navigate("/")}} > - Home </SideBarTab>
                    {   
                        cancan("Master") &&
                        <>
                            <SideBarTab showSB={showSB} setShowSB={setShowSB} page={"administrador"}  handleClick={()=>{navigate("/administrador/list")}} > - Administrador </SideBarTab>
                            <SideBarTab showSB={showSB} setShowSB={setShowSB} page={"colegios"}  handleClick={()=>{navigate("/colegios/list")}} > - Colegios </SideBarTab>                    
                        </>
                    }
                    {
                        cancan("Administrador de Colegio") &&
                        <>
                            <SideBarTab showSB={showSB} setShowSB={setShowSB} page={"alumnos"}  handleClick={()=>{navigate("/alumnos")}}>- Alumnos</SideBarTab>
                            <SideBarTab showSB={showSB} setShowSB={setShowSB} page={"clases"}  handleClick={()=>{navigate("/clases")}}>- Clases</SideBarTab>
                            <SideBarTab showSB={showSB} setShowSB={setShowSB} page={"materia"}  handleClick={()=>{navigate("/materias")}}>- Materias</SideBarTab>
                            <SideBarTab showSB={showSB} setShowSB={setShowSB} page={"profesor"}  handleClick={()=>{navigate("/profesor")}}>- Profesor</SideBarTab>
                        </>
                    }

                </>}
            </div>
        </div>
    </>
}

const SideBarTab = ({ children, page="", handleClick = () => {}, showSB, setShowSB = ()=>{} }) => {
    const { currentPage, setCurrentPage } = useGeneralContext()
    const selected = currentPage.includes(page)

    return <>
        <div className={styles.tabContainer} onClick={()=>{ handleClick(); setShowSB(!showSB); setCurrentPage(page)}}>
            <span className={`${styles.sbt} ${selected ? styles.selected: ""}`} >
                {children}
            </span>
        </div>
    </>
}

const SideBarClose = ({ setShowSB = () => {}, children }) => {
    const { setCurrentPage } = useGeneralContext()
    return <>
        <div className={styles.tabContainer} onClick={() => { setShowSB(false); setCurrentPage("home") }}>
                {children}
        </div>
    </>
}

export default SideBar