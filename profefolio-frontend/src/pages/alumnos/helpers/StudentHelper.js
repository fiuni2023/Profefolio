import axios from "axios";
import APILINK from "../../../components/link";

const getStudentsPage = async (page, token) => {
    const result = await axios.get(`${APILINK}/api/ColegiosAlumnos/all/page/${page}`,
        {
            headers: {
                Authorization: 'Bearer ' + token
            }
        })
    return result.status === 200 ? result : null
}

const getAllClassStudents = async (idClase, token) => {
    const result = await axios.get(`${APILINK}/api/ClasesAlumnosColegio/${idClase}`,
        {
            headers: {
                Authorization: 'Bearer ' + token
            }
        })
    return result.status === 200 ? result : null
}

const getAllNotClassStudents = async (idClase, token) => {
    const result = await axios.get(`${APILINK}/api/ColegiosAlumnos/NoAssignedAlumnos/${idClase}`,
        {
            headers: {
                Authorization: 'Bearer ' + token
            }
        })
    return result.status === 200 ? result : null
}

const createStudent = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/alumnos`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 || result.status === 230 ? result : null
}

const updateStudent = async (id, body, token) => {
    const result = await axios.put(`${APILINK}/api/alumnos/${id}`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const deleteStudent = async (id, token) => {
    const result = await axios.delete(`${APILINK}/api/colegiosalumnos/${id}`,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}
const updateStudentsList = async (body, token) => {
    const result = await axios.put(`${APILINK}/api/ClasesAlumnosColegio`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const addStudentToSchool = async (body, token) =>{
    const result = await axios.post(`${APILINK}/api/ColegiosAlumnos`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const StudentsService = { getStudentsPage, getAllNotClassStudents, createStudent, updateStudent, deleteStudent, getAllClassStudents, updateStudentsList, addStudentToSchool }
export default StudentsService