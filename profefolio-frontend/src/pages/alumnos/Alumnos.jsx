import React, { useEffect, useState } from 'react'
import { useGeneralContext } from '../../context/GeneralContext'
import { AiOutlinePlus } from 'react-icons/ai'
import { AddButton, MainContainer, TableContainer } from './styles/Styles'
import StudentHelper from './helpers/StudentHelper'
import Tabla from '../../components/Tabla';
import { useNavigate } from 'react-router-dom';
import { useFetchEffect } from '../../components/utils/useFetchEffect';
import StyleComponentBreadcrumb from '../../components/StyleComponentBreadcrumb';
import ModalAlumnos from './components/ModalAlumnos'
import Paginations from '../../components/Paginations'
import Spinner from '../../components/componentsStyles/SyledSpinner'

const Alumnos = () => {
    const { getToken, cancan, verifyToken } = useGeneralContext()
    const [condFetch, setCondFetch] = useState(false)
    const [currentPage, setCurrentPage] = useState(0)
    const [next, setNext] = useState(true)
    const [total_pages, setTotalPages] = useState(0)
    const [selected_student, setSelectedStudent] = useState(null)

    const [datosTabla, setDatosTabla] = useState({
        tituloTabla: "Lista_de_alumnos",
        titulos: [{ titulo: "CI" }, { titulo: "Nombre" }, { titulo: "Fecha de nacimiento" }, { titulo: "Dirección" }]
    });

    const parseToDate = (d = new Date()) => {
        return `${d.getFullYear()}-${d.getMonth() > 8 ? d.getMonth() + 1 : `0${d.getMonth() + 1}`}-${d.getDate() > 9 ? d.getDate() : `0${d.getDate()}`}`
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
        setSelectedStudent(data)
        setShow(true)
    }

    // eslint-disable-next-line no-unused-vars
    const { doFetch, loading, error} = useFetchEffect(
        () => {
            return StudentHelper.getStudentsPage(currentPage, getToken())
        },
        [currentPage, getToken, condFetch],
        {
            condition: condFetch,
            handleSuccess: (r) => {
                setNext(r.data.next)
                setTotalPages(r.data.totalPage)
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
            handleError: (e) => {
                
            }
        }
    )
    const [show, setShow] = useState(false);

    const handleHideModal = () => {
        setSelectedStudent(null)
        setShow(false)
    }
    return (
        <MainContainer>
            <StyleComponentBreadcrumb nombre="Alumnos" />
            {loading ? <Spinner height={'calc(100vh - 80px)'} />
                    : <>
                        <TableContainer>
                            <Tabla datosTabla={datosTabla} />
                            <Paginations currentPage={currentPage} totalPage={total_pages} setCurrentPage={setCurrentPage} next={next} />

                        </TableContainer >
                        
                    </>
            }
            <AddButton onClick={() => { setShow(true) }}>
                <AiOutlinePlus size={"35px"} />
            </AddButton>
            <ModalAlumnos show={show} fetchFunc={doFetch} onHide={handleHideModal} selected_data={selected_student} handleExistingStudent={setSelectedStudent} setShow={setShow} />
        </MainContainer >


    )
}

export default Alumnos