import axios from "axios";

const PostLogin = async (mail, password)=>{
    const result = await axios.post("https://localhost:7063/login",
    {
        "email": "CARLOS.TORRES@123MAIL.COM",
        "password": "Carlos.Torres123"
    })
    return result.status === 200? result : null
}

export default { PostLogin }