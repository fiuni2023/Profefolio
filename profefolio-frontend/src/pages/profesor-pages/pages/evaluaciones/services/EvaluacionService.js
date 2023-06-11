import axios from "axios"
import APILINK from "../../../../../components/link"

const getAll = async (materiaId, token) => {
    const result = await axios.get(`${APILINK}/api/Calificacion/${materiaId}`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const createNewEvent = async (body, token)=>{
    const result = await axios.post(`${APILINK}/api/Evento`,
    body,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}
const putCalification = async (materiaId, body, token) => {
    const result = await axios.put(`${APILINK}/api/Calificacion/${materiaId}`,
    body,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}

const EvaluacionService = {getAll, createNewEvent, putCalification}

export default EvaluacionService