import axios from "axios";
import APILINK from "../../../../../components/link";

const updateAsistencia = async (materiaId, body, token) => {
    const result = await axios.put(`${APILINK}/api/Asistencia/${materiaId}`,
    body,
    {
        headers:{
            Authorization: 'Bearer ' + token,
            "Content-Type": "application/json"
        }
    })
    return result.status === 200? result : null
}

const AsistenciaService = {updateAsistencia}

export default AsistenciaService