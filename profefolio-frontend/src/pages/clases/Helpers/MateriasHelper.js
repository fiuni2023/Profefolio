import axios from "axios"
import APILINK from "../../../components/link"

const createMateria = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/materia`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const getMateriaNoAssigned = async (idClase, token) => {
    const result = await axios.get(`${APILINK}/api/materia/NoAsignadas/${idClase}`,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}


const updateMateria= async (id, body, token) => {
    const result = await axios.put(`${APILINK}/api/materia/${id}`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result
}

const deleteMateria = async (id, token) => {
    const result = await axios.delete(`${APILINK}/api/materia/${id}`,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const MateriasService = { createMateria, getMateriaNoAssigned, updateMateria, deleteMateria }
export default MateriasService