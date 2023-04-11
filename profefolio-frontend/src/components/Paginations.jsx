import React from "react";
import Pagination from 'react-bootstrap/Pagination';



const Paginations = ({ totalPage, currentPage, setCurrentPage, next }) => {
    const getPages = () => {
        return (
            <>
                <Pagination.Prev disabled={currentPage <= 0} onClick={() => {
                    setCurrentPage(currentPage - 1)
                }} />
                <Pagination.Item disabled >{currentPage + 1} DE {totalPage}</Pagination.Item>
                <Pagination.Next disabled={!next} onClick={() => {
                    setCurrentPage(currentPage + 1)
                }}/>
            </>
        )
    }
    //Otra version de paginacion
    /*  <div className={styles.pagination}>
            <button className={styles.buttonPag} disabled={currentPage <= 0} onClick={() => {
                setCurrentPage(currentPage - 1)
            }}><AiFillCaretLeft /></button>
            <div className={styles.paginas}>{currentPage + 1} DE {totalPage}</div>
            <button className={styles.buttonPag} disabled={!next} onClick={() => {
                setCurrentPage(currentPage + 1)
            }}><AiFillCaretRight /></button>
        </div>
     */
    return (<>

       
        <Pagination size="sm mt-3" >
            {getPages()}
        </Pagination>



    </>)
}
export default Paginations