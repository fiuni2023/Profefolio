import React, { useEffect, useState } from "react";
import styled from "styled-components";
import TextButton from "../../../../../components/TextButton";
import { toast } from "react-hot-toast";
import { useModularContext } from "../../../context";
import { useGeneralContext } from "../../../../../context/GeneralContext";
import AnotationsService from "../../../services/AnotationsService";

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
    border: 1px solid #DDDDDD;
    outline: none;
    border-radius: 10px;
    padding: 10px;
    font-size: 20px;
    &:hover{
        filter: brightness(0.8);
    }
`

const InputContenido = styled.textarea`
    border: 1px solid #DDDDDD;
    outline: none;
    border-radius: 10px;
    padding: 10px;
    resize: none;
    min-height: 50vh;
    font-size: 20px;
    &:hover{
        filter: brightness(0.8);
    }
`

const AnotacionShow = ({
    selectedAnotation = {titulo: "", contenido: ""},
    setSelectedAnotation = ()=>{},
    doFetch = ()=>{}
}) => {

    const {getToken} = useGeneralContext()
    const {stateController} = useModularContext()
    const {materiaId} = stateController
    const token = getToken()

    const [titulo, setTitulo] = useState("")
    const [contenido, setContenido] = useState("")
    const [erasing, setErasing] = useState(false)

    useEffect(()=>{
        setTitulo(selectedAnotation?.titulo)
        setContenido(selectedAnotation?.contenido)
    }, [selectedAnotation])

    const handleDelete = () => {
        if(!!selectedAnotation.id){
            AnotationsService.Delete(selectedAnotation.id, token)
            .then(()=>{
                doFetch()
                setErasing(false)
                setSelectedAnotation({})
                setContenido("")
                setTitulo("")
            })
        } else {
            setErasing(false)
            return toast.error("no esta seleccionado una anotacion")
        }
    }

    const handleCreate = (titulo, contenido) => {
        if(titulo === "") return toast.error("Revise los Campos")
        if(contenido === "") return toast.error("Revise los Campos")
        let body = {
            "titulo": titulo,
            "contenido": contenido,
            "materiaListaId": materiaId
        }
        console.log(body)
        AnotationsService.Post(body, token)
        .then(()=>{
            doFetch()
            setContenido("")
            setTitulo("")
        })
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
            
            <div className="d-flex justify-content-between">
                {erasing?
                    <>
                        <TextButton buttonType={"cancel"} enabled={true} onClick={()=>{setErasing(false)}} />
                        <TextButton buttonType={"confirm"} enabled={true} onClick={()=>{handleDelete(titulo, contenido)}} />
                    </>
                    :
                    <>
                        <TextButton buttonType={"danger"} enabled={true} onClick={()=>{setErasing(true)}} />
                    </>

                }
                <TextButton buttonType={"save"} enabled={true} onClick={()=>{handleCreate(titulo, contenido)}} />
            </div>
        </AnotacionShowDiv>
    </>
}

export default AnotacionShow