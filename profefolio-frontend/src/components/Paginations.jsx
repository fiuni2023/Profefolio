import React from "react";
import Pagination from 'react-bootstrap/Pagination';
import styles from './Paginations.module.css'
import IconButton from './IconButton'
import { toast } from "react-hot-toast";
const Paginations = ({ totalPage, currentPage, setCurrentPage, next }) => {
    const getPages = () => {
        return (
            <>
                <Pagination.Prev disabled={currentPage <= 0} onClick={() => {
                    setCurrentPage(currentPage - 1)
                }} />

                <Pagination.Next disabled={!next} onClick={() => {
                    setCurrentPage(currentPage + 1)
                }} />
            </>
        )
    }
    const getPage = (event) => {

        if (event.target.value >= totalPage) {
           toast.error("No existe la Pagina nro."+event.target.value)
        }
        else {
            setCurrentPage(event.target.value);
        }

    }
    //Otra version de paginacion
    /*  <div className={styles.pagination}>
            
            <div className={styles.paginas}>{currentPage + 1} DE {totalPage}</div>
            
        </div>
     */
    return (<>

        <div className={styles.pagination}>
            <IconButton enabled={currentPage > 0} buttonType='arrowL' onClick={() => setCurrentPage(currentPage - 1)} />
            <div className={styles.texto}>
                <input type="number" value={currentPage + 1} onChange={event => getPage(event)} className={styles.inputCurrent} />

                de {totalPage}
            </div>

            <IconButton enabled={next} buttonType='arrowR' onClick={() => setCurrentPage(currentPage + 1)} />



        </div>




    </>)
}
export default Paginations