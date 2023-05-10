import axios from "axios";
import APILINK from "../components/link";

const getFirstPage = async (token)=>{
    const result = await axios.get(`${APILINK}/api/Materia/page/0`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const MateriaService = {getFirstPage}
export default MateriaService