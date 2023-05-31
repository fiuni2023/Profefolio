import React, { useContext, useState } from "react";
import { createContext } from "react";

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

    //----------------------------------------------------Effect Hooks----------------------------------------------------

    //----------------------------------------------------Functions----------------------------------------------------

    const handleAddEtapa = () => {
        setEtapas((before)=>{return [...before, []]})
        setEvalAlumnos(evalAlumnos.map((e)=>{
            let newE = e
            newE.etapas = [...e.etapas, []]
            return newE
        }))
    }
    const handleDeleteEtapa = () => {
        console.log("not implemented")
    }
    //----------------------------------------------------Return Values----------------------------------------------------


    const dataSet = {
        evalAlumnos,
        etapas
    }

    const functions = {
        handleAddEtapa,
        handleDeleteEtapa
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