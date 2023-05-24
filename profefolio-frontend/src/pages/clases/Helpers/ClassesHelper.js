import axios from "axios";
import APILINK from "../../../components/link";

const getClassesPage = async (page, idColegio, token) => {
    const result = await axios.get(`${APILINK}/api/Clase/page/${idColegio}/${page}`,
        {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    return result.status === 200 ? result : null
}

const getClassesByIdNombre = async (id, token) => {
    try {
      const result = await axios.get(`${APILINK}/api/clase/${id}`, {
        headers: {
          Authorization: "Bearer " + token,
        },
      });
      if (result.status === 200) {
        const { nombre } = result.data; // Suponemos que la respuesta de tu API es un objeto con el nombre de la clase
        return nombre;
      } else {
        return null;
      }
    } catch (error) {
      console.error("Error al obtener la clase: ", error);
      return null;
    }
  };


  const getProfesoresParaClase = async (token) => {
    const result = await axios.get(`${APILINK}/api/profesor/misprofesores`,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },
        })
    return result.status === 200 ? result : null
}

//
const getMateriasProfesores= async (id, token) => {
      const result = await axios.get(`${APILINK}/api/administrador/materia/profesores/${id}`, {
        headers: {
            "Authorization": 'Bearer ' + token,
            "Content-Type": "application/json"

        },
      });
    
    return result.status === 200 ? result : null

  };



//



const getClassesById = async (id, token) => {
    const result = await axios.get(`${APILINK}/api/clase/${id}`,
        {
            headers: {
                Authorization: 'Bearer ' + token
            }
        })
    return result.status === 200 ? result : null
}


const createClasse = async (body, token) => {
    const result = await axios.post(`${APILINK}/api/clase`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}

const updateClasse = async (id, body, token) => {
    const result = await axios.put(`${APILINK}/api/clase/${id}`,
        body,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result
}

const deleteClasse = async (id, token) => {
    const result = await axios.delete(`${APILINK}/api/clase/${id}`,
        {
            headers: {
                "Authorization": 'Bearer ' + token,
                "Content-Type": "application/json"
            },

        })
    return result.status === 200 ? result : null
}




const createMateriaProfesor = async (body, token) => {
    console.log('createMateriaProfesor body:', body);
    const result = await axios.put(
      `${APILINK}/api/administrador/materia/profesores`,
      body,
      {
        headers: {
          "Authorization": 'Bearer ' + token,
          "Content-Type": "application/json"
        }
      }
    );
    return result.status === 200 ? result : null;
  };
  



const getProfesores = (token) => {

    
    return axios.get(`${APILINK}/api/profesor/misprofesores`, {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    })
    .then(response => {
      return response.data;
    })
    .catch(error => {
      console.error(error);
      return [];
    });
  }
  
  



const ClassesService = { getClassesPage, getClassesByIdNombre,getClassesById, createClasse, updateClasse, deleteClasse, getProfesoresParaClase ,getProfesores, createMateriaProfesor,getMateriasProfesores}
export default ClassesService