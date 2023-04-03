import React from 'react';
import styled from 'styled-components';

const Table = styled.table`
        width: ${props => props.width};
        box-shadow: 2px 2px 10px 2px rgba(0, 0, 0, 0.1);
        border-radius: 20px;

        tr:first-child th:first-child {
            border-top-left-radius: 20px;
        };
        tr:first-child th:last-child {
            border-top-right-radius: 20px;
        };
        tr:last-child td:first-child {
            border-bottom-left-radius: 20px;
        };
        tr:last-child td:last-child {
            border-bottom-right-radius: 20px;
        };
    `;
const Thead = styled.thead`
        background: ${props => props.background};
        height: "2em";
        font-family: 'Poppins';
        font-style: normal;
        font-weight: 500;
        font-size: 1em;
        line-height: 2em;
    `;
const Tbody = styled.tbody`
        font-family: 'Poppins';
        font-style: normal;
        font-weight: 300;
        font-size: 1em;
        line-height: 2em;

`;
const TR = styled.tr`
    ${props =>
        props.selected &&
        `
            background: #9C8CBB;
        `}
        ${props => 
        props.clickable &&
        `
            :hover{
                color: #9C8CBB;
                cursor: pointer; 
            }
        `             
        }
    `
    ;
const TH = styled.th`
        padding: 5px 10px;
        text-align: left; 
    `;
const TD = styled.td`
        padding: 6px 10px;
        text-align: left;
        border-top: 1px #C2C2C2 solid;
    `;

/*Estructura del datosTabla todos los campos son opcionales

datosTabla = {
    tituloTabla: Titulo de la tabla
    titulos: [ titulos de la tabla ]
    clickable: { action: funcion al haer click } 
    colorHeader: Color para el header, en caso de querer que sea de otro color
    tableWidth: Ancho de la tabla 
    filas: {
        fila: es la fila, para la funcion de click
        datos: [{
            dato: string que va en la celda correspondiente
        }]
    }
}

*/ 
function Tabla({datosTabla, selected}){

    return (
        <>
        {datosTabla &&(
            <Table  key = {datosTabla?.tituloTabla}
                    width={datosTabla?.tableWidth ? datosTabla.tableWidth : "100%"}>
                {datosTabla?.titulos && (
                    <Thead  background={datosTabla?.colorHeader ? datosTabla.ColorHeader : "#DDDDDD"}><TR key="thead">
                        {datosTabla.titulos.map((titulo, index) => {
                            let titulos = null;
                            titulos = (
                                <TH key={titulo?.titulo}>{titulo?.titulo}</TH>
                            );
                            return titulos;
                        })}
                    </TR></Thead>
                )}
                {datosTabla?.filas && (
                    <Tbody>               
                        {datosTabla.filas.map((fila, index)=>{
                            return <TR  key = {index} 
                                        selected={fila?.fila?.id === selected ? true : false} 
                                        clickable={datosTabla?.clickable ? true : false}
                                        onClick = {datosTabla?.clickable ? ()=>datosTabla.clickable?.action(fila?.fila) : null}>{fila?.datos.map((dato, indexDato) =>{
                                return <TD
                                key={dato.dato ? dato.dato : indexDato}>{dato?.dato}</TD>
                            })}</TR>
                        })}
                    </Tbody>
                )}
            </Table>
        )}
        </>
    )

}

export default Tabla;