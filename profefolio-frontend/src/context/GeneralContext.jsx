import React, { useContext, useState } from "react";
import { createContext } from "react";
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

    console.log(localStorage.getItem('loginData'))

    const values = {
        currentPage, 
        setCurrentPage,
        showSB, 
        setShowSB,
        isLogged,
        setIsLogged
    }

    return (
        <GeneralContext.Provider value={values}>
            {
            isLogged?
            children
            :
            <>
                <Login changeState={()=>{setIsLogged(!isLogged)}} />
            </>
            }
        </GeneralContext.Provider>
    )
}