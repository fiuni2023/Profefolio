import axios from "axios";
import APILINK from "../../../components/link";

const getStudentsPage = async (page, token)=>{
    const result = await axios.get(`${APILINK}/api/ColegiosAlumnos/page/${page}`,
    {
        headers:{
            Authorization: 'Bearer ' + token
        }
    })
    return result.status === 200? result : null
}

const createStudent = async (body, token)=>{
    const result = await axios.post(`${APILINK}/api/alumnos`,
    body,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}

const updateStudent = async (id, body, token)=>{
    const result = await axios.put(`${APILINK}/api/alumnos/${id}`,
    body,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}

const deleteStudent = async (id, token)=>{
    const result = await axios.delete(`${APILINK}/api/alumnos/${id}`,
    {
        headers:{
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
        
    })
    return result.status === 200? result : null
}

const StudentsService = { getStudentsPage, createStudent, updateStudent, deleteStudent }
export default StudentsService