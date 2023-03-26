import React from "react";
import styles from './Table.module.css'

export const Table = ({headers = [], datas=[], className = "", parseToRow =() =>{return <tr><td>create a parseToRow function for your tablerow</td></tr>}}) => {
    return(<>
        <table className={`${styles.CustomTable} ${className}`}>
            <thead>
                <tr>
                    {headers.map((h, index)=>{return <th key={index}>{h}</th>})}
                </tr>
            </thead>
            <tbody>
                    {datas.map((d,index)=>{return parseToRow(d,index)})}
            </tbody>
        </table>
    </>)
}