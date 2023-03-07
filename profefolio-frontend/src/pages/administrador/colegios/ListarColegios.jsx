import React from "react";
import Table from 'react-bootstrap/Table';
import './ListarColegios.css'
const ListarColegios = () => {
  const colegios = [
    { id: 0, nombre: "Marcelina Bogado", direccion: "Coronel Bogado" },
    { id: 1, nombre: "San Jose", direccion: "Coronel Bogado" },
    { id: 2, nombre: "Santa Clara", direccion: "Coronel Bogado" },
    { id: 3, nombre: "Colegio verde", direccion: "Coronel Bogado" },
  ];
  return (
    <>
    <div className="container-principal">
      <div className="nombre-pagina">
        <button>volver</button>
        <span>Colegios</span>
      </div>
      <div className="table-principal" >
      <Table bordered >
        <thead>
          <tr>

            <th>Nombre</th>
            <th>Direccion</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody >
        {colegios.map((colegio, index) => (  
              <tr data-index={index}>  
                <td>{colegio.nombre}</td>  
                <td>{colegio.direccion}</td>
                <td><button>hola</button></td>  
              </tr>  
            ))} 
        </tbody>
      </Table>
      </div>
      </div>
    </>)
}
export default ListarColegios