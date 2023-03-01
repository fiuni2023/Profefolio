import React, { useState } from "react";

export const TextInput = ({placeholder = "pon un placeholder", value , handleChange = () => {}, handleBlur = () => {}, handleFocus = () => {}, height="40px", width="120px"}) => {
    return (
        <>
            <input className="input" type={'text'} placeholder = {placeholder} onChange={handleChange} onBlur={handleBlur} onFocus={handleFocus} value={value}/>
            <style jsx = "true">{`
            .input{
                height: ${height};
                width: ${width};
                outline: none;
                border-radius: 10px;                
            }
            `}</style>
        </>
    )
}   

export const ButtonInput = ({text = "Texto", handleClick = () => {}, variant = "primary", disabled= false, width= "120px", height="40px", fontSize ="15px"}) => {
    const returnVariantColor = (variant) => {
        return `
        background-color: #363636; 
        color: white;
        `
    }
    return(
        <>
            <button className="button" onClick={handleClick} disabled={disabled}>{text}</button>
            <style jsx = "true">{`
            .button{
                border-radius: 10px;
                border: none;
                outline: none;
                ${returnVariantColor(variant)}
                width: ${width};
                height: ${height};
                font-size: ${fontSize};
                transition: 0.25s all;
            }
            .button:hover{
                filter: brightness(80%);
            }
            .button:active{
                filter: brightness(50%);
            }   
            `}</style>
        </>
    )
}

export const SelectInput = ({
    SItop_gap = "100%",
    SIheight = "40px",
    SIwidth = "120px",
    options = ["a","b","c"],
}) => {
    const [word, setWord] = useState("")
    const [showOptions, setShowOptions] = useState(false)
    return(
        <>
            <div className="relative_container">
                <input className="SIinput" type={"text"} value={word} onChange={(event)=>{setWord(event.target.value)}} onFocus={()=>{setShowOptions(true)}} onBlur={() => {setShowOptions(false)}} onSelect={()=>{}} height={SIheight} width={SIwidth} />
                {showOptions && <div className={`datalist`}>
                    {options.map((opcion, index)=>{
                        return <SIOption key={index} text={"texto"} handleClick={()=>{console.log(opcion)}}/>
                    })}
                </div>}
            </div>
            <style jsx="true">{`
                .SIinput{
                    height: ${SIheight};
                    width: ${SIwidth};
                    outline: none;
                    border-radius: 10px;                
                }
                .relative_container{
                    position: relative;
                }
                .datalist{
                    position: absolute;
                    top: ${SItop_gap};
                    width: ${SIwidth};
                    border: 2px solid gray;
                    outline: none;
                    border-radius: 10px; 
                    padding: 2px;
                }
            `}</style>
        </>
    )
}

const SIOption = ({
    handleClick = () => {},
    text = ""
}) =>{
    return(
        <>
            <div onClick={handleClick}>
                <span>
                    {text}
                </span>
            </div>
        </>
    )
}