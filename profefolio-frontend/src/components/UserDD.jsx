import React from "react";
import { useNavigate } from "react-router-dom";
import { useGeneralContext } from "../context/GeneralContext";
import styles from './UserDD.module.css'
import { BsChevronCompactUp } from "react-icons/bs";
import { FaUserCircle } from "react-icons/fa";

const UserDD = ({ showSB = false, setShowSB = ()=>{}}) => {
    
    const navigate = useNavigate()
    const { isLogged, setIsLogged, getUserName } = useGeneralContext()

    const handleLogOut = () => {
        setShowSB(false)
        localStorage.removeItem('loginData')
        sessionStorage.removeItem('loginData')
        navigate("/")
        setIsLogged(!isLogged)
    }

    const style = {
        height: showSB? "100%" : "0%",
        paddingTop: showSB? "2px": "0px"
    }

    return <>
        <div className={styles.container} style={{
            height: showSB? "100%" : "0%"
        }}  onClick={(e)=>{
            e.stopPropagation()
            setShowSB(!showSB)
        }}>
            <div className={styles.background} style={style} onClick={(e)=>{e.stopPropagation()}}>
                {showSB &&
                <>
                    <div className={styles.ExitContainer} onClick={(e)=>{
                        e.stopPropagation()
                        setShowSB(!showSB)
                    }}>
                        <BsChevronCompactUp size={20}/>
                    </div>
                    <div className={styles.sbt}>
                        {getUserName()}
                    </div>
                    <div className={styles.sbt}>
                        <FaUserCircle size={100}/>
                    </div>
                    <div className={styles.sbt2} onClick={handleLogOut}>
                        CERRAR SESIÓN
                    </div>
                </>
                }
            </div>
        </div>
    </>
}



export default UserDD