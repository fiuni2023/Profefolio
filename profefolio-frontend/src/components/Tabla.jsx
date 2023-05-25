import React from 'react';
import {Table, Thead, Tbody, TR, TH, TD, ScrollTable} from "./componentsStyles/StyledTable"; 


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
        { (datosTabla?.filas) ? datosTabla &&(
            <ScrollTable>
                <Table  key = {datosTabla?.tituloTabla}
                        width={datosTabla?.tableWidth ? datosTabla.tableWidth : "100%"}>
                    {datosTabla?.titulos && (
                        <Thead small={datosTabla?.small ?? false}  background={datosTabla?.colorHeader ? datosTabla.ColorHeader : "#DDDDDD"}><TR key="thead">
                            {datosTabla.titulos.map((titulo, index) => {
                                return (
                                    <TH key={index}>
                                      {titulo.componente ? titulo.componente : titulo.titulo}
                                    </TH>
                                    );
                                })}
                        </TR></Thead>
                    )}
                    {datosTabla?.filas && (
                        <Tbody small={datosTabla?.small ?? false}>               
                            {datosTabla.filas.map((fila, index)=>{
                                return <TR  small={datosTabla?.small ?? false}
                                key = {index} 
                                selected={fila?.fila?.id === selected ? true : false} 
                                clickable={datosTabla?.clickable ? true : false}
                                onClick = {datosTabla?.clickable ? ()=>datosTabla.clickable?.action(fila?.fila) : null}>
                                                {fila?.datos.map((dato, indexDato) => (
                                                    <TD key={indexDato}>
                                                        {dato.componente ? dato.componente : dato.dato}
                                                    </TD>
                                                    ))}</TR>
                                            })}
                        </Tbody>
                    )}
                </Table>
            </ScrollTable>
        ) : <span><b>No hay datos para mostrar.</b></span>}
        </>
    )

}

export default Tabla;