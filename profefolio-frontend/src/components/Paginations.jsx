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
    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
          console.log('do validate')
        }
      }
    const onChangeValue = (e) => {
        let cambio = parseInt(e.target.value)
        console.log(cambio);
        if (e.key === 'Enter') {
            console.log('do validate')
          }
        if(!(isNaN(cambio))){
            setCurrentPage(parseInt(e.target.value));
        }
        else{
            
        }
    };

    return (<>

        <div className={styles.pagination}>

            <IconButton enabled={currentPage > 0} buttonType='arrowL' onClick={restar} />
            <input
                className="text-center"
                type="number"
                value={isNaN(currentPage)? 0 : currentPage+1}
                onChange={onChangeValue}
                max={totalPage - 1}
            />
            de {totalPage}
            <IconButton enabled={next} buttonType='arrowR' onClick={sumar} />

        </div>




    </>)
}
export default Paginations