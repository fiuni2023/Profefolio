import React from "react";
import { AiOutlinePlus } from "react-icons/ai";
import styled from "styled-components";

const AddButtonStyle = styled.button`
    width: 50px;
    height: 50px;
    padding: 7px;
    color: white;
    background-color: #F0544F;
    border-radius: 50%;
    position: fixed;
    bottom: 1.5%;
    right: 1%;
    cursor: pointer;
    border: none;
&:hover {
    filter: brightness(0.95);
&:active {
    filter: brightness(0.8);
  }
`;

const AddButton = ({
    onClick= ()=>{}
}) => {
    return(
        <AddButtonStyle onClick={onClick}>
            <AiOutlinePlus size={"35px"} />
        </AddButtonStyle>
    )
}

export default AddButton
