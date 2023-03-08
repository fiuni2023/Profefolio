import React from "react";
//import { useGeneralContext } from "../context/GeneralContext";
import { BsArrowLeft } from 'react-icons/bs'
import {Link } from "react-router-dom";




const NavAdmin = () => {
    //const { showSB ,setShowSB} = useGeneralContext()

    

    return(
        <>


        
            <div className="NavbarAdmin">
              
           
                <div className="NButtonForSideAdmin">
                    <button className="buttonNavBarAdmin">  
                    <Link to="/administrador" className="buttonNavBarAdmin "><BsArrowLeft />  </Link>
                     </button> 
                     
                    
                    
                     <div class="">
                        <div>
                            <span class="glyphicon glyphicon-info-sign customIcon"></span></div>
                        <div class="customSpan">Profesores</div>
                    </div>     
                </div>


<div className="a">


                <div className="Buscador">

                <input type="search" placeholder="Busca tu Archivo" id="" />

                </div>

               

                <br/>

                </div>          




             








                
            </div>
            <style jsx="true">{`
            
            .flexContainer {
                display:flex;
                align-items:center;
              }
              
              .customSpan {
                padding: 6px;
                margin:0 0 10px 10px;  
                font-size:15px;
                font-weight: bold;
              }
              .customIcon {
                color:dodgerblue;
                font-size:25px;
              }
                .NavbarAdmin{
                    width: 100%;
                    height: 100%;
                    background-color: white;
                    display: flex;
                    background-color: #FDF0D5;
                }
                .NButtonForSideAdmin{
                    display: flex;
                    font-size:20px;
                }
                .buttonNavBarAdmin{
                    font-weight: bold;
                    width: 100%;
                    height: 100%;
                    outline: none;
                    border: none;
                    background-color: #FDF0D5;
                    font-size: 20px;
                    color: black;
                }
                .navbarmainAdmin{
                    width: 97.5%;
                    display: flex;
                    justify-content: space-between;
                }

                .letra{
                    margin:0 0 4px 4px;  
                }

                .a{
                    width: 97.5%;
                    display: flex;
                    justify-content: space-between;
                }
                .Buscador {
        
                    background-size: 24px;
                    width: 100%;
                    border: none;
                   
                    padding: 10px 10px 10px 30px;
                    outline: none;
                   
                    text-align:right;
                  }

            

                
               
            
            `}</style>
        </>
    )
}

export default NavAdmin;