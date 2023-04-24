import React, { useState } from 'react'
import ScrolleableTable from '../../../../components/ScrolleableTable'
import StudentHelper from '../../../alumnos/helpers/StudentHelper'
import { useFetchEffect } from '../../../../components/utils/useFetchEffect'
import { useGeneralContext } from '../../../../context/GeneralContext';
import { toast } from 'react-hot-toast';
const AlumnosInscriptos = ({colegio}) => {
    const [listaAlumnos, setListaAlumnos] = useState([])
    const { getToken} = useGeneralContext();
    const { loading } = useFetchEffect(
        () => {
            return StudentHelper.getStudentsPage(0, getToken())
        },
        [ getToken],
        {
            condition: true,
            handleSuccess: (r) => {
                setListaAlumnos(r.data.dataList)
                console.log(r.data.dataList)
            },
            handleError: () => {
                toast.error("No se pudieron obtener los alumnos. Intente recargar la página")
            }
        }
    )
    const Alumnos = {
        onSubmit: () => console.log("Guardado"),
        enabled: true,
        header: {
            title: "Lista de Alumnos inscriptos",
        },
        addTitle: "Agregar alumnos",
        selectTitle: "Seleccionar alumno",
        options: [
            { label: "Carlos", value: 1 },
            { label: "Gabriela", value: 1 }
        ],
        list: listaAlumnos
    }
    return <>
        <ScrolleableTable isLoading={loading} studentsList={Alumnos} />
    </>

}

export default AlumnosInscriptos