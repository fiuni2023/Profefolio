import React from "react";
import { useGeneralContext } from "../context/GeneralContext";
import { BsJustify } from 'react-icons/bs'
import { FaUserCircle } from 'react-icons/fa'
import { LogoNavBar } from "../assets";

const Navbar = () => {
    const { showSB ,setShowSB} = useGeneralContext()

    return(
        <>
            <div className="navbar">
                <div>

                </div>
                <div className="navbarButtonForSide" onClick={()=>{setShowSB(!showSB)}}>
                    <button className="buttonNavBar">  <BsJustify /> </button>
                </div>
                <div className="navbarmain">
                    <div className="logo"> <LogoNavBar width="25px" height="20px" /> </div>
                    <div className="user"> <span>UserName</span> <FaUserCircle /> </div>
                </div>
            </div>
            <style jsx="true">{`
                .navbar{
                    width: 100%;
                    height: 100%;
                    background-color: white;
                    display: flex;
                    background-color: #F0544F;
                }
                .navbarButtonForSide{
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
                    width: 10%;
                    height: 100%;
                }
                .user{
                    width: 10%;
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
            `}</style>
        </>
    )
}

export default Navbar