import React, { useContext, useState } from "react";
import { createContext } from "react";
import ProfesorPage from "../pages/dashboard";
import ProfesorClase from "../pages/clase";
import ProfesorMateriaShow from "../pages/materiashow";
import ProfesorMateria from "../pages/materia";
import Anotacion from "../pages/anotacion";
import Documentos from "../pages/documentos/list/ListarDocumento";

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
        anotacion: <Anotacion />,
        documentos: <Documentos />
    }

    const [currentPage, setCurrentPage] = useState(pages.dashboard)

    const setPage = (page = "") =>{
        if(page === "dashboard") return setCurrentPage(pages.dashboard)
        if(page === "clase") return setCurrentPage(pages.clase)
        if(page === "materia") return setCurrentPage(pages.materia)
        if(page === "materiashow") return setCurrentPage(pages.materiashow)
        if(page === "anotacion") return setCurrentPage(pages.anotacion)
        if(page === "documentos+") return setCurrentPage(pages.documentos)
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