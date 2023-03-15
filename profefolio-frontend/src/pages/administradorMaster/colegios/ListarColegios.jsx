import React, { useEffect, useState } from "react";
import Table from 'react-bootstrap/Table';
import './ListarColegios.css'
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi"
import { BiPencil } from "react-icons/bi"
import { BiTrash } from "react-icons/bi"
import { BiInfoCircle } from "react-icons/bi"
import ModalAgregarColegios from './ModalAgregarColegios'
import axios from "axios";
import Pagination from 'react-bootstrap/Pagination';
const ListarColegios = () => {
  const navigate = useNavigate()
  const [totalPage, setTotalPage] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [nextPage, setNetPage] = useState(false);
  const [colegios, setColegios] = useState([]);
  var config = {
    method: 'get',
    url: `https://localhost:7063/api/ColegiosFull/page/0`,
    headers: {
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicHJ1ZWJhQGdtYWlsLmNvbSIsImp0aSI6IjE5OGRjNGRhLTgxMzQtNDkwMC04NTNjLTNlZjY5MDE0ZGVhZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluaXN0cmFkb3IgZGUgQ29sZWdpbyIsImV4cCI6MTY3ODc3Mzk2NywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.NyBXTa2FMx9JinQe9GQdOL2LXAJ90JxS4DiKQaR5OQ8'
    }
  };
  useEffect(() => {
    axios(config)
      .then(function (response) {
        setColegios(response.data.dataList); //Guarda los datos
        setTotalPage(response.data.totalPage);//Total de Paginas
        setCurrentPage(response.data.currentPage)//Actualiza la pagina en donde estan los datos

      })
      .catch(function (error) {
        console.log(error);
      });

  }, [])

const recargaDatos=(id)=>{
 var config = {
    method: 'get',
    url: `https://localhost:7063/api/ColegiosFull/page/${id}`,
    headers: {
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicHJ1ZWJhQGdtYWlsLmNvbSIsImp0aSI6IjE5OGRjNGRhLTgxMzQtNDkwMC04NTNjLTNlZjY5MDE0ZGVhZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluaXN0cmFkb3IgZGUgQ29sZWdpbyIsImV4cCI6MTY3ODc3Mzk2NywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.NyBXTa2FMx9JinQe9GQdOL2LXAJ90JxS4DiKQaR5OQ8'
    }
  };
    axios(config)
      .then(function (response) {
       
        setColegios(response.data.dataList); //Guarda los datos
        setTotalPage(response.data.totalPage);//Total de Paginas
        setCurrentPage(response.data.currentPage)//Actualiza la pagina en donde estan los datos

      })
      .catch(function (error) {
        console.log(error);
      });
     
}
  let items = [];
 
  for (let number = 0; number < totalPage; number++) {
    items.push(
      <Pagination.Item key={number} >
        {number}
      </Pagination.Item>,
    );
  }

  const handleCurrentPage=(idPage)=>{
    setCurrentPage(idPage);
    recargaDatos(idPage);
    
}
    
  
  return (
    <>
      <div className="container-principal">
        <div className="nombre-pagina">
          <button className="button-back" onClick={() => { navigate('/administrador') }}><BiArrowBack /></button>
          <span>Colegios</span>
        </div>
        <div className="table-principal" >
          <Table bordered >
            <thead>
              <tr>
              <th id="table-border">Numero</th>
                <th id="table-border">Nombre</th>
                <th id="table-border">Administrador</th>
                
                <th className="actions-th" id="table-border">Acciones</th>
              </tr>
            </thead>
            <tbody >
              {colegios.map((colegio, index) => (
                <tr key={colegio.id}>
                  <td id="table-border" className="numero-td" >{index+1}</td>
                  <td id="table-border" >{colegio.nombre}</td>
                  <td id="table-border">{colegio.nombreAdministrador} {colegio.apellido}</td>
                  <td className="actions-td" id="table-border"><button className="information-buttons"><BiTrash /></button> <button className="information-buttons"><BiPencil /> </button> <button className="information-buttons"><BiInfoCircle /></button></td>
                </tr>
                
              ))}
              
               
            </tbody>
          </Table>
        </div>
        <div className="paginacion">
          
            <Pagination onClick={e => handleCurrentPage(e.target.text)}  size="sm">{items} </Pagination>
          
        </div>
        <ModalAgregarColegios></ModalAgregarColegios>
      </div>
    </>)
}
export default ListarColegios