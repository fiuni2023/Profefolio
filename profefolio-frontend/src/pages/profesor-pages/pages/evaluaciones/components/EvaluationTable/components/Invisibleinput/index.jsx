import React, { useEffect, useState } from "react";
import styled from "styled-components";

const II = styled.input`
    width: ${(props)=>{return props.width}};
    height: ${(props)=>{return props.height}};
    outline: none;
    border: none;
    text-align:center;
    appearance: none;
    background-color: ${(props)=>{return props.back === "gray"? `#DDDDDD` : `#FFFFFF` }};
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
    type= "text",
    back = "gray",
    max = 100,
}) => {
    const [ temporal, setTemporal ] = useState("")
    const [inputValue, setInputValue] = useState("")
    useEffect(()=>{
        setInputValue(value)
        setTemporal(value)
    }, [value])
    return <II type={type} height={height} width={width} value={temporal} back = {back} max={max}
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