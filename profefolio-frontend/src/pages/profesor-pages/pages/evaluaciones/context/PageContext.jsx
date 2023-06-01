import React, { useContext, useState } from "react";
import { createContext } from "react";
import { toast } from "react-hot-toast";

const PageContext = createContext();

export const usePageContext = () => {
    return useContext(PageContext)
}

export const PageProvider = ({ children }) => {
    

    //----------------------------------------------------States----------------------------------------------------
    
    const [ etapas, setEtapas ] = useState([
        [
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
        ],
        [
            {
                id: 3,
                nombre: "Tarea 3",
                puntaje_total: 15
            }
        ]
    ])
    const [ evalAlumnos, setEvalAlumnos ] = useState([
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
    ])
    const [nextId, setNextId] = useState(4)

    //----------------------------------------------------Effect Hooks----------------------------------------------------

    //----------------------------------------------------Functions----------------------------------------------------

    const getNextId=()=>{
        let newId = nextId
        setNextId(nextId+1)
        return newId
    }

    const handleAddEtapa = () => {
        if(etapas.length === 3) return toast.error("se intento agregar mas de 3 etapas")
        setEtapas((before)=>{return [...before, []]})
        setEvalAlumnos(evalAlumnos.map((e)=>{
            let newE = e
            newE.etapas = [...newE.etapas, []]
            return newE
        }))
    }

    const handleDeleteEtapa = (index) => {
        let newEtapas = etapas
        etapas.splice(index, 1)
        setEtapas(newEtapas)
        let newEvalAlumno = evalAlumnos.map((e)=>{
            let newAlumno = e
            let newEtapas = e.etapas
            newEtapas.splice(index, 1)
            newAlumno.etapas = newEtapas
            return newAlumno
        })
        setEvalAlumnos(newEvalAlumno)
    }

    const handleAddEvent = (i) => {
        let newEtapas = etapas
        newEtapas[i] = [...newEtapas[i], 
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
    }

    const handleEditEventName = (id, name) => {
        let newEtapas = etapas
        newEtapas.map((e)=>{
            let returnValue = e
            returnValue.map((t)=>{
                let returnValue = t
                if(returnValue.id === id) returnValue.nombre = name
                return returnValue
            })
            return returnValue
        })
        toast.success("se ha editado")
        setEtapas(newEtapas)
        console.log(newEtapas)
    }

    //----------------------------------------------------Return Values----------------------------------------------------


    const dataSet = {
        evalAlumnos,
        etapas
    }

    const functions = {
        handleAddEtapa,
        handleDeleteEtapa,
        handleAddEvent,
        handleEditEventName
    }

    const values = {
        dataSet,
        functions
    }

    return (
        <PageContext.Provider value={values}>
            {children}
        </PageContext.Provider>
    )
}