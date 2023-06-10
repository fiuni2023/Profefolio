import React from "react";
import { useGeneralContext } from "../context/GeneralContext";
import { BsJustify } from 'react-icons/bs'
import { FaUserCircle } from 'react-icons/fa'
import { NavCenter, NavLeft, NavRight, SButtonforBar, SIcon, SMainLogo, SMainName, SMainUser, SNavBar, SSchoolName, SUserName, SmainLogoImg, navbarLogoImage } from "./componentsStyles/StyledNavBar";
import { useNavigate } from "react-router-dom";
//import { useModularContext } from "../pages/profesor-pages/context";

const Navbar = ({
    showSB = false, 
    setShowSB = () => {},
    showDD = false,
    setShowDD = ()=> {}
}) => {
    const { getUserName, getColegioName, cancan } = useGeneralContext()
    const navigate = useNavigate();
    const isProfesor = cancan("Profesor")
    
    const handleClick = () =>{
        navigate("/")
    }

    return(
        <>
            <SNavBar>
                <NavLeft>
                    {!isProfesor &&
                    <SButtonforBar onClick={()=>{
                        setShowDD(false)
                        setShowSB(!showSB)}} >
                            <BsJustify />
                    </SButtonforBar>
                    }
                    <SMainLogo clickable={!isProfesor} onClick={handleClick}>
                        <SmainLogoImg src={navbarLogoImage} alt="Logo" />
                    </SMainLogo>
                </NavLeft>
                <NavCenter>
                    <SSchoolName>{cancan("Administrador de Colegio") ? getColegioName() : ""}</SSchoolName>
                </NavCenter>
                <NavRight>
                    {!showDD && <SMainUser
                        onClick={()=>{
                            setShowSB(false)
                            setShowDD(!showDD)
                        }}> 
                            <SMainName>
                                <SUserName>{getUserName()}</SUserName>
                                <SIcon><FaUserCircle width={"30px"}/></SIcon>
                            </SMainName>
                        </SMainUser>
                    }
                    
                </NavRight>
            </SNavBar>
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