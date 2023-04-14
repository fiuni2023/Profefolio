import { Pagination } from 'react-bootstrap'


const ClasesPaginacion = ({ currentPage, setCurrentPage, nextPage, isLoading, error }) => {

    return <>
        <Pagination size="sm mt-3">
            
                <Pagination.Prev disabled={currentPage <= 0} onClick={() => {
                    setCurrentPage(currentPage - 1)
                }}
                />
                <Pagination.Item disabled >{currentPage + 1}</Pagination.Item>
                <Pagination.Next disabled={!nextPage || isLoading || error} onClick={() => {
                    setCurrentPage(currentPage + 1)
                }} />
            
        </Pagination>
    </>
}

export default ClasesPaginacion