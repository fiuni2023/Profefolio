import React from "react";
import { useNavigate } from "react-router-dom";
import { useGeneralContext } from "../context/GeneralContext";
import styles from './UserDD.module.css'

const UserDD = ({ showSB = false, setShowSB = ()=>{}}) => {
    
    const navigate = useNavigate()
    const { isLogged, setIsLogged} = useGeneralContext()

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
        <div className={styles.container} style={{
            height: showSB? "100%" : "0%"
        }}  onClick={(e)=>{
            e.stopPropagation()
            setShowSB(!showSB)
        }}>
            <div className={styles.background} style={style} onClick={(e)=>{e.stopPropagation()}}>
                
            </div>
        </div>
    </>
}



export default UserDD