import React, { useState } from "react";
import { PanelContainerBG } from "../../../components/LayoutAdmin.jsx";
import { Table } from "../../../components/Table";
import NavAdmin from "../../../components/NavAdmin";
import {Link } from "react-router-dom";

import CreateModal from "./components/CreateModal.jsx";

import "./components/Index.css";


import {BsTrash , BsPencilFill, BsInfoCircle,BsFillPlusCircleFill} from 'react-icons/bs';


const Profesores = () => {
    

    const [show, setShow] = useState(false);
    return(


        
        <>

            <div className="page">
            <NavAdmin/>
           
            <PanelContainerBG>
           
           
                <Table 
                    headers ={['CI', 'Nombre', "Fecha de nacimiento","Direccion","Telefono","Acciones"]}
                    datas = {[  {ci: 5555, nombre:"Juan Perez", fecha_nacimiento:"24/30/1977",direccion:"Encarnacion", telefono:"099785855"}]}
                    parseToRow = {(data, index) => {
                        return <tr key={index}>
                            <td>{data.ci || ""}</td>
                            <td>{data.nombre || ""}</td>
                            <td>{data.fecha_nacimiento || ""}</td>
                            <td>{data.direccion || ""}</td>
                            <td>{data.telefono || ""}</td>
                            <td> <BsTrash/>  <BsPencilFill/> <BsInfoCircle/> </td>
                        </tr>
                    }}
                />

           
               

               
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

          
        
             {/* 
            <footer>

            <div className="NButtonForSideA" >
                    <button className="buttonNavBarAa">  
                    <Link to="/profesor/create" className="buttonNavBarA"><BsFillPlusCircleFill /></Link>
                     </button>


                     
                </div>
            </footer>*/}



           


           

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

            
            `}</style>
        </>
    )
}

export default Profesores