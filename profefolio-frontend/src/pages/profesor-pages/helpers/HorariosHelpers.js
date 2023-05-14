import axios from "axios";
import APILINK from "../../../components/link";


const getHorariosColegios = async (token) => {
    const response = await axios.get(`${APILINK}/api/HorasCatedrasMaterias`, {
        headers: {
            Authorization: "Bearer " + token,
        }
    });
    return response.status === 200 ? response.data : null
}

const getColegiosAndHorarios = async (token) => {
    const response = await axios.get(`${APILINK}/api/DashboardProfesor/colegios`, {
        headers: {
            Authorization: "Bearer " + token,
        }
    });
    return response.status === 200 ? response.data : null
}
const HorarioService = { getHorariosColegios, getColegiosAndHorarios }

export default HorarioService