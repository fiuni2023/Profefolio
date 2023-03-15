import axios from "axios";
import APILINK from "../../../components/link";

const PostLogin = async (mail, password)=>{
    const result = await axios.post(`${APILINK}/login`,
    {
        "email": mail,
        "password": password
    })
    return result.status === 200? result : null
}

const LoginService = { PostLogin }
export default LoginService