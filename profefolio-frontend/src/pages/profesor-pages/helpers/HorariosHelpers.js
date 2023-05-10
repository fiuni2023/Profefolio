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
const HorarioService = { getHorariosColegios }

export default HorarioService