import React from 'react';
import styled from 'styled-components';

const Table = styled.table(props => ({
    width: props.width,
}));
const Thead = styled.thead(props => ({
    background: props.background,
    height: "2em",
}));
const Tbody = styled.tbody``;
const TR = styled.tr`
    ${props =>
    props.selected &&
    `
      color: cornflowerblue;
    `}`;
const TH = styled.th({
    border: "1px black solid",
});;
const TD = styled.td({
    border: "1px black solid",
});;

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
                    <Thead  background={datosTabla?.colorHeader ? datosTabla.ColorHeader : "#C6D8D3"}><TR key="thead">
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
                            return <TR key = {index} selected={fila?.fila?.id === selected ? true : false} onClick = {datosTabla?.clickable ? ()=>datosTabla.clickable?.action(fila?.fila) : null}>{fila?.datos.map((dato, indexDato) =>{
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