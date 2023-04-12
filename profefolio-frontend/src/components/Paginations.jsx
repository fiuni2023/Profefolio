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

    const onChangeValue = (e) => {
        setCurrentPage(parseInt(e.target.value));
    };

    return (<>

        <div className={styles.pagination}>

            <IconButton enabled={currentPage > 0} buttonType='arrowL' onClick={restar} />
            <input
                className="text-center"
                type="number"
                value={currentPage+1}
                onChange={onChangeValue}
                min={currentPage+1}
                max={currentPage-1}
            />
            de {totalPage}
            <IconButton enabled={next} buttonType='arrowR' onClick={sumar} />

        </div>




    </>)
}
export default Paginations