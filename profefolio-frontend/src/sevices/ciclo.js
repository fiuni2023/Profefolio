import axios from "axios";
import APILINK from "../components/link";

const getCiclos = async (token)=>{
    const result = await axios.get(`${APILINK}/api/Ciclo`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const CicloService = {getCiclos}
export default CicloService