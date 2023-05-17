import React from "react";
import styled from "styled-components";
import { BiArrowBack } from 'react-icons/bi'
import { useModularContext } from "../../context";

const BBdiv = styled.div`
    cursor: pointer;
`

const BackButton = ({
    to = ""
}) => {
    const {setPage} = useModularContext()
    return <>
        <BBdiv>
            <BiArrowBack size={20} style={{cursor: "pointer"}} onClick={()=>setPage(to)} />
        </BBdiv>

    </>
}

export default BackButton