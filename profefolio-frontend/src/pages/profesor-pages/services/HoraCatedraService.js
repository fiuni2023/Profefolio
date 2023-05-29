import axios from "axios"
import APILINK from "../../../components/link"

const Post = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/HoraCatedra`,
    body,
    {
        headers:{
            "Authorization": 'bearer ' + token,
            'Content-Type': 'application/json'
        }
    })
    return result.status === 200 ? result : null
}

const Second = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/HorasCatedrasMaterias`,
    body,
    {
        headers:{
            "Authorization": 'bearer ' + token,
            'Content-Type': 'application/json'
        }
    })
    return result.status === 200 ? result : null
}

const HoraCatedraService = { Post, Second }
export default HoraCatedraService