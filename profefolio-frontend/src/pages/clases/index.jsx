import axios from 'axios';
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';
import APILINK from '../../components/link';
import { useGeneralContext } from '../../context/GeneralContext';
import NavAdminClases from './components/NavAdminClase.jsx';
import { PanelContainerBG } from "../../components/Layout.jsx";
import ModalCreateClase from './components/create/ModalCreateClase.jsx';
import BtnAdd from './components/create/BtnAdd';
import useAxiosGet from './hooks/useAxiosGet';
import { toast } from 'react-hot-toast';
import { Pagination } from 'react-bootstrap';
import { AddButton, MainContainer, TableContainer } from '../alumnos/styles/Styles';
import StyleComponentBreadcrumb from '../../components/StyleComponentBreadcrumb';
import Tabla from '../../components/Tabla';
import { AiOutlinePlus } from 'react-icons/ai';
import ClassesHelper from "./Helpers/ClassesHelper";
import { useFetchEffect } from '../../components/utils/useFetchEffect';
import ClasesTable from './components/table/ClasesTable';
import ShowClase from './pages/view/ShowClase';


const Clases = () => {

    const { getToken, cancan, verifyToken, getUserMail } = useGeneralContext();
    const [condFetch, setCondFetch] = useState(false)

    const [showModal, setShowModal] = useState(false);

    const nav = useNavigate()

    // eslint-disable-next-line no-unused-vars
    const [{ id: colegioId, nombre: colegioNombre }, loadingColegio, errorColegio, setColegio] = useAxiosGet(`api/administrador/${getUserMail()}`, getToken());

    
    /*     useEffect(() => {
            verifyToken()
            if (!cancan("Administrador de Colegio")) {
                nav("/")
            } else {
                //axios.get(`https://miapi.com/products?page=${page}&size=${size}`, {
    
                colegio && axios.get(`${APILINK}/api/clase/page/${colegio?.id}/${page}`, {
                    headers: {
                        Authorization: `Bearer ${getToken()}`,
                    }
                })
                    .then(response => {
                        setClases(response.data.dataList);
                        setNextPage(response.data.next);
                    })
                    .catch(error => {
                        //console.error(error);
                        toast.error("Recargue la pagina, no se pudo obtener las clases");
                    });
            }
        }, [page, cancan, verifyToken, nav, getToken, colegio]);
     */

    const handleShowModal = () => {
        setShowModal(true);
    }
    const handelCloseModal = () => {
        setShowModal(false);
    }
    const [tabla, setTabla] = useState(<div>Cargando tabla...</div>);

    useEffect(() => {
        verifyToken()
        if (!cancan("Administrador de Colegio")) {
            nav('/')
        } else {
            setCondFetch(true)
        }
    }, [cancan, verifyToken, nav])

    useEffect(() => {
        if (colegioId !== null) {
            setTabla(<ClasesTable condFetch={condFetch} colegioId={colegioId} getToken={getToken} doChangeStudent={doChangeStudent} />);
        }
    }, [colegioId, condFetch, getToken])


    
    const doChangeStudent = (data) => {
        console.log("Seleccionado", data)
        nav(`view/${data.id}`)
    }


    /* console.log(clases)

    const doFetch = (clase) => {
        setClases(clase)
    }; */



    const [show, setShow] = useState(false);


    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    ///////////////////////////////////

    const [currentPage, setCurrentPage] = useState(0)

    return <>
        
            <MainContainer>
                <StyleComponentBreadcrumb nombre="Clases" />

                {tabla}


                {/* <BtnAdd handleShowModal={handleShow} /> */}
                <AddButton>
                        <AiOutlinePlus size={"35px"} onClick={handleShowModal}/>
                </AddButton>

                <ModalCreateClase title="Agregar Clase" handleClose={handelCloseModal} show={showModal}
                    triggerState={(profesor) => { /* doFetch(profesor) */ }} handlePage={(pag) => {
                        /* setNextPage(pag);
                        setPage(0); */
                    }}>
                </ModalCreateClase>


            </MainContainer >
            
   

        <style jsx='true'>{`
    .page{
        display: grid;
        grid-template-rows: 5% 95%;
        width: 100%;
        height: 100vh;
    }
    .content{
        width: 100%;
        height: 100%;
    }
    
    .NavbarA{
        width: 100%;
        height: 100%;
        background-color:  #F0544F;
        display: flex;
        background-color: #F0544F;
    }
    .NButtonForSideA{
        width: 25%;
        position: absolute;
        bottom: 5px;
        right : 5px;
    }
    .buttonNavBarA{
        width: 100%;
        height: 100%;
        outline: none;
        border: none;
        background-color: #FFFFFF;
        font-size: 50px;
        color: #F0544F;
    }
    .pag{
        outline: none;
        border: none;
        background-color: #FFFFFF;
        font-size: 10px;
        color: #F0544F;
    }

    .buttonNavBarAa{
        outline: none;
        border: none;
        background-color: #FFFFFF;
        font-size: 20px;
        color: black;
    }
    .navbarmainAd{
        width: 97.5%;
        display: flex;
        justify-content: space-between;
    }

    .CustomTable{
        width: 100%;
        border-spacing: 0px;
    }
    .CustomTable>thead>tr>th{
        border: 1px solid black;
        padding-left: 5px;
    }
    .CustomTable>tbody>tr>td{
        text-align: center;
        border: 1px solid black;
    }
    
    .container-table{
        border: 0px !important;
        
    }
    .btn-valid-page{
        /*background: #FDF0D5;*/
        margin-left:1rem;
        /*border: 1px solid black;*/
    }

    .btn-valid-page:hover{
        /*background: #FADAAA;*/
        margin-left:1rem;
        border: 1px solid black;
    }
    
    `}</style>
    </>
}

export default Clases
