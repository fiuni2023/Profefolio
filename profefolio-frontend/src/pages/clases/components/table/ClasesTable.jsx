import React, { useState } from 'react'
import { TableContainer } from '../../../alumnos/styles/Styles'
import Tabla from '../../../../components/Tabla'
import { useFetchEffect } from '../../../../components/utils/useFetchEffect'
import ClassesHelper from '../../Helpers/ClassesHelper'
import { toast } from 'react-hot-toast'
import ClasesPaginacion from './ClasesPaginacion'

const ClasesTable = ({ condFetch, colegioId, getToken, doChangeClase, triggerUpdate }) => {
    const [currentPage, setCurrentPage] = useState(0);
    const [nextPage, setNextPage] = useState(false);
    const [classesTable, setClassesTable] = useState(
        {
            tituloTabla: "ClassesList",
            titulos: [{ titulo: "Id" }, { titulo: "Nombre" }, { titulo: "Turno" }, { titulo: "Ciclo" }, { titulo: "Año" }]
        }
    );

    const { isLoading, error } = useFetchEffect(
        () => {
            return ClassesHelper.getClassesPage(currentPage, colegioId, getToken())
        },
        [currentPage, getToken, condFetch, triggerUpdate],
        {
            condition: condFetch,
            handleSuccess: (r) => {
                setNextPage(r.data.next)
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
            },
            handleError: (e) => {
                let newText = "error";
                if (typeof (e.response.data) === "string" && `${e.response.data}`.includes("No existe la pagina:")) {
                    const text = e.response.data.trim().split(": ")
                    const numPag = parseInt(text[1]) + 1;
                    newText = `${text[0]} ${numPag}`;
                    if (numPag === 1) {
                        toast.error("No tiene Clases creadas todavia.")
                    } else {
                        toast.error(newText)
                    }
                } else {
                    toast.error(e.response.data)
                }

            }
        }
    )



    return <>

        <TableContainer>
            <Tabla datosTabla={classesTable} />

            <ClasesPaginacion
                currentPage={currentPage}
                setCurrentPage={setCurrentPage}
                nextPage={nextPage}
                isLoading={isLoading}
                error={error}
            />



        </TableContainer >

    </>
}

export default ClasesTable