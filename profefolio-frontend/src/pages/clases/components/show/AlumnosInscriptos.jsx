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
        [],
        {
            condition: true,
            handleSuccess: (r) => {
                setListaAlumnos(r.data)
                setNuevaListaAlumnos(r.data)
            },
            handleError: () => {
                toast.error("No se pudieron obtener los alumnos de la clase. Intente recargar la página")
            }
        }
    )
    const { loading: loadingSelect } = useFetchEffect(
        () => {
            return StudentHelper.getAllNotClassStudents(getClaseSelectedId(), getToken())
        },
        [],
        {
            condition: true,
            handleSuccess: (r) => {
                setAlumnosSelect(r.data)
            },
            handleError: () => {
                toast.error("No se pudieron obtener los alumnos para seleccionar. Intente recargar la página")
            }
        }
    )
    const handleDeleteStudent = (idAlumno) => {
        let index = listaAlumnos.findIndex(alumno => alumno.id === idAlumno);
        if (index === -1) {
            index = nuevaListaAlumnos.findIndex(student => student.id === idAlumno);
            const deletedStudent = nuevaListaAlumnos[index];
            setAlumnosSelect([...alumnosSelect, deletedStudent]);
            setNuevaListaAlumnos([...nuevaListaAlumnos.slice(0, index), ...nuevaListaAlumnos.slice(index + 1)]);
        } else {
            index = nuevaListaAlumnos.findIndex(student => student.id === idAlumno);
            const updatedAlumno = [...nuevaListaAlumnos];
            updatedAlumno[index] = {
                    ...updatedAlumno[index],
                    status: 'D'
                };
                setNuevaListaAlumnos(updatedAlumno);
            }
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
    }

    const handleStudent = (idAlumno, status) => {
        status === 'D' ? handleRestoreStudent(idAlumno)
            : handleDeleteStudent(idAlumno)
    }
    const handleSelectOption = (event) => {
        const index = alumnosSelect.findIndex(alumno => alumno.id.toString() === event.target.value);
        const selectedStudent = { ...alumnosSelect[index], status: 'N' };
        setNuevaListaAlumnos([...nuevaListaAlumnos, selectedStudent]);
        setAlumnosSelect([...alumnosSelect.slice(0, index), ...alumnosSelect.slice(index + 1)]);
        event.target.value = "";
    };
    const useHandleSubmit = (e) => {
        e.preventDefault()
        let list = []
        for (let index = 0; index < nuevaListaAlumnos.length; index++) {
            let alumno = nuevaListaAlumnos[index];
            if (alumno.status === 'N') {
                list = [...list, { colegioAlumnoId: alumno.id, estado: "N" }]
            }
            else if (alumno.status === 'D') {
                list = [...list, { colegioAlumnoId: alumno.id, estado: "D" }]
            }
        }
        const body = { "claseId": getClaseSelectedId(), "listaAlumnos": list }
        StudentHelper.updateStudentsList(body, getToken())
            .then(() => {
                toast.success("Los datos fueron enviados correctamente.")
                window.location.reload()
            })
            .catch(() => {
                toast.error("No se pudieron guardar los cambios. Intente de nuevo o recargue la página.")
            })
    }
    const Alumnos = {
        onSubmit: useHandleSubmit,
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
            handleStudent={handleStudent}
            handleSubmit={useHandleSubmit} />
    </>

}

export default AlumnosInscriptos