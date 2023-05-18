import React, { useContext, useState } from "react";
import { createContext } from "react";
import ProfesorPage from "../pages/dashboard";
import ProfesorClase from "../pages/clase";
import ProfesorMateriaShow from "../pages/materiashow";
import ProfesorMateria from "../pages/materia";
import Anotaciones from "../pages/anotaciones";

const ModularContext = createContext();

export const useModularContext = () => {
    return useContext(ModularContext)
}

export const ModularProvider = ({ children }) => {
    
    const pages = {
        dashboard: <ProfesorPage />,
        clase: <ProfesorClase />,
        materia: <ProfesorMateria />,
        materiashow: <ProfesorMateriaShow />,
        anotaciones: <Anotaciones />
    }

    const [currentPage, setCurrentPage] = useState(pages.dashboard)

    const setPage = (page = "") =>{
        if(page === "dashboard") return setCurrentPage(pages.dashboard)
        if(page === "clase") return setCurrentPage(pages.clase)
        if(page === "materia") return setCurrentPage(pages.materia)
        if(page === "materiashow") return setCurrentPage(pages.materiashow)
        if(page === "anotaciones") return setCurrentPage(pages.anotaciones)
        setCurrentPage(pages.dashboard)
        
    }

    const values = {
        currentPage,
        setPage
    }

    return (
        <ModularContext.Provider value={values}>
            {children}
        </ModularContext.Provider>
    )
}