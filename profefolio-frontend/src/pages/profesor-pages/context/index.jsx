import React, { useContext, useEffect, useState } from "react";
import { createContext } from "react";
import ProfesorPage from "../pages/dashboard";
import ProfesorClase from "../pages/clase";
import ProfesorMateriaShow from "../pages/materiashow";
import ProfesorMateria from "../pages/materia";
import Anotacion from "../pages/anotacion";
import ProfesorPagesService from "../services/ProfesorPagesService";
import { useGeneralContext } from "../../../context/GeneralContext";

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
        anotacion: <Anotacion />
    }
    
    const { getToken } = useGeneralContext()
    const token = getToken()

    
    const [ fetch_data, setFetchData ] = useState(false)
    const [ currentPage, setCurrentPage ] = useState(pages.dashboard)
    const [ colegios, setColegios ] = useState([])
    const [colegioId, setColegioId] = useState(0) 
    const [ clases, setClases ] = useState([])
    const [ claseId, setClaseId] = useState(0)
    const [ materias, setMaterias ] = useState([])
    const [ materiaId, setMateriaId ] = useState(0)
    const [ materiaShow, setMateriaShow ] = useState({})
    const [ materiaName, setMateriaName ] = useState("")

    useEffect(()=>{
        // const body = {opcion: 'card-clases', id: 1, anho: 2023}
        ProfesorPagesService.GetColegios(token)
        .then(d=>setColegios(d.data))
    },[fetch_data, token])

    useEffect(()=>{
        if(colegioId){
            const body = {opcion: 'card-clases', id: colegioId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(d=>setClases(d.data))
        }
    },[fetch_data, token, colegioId])

    useEffect(()=>{
        if(claseId){
            const body = {opcion: 'card-materias', id: claseId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(d=>setMaterias(d.data))
        }
    },[fetch_data, token, claseId])

    useEffect(()=>{
        if(materiaId){
            const body = {opcion: 'cards-materia', id: materiaId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(d=>setMateriaShow(d.data))
        }
    },[fetch_data, token, materiaId])


    const setPage = (page = "") =>{
        if(page === "dashboard") return setCurrentPage(pages.dashboard)
        if(page === "clase") return setCurrentPage(pages.clase)
        if(page === "materia") return setCurrentPage(pages.materia)
        if(page === "materiashow") return setCurrentPage(pages.materiashow)
        if(page === "anotacion") return setCurrentPage(pages.anotacion)
        setCurrentPage(pages.dashboard)
        
    }

    const fetchData = () => {
        setFetchData((before)=>{return !before})
    }

    const stateController = {
        colegioId, 
        setColegioId,
        claseId,
        setClaseId,
        materiaId, 
        setMateriaId,
        setMateriaName
    }

    const dataSet = {
        colegios,
        clases,
        materias,
        materiaShow,
        materiaName
    }

    const values = {
        currentPage,
        setPage,
        fetchData,
        stateController,
        dataSet
    }

    return (
        <ModularContext.Provider value={values}>
            {children}
        </ModularContext.Provider>
    )
}