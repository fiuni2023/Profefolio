import React from "react";
import { Logo } from "../../assets";
import { ButtonInput } from "../../components/Inputs";
import styles from './index.module.css'

const Login = ({handleLogin = () => {}}) => {
    return(
        <>
            <div className={styles.LoginContainer}>
                <div className={styles.LoginPanel}>
                    <Logo className={styles.Image} width="367px" height="70px"/>
                    <h3 className={styles.Loginh3}>Iniciar Sesión</h3>
                    <LoginInput placeholder={"Correo Electrónico"}/>
                    <LoginInput placeholder={"Contraseña"}/>
                    <ButtonInput className={styles.LButton} text={"Ingresar"} variant={"secondary-black"} handleClick={handleLogin} />
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