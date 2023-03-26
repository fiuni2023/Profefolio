import axios from "axios";
import APILINK from "../components/link";

const PostLogin = async (mail, password)=>{
    console.log({
        "email": mail,
        "password": password
    })

    const result = await axios.post(`${APILINK}/login`,
    {
        "email": mail,
        "password": password
    })
    console.log(result)
    return result.status === 200? result : null
}

const LoginService = { PostLogin }
export default LoginService