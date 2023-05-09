import React, { useContext, useState } from "react";
import { createContext } from "react";
import { Toaster } from "react-hot-toast";
import { useLocation } from "react-router-dom";
import Login from "../pages/login";

const GeneralContext = createContext();

export const useGeneralContext = () => {
    return useContext(GeneralContext)
}

export const GeneralProvider = ({ children }) => {
    const location = useLocation()
    const [currentPage, setCurrentPage] = useState(location.pathname)
    const [showSB, setShowSB] = useState(false)
    const [isLogged, setIsLogged] = useState(localStorage.getItem('loginData') ? true : false)

    //dato de colegio
    const [colegio, setColegio] = useState({ id: 0, nombre: "" })

    const getLoginData = () => {
        if (localStorage.getItem('loginData') ? true : false) return JSON.parse(localStorage.getItem('loginData'))
        if (sessionStorage.getItem('loginData') ? true : false) return JSON.parse(sessionStorage.getItem('loginData'))
        return null
    }

    const depricateLoginData = () => {
        if (localStorage.getItem('loginData') ? true : false) localStorage.removeItem('loginData')
        if (sessionStorage.getItem('loginData') ? true : false) sessionStorage.removeItem('loginData')
        setIsLogged(false)
    }

    const verifyToken = () => {
        const expire = new Date(getLoginData()?.expires)
        const now = new Date()
        if (now > expire) depricateLoginData()
    }

    const getToken = () => {
        return getLoginData().token
    }

    const getRole = () => {
        return getLoginData().roles[0]
    }

    const cancan = (role) => {
        const hasRole = getRole()
        return hasRole === role
    }

    const getUserName = () => {
        return getLoginData()?.email?.split("@")[0]
    }

    const getUserMail = () => {
        return getLoginData()?.email
    }

    const getColegioId = () => {
        return getLoginData()?.colegioId
    }

    const values = {
        currentPage,
        setCurrentPage,
        showSB,
        setShowSB,
        isLogged,
        setIsLogged,
        getLoginData,
        getToken,
        verifyToken,
        cancan,
        getUserName,
        getUserMail,
        colegio,
        getColegioId, 
        setColegio
    }

    verifyToken()

    return (
        <GeneralContext.Provider value={values}>
            <>
                <Toaster />
                {
                    isLogged ?
                        children
                        :
                        <>
                            <Login changeState={() => { setIsLogged(!isLogged) }} />
                        </>
                }
            </>
        </GeneralContext.Provider>
    )
}