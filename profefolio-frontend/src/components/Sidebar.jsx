import React from "react";
import { useNavigate } from "react-router-dom";
import { useGeneralContext } from "../context/GeneralContext";

const SideBar = () => {
    
    const navigate = useNavigate()
    const { currentPage, showSB, isLogged, setIsLogged } = useGeneralContext()

    const handleLogOut = () => {
        localStorage.removeItem('loginData')
        setIsLogged(!isLogged)
        
    }

    return <>
        <div className="container">
            {showSB && <>
                <SideBarClose> Cerrar </SideBarClose>
                <SideBarTab page={"cerrarSecionBtn"} current={currentPage} handleClick={handleLogOut} > Cerrar Sesión </SideBarTab>
                <SideBarTab page={"administrador"} current={currentPage} handleClick={()=>{navigate("/administrador/list")}} > - Administrador </SideBarTab>
                <SideBarTab page={"pagina1"} current={currentPage} handleClick={()=>{navigate("/pagina1/list")}} > - Página1 </SideBarTab>
                <SideBarTab page={"pagina2"} current={currentPage} handleClick={()=>{navigate("/pagina2/list")}} > - Página2 </SideBarTab>

            </>}
        </div>
        <style jsx="true">{`
            .container{
                position: fixed;
                display: flex;
                flex-direction: column;
                width: 20%;
                height: ${showSB? "100%" : "0%"};
                background-color: #363636;
                border: none;
                gap: 2px;
                padding-top: ${showSB? "20px": "0px"};
                transition: 0.25s all;
                background-color: #F0544F;
                z-index: 100000000;
            }
        `}</style>
    </>
}

const SideBarTab = ({ children, page="", current="", handleClick = () => {} }) => {
    const selected = current.includes(page)
    const {setCurrentPage, showSB, setShowSB} = useGeneralContext()

    return <>
        <div className="tab-container" onClick={()=>{setCurrentPage(page); handleClick(); setShowSB(!showSB)}}>
            <span className={`sbt ${selected ? "selected" : ""}`} >
                {children}
            </span>
        </div>
        <style jsx="true" >{`
            .tab-container{
                display: flex;
                width: 85%;
                height: 30px;
                cursor: pointer;
                align-items: center;
                justify-content: flex-start;
                padding-left: 15%;
            }
            .sbt{
                font-size: 16px;
                color: white;
                font-weight: 500;
            }
            .selected{
                color: white;
                font-weight: 600;
            }
        `}</style>
    </>
}

const SideBarClose = ({ children }) => {
    const { setShowSB } = useGeneralContext()

    return <>
        <div className="tab-container" onClick={() => { setShowSB(false) }}>
            <span className={`sbt`} >
                {children}
            </span>
        </div>
        <style jsx="true" >{`
            .tab-container{
                display: flex;
                width: 85%;
                height: 30px;
                cursor: pointer;
                align-items: center;
                justify-content: flex-start;
                padding-left: 15%;
            }
            .sbt{
                font-size: 16px;
                color: #B4B4B4;
            }
        `}</style>
    </>
}

export default SideBar