import React, {useMemo, useState } from 'react'

import { useParams } from 'react-router-dom';

import ShowContainer from '../../components/ShowContainer';
import InfoClase from '../../components/show/InfoClase';
import AlumnosInscriptos from '../../components/show/AlumnosInscriptos';
import MateriasDeClase from '../../components/show/MateriasDeClase';
import { useClaseContext } from '../../context/ClaseContext';

import ClassesService from '../../Helpers/ClassesHelper';
import { useGeneralContext } from '../../../../context/GeneralContext';

const ShowClase = () => {
    const { idClase } = useParams();
    const { getColegioId} = useClaseContext();

    const { getToken } = useGeneralContext();

    const [nombreClase, setNombreClase] = useState("");


    useMemo(() => {
        const response = ClassesService.getClassesByIdNombre(idClase, getToken())
      
        if (response !== null) {
          response.then((r) => {
            setNombreClase(r) // Modifica aquí para mostrar solo el nombre de la clase
          }).catch(e => {
            setNombreClase()
          })
        }
      }, [idClase, getToken, setNombreClase])
    /**
     * TO DO
     * Validar logeo
     */

    // contiene el titulo que tendra la pagina y la lista de componentes a mostrar
    const componentes = {
        title: `Nombre de clase ${nombreClase}`,
        componentes: [
            <InfoClase/>,
            <AlumnosInscriptos colegio={getColegioId()}/>,
            <MateriasDeClase/>
        ]
    };

    return <>
            <ShowContainer data={componentes}/>
    </>

}

export default ShowClase