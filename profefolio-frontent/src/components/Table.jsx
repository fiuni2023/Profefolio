import React from "react";

export const Table = ({headers = [], datas=[], parseToRow =() =>{return <tr><td>create a parseToRow function for your tablerow</td></tr>}}) => {
    return(<>
        <table className="table">
            <thead>
                <tr>
                    {headers.map((h, index)=>{return <th key={index}>{h}</th>})}
                </tr>
            </thead>
            <tbody>
                    {datas.map((d,index)=>{return parseToRow(d,index)})}
            </tbody>
        </table>
        <style jsx="true">{`
            .table{
                width: 100%;
                border-spacing: 0px;
            }
            .table>thead>tr>th{
                border-bottom: 2px solid black;
                height: 5vh;
            }
            .table>tbody>tr>td{
                text-align: center;
                height: 5vh;
                border-bottom: 0.5px solid black;
            }
        `}</style>
    </>)
}