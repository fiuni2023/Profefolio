import React, { useState, useEffect } from 'react';
import { PanelContainerBG } from "./components/LayoutAdmin.jsx";
import { Table } from "../../components/Table.jsx";
import NavAdmin from "./components/NavAdmin.jsx";
import CreateModal from "./components/create/CreateModal.jsx";

import axios from 'axios';

import "./components/create/Index.css";


import {BsTrash , BsPencilFill, BsInfoCircle,BsFillPlusCircleFill} from 'react-icons/bs';


const token =  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicHJ1ZWJhQGdtYWlsLmNvbSIsImp0aSI6IjIwNGI4MGMyLTUxMjAtNDliZS04OTdjLTVlZWNkNjY1Yjc4MCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluaXN0cmFkb3IgZGUgQ29sZWdpbyIsImV4cCI6MTY3ODc3MTgxOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.X7YzCqxnRi78GM_HRUrcLyYZo_n3QxkTsgsVPmQowjE";
function Profesores() {
    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(0);
    const [size, setSize] = useState(10);
  
    useEffect(() => {

        //axios.get(`https://miapi.com/products?page=${page}&size=${size}`, {
       // https://localhost:7063/api/administrador/page
      axios.get(`https://localhost:7063/api/profesor/page/${page}`, {
        headers: {
         // Authorization: `Bearer ${token}`

        
          Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibHVsbGFwZXJlekBnbWFpbC5jb20iLCJqdGkiOiI2OWYxNjA3My03NjUzLTQ4YTgtOTM4Yy1kMjcyY2Y1NTFkM2QiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJQcm9mZXNvciIsImV4cCI6MTY3ODc3NjU5MSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.QPBZJZ5J-y9NaFT2R6b5QRefgTqlusjeZU7Ir41vcZw',
        }
        })

      .then(response => {
        setProducts(response.data.dataList);

    

        console.log(JSON.stringify(response.data.dataList))
      })
      .catch(error => {
        console.error(error);
      });
    }, [page, size, token]);
  
    const handlePrevClick = () => {
      setPage(page - 1);
    };
  
    const handleNextClick = () => {
      setPage(page + 1);
    };

    

    const [show, setShow] = useState(false);

  
    return(


        
        <>

            <div className="page">
            <NavAdmin/>
           
            <PanelContainerBG>
          

                        <div>
                            <table className="CustomTable">
                                <thead>
                                <tr>
                                    <th>CI</th>
                                    <th>Nombre</th>
                                    <th>Apellido</th>
                                    <th>Fecha de nacimiento</th>
                                    <th>Direccion</th>
                                    <th>Telefono</th>
                                    <th>Acciones</th>
                                </tr>
                                </thead>
                                <tbody>
                                 {products.map(product => (
                                    <tr key={product.id}>
                                    <td>{product.documento}</td>
                                    <td>{product.nombre}</td>
                                    <td>{product.apellido}</td>
                                    <td>{product.nacimiento}</td>
                                    <td>{product.direccion}</td>
                                    <td>{product.telefono}</td>
                                    <td>Acciones</td>

                                    </tr>
                                ))}
                                </tbody>
                            </table>
                            <button onClick={handlePrevClick} disabled={page === 1}>Anterior</button>
                            <button onClick={handleNextClick}>Siguiente</button>
                            </div>
    

           
               

               
            </PanelContainerBG>
            <footer>
             <div className="NButtonForSideA "> 
             <button className="buttonNavBarAa">  
            <button  className="buttonNavBarA" onClick={() => setShow(true)}><BsFillPlusCircleFill/></button>
            <CreateModal title="My Modal" onClose={() => setShow(false)} show={show}>
            </CreateModal>

            </button>
            </div>

            </footer>

          
        
             



           


           

            </div>

            <style jsx='true'>{`
            .page{
                display: grid;
                grid-template-rows: 5% 95%;
                width: 100%;
                height: 100vh;
            }
            .content{
                width: 100%;
                height: 100%;
            }
            
            .NavbarA{
                width: 100%;
                height: 100%;
                background-color:  #F0544F;
                display: flex;
                background-color: #F0544F;
            }
            .NButtonForSideA{
               
            }
            .buttonNavBarA{
                width: 100%;
                height: 100%;
                outline: none;
                border: none;
                background-color: #FFFFFF;
                font-size: 50px;
                color: #F0544F;
            }

            .buttonNavBarAa{
                outline: none;
                border: none;
                background-color: #FFFFFF;
                font-size: 20px;
                color: black;
            }
            .navbarmainAd{
                width: 97.5%;
                display: flex;
                justify-content: space-between;
            }

            .CustomTable{
                width: 100%;
                border-spacing: 0px;
            }
            .CustomTable>thead>tr>th{
                border: 1px solid black;
                padding-left: 5px;
            }
            .CustomTable>tbody>tr>td{
                text-align: center;
                border: 1px solid black;
            }

            
            `}</style>
        </>
    )
}

export default Profesores