import axios from "axios";
import APILINK from "../components/link";

const getFirstPage = async (token)=>{
    const result = await axios.get(`${APILINK}/api/Profesor/MisProfesores`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const ProfesorService = {getFirstPage}
export default ProfesorService