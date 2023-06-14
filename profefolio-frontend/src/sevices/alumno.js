import axios from "axios";
import APILINK from "../components/link";

const getFirstPage = async (token)=>{
    const result = await axios.get(`${APILINK}/api/ColegiosAlumnos/all/page/0`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const AlumnoService = {getFirstPage}
export default AlumnoService