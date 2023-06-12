import axios from "axios";
import APILINK from "../../../components/link";

const createProfesor = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/profesor`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 || result.status === 230 ? result : null
}

const addProfesorToSchool = async (body, token) =>{
    const result = await axios.post(`${APILINK}/api/ColegioProfesor`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const ProfesorService = { addProfesorToSchool,createProfesor }
export default ProfesorService