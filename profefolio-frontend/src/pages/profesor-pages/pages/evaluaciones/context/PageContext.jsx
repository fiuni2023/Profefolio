import React, { useContext, useEffect, useState } from "react";
import { createContext } from "react";
import { toast } from "react-hot-toast";
import { useModularContext } from "../../../context";
import EvaluacionService from "../services/EvaluacionService";
import { useGeneralContext } from "../../../../../context/GeneralContext";

const PageContext = createContext();

export const usePageContext = () => {
    return useContext(PageContext)
}

export const PageProvider = ({ children }) => {

    const { getToken } = useGeneralContext()
    const token = getToken()
    const { stateController } = useModularContext()
    const { materiaId } = stateController

    //----------------------------------------------------States----------------------------------------------------

    /*
    [
        {
            etapa: "a",
            etapas:[
                {
                    id: 1,
                    nombre: "Tarea 1",
                    puntaje_total: 10
                },
                {
                    id: 2,
                    nombre: "Tarea 2",
                    puntaje_total: 10
                }
            ]
        },
        {
            etapa: "b",
            etapas: [
                {
                    id: 3,
                    nombre: "Tarea 3",
                    puntaje_total: 15
                }
            ]
        }
    ]
    */
    const [etapas, setEtapas] = useState([
        {
            etapa: "Primera",
            etapas: []
        },
        {
            etapa: "Segunda",
            etapas: []
        }
    ])
    /*
    [
        {
            id: 1,
            nombreAlumno: "Alumno 1",
            etapas: [[8, 5], [10]]
        },
        {
            id: 2,
            nombreAlumno: "Alumno 2",
            etapas: [[10, 10], [15]]
        }
    ]
    */
    const [evalAlumnos, setEvalAlumnos] = useState([])


    const [showModal, setShowModal] = useState(false)
    const [etapaName, setEtapaName] = useState("")
    const [newEvalName, setNewEvalName] = useState("")
    const [newEvalPoint, setNewEvalPoint] = useState(0)
    const [newEvalType, setNewEvalType] = useState("Examen")
    const [fetch_data, setFetchData] = useState([])
    const [idCounter, setIdCounter] = useState(0)

    //----------------------------------------------------Effect Hooks----------------------------------------------------

    //----------------------------------------------------Functions----------------------------------------------------

    const handleAddEtapa = () => {
        if (etapas.length === 2) return toast.error("se intento agregar mas de 2 etapas")
        setEtapas((before) => {
            return [...before, {
                etapa: etapas.length === 0 ? "Primera" : "Segunda",
                etapas: []
            }]
        })
        setEvalAlumnos(evalAlumnos?.length > 1 ?
            evalAlumnos.map((e) => {
                let newE = e
                newE.etapas = [...newE.etapas, []]
                return newE
            })
            :
            []
        )
    }

    const handleDeleteEtapa = (index) => {
        let newEtapas = etapas
        etapas.splice(index, 1)
        setEtapas(newEtapas)
        let newEvalAlumno = evalAlumnos.map((e) => {
            let newAlumno = e
            let newEtapas = e.etapas
            newEtapas.splice(index, 1)
            newAlumno.etapas = newEtapas
            return newAlumno
        })
        setEvalAlumnos(newEvalAlumno)
    }

    const handleAddEvent = () => {
        /* 
            let newEtapas = etapas
            newEtapas[i].etapas = [...newEtapas[i].etapas, 
            {
                id: getNextId(),
                nombre: "Tarea Nueva",
                puntaje_total: 10
            }]
            setEtapas(newEtapas)
            let newEval = evalAlumnos.map(e=>{
                let newAlumno = e
                newAlumno.etapas[i] = [...newAlumno.etapas[i],0]
                return newAlumno
            })
            setEvalAlumnos(newEval)
        */
        let body = {
            "tipo": newEvalType,
            "nombre": newEvalName,
            "etapa": etapaName,
            "fecha": new Date().toISOString(),
            "idMateriaLista": materiaId,
            "puntaje": parseInt(newEvalPoint)
        }
        EvaluacionService.createNewEvent(body, token)
            .then(r => {
                toast.success("Se agrego la tarea con exito")
                doFetch()
            })
    }

    const handleEditEventName = (id, name) => {
        let newEtapas = etapas
        newEtapas.map((e) => {
            e.map((t) => {
                if (t.id === id) t.nombre = name
                return t
            })
            return e
        })
        toast.success("se ha editado")
        setEtapas(newEtapas)
    }

    const handleEditCalification = (id, puntaje_total, valor, nombre) => {
        let valorVerdadero = valor>puntaje_total? puntaje_total : valor
        let body = {
            "idEvaluacion": id,
            "puntaje": valorVerdadero,
            "modo": "p",
            "nombreEvaluacion": nombre,
            "puntajeTotal": puntaje_total
        }
        EvaluacionService.putCalification(materiaId, body, token)
        .then(r=>{
            console.log(r)
            toast.success("Se cambio el puntaje con exito")
            doFetch()
        })
    }

    const handleEditCalificationNombre = (id, puntaje_total, valor, nombre) => {
        let valorVerdadero = valor>puntaje_total? puntaje_total : valor
        let body = {
            "idEvaluacion": id,
            "puntaje": valorVerdadero,
            "modo": "nn",
            "nombreEvaluacion": nombre,
            "puntajeTotal": puntaje_total
        }
        EvaluacionService.putCalification(materiaId, body, token)
        .then(r=>{
            console.log(r)
            toast.success("Se cambio el nombre con exito")
            doFetch()
        })
    }

    const handleEditCalificationPT = (id, puntaje_total, valor, nombre) => {
        let valorVerdadero = valor>puntaje_total? puntaje_total : valor
        let body = {
            "idEvaluacion": id,
            "puntaje": valorVerdadero,
            "modo": "pt",
            "nombreEvaluacion": nombre,
            "puntajeTotal": puntaje_total
        }
        EvaluacionService.putCalification(materiaId, body, token)
        .then(r=>{
            console.log(r)
            toast.success("Se cambio el punto Total con exito")
            doFetch()
        })
    }

    const doFetch = () => {
        setFetchData(before=>[before])
    }

    //----------------------------------------------------Return Values----------------------------------------------------



    useEffect(() => {
        let etapasP = []
        let etapasS = []
        let newAlumnoState = []
        let alumnoNew = {}
        let alumnoCalificacionP = []
        let alumnoCalificacionS = []
        EvaluacionService.getAll(materiaId, token)
            .then((r) => {
                if (r.data?.alumnos.length > 1) {
                    r.data.alumnos[0].etapas.map((e, i) => {
                        e.puntajes?.map((p) => {
                            if (i === 0) {
                                etapasP = [...etapasP, {id: p.idEvaluacion, puntaje: p.puntajeLogrado, puntaje_total: p.puntajeTotal, porcentaje_logrado: p.porcentajeLogrado, nombre: p.nombreEvaluacion}]
                            } else {
                                etapasS = [...etapasS, {id: p.idEvaluacion, puntaje: p.puntajeLogrado, puntaje_total: p.puntajeTotal, porcentaje_logrado: p.porcentajeLogrado, nombre: p.nombreEvaluacion}]
                            }
                            return p
                        })
                        return e
                    })
                    setEtapas([
                        {
                            etapa: "Primera",
                            etapas: etapasP
                        },
                        {
                            etapa: "Segunda",
                            etapas: etapasS
                        }
                    ])
    
                    //Alumnos Array
                    r.data.alumnos.map((a)=>{
                        alumnoCalificacionP = []
                        alumnoCalificacionS = []
                        alumnoNew = {id: a.alumnoId, nombreAlumno: `${a.nombre} ${a.apellido}`}
                        a.etapas?.map((e)=>{
                            e.puntajes.map((p)=>{
                                if(e.etapa === "Primera"){
                                    alumnoCalificacionP = [...alumnoCalificacionP, {id: p.idEvaluacion, puntaje: p.puntajeLogrado, puntaje_total: p.puntajeTotal, porcentaje_logrado: p.porcentajeLogrado, nombre: p.nombreEvaluacion}]
                                }else{
                                    alumnoCalificacionS = [...alumnoCalificacionS, {id: p.idEvaluacion, puntaje: p.puntajeLogrado, puntaje_total: p.puntajeTotal, porcentaje_logrado: p.porcentajeLogrado, nombre: p.nombreEvaluacion}]
                                }
                                return p
                            })
                            return e
                        })
                        alumnoNew = {...alumnoNew, etapas: [alumnoCalificacionP, alumnoCalificacionS]}
                        newAlumnoState=[...newAlumnoState, alumnoNew]
                        return a
                    })
                    setEvalAlumnos(newAlumnoState)
                }
            })
    // eslint-disable-next-line 
    }, [materiaId, token, fetch_data])

    /*
    [
        {
            id: 1,
            nombreAlumno: "Alumno 1",
            etapas: [[8, 5], [10]]
        },
        {
            id: 2,
            nombreAlumno: "Alumno 2",
            etapas: [[10, 10], [15]]
        }
    ]
    */

    //----------------------------------------------------Return Values----------------------------------------------------   


    const dataSet = {
        evalAlumnos,
        etapas,
        showModal,
        etapaName,
        newEvalName,
        newEvalPoint,
        newEvalType
    }

    const functions = {
        handleAddEtapa,
        handleDeleteEtapa,
        handleAddEvent,
        handleEditEventName,
        handleEditCalification,
        handleEditCalificationNombre,
        handleEditCalificationPT,
        doFetch
    }

    const stateHandlers = {
        setShowModal,
        setEtapaName,
        setNewEvalName,
        setNewEvalPoint,
        setNewEvalType
    }

    const values = {
        dataSet,
        functions,
        stateHandlers
    }

    return (
        <PageContext.Provider value={values}>
            {children}
        </PageContext.Provider>
    )
}