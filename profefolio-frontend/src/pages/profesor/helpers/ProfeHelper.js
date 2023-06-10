import axios from "axios";
import APILINK from "../../../components/link";

const addProfesorToSchool = async (body, token) =>{
    const result = await axios.post(`${APILINK}/api/colegioprofesor`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const ProfesorService = { addProfesorToSchool }
export default ProfesorService