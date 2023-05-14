import React from "react";
import { useGeneralContext } from "../context/GeneralContext";
import { BsJustify } from 'react-icons/bs'
import { FaUserCircle } from 'react-icons/fa'
import { LogoNavBar } from "../assets";

const Navbar = ({
    showSB = false, 
    setShowSB = () => {},
    showDD = false,
    setShowDD = ()=> {}
}) => {
    const { getUserName, getColegioName, cancan } = useGeneralContext()

    return(
        <>
            <div className="Navbar">
                <div className="NButtonForSide" onClick={()=>{
                    setShowDD(false)
                    setShowSB(!showSB)
                }}>
                    <button className="buttonNavBar">  <BsJustify /> </button>
                </div>
                <div className="navbarmain">
                    <div className="logonameset">
                        <div className="logo"> <LogoNavBar width="100%" height="100%" /> </div>
                        {cancan("Administrador de Colegio")? getColegioName():"Master"}
                    </div>
                    {!showDD &&
                        <div className="user" onClick={()=>{
                            setShowSB(false)
                            setShowDD(!showDD)
                        }}> <span>{getUserName()}</span> <FaUserCircle size={25}/> </div>
                    }
                </div>
            </div>
            <style jsx="true">{`
                .Navbar{
                    width: 100%;
                    height: 100%;
                    background-color: white;
                    display: flex;
                    background-color: #F0544F;
                }
                .NButtonForSide{
                    width: 2.5%;
                }
                .buttonNavBar{
                    width: 100%;
                    height: 100%;
                    outline: none;
                    border: none;
                    background-color: #F0544F;
                    font-size: 20px;
                    color: white;
                }
                .navbarmain{
                    width: 97.5%;
                    display: flex;
                    justify-content: space-between;
                }
                .logo{
                    padding: 5px;
                    width: 45%;
                    height: 100%;
                }
                .user{
                    width: 20%;
                    height: 100%;
                    display: flex;
                    justify-content: space-evenly;
                    align-items: center;
                    color: white;
                    cursor: pointer;
                }
                .user span{
                    font-size: 15px;
                }
                .user svg{
                    font-size: 20px;
                }
                .logonameset{
                    display: flex;
                    width: 25%;
                    align-content: center;
                    flex-wrap: wrap;
                    gap: 10px;
                    align-items: center;
                    font-size: 20px;
                    color: white;
                }
            `}</style>
        </>
    )
}

export default Navbar