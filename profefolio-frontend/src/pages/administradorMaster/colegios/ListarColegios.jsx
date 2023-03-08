import React from "react";
import Table from 'react-bootstrap/Table';
import './ListarColegios.css'
import { useNavigate } from "react-router-dom";
import {BiArrowBack} from "react-icons/bi"
import {BiPencil} from "react-icons/bi"
import {BiTrash} from "react-icons/bi"
import {BiInfoCircle} from "react-icons/bi"


const ListarColegios = () => {
  const navigate = useNavigate()
  const colegios = [
    { id: 0, nombre: "Marcelina Bogado", direccion: "Coronel Bogado", administrador:"admin1" },
    { id: 1, nombre: "San Jose", direccion: "Coronel Bogado", administrador:"admin2" },
    { id: 2, nombre: "Santa Clara", direccion: "Coronel Bogado", administrador:"admin3" },
    { id: 3, nombre: "Colegio verde", direccion: "Coronel Bogado", administrador:"admin4" },
  ];
  return (
    <>
    <div className="container-principal">
      <div className="nombre-pagina">
        <button className="button-back" onClick={()=>{navigate('/administrador')}}><BiArrowBack/></button>
        <span>Colegios</span>
      </div>
      <div className="table-principal" >
      <Table bordered >
        <thead>
          <tr>

            <th id="table-border">Nombre</th>
            <th id="table-border">Direccion</th>
            <th id="table-border">Administrador</th>
            <th className="actions-th" id="table-border">Acciones</th>
          </tr>
        </thead>
        <tbody >
        {colegios.map((colegio, index) => (  
              <tr key={colegio.id}>  
                <td id="table-border" >{colegio.nombre}</td>  
                <td id="table-border">{colegio.direccion}</td>
                <td id="table-border">{colegio.administrador}</td>
                <td className="actions-td" id="table-border"><button className="information-buttons"><BiTrash /></button> <button className="information-buttons"><BiPencil /> </button> <button className="information-buttons"><BiInfoCircle/></button></td>  
              </tr>  
            ))} 
        </tbody>
      </Table>
      </div>
      </div>
    </>)
}
export default ListarColegios