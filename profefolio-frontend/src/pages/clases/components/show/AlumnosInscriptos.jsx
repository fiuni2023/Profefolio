import React, { useState } from 'react'
import ScrolleableTable from '../../../../components/ScrolleableTable'
import StudentHelper from '../../../alumnos/helpers/StudentHelper'
import { useFetchEffect } from '../../../../components/utils/useFetchEffect'
import { useGeneralContext } from '../../../../context/GeneralContext';
import { toast } from 'react-hot-toast';
import { useClaseContext } from '../../context/ClaseContext';
const AlumnosInscriptos = () => {
    const [listaAlumnos, setListaAlumnos] = useState([])
    const [nuevaListaAlumnos, setNuevaListaAlumnos] = useState([])
    const [alumnosSelect, setAlumnosSelect] = useState([])
    const { getClaseSelectedId } = useClaseContext();
    const { getToken } = useGeneralContext();
    const { loading } = useFetchEffect(
        () => {
            return StudentHelper.getAllClassStudents(getClaseSelectedId(), getToken())
        },
        [getToken],
        {
            condition: true,
            handleSuccess: (r) => {
                setListaAlumnos(r.data)
                setNuevaListaAlumnos(r.data)
                console.log(r.data)
            },
            handleError: () => {
                toast.error("No se pudieron obtener los alumnos de la clase. Intente recargar la página")
            }
        }
    )
    const { loadingSelect } = useFetchEffect(
        () => {
            return StudentHelper.getAllNotClassStudents(getClaseSelectedId(), getToken())
        },
        [getToken],
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
        list: nuevaListaAlumnos,
        options: alumnosSelect
    }

    const handleDeleteStudent = (idAlumno) => {
        const index = nuevaListaAlumnos.findIndex(student => student.id === idAlumno);
        const updatedAlumno = [...nuevaListaAlumnos];
        updatedAlumno[index] = {
            ...updatedAlumno[index],
            status: 'D'
        };
        setNuevaListaAlumnos(updatedAlumno);
        console.log(nuevaListaAlumnos)
    }

    const handleRestoreStudent = (idAlumno) => {
        const index = listaAlumnos.findIndex(student => student.id === idAlumno);
        const newStudent = listaAlumnos.findIndex(student => student.id === idAlumno)<0;
        const updatedAlumno = [...nuevaListaAlumnos];

        updatedAlumno[index] = {
            ...updatedAlumno[index],
            status: newStudent ? 'N' : ''
        };
        setNuevaListaAlumnos(updatedAlumno);
        console.log(nuevaListaAlumnos)
    }

    const handleSelectOption = (event) => {
        const index = alumnosSelect.findIndex(option => option.alumnoId === event.target.value);
        const selectedStudent = { ...alumnosSelect[index], status: 'N' };
        console.log(selectedStudent)
        setNuevaListaAlumnos([...nuevaListaAlumnos, selectedStudent]);
        setAlumnosSelect([...alumnosSelect.slice(0, index), ...alumnosSelect.slice(index + 1)]);
    };
    return <>
        <ScrolleableTable
            isLoading={loading}
            loadingSelect={loadingSelect}
            studentsList={Alumnos}
            handleSelectOption={handleSelectOption}
            handleDeleteStudent={handleDeleteStudent} 
            handleRestoreStudent={handleRestoreStudent}/>
    </>

}

export default AlumnosInscriptos