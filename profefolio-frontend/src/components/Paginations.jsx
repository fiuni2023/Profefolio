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
        const inputValue = e.target.value;
        if (!e.key === 'Enter'&& !(isNaN(inputValue))) {
           console.log(inputValue)
           setCurrentPage(parseInt(inputValue))
           
        } 
    };

    return (<>

        <div className={styles.pagination}>

            <IconButton enabled={currentPage > 0} buttonType='arrowL' onClick={restar} />
            <input

                className="text-center"
                type="number"
                value={currentPage+1}
                onChange={onChangeValue}
                
                
               
            />

            de {totalPage}
            <IconButton enabled={next} buttonType='arrowR' onClick={sumar} />

        </div>




    </>)
}
export default Paginations