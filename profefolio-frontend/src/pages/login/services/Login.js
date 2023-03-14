import axios from "axios";

const PostLogin = async (mail, password)=>{
    const result = await axios.post("https://localhost:7063/login",
    {
        "email": mail,
        "password": password
    })
    return result.status === 200? result : null
}

const LoginService = { PostLogin }
export default LoginService