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
        const index = nuevaListaAlumnos.findIndex(student => student.id === idAlumno);
        const newStudent = listaAlumnos.findIndex(student => student.id === idAlumno) < 0;
        const updatedAlumno = [...nuevaListaAlumnos];
        updatedAlumno[index] = {
            ...updatedAlumno[index],
            status: newStudent ? 'N' : ''
        };
        setNuevaListaAlumnos(updatedAlumno);
        console.log(nuevaListaAlumnos)
    }

    const handleStudent = (idAlumno, status) => {
        status === 'D' ? handleRestoreStudent(idAlumno)
            : handleDeleteStudent(idAlumno)
    }
    const handleSelectOption = (event) => {
        const index = alumnosSelect.findIndex(option => option.alumnoId === event.target.value);
        const selectedStudent = { ...alumnosSelect[index], status: 'N' };
        console.log(selectedStudent)
        setNuevaListaAlumnos([...nuevaListaAlumnos, selectedStudent]);
        setAlumnosSelect([...alumnosSelect.slice(0, index), ...alumnosSelect.slice(index + 1)]);
    };

    const handleSubmit = (e) => {
        e.preventDefault()
        const list = []
        for (let index = 0; index < nuevaListaAlumnos.length; index++) {
            let alumno = nuevaListaAlumnos[index];
            if (alumno.status === 'N') list.push({ "colegioAlumnoId": alumno.id, "estado": "N" })
            else if (alumno.status === 'D') list.push({ "colegioAlumnoId": alumno.id, "estado": "D" })
        }
        const body = {
            "claseId": 3,
            "listaAlumnos": list,
        }
        console.log(body)

        // const { loading: enviandoDatos } = useFetchEffect(
        //     () => {
        //         return StudentHelper.updateStudentsList(body, getToken())
        //     },
        //     [getToken],
        //     {
        //         condition: true,
        //         handleSuccess: (r) => {
        //             setListaAlumnos(r.data)
        //             setNuevaListaAlumnos(r.data)
        //             console.log(r.data)
        //         },
        //         handleError: () => {
        //             toast.error("No se pudieron obtener los alumnos de la clase. Intente recargar la página")
        //         }
        //     }
        // )
    }

    const Alumnos = {
        onSubmit: handleSubmit,
        enabled: true,
        header: {
            title: "Lista de Alumnos inscriptos",
        },
        addTitle: "Agregar alumnos",
        selectTitle: "Seleccionar alumno",
        list: nuevaListaAlumnos,
        options: alumnosSelect
    }

    return <>
        <ScrolleableTable
            isLoading={loading}
            loadingSelect={loadingSelect}
            studentsList={Alumnos}
            handleSelectOption={handleSelectOption}
            handleStudent={handleStudent} />
    </>

}

export default AlumnosInscriptos