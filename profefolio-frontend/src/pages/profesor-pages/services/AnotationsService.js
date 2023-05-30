import axios from "axios"
import APILINK from "../../../components/link"

const Post = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/Anotacion`,
    body,
    {
        headers:{
            "Authorization": 'bearer ' + token,
            'Content-Type': 'application/json'
        }
    })
    return result.status === 200 ? result : null
}

const Get = async (token) => {
    const result = await axios.get(`${APILINK}/api/Anotacion`,
    {
        headers:{
            "Authorization": 'bearer ' + token
        }
    })
    return result.status === 200 ? result : null
}

const Delete = async (id, token) => {
    const result = await axios.delete(`${APILINK}/api/Anotacion/${id}`,
    {
        headers:{
            "Authorization": 'bearer ' + token
        }
    })
    return result.status === 200 ? result : null
}

const AnotationsService = { Post, Get, Delete }
export default AnotationsService