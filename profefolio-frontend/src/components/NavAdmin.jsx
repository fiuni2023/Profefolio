import React from "react";
//import { useGeneralContext } from "../context/GeneralContext";
import { BsArrowLeft } from 'react-icons/bs'
import {Link } from "react-router-dom";


const NavAdmin = () => {
    //const { showSB ,setShowSB} = useGeneralContext()

    

    return(
        <>
            <div className="NavbarAdmin">
              
                <div className="NButtonForSideAdmin" >
                    <button className="buttonNavBarAdmin">  
                    <Link to="/administrador" className="buttonNavBarAdmin"><BsArrowLeft /></Link>
                     </button>


                     
                </div>
                
            </div>
            <style jsx="true">{`
                .NavbarAdmin{
                    width: 100%;
                    height: 100%;
                    background-color: white;
                    display: flex;
                    background-color: #FDF0D5;
                }
                .NButtonForSideAdmin{
                    width: 2.5%;
                }
                .buttonNavBarAdmin{
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
               
            
            `}</style>
        </>
    )
}

export default NavAdmin;