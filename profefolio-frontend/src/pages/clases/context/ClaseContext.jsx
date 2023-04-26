import React, { useContext, useState } from "react";
import { createContext } from "react";

const ClaseContext = createContext();

export const useClaseContext = () => {
    return useContext(ClaseContext)
}

export const ClaseProvider = ({ children }) => {
    const [profesoresOption, setProfesoresOption] = useState();
    const [listaMaterias, setListaMaterias] = useState([
        { id: 1, nombre: "Matematicas", status: "reload", profesores: [{ id: 1, nombre: "John Foe", status: "new" }, { id: 21, nombre: "Juan Foe", status: "new" }] },
        { id: 2, nombre: "Matematicas", status: "reload", profesores: [{ id: 1, nombre: "John Foe", status: "new" }, { id: 21, nombre: "Juan Foe", status: "new" }] },
        { id: 3, nombre: "Matematicas", status: "new", profesores: [{ id: 3, nombre: "John Dafoe", status: "new" }, { id: 22, nombre: "Luis Foe", status: "new" }, { id: 23, nombre: "John Foe", status: "new" }, { id: 24, nombre: "John Foe", status: "new" }] },
        { id: 4, nombre: "Matematicas", status: "new", profesores: [{ id: 4, nombre: "John Foe Garcia", status: "new" }] },
        { id: 5, nombre: "Matematicas", status: "new", profesores: [{ id: 5, nombre: "John Foe Gonzalez Perez", status: "new" }] },
        { id: 6, nombre: "Matematicas", status: "new", profesores: [{ id: 6, nombre: "John Foe", status: "new" }] },
        { id: 7, nombre: "Matematicas", status: "new", profesores: [{ id: 7, nombre: "John Foe", status: "new" }] },
        { id: 8, nombre: "Matematicas", status: "new", profesores: [{ id: 8, nombre: "John Foe", status: "new" }] },
        { id: 9, nombre: "Matematicas", status: "new", profesores: [{ id: 9, nombre: "John Foe", status: "new" }] },
        { id: 10, nombre: "Matematicas", status: "new", profesores: [{ id: 10, nombre: "John Foe", status: "new" }] },
        { id: 11, nombre: "Matematicas", status: "new", profesores: [{ id: 11, nombre: "John Foe", status: "new" }] },
        { id: 12, nombre: "Matematicas", status: "new", profesores: [{ id: 12, nombre: "John Foe", status: "new" }] },
    ]);

    const getListaMaterias = () => {
        return listaMaterias;
    }

    const setStatusProfesorMateria = (idMateria, id, status) => {
        const index = listaMaterias.findIndex(a => a.id === idMateria)
        if (index >= 0) {
            const indexProf = listaMaterias[index].profesores.findIndex(a => a.id === id)
            if (indexProf >= 0) {
                listaMaterias[index].profesores[indexProf].status = status;
                setListaMaterias([...listaMaterias])
            }
        }
    }

    const setStatusMateria = (idMateria, status) => {
        const index = listaMaterias.findIndex(a => a.id === idMateria)
        if (index >= 0) {

            listaMaterias[index].status = status;
            setListaMaterias([...listaMaterias])
        }
    }

    const addMateriaToList = (nombre) => {
        const newMateria = {
            id: Date.now().toString(),
            nombre,
            status: "new",
            profesores: []
        }
        setListaMaterias([...listaMaterias, newMateria])
    }

    const getClaseSelectedId = () => {
        const json = localStorage.getItem("id-clase-selected")
        const value = JSON.parse(json);
        return value;
    }

    const setClaseSelectedId = (id) => {
        localStorage.setItem("id-clase-selected", id)
    }

    const getProfesoresOption = (profesFilter = []) => {
        //se filtra los profes a partir de un array de profes
        return profesoresOption.filter(a => !(profesFilter.find(b => b.id === a.id)))
    }

    const setProfesoresOptions = (profes) =>{
        setProfesoresOption(profes)
    
    const setColegioId = (colegioId) => {
        localStorage.setItem("id-colegio", colegioId)
    }
    const getColegioId = () => {
        if (localStorage.getItem('id-colegio') ? true : false) return JSON.parse(localStorage.getItem('id-colegio'))
    }

    const values = {
        getClaseSelectedId,
        setClaseSelectedId,
        getListaMaterias,
        setStatusProfesorMateria,
        setStatusMateria,
        addMateriaToList,
        getProfesoresOption,
        setProfesoresOptions,
        setColegioId,
        getColegioId
    }

    return (
        <ClaseContext.Provider value={values}>
            <>
                {children}
            </>
        </ClaseContext.Provider>
    )
}