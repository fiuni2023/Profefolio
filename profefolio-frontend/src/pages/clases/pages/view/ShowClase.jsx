import React from 'react'
import { useParams } from 'react-router-dom';
import { map } from "lodash";
import { SContainer, SRow } from '../../../../components/componentsStyles/StyledForm';
import { Col } from 'react-bootstrap';

const ShowClase = () => {
    const combinaciones = [[12], [6, 6], [12, 6, 6]]; // combinacion de tamanhos de columnas
    const { idClase } = useParams();

    const dato = {

        /**
         * TO DO
         * Validar logeo
         */

        title: "Primer grado",
        componentes: [
            <div><h1>Componente 1</h1></div>,
            <div><h1>Componente 2</h1></div>,
            <div><h1>Componente 3</h1></div>
        ]
    };

    return <>
        <SContainer>
            <h4>{dato.title}</h4>
            <SRow className="srow-showclase">
                {map(dato.componentes, (e, i) => <Col className="scol-showclase" key={i} md={combinaciones[dato.componentes.length - 1 >= 0 ? dato.componentes.length - 1 : 0 ][i]}>{e}</Col>)}
            </SRow>
            <div>User Profile for User ID: {idClase}</div>
        </SContainer>

        <style jsx="true">
            {`
                .srow-showclase{
                    flex-wrap: wrap;
                }
                .scol-showclase {
                    box-sizing: border-box;
                    padding: 1rem;
                    border: 1px solid red;
                }
                
            `}
        </style>
    </>

}

export default ShowClase