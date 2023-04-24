import axios from "axios";
import APILINK from "../../../components/link";

const getClassesPage = async (page, idColegio, token) => {
    const result = await axios.get(`${APILINK}/api/Clase/page/${idColegio}/${page}`,
        {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    return result.status === 200 ? result : null
}

const getClassesById = async (id, token) => {
    const result = await axios.get(`${APILINK}/api/clase/${id}`,
        {
            headers: {
                Authorization: 'Bearer ' + token
            }
        })
    return result.status === 200 ? result : null
}


const createClasse = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/clase`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const updateClasse = async (id, body, token) => {
    const result = await axios.put(`${APILINK}/api/clase/${id}`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result
}

const deleteClasse = async (id, token) => {
    const result = await axios.delete(`${APILINK}/api/clase/${id}`,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const getProfesoresParaClase = async (token) => {
    const result = await axios.get(`${APILINK}/api/profesor/misprofesores`,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },
        })
    return result.status === 200 ? result : null
}


const ClassesService = { getClassesPage, getClassesById, createClasse, updateClasse, deleteClasse, getProfesoresParaClase }
export default ClassesService