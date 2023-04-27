import React, { useState } from 'react'
import ScrolleableTable from '../../../../components/ScrolleableTable'
import StudentHelper from '../../../alumnos/helpers/StudentHelper'
import { useFetchEffect } from '../../../../components/utils/useFetchEffect'
import { useGeneralContext } from '../../../../context/GeneralContext';
import { toast } from 'react-hot-toast';
import { useClaseContext } from '../../context/ClaseContext';
const AlumnosInscriptos = () => {
    const [listaAlumnos, setListaAlumnos] = useState([])
    const [alumnosSelect, setAlumnosSelect] = useState([])
    const { getClaseSelectedId} = useClaseContext();
    const { getToken} = useGeneralContext();
    const { loading } = useFetchEffect(
        () => {
            return StudentHelper.getAllClassStudents( getClaseSelectedId(), getToken())
        },
        [ getToken],
        {
            condition: true,
            handleSuccess: (r) => {
                setListaAlumnos(r.data.dataList)
            },
            handleError: () => {
                toast.error("No se pudieron obtener los alumnos de la clase. Intente recargar la página")
            }
        }
    )
    const { loadingSelect } = useFetchEffect(
        () => {
            return StudentHelper.getAllNotClassStudents( getClaseSelectedId(), getToken())
        },
        [ getToken],
        {
            condition: true,
            handleSuccess: (r) => {
                setAlumnosSelect(r.data)
                console.log(r.data)
            },
            handleError: () => {
                toast.error("No se pudieron obtener los alumnos para seleccionar. Intente recargar la página")
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
        list: listaAlumnos,
        options: alumnosSelect
    }
    return <>
        <ScrolleableTable isLoading={loading} loadingSelect={loadingSelect} studentsList={Alumnos} />
    </>

}

export default AlumnosInscriptos