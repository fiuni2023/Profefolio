import React, { useState } from "react";

import styles from './Inputs.module.css'

export const TextInput = ({placeholder = "pon un placeholder", value , handleChange = () => {}, handleBlur = () => {}, handleFocus = () => {}, name = "", height="40px", width="120px"}) => {
    return (
        <>
            <input className={styles.TI} name={name} type={'text'} style={{height: `${height}`, width: `${width}`}} placeholder = {placeholder} onChange={handleChange} onBlur={handleBlur} onFocus={handleFocus} value={value}/>
        </>
    )
}   

export const ButtonInput = ({text = "Texto", className ="", handleClick = () => {}, variant = "primary", disabled= false, width= "120px", height="40px", fontSize ="15px"}) => {
    const returnVariantColor = (variant) => {
        if (variant === "primary") return {backgroundColor:"#331832", color: "white", width: width, height: height, fontSize: fontSize}
        if (variant === "primary-inv") return { border:"2px solid #331832", color: "#331832", width: width, height: height, fontSize: fontSize}
        if (variant === "danger") return {backgroundColor:"#D81E5B", color: "white", width: width, height: height, fontSize: fontSize}
        if (variant === "danger-inv") return { border:"2px solid #D81E5B", backgroundColor: "#FFE3E9", color: "#D81E5B", width: width, height: height, fontSize: fontSize}
        if (variant === "secondary") return {backgroundColor:"#D3D3D3", color: "white", width: width, height: height, fontSize: fontSize}
        if (variant === "secondary-black") return {backgroundColor:"#D3D3D3", color: "black", width: width, height: height, fontSize: fontSize}
    }

    const styleBtn=returnVariantColor(variant)
    return(
        <>
            <button className={`${styles.BI} ${className}`} style={styleBtn} onClick={handleClick} disabled={disabled}>{text}</button>
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

export const DateInput = ({
    value = "",
    handleChange= () => {},
    width = "120px",
    height = "40px",
    name = ""
}) => {
    return(
        <>
            <input name = {name} className="DateInput" type="date" value={value} onChange={handleChange} />
            <style jsx = "true">{`
            .DateInput{
                height: ${height};
                width: ${width};
                outline: none;
                border-radius: 10px;                
            }
            `}</style>
        </>
    )
}