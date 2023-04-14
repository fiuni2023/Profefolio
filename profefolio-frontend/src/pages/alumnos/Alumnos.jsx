import React, { useEffect, useState } from 'react'
import { useGeneralContext } from '../../context/GeneralContext'
import { AiOutlinePlus } from 'react-icons/ai'
import { AddButton, MainContainer, TableContainer } from './styles/Styles'
import StudentHelper from './helpers/StudentHelper'
import { Pagination } from "react-bootstrap";
import Tabla from '../../components/Tabla';
import { toast } from "react-hot-toast";
import { useNavigate } from 'react-router-dom';
import { useFetchEffect } from '../../components/utils/useFetchEffect';
import StyleComponentBreadcrumb from '../../components/StyleComponentBreadcrumb';
import ModalAlumnos from './components/ModalAlumnos'

const Alumnos = () => {
    const { getToken, cancan, verifyToken } = useGeneralContext()
    const [condFetch, setCondFetch] = useState(false)
    const [currentPage, setCurrentPage] = useState(0)
    const [next, setNext] = useState(true)
    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "studentsList",
        titulos: [{ titulo: "CI" }, { titulo: "Nombre" }, { titulo: "Fecha de nacimiento" }, { titulo: "Dirección" }]
    });

    const parseToDate = (d = new Date()) => {
        return `${d.getFullYear()}-${d.getMonth() > 10 ? d.getMonth() + 1 : `0${d.getMonth() + 1}`}-${d.getDate() > 9 ? d.getDate() : `0${d.getDate()}`}`
    }

    const nav = useNavigate()

    useEffect(() => {
        verifyToken()
        if (!cancan("Administrador de Colegio")) {
            nav('/')
        } else {
            setCondFetch(true)
        }
    }, [cancan, verifyToken, nav])

    const doChangeStudent = (data) => {
        console.log("Seleccionado", data)
    }

    const { isLoading, error } = useFetchEffect(
        () => {
            return StudentHelper.getStudentsPage(currentPage, getToken())
        },
        [currentPage, getToken, condFetch],
        {
            condition: condFetch,
            handleSuccess: (r) => {
                setNext(r.data.next)
                setDatosTabla({
                    ...datosTabla, clickable: { action: doChangeStudent },
                    filas: r.data.dataList.map((dato) => {
                        return {
                            fila: dato,
                            datos: [
                                { dato: dato?.documento ? dato.documento : "" },
                                { dato: dato?.nombre && dato.apellido ? dato.nombre + " " + dato.apellido : "" },
                                { dato: dato?.nacimiento ? parseToDate(new Date(dato.nacimiento)) : "" },
                                { dato: dato?.direccion ? dato.direccion : "" }]
                        }
                    })

                })
            },
            handleError: () => {
                toast.error("No se pudieron obtener los datos. Intente recargar la página")
            }
        }
    )

    const [show, setShow] = useState(false);
    const [disabled, setDisabled] = useState(false);
    const [tituloModal, setTituloModal] = useState("Agregar Alumno")
    // const handleShow = (id) => {
    //     setDatoIdColegio(id);

    //     setShow(true);
    // }

    // const handleChangeTituloModal = (titulo) => {
    //     setTituloModal(titulo);
    // }

    const openNew = () => {
        setShow(!show);
    }

    const getPages = () => {
        return (
            <>
                <Pagination.Prev disabled={currentPage <= 0} onClick={() => {
                    setCurrentPage(currentPage - 1)
                }} />
                <Pagination.Item disabled >{currentPage + 1}</Pagination.Item>
                <Pagination.Next disabled={!next || isLoading || error} onClick={() => {
                    setCurrentPage(currentPage + 1)
                }} />
            </>
        )
    }

    return (
        <>
            <MainContainer>
                <StyleComponentBreadcrumb nombre="Alumnos" />
                <TableContainer>
                    <Tabla datosTabla={datosTabla} />
                    <Pagination size="sm mt-3">
                        {getPages()}
                    </Pagination>
                    <AddButton onClick={openNew}>
                        <AiOutlinePlus size={"35px"} />
                    </AddButton>
                </TableContainer >
                <ModalAlumnos tituloModal={tituloModal} isOpen={show} disabled={disabled}></ModalAlumnos>  

            </MainContainer >
        </>
    )
}

export default Alumnos