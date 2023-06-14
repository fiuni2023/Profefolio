import axios from "axios"
import APILINK from "../../../components/link"


const Get = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/AnotacionAlumno/getWithInfo `,
    body,
    {
        headers:{
            "Authorization": 'bearer ' + token,
            'Content-Type': 'application/json'
        }
    })
    return result.status === 200 ? result : null
}


const AlumnoServicePage = { Get}
export default AlumnoServicePage