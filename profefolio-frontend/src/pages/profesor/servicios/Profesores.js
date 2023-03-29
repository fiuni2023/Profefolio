import axios from 'axios';



axios.get('https://localhost:7063/api/profesor', {
  headers: {
    Authorization: `Bearer ${token}` // Aquí debes pasar tu token JWT
  }
})
.then(response => {
  console.log(response.data);
})
.catch(error => {
  console.error(error);
});