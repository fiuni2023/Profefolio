import axios from "axios";
import APILINK from "../components/link";

const GetDataChart = async (token) => {
    const response = await axios.get(`${APILINK}/api/Clase/grafico/admin`,
        {
            headers: {
                Authorization: 'Bearer ' + token
            }
        });
        
    const { clases, cantidades } = response.status === 200 ? response.data : { clases: [], cantidades: [] }
    return { clases, cantidades }
}
const AdminColegioService = {GetDataChart}
export default AdminColegioService