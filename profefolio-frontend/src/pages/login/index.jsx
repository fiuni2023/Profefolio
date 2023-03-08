import React from "react";
import { Logo } from "../../assets";
import { ButtonInput } from "../../components/Inputs";
import styles from './index.module.css'

const Login = ({handleLogin = () => {}}) => {
    return(
        <>
            <div className={styles.LoginContainer}>
                <div className={styles.LoginPanel}>
                    <Logo className={styles.Image} width="75%" height="13%"/>
                    <h3 className={styles.Loginh3}>Iniciar Sesión</h3>
                    <LoginInput placeholder={"Correo Electrónico"} type={"text"} name={"mail"}/>
                    <LoginInput placeholder={"Contraseña"} type={"password"} name={"password"}/>
                    <ButtonInput className={styles.LButton} text={"Ingresar"} variant={"secondary-black"} handleClick={handleLogin} width={"50%"} height={"10%"}/>
                </div>
            </div>
        </>
    )
}

const LoginInput = ({placeholder ="algo", type = "text", name}) => {
    return <>
        <input name={name} type={type} placeholder={placeholder} className={styles.LICTextInput} />
    </>
}

export default Login