import React from "react";
import { Logo } from "../../assets";
import { ButtonInput } from "../../components/Inputs";
import styles from './index.module.css'
import LoginService from './services/Login'

const Login = ({changeState = () => {}}) => {

    const handleLogin = () => {
        LoginService.PostLogin("","")
        .then(r=>{
            console.log(JSON.stringify(r.data))
            localStorage.setItem('loginData', JSON.stringify(r.data))
        })
        .catch(error=>console.log)
    }

    return(
        <>
            <div className={styles.LoginContainer}>
                <div className={styles.LoginPanel}>
                    <Logo className={styles.Image} width="80%" height="14%"/>
                    <h3 className={styles.Loginh3}>Iniciar Sesión</h3>
                    <LoginInput placeholder={"Correo Electrónico"}/>
                    <LoginInput placeholder={"Contraseña"}/>
                    <ButtonInput className={styles.LButton} text={"Ingresar"} variant={"secondary-black"} handleClick={()=>{handleLogin()}} width={"35%"} height={"10%"} />
                </div>
            </div>
        </>
    )
}

const LoginInput = ({placeholder ="algo"}) => {
    return <>
        <input type={"text"} placeholder={placeholder} className={styles.LICTextInput} />
    </>
}

export default Login