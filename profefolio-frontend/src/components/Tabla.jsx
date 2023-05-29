import React, { useEffect, useState } from 'react';
import {Table, Thead, Tbody, TR, TH, TD, ScrollTable} from "./componentsStyles/StyledTable"; 
import ExcelExport from './utils/excelExport';


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

    const [exp, setExp] = useState([]); 

    useEffect(() => {
       console.log(datosTabla);
       let filas = datosTabla?.filas;
       let titulos = datosTabla?.titulos;
       let current = null;
       let newExp = [];
       let i = 0;
       if(filas && titulos){
        filas.forEach(fila => {
            current = fila?.datos;
            let newFila = {};
            if(current){
                let j = 0; 
                titulos.forEach(element => {
                   newFila[element] = current[j];
                   j++; 
                });
                newExp[i] = newFila;
                i++;
            }
        });
        setExp(newExp);
       }  
    }, [datosTabla]);

    return (
        <>
         {datosTabla && datosTabla?.filas && !datosTabla.small && <>
            <ExcelExport
                excelData={exp}
                fileName={datosTabla?.tituloTabla ? datosTabla?.tituloTabla : "descargado_de_ProfeFolio"}>
            </ExcelExport></>}
        { (datosTabla?.filas) ? datosTabla &&(
            <ScrollTable>
                <Table  key = {datosTabla?.tituloTabla}
                        width={datosTabla?.tableWidth ? datosTabla.tableWidth : "100%"}>
                    {datosTabla?.titulos && (
                        <Thead small={datosTabla?.small ?? false}  background={datosTabla?.colorHeader ? datosTabla.ColorHeader : "#DDDDDD"}><TR key="thead">
                            {datosTabla.titulos.map((titulo, index) => {
                                return (
                                    titulo?.componentes ? (
                                        <TH key={index + 'date'} clickable={true}>
                                            {titulo?.componentes?.map((componente, index)=>{
                                               return (
                                            <div style={{display: 'inline'}} key={index + componente}
                                                clickable='true'
                                                onClick = {componente?.action ? (e)=>componente.action(e) : null}>
                                                {componente.componente}</div>
                                                );
                                            })}
                                        </TH>
                                    ) : (
                                         <TH key={titulo?.titulo}>{titulo?.titulo}</TH> )
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
                                                {fila?.datos.map((dato, indexDato) => {
                                                    return<TD key={dato.dato ? `${dato.dato}${indexDato}` : indexDato}>
                                                        {dato.componente ? dato.componente : dato.dato}
                                                    </TD>
                                                })}</TR>
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