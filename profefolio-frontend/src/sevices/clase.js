import axios from "axios";
import APILINK from "../components/link";

const getFirstPage = async (idColegio, token)=>{
    const result = await axios.get(`${APILINK}/api/Clase/byColegio/${idColegio}`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const ClaseService = {getFirstPage}
export default ClaseService