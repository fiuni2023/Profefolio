import React, { useEffect, useState } from "react";
import styled from "styled-components";
import { useModularContext } from '../../context';
import { SBody, SCard, SHeader } from '../../../../components/componentsStyles/StyledDashComponent';
import Tabla from '../../../../components/Tabla';
import styles from './index.module.css';

const ETtable = styled.table`
    width: 100%;
    height: 100%;
`;

const ETH = styled.th`
    background-color: #DDDDDD;
`;

const ETR = styled.tr`
    background-color: ${(props) => props.color ?? "#C8BFD9"};
`;

const ETD = styled.td``;

const EventosTabla = () => {
    const has_clase = true;
    const has_colegio = true;
    const colors = ["#C8BFD9", "#C1E1FA", "#FCC6AC", "#F6E7A7"];

    const getColor = (i = 0) => {
        return colors[i % 4];
    };

    const { dataSet } = useModularContext();
    const { eventos } = dataSet;

    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "adminsList",
        titulos: [
            { titulo: "Tipo" },
            { titulo: "Fecha" },
            { titulo: "Materia" },
            { titulo: "Clase" },
            { titulo: "Colegio" },
        ],
        filas: [], // Inicializar filas como un arreglo vacío
    });
    const getRowColor = (tipo) => {
        switch (tipo) {
            case "parcial":
                return "#C1E1FA"; // Color para el tipo "parcial"
            case "examen":
                return "#F6E7A7"; // Color para el tipo "examen"
            case "prueba":
                return "#FCC6AC"; // Color para el tipo "prueba sumatoria"
            default:
                return "#C8BFD9"; // Color predeterminado
        }
    };

    useEffect(() => {
        console.log(eventos); // Imprimir eventos en la consola
        const newList = eventos.map((a) => {
            const fechaFormateada = new Date(a.fecha).toLocaleDateString("es-ES");
            return {
                datos: [
                    { dato: `${a.tipo}` },
                    { dato: fechaFormateada },
                    { dato: `${a.nombreMateria}` },
                    { dato: `${a.nombreClase}` },
                    { dato: `${a.nombreColegio}` },
                ],
            };
        });
        setDatosTabla((prevState) => ({
            ...prevState,
            filas: newList,
        }));
    }, [eventos]);

    return (
        <>
            <ETtable className={styles.tableroundcorner}>
                <thead className={styles.Header}>
                    <tr>
                        <ETH>Tipo</ETH>
                        <ETH>Fecha</ETH>
                        <ETH>Materia</ETH>
                        {has_clase && <ETH>Clase</ETH>}
                        {has_colegio && <ETH>Colegio</ETH>}
                    </tr>
                </thead>
                <tbody className={styles.Body}>
                    {datosTabla.filas.map((fila, index) => (
                        <ETR key={index} color={getRowColor(fila.datos[0].dato)}>
                            {fila.datos.map((dato, index) => (
                                <ETD key={index}>{dato.dato}</ETD>
                            ))}
                        </ETR>
                    ))}
                </tbody>
            </ETtable>
        </>
    );
}

export default EventosTabla;
