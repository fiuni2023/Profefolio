import axios from "axios"
import APILINK from "../../../components/link"

const Get = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/DashboardProfesor`,
    body,
    {
        headers:{
            "Authorization": 'bearer ' + token,
            'Content-Type': 'application/json'
        }
    })
    return result.status === 200 ? result : null
}

const GetColegios = async (token) => {
    const result = await axios.get(`${APILINK}/api/DashboardProfesor/colegios`,
    {
        headers:{
            "Authorization": 'bearer ' + token,
        }
    })
    return result.status === 200 ? result : null
}

const GetEventos = async (token) => {
    const result = await axios.get(`${APILINK}/api/Evento`,
    {
        headers:{
            "Authorization": 'bearer ' + token,
        }
    })
    return result.status === 200 ? result : null
}

const ProfesorPagesService = { GetColegios, Get, GetEventos }
export default ProfesorPagesService