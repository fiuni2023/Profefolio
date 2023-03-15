import React, { useContext, useState } from "react";
import { createContext } from "react";
import { Toaster } from "react-hot-toast";
import { useLocation } from "react-router-dom";
import Login from "../pages/login";

const GeneralContext = createContext();

export const useGeneralContext = () =>{
    return useContext(GeneralContext)
}

export const GeneralProvider = ({children}) => {
    const location = useLocation()
    const [currentPage, setCurrentPage] = useState(location.pathname)
    const [showSB, setShowSB] = useState(false)
    const [isLogged, setIsLogged] = useState(localStorage.getItem('loginData')? true : false)
    

    const getLoginData = () => {
        if(localStorage.getItem('loginData')? true : false) return JSON.parse(localStorage.getItem('loginData'))
        if(sessionStorage.getItem('loginData')? true : false) return JSON.parse(sessionStorage.getItem('loginData')) 
        return null
    }

    const depricateLoginData = () => {
        if(localStorage.getItem('loginData')? true : false) localStorage.removeItem('loginData')
        if(sessionStorage.getItem('loginData')? true : false) sessionStorage.removeItem('loginData')
    }

    const verifyToken = () => {
        const expire = new Date(getLoginData()?.expires)
        const now = new Date()
        if(now>expire) depricateLoginData()
    }

    const getToken = () => {
        return getLoginData().token

    }

    const values = {
        currentPage, 
        setCurrentPage,
        showSB, 
        setShowSB,
        isLogged,
        setIsLogged,
        getLoginData,
        getToken
    }

    verifyToken()

    return (
        <GeneralContext.Provider value={values}>
            <>
                <Toaster />
                {
                    isLogged?
                    children
                    :
                    <>
                    <Login changeState={()=>{setIsLogged(!isLogged)}} />
                </>
                }
            </>
        </GeneralContext.Provider>
    )
}