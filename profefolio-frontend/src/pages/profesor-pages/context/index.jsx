import React, { useContext, useEffect, useState } from "react";
import { createContext } from "react";
import ProfesorPage from "../pages/dashboard";
import ProfesorClase from "../pages/clase";
import ProfesorMateriaShow from "../pages/materiashow";
import ProfesorMateria from "../pages/materia";
import Anotacion from "../pages/anotacion";
import Documentos from "../pages/documentos/list/ListarDocumento";
import Asistencia from "../pages/asistencia/Asistencia";
import ProfesorPagesService from "../services/ProfesorPagesService";
import { useGeneralContext } from "../../../context/GeneralContext";
import Evaluaciones from "../pages/evaluaciones";

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
        documentos: <Documentos />,
        asistencia: <Asistencia />,
        evaluaciones: <Evaluaciones />
    }
    
    const { getToken } = useGeneralContext()
    const token = getToken()

    //----------------------------------------------------States----------------------------------------------------
    
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
    const [ asistencias, setAsistencias ] = useState([])
    const [ puntajes, setPuntajes ] = useState([])
    const [ alumnos, setAlumnos ] = useState([])
    const [ loading, setLoading ] = useState(true)
    //----------------------------------------------------Effect Hooks----------------------------------------------------

    useEffect(()=>{
        // const body = {opcion: 'card-clases', id: 1, anho: 2023}
        
        ProfesorPagesService.GetColegios(token)
        .then(function(d){
            setColegios(d.data)
            setLoading(false);
        })
    },[fetch_data, token])

    useEffect(()=>{
        if(colegioId){
            let body = {opcion: 'card-clases', id: colegioId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(function(d){
                setClases(d.data)
                setLoading(false)
            })
        } else {
            setLoading(false)
        }
    },[fetch_data, token, colegioId])

    useEffect(()=>{
        if(claseId){
            let body = {opcion: 'card-materias', id: claseId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(d=>setMaterias(d.data))
            body = {opcion: 'lista-alumnos', id: claseId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(function (d){
                setAlumnos(d.data)
                setLoading(false)
            })
        } else {
            setLoading(false)
        }
    },[fetch_data, token, claseId])

    useEffect(()=>{
        if(materiaId){
            let isMaterias, isAsistencia, isPuntajes = false; 
            let body = {opcion: 'cards-materia', id: materiaId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(function(d){
                setMateriaShow(d.data)
                isMaterias = true
                handleSetLoading(isMaterias, isAsistencia, isPuntajes)
            })
            body = {opcion: 'promedio-asistencias', id: materiaId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(function(d){
                setAsistencias(d.data)
                isAsistencia = true
                handleSetLoading(isMaterias, isAsistencia, isPuntajes)
            })
            body = {opcion: 'promedio-puntajes', id: materiaId, anho: 2023}
            ProfesorPagesService.Get(body, token)
            .then(function(d){
                isPuntajes = true
                handleSetLoading(isMaterias, isAsistencia, isPuntajes)
                setPuntajes(d.data)
            })
        } else {
            setLoading(false);
        }
    },[fetch_data, token, materiaId])

    //----------------------------------------------------Functions----------------------------------------------------

    const setPage = (page = "", isBackButton = false) =>{
        setLoading(true)
        //---las siguientes lineas deben ser borradas luego de implementar las paginas de anotacion, documento, asistencia y evaluaciones
        let aux = ["anotacion", "documento", "asistencia", "evaluaciones"]
        if (aux.includes(page)){
            setTimeout(function(){
                setLoading(false)
            }, 1000);
        }
        //----------------------------------

        if(isBackButton)setTimeout(function(){setLoading(false)},500)
        if(page === "dashboard") return setCurrentPage(pages.dashboard)
        if(page === "clase") return setCurrentPage(pages.clase)
        if(page === "materia") return setCurrentPage(pages.materia)
        if(page === "materiashow") return setCurrentPage(pages.materiashow)
        if(page === "anotacion") return setCurrentPage(pages.anotacion)
        if(page === "documento") return setCurrentPage(pages.documentos)
        if(page === "asistencia") return setCurrentPage(pages.asistencia)
        if(page === "evaluaciones") return setCurrentPage(pages.evaluaciones)
        setCurrentPage(pages.dashboard)

        
    }

    const fetchData = () => {
        setLoading(true)
        setFetchData((before)=>{return !before})
    }

    const handleSetLoading = (b1, b2, b3) => {
        if (b1 && b2 && b3) setLoading(false)
    }

    //----------------------------------------------------Return Values----------------------------------------------------

    const stateController = {
        colegioId, 
        setColegioId,
        claseId,
        setClaseId,
        materiaId, 
        setMateriaId,
        setMateriaName,
        setLoading
    }

    const dataSet = {
        colegios,
        clases,
        materias,
        materiaShow,
        materiaName,
        asistencias,
        puntajes,
        alumnos,
        loading
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