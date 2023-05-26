import React, { useState } from 'react'
import { TableContainer } from '../../../alumnos/styles/Styles'
import Tabla from '../../../../components/Tabla'
import { useFetchEffect } from '../../../../components/utils/useFetchEffect'
import ClassesHelper from '../../Helpers/ClassesHelper'
import { toast } from 'react-hot-toast'
import Paginations from '../../../../components/Paginations'
import Spinner from '../../../../components/componentsStyles/SyledSpinner'
import Text from '../../../../components/componentsStyles/StyledText'

const ClasesTable = ({ condFetch, colegioId, getToken, doChangeClase, triggerUpdate }) => {
    const [currentPage, setCurrentPage] = useState(0);
    const [nextPage, setNextPage] = useState(false);
    const [totalPage, setTotalPage] = useState(0)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState(false)
    const [classesTable, setClassesTable] = useState(
        {
            tituloTabla: "ClassesList",
            titulos: [{ titulo: "Id" }, { titulo: "Nombre" }, { titulo: "Turno" }, { titulo: "Ciclo" }, { titulo: "Año" }]
        }
    );

    useFetchEffect(
        () => {
            return ClassesHelper.getClassesPage(currentPage, colegioId, getToken())
        },
        [currentPage, getToken, condFetch, triggerUpdate],
        {
            condition: condFetch,
            handleSuccess: (r) => {
                setNextPage(r.data.next)
                console.log(r)
                setTotalPage(r.data.totalPage)
                setClassesTable({
                    ...classesTable, clickable: { action: doChangeClase },
                    filas: r.data.dataList.map((dato) => {
                        return {
                            fila: dato,
                            datos: [
                                { dato: dato?.id ? dato.id : 0 },
                                { dato: dato?.nombre ? dato.nombre : "" },
                                { dato: dato?.turno ? dato.turno : "" },
                                { dato: dato?.ciclo ? dato.ciclo : "" },
                                { dato: dato?.anho ? dato.anho : 0 }]
                        }
                    })

                })
                setLoading(false)
            },
            handleError: (e) => {
                let newText = "error";
                if (typeof (e.response.data) === "string" && `${e.response.data}`.includes("No existe la pagina:")) {
                    const text = e.response.data.trim().split(": ")
                    const numPag = parseInt(text[1]) + 1;
                    newText = `${text[0]} ${numPag}`;
                    // if (numPag === 1) {
                    //     toast.error("No tiene Clases creadas todavia.")
                    // } else {
                    //     toast.error(newText)
                    // }
                } else {
                    toast.error(e.response.data)
                setError(true)
                }
                setLoading(false)
            }
        }
    )


    return <>

        {loading ? <Spinner height={'calc(100vh - 80px)'} />
            : error ? <Text>Lamentamos esto, ha ocurrido un error al obtener los datos.</Text>
                : <TableContainer>
                    <Tabla datosTabla={classesTable} />
                    <Paginations next={nextPage} currentPage={currentPage} setCurrentPage={setCurrentPage} totalPage={totalPage} />
                </TableContainer >
        }
    </>
}

export default ClasesTable