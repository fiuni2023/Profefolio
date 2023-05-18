import React from "react";
import styled from "styled-components";
import styles from './index.module.css'

const ETtable = styled.table`
    width: 100%;
    height: 100%;
`
const ETH = styled.th`
    background-color: #DDDDDD;
`
const ETR = styled.tr`
    background-color: ${props => props.color ?? "#C8BFD9"};
`
const ETD = styled.td`

`


const EventosTabla = ({
    list = [],
    has_clase = false,
    has_colegio
}) => {
    const colors = ["#C8BFD9", "#C1E1FA", "#FCC6AC", "#F6E7A7"]
    
    const getColor = (i = 0) => {
        return colors[i%4]
    }   
    getColor()
    
    return <>
        <ETtable className={styles.tableroundcorner}>
            <thead className={styles.Header}>
                <tr>
                    <ETH>Tipo</ETH>
                    <ETH>Fecha</ETH>
                    <ETH>Materia</ETH>
                    {has_clase &&
                        <ETH>Clase</ETH>
                    }
                    {has_colegio &&
                        <ETH>Colegio</ETH>
                    }
                </tr>
            </thead>
            <tbody className={styles.Body}>
                <ETR color={getColor(3)}>
                    <ETD>Ex.</ETD>
                    <ETD>24/02</ETD>
                    <ETD>Matematica</ETD>
                    {has_clase &&
                        <ETD>Clase</ETD>
                    }
                    {has_colegio &&
                        <ETD>Colegio</ETD>
                    }
                </ETR>
            </tbody>
        </ETtable>
    </>
}

export default EventosTabla