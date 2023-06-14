import axios from "axios";

import APILINK from "../../../../../components/link";

const createDocumento = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/Documento`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const getDocumento= async (id, token) => {
    const result = await axios.get(`${APILINK}/api/Documento/all/${id}`, {
      headers: {
          "Authorization": 'Bearer ' + token,
          "Content-Type": "application/json"

      },
    });
  
  return result.status === 200 ? result : null
};

const putDocumento= async (id, body, token) => {
    const result = await axios.put(`${APILINK}/api/Documento/${id}`, 
        body, {
        headers: {
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"
        },
    });
  
  return result.status === 200 ? result : null
};

const ClassesService = { createDocumento, getDocumento, putDocumento }
export default ClassesService