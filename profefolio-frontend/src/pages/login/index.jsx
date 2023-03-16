import React, { useState } from "react";
import { toast } from "react-hot-toast";
import { Logo } from "../../assets";
import { ButtonInput } from "../../components/Inputs";
import styles from './index.module.css'
import LoginService from './services/Login'

const Login = ({changeState = () => {}}) => {

    const [mail, setMail] = useState("")
    const [pass, setPass] = useState("")
    const [remember, setRemember] = useState(false)

    const handleLogin = () => {
        if(mail === "" || pass === "") return toast.error("Ingresa todas las credenciales para intentar ingresar a la plataforma")
        LoginService.PostLogin(mail,pass)
        .then(r=>{
            if (remember) localStorage.setItem('loginData', JSON.stringify(r.data))
            else sessionStorage.setItem('loginData', JSON.stringify(r.data))
            toast.success("Se ha logueado con exito!")
            changeState()
        })
        .catch(error=>{toast.error(error.response.data)})
        
    }

    return(
        <>
            <div className={styles.LoginContainer}>
                <div className={styles.LoginPanel}>
                    <Logo className={styles.Image} width="80%" height="14%"/>
                    <h3 className={styles.Loginh3}>Iniciar Sesión</h3>
                    <LoginInput placeholder={"Correo Electrónico"} value={mail} handleChange={(event)=>{setMail(event.target.value)}} />
                    <LoginInput placeholder={"Contraseña"} type={"password"} value={pass} handleChange={(event)=>{setPass(event.target.value)}} />
                    <div className={styles.Checkbox}>
                        <input type={"checkbox"}  value={remember} onChange={()=>{setRemember(!remember)}} />
                        <label>Recordarme</label>
                    </div>
                    <ButtonInput className={styles.LButton} text={"Ingresar"} variant={"secondary-black"} handleClick={()=>{handleLogin()}} width={"35%"} height={"10%"} />
                </div>
            </div>
        </>
    )
}

const LoginInput = ({ type="text", placeholder ="algo", value = "" , handleChange= ()=>{} }) => {
    return <>
        <input type={type} placeholder={placeholder} className={styles.LICTextInput} value={value} onChange={handleChange} />
    </>
}

export default Login