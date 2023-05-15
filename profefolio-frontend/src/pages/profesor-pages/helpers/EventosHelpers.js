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


const EventosService = { getEventos }

export default EventosService