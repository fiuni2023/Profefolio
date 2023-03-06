import React, { useState } from "react";
import { PanelContainerBG } from "../../../components/LayoutAdmin.jsx";
import { Table } from "../../../components/Table";
import NavAdmin from "../../../components/NavAdmin";

import CreateModal from "./components/CreateModal.jsx";

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

            <div className="navbarmainAdmin "> 
            <button  className="buttonFooterAdmin float-right" onClick={() => setShow(true)}><BsFillPlusCircleFill/></button>
            <CreateModal title="My Modal" onClose={() => setShow(false)} show={show}>
            </CreateModal>
            </div>

           

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
            .NButtonForSide{
                width: 2.5%;
            }
            .buttonFooterAdmin{
                
                outline: none;
                border: none;
                font-size: 50px;
                color:  #F0544F;
            }
            .navbarmainAdmin{
                padding: 50px;
                width: 97.5%;
                float: right;
                
               
            }
            `}</style>
        </>
    )
}

export default Profesores