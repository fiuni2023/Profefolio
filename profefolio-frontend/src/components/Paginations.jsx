import React from "react";
import styles from './Paginations.module.css'
import IconButton from './IconButton'

const Paginations = ({ totalPage, currentPage, setCurrentPage, next }) => {
    const sumar = () => {

        setCurrentPage((currentPage) => currentPage + 1);
    };
    const restar = () => {
        setCurrentPage((currentPage) => currentPage - 1)

    };



    return (<>

        <div className={styles.pagination}>

            <IconButton enabled={currentPage > 0} buttonType='arrowL' onClick={restar} />
            <p className={styles.text}>{currentPage+1} de {totalPage} </p>

            
            <IconButton enabled={next} buttonType='arrowR' onClick={sumar} />

        </div>




    </>)
}
export default Paginations