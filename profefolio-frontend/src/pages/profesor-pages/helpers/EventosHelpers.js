import axios from "axios";
import APILINK from "../../../components/link";


const getEventos = async (token) => {
    const response = await axios.get(`${APILINK}/api/Evento`, {
        headers: {
            Authorization: "Bearer " + token,
        }
    });
    return response.status === 200 ? response.data : null
}

const postEvento = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/Evento`,
    body,
    {
        headers: {
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
    })

    return result.status === 200 ? result.status : null

}


const EventosService = { getEventos, postEvento }

export default EventosService