import React from "react"
import styled from "styled-components"

const AnotationShowDiv = styled.div`
    display: flex;
    flex-direction: column;
    padding: 20px;
    width: 100%;
    height: 100%;
    border-radius: 15px;
    border: 1px solid black;
`
const AnotationInput = styled.input`
    margin-bottom: 5px;
    height: "40px";
    border-radius: 15px;
    outline: none;
    border: 1px solid #DDDDDD;
    padding: 10px;
`
const AnotationTextArea = styled.textarea`
    margin-bottom: 5px;
    border-radius: 15px;
    outline: none;
    border: 1px solid #DDDDDD;
    resize: none;
    height: 80%;
    padding: 10px;
`

const AnotationShow = ({
    selected = {}
}) => {
    return <>
        <AnotationShowDiv>
            <AnotationInput type="text" />
            <AnotationTextArea type="textarea" cols="50"/>
        </AnotationShowDiv>
    </>
}

export default AnotationShow