import React, { useContext } from "react";
import { createContext } from "react";

const ClaseContext = createContext();

export const useClaseContext = () => {
    return useContext(ClaseContext)
}

export const ClaseProvider = ({ children }) => {

    const getClaseSelectedId = () => {
        const json = localStorage.getItem("id-clase-selected")
        const value = JSON.parse(json);
        return value;
    }

    const setClaseSelectedId = (id) => {
        localStorage.setItem("id-clase-selected", id)
    }
    const values = {
        getClaseSelectedId,
        setClaseSelectedId
    }

    return (
        <ClaseContext.Provider value={values}>
            <>
                {children}
            </>
        </ClaseContext.Provider>
    )
}