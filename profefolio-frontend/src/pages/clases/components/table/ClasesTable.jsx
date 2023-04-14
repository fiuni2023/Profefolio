import React, { useState } from 'react'
import { TableContainer } from '../../../alumnos/styles/Styles'
import Tabla from '../../../../components/Tabla'
import { useFetchEffect } from '../../../../components/utils/useFetchEffect'
import ClassesHelper from '../../Helpers/ClassesHelper'
import { toast } from 'react-hot-toast'
import ClasesPaginacion from './ClasesPaginacion'

const ClasesTable = ({condFetch, colegioId, getToken, doChangeStudent}) => {
    const [currentPage, setCurrentPage] = useState(0);
    const [nextPage, setNextPage] = useState(false);
    //const [page, setPage] = useState(0);
    const [classesTable, setClassesTable] = useState(
        {
            tituloTabla: "ClassesList",
            titulos: [{ titulo: "Id" }, { titulo: "Nombre" }, { titulo: "Turno" }, { titulo: "Ciclo" }, { titulo: "Año" }]
        }
    );

    /* const handlePrevClick = () => {
        page > 0 && setPage(page - 1);
    };

    const handleNextClick = () => {
        nextPage && setPage(page + 1);
    }; */

    const { isLoading, error } = useFetchEffect(
        () => {
            return ClassesHelper.getClassesPage(currentPage, colegioId, getToken())
        },
        [currentPage, getToken, condFetch],
        {
            condition: condFetch,
            handleSuccess: (r) => {
                console.log(r)
                setNextPage(r.data.next)
                setClassesTable({
                    ...classesTable, clickable: { action: doChangeStudent },
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
                toast.error(e.response.data)
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