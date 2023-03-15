import axios from "axios";
import APILINK from "../../../components/link";

const getList = async (page, token)=>{
    const result = await axios.get(`${APILINK}/api/administrador/page/${page}`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const createAdmin = async (body, token)=>{
    const result = await axios.post(`${APILINK}/api/administrador`,
    body,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}

const AdminService = { getList, createAdmin }
export default AdminService