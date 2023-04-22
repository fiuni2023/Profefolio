import React from 'react'
import { useParams } from 'react-router-dom';
//import { map } from "lodash";
//import { SContainer, SRow } from '../../../../components/componentsStyles/StyledForm';
//import { Col } from 'react-bootstrap';
import ShowContainer from '../../components/ShowContainer';
//import InfoClase from '../../components/show/InfoClase';

const ShowClase = () => {
    const { idClase } = useParams();

    /**
     * TO DO
     * Validar logeo
     */

    // contiene el titulo que tendra la pagina y la lista de componentes a mostrar
    const componentes = {
        title: `Nombre de clase ${idClase}`,
        componentes: [
            <div><h5>Editar datos de grado</h5></div>,
            <div><h5>Lista de alumnos inscriptos</h5></div>,
            <div><h5>Lista de materias de la clase</h5></div>
        ]
    };

    return <>
            <ShowContainer data={componentes}/>
    </>

}

export default ShowClase