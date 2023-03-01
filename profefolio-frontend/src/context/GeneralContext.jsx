import React, { useContext, useState } from "react";
import { createContext } from "react";
import { useLocation } from "react-router-dom";

const GeneralContext = createContext();

export const useGeneralContext = () =>{
    return useContext(GeneralContext)
}

export const GeneralProvider = ({children}) => {
    const location = useLocation()
    const [currentPage, setCurrentPage] = useState(location.pathname)
    const [showSB, setShowSB] = useState(false)


    const values = {
        currentPage, 
        setCurrentPage,
        showSB, 
        setShowSB
    }
    return (
        <GeneralContext.Provider value={values}>
            {children}
        </GeneralContext.Provider>
    )
}