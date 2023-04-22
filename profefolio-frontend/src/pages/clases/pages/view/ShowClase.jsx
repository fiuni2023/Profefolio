import React from 'react'
import { useParams } from 'react-router-dom';

import ShowContainer from '../../components/ShowContainer';
import InfoClase from '../../components/show/InfoClase';
import AlumnosInscriptos from '../../components/show/AlumnosInscriptos';
import MateriasDeClase from '../../components/show/MateriasDeClase';

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
            <InfoClase/>,
            <AlumnosInscriptos/>,
            <MateriasDeClase/>
        ]
    };

    return <>
            <ShowContainer data={componentes}/>
    </>

}

export default ShowClase