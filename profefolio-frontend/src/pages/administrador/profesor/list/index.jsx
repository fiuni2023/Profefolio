import React, { useState } from "react";
import { ButtonInput, SelectInput, TextInput } from "../../../../components/Inputs.jsx";
import { PanelContainerBG } from "../../../../components/Layout.jsx";
import { Table } from "../../../../components/Table.jsx"

const ProfesorList = () => {
    const [text, setText] = useState("")
    return(
        <>
            <PanelContainerBG>
                <Table 
                    headers ={['Id', 'Name', "data"]}
                    datas = {[  {id: 1, name:"nombre1", dato:"opcionalopcionalopcional"},
                                {id: 2, name:"nombre2", dato:"opcionalopcionalopcional"}]}
                    parseToRow = {(data, index) => {
                        return <tr key={index}>
                            <td>{data.id || ""}</td>
                            <td>{data.name || ""}</td>
                            <td>{data.dato || ""}</td>
                        </tr>
                    }}
                />
                <ButtonInput />
                <TextInput  value={text} handleChange={(event)=>{setText(event.target.value)}} width={"20px"}/>
                <SelectInput  width="40px"/>
            </PanelContainerBG>
        </>
    )
}

export default ProfesorList