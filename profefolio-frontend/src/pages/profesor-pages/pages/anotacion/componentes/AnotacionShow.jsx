import React, { useEffect, useState } from "react";
import styled from "styled-components";
import TextButton from "../../../../../components/TextButton";
import { toast } from "react-hot-toast";

const AnotacionShowDiv = styled.div`
    margin: 5px;
    padding: 5%;
    border: 1px solid #DDDDDD;
    border-radius: 15px;
    height: 100%;
    display: flex;
    flex-direction: column;
    gap: 2vh;
`

const InputTitulo = styled.input`
    border: none;
    outline: none;
    border-radius: 10px;
    padding: 10px;
    font-size: 20px;
    &:hover{
        filter: brightness(0.8);
    }
`

const InputContenido = styled.textarea`
    border: none;
    outline: none;
    border-radius: 10px;
    padding: 10px;
    resize: none;
    min-height: 100vh;
    font-size: 20px;
    &:hover{
        filter: brightness(0.8);
    }
`

const AnotacionShow = ({
    selectedAnotation = {Titulo: "", Contenido: ""}
}) => {

    const [titulo, setTitulo] = useState("")
    const [contenido, setContenido] = useState("")
    const [tituloTemp, setTituloTemp] = useState("")
    const [contenidoTemp, setContenidoTemp] = useState("")

    useEffect(()=>{
        setTitulo(selectedAnotation?.Titulo)
        setContenido(selectedAnotation?.Contenido)
        setTituloTemp(selectedAnotation?.Titulo)
        setContenidoTemp(selectedAnotation?.Contenido)
    }, [selectedAnotation])

    const handleCheck = () => {
        if(titulo === tituloTemp) toast.success("titulo igual")
        else toast.error("Diferente titulo")
        if(contenido === contenidoTemp) toast.success("contenido igual")
        else toast.error("Diferente Contenido")
    }

    return <>
        <AnotacionShowDiv>
            <InputTitulo value={titulo} 
            onChange={(event)=>{
                setTitulo(event?.target?.value)
            }} />
            <InputContenido value={contenido} 
            onChange={(event)=>{
                setContenido(event?.target?.value)
            }} />

            <TextButton buttonType={"save"} enabled={true} onClick={handleCheck} />
        </AnotacionShowDiv>
    </>
}

export default AnotacionShow