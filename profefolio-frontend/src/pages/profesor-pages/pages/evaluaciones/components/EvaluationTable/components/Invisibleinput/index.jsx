import React, { useEffect, useState } from "react";
import styled from "styled-components";

const II = styled.input`
    width: ${(props)=>{return props.width}};
    height: ${(props)=>{return props.height}};
    outline: none;
    border: none;
    text-align:center;
    background-color: #C6D8D3;
    &:hover {
        filter: brightness(0.95);
    }
`;

const InvisibleInput = ({
    handleBlur = ()=>{},
    textFilter = (t)=>{return t},
    value = "",
    height = "100%",
    width = "100%",
}) => {
    const [ temporal, setTemporal ] = useState("")
    const [inputValue, setInputValue] = useState("")
    useEffect(()=>{
        setInputValue(value)
        setTemporal(value)
    }, [value])
    return <II type="text" height={height} width={width} value={temporal} 
        onChange={(e)=>{
            setTemporal(textFilter(e?.target?.value))
        }} 
        onBlur={(e)=>{
            if(temporal !== inputValue){
                handleBlur(e?.target?.value)
            }
        }} 
        />
}

export default InvisibleInput