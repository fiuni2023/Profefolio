import axios from "axios";
import APILINK from "../components/link";

const getList = async (page, token)=>{
    const result = await axios.get(`${APILINK}/api/administrador/page/${page}`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const getAll = async (token)=>{
    const result = await axios.get(`${APILINK}/api/administrador`,
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

const updateAdmin = async (id, body, token)=>{
    const result = await axios.put(`${APILINK}/api/administrador/${id}`,
    body,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}

const deleteAdmin = async (id, token)=>{
    const result = await axios.delete(`${APILINK}/api/administrador/${id}`,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}

const AdminService = { getList, getAll, createAdmin, updateAdmin, deleteAdmin }
export default AdminService