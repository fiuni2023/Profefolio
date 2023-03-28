import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';
import APILINK from '../../components/link';
import { useGeneralContext } from '../../context/GeneralContext';
import NavAdminClases from './components/NavAdminClase.jsx';
import { PanelContainerBG } from "../../components/Layout.jsx";
import ModalCreateClase from './components/create/ModalCreateClase.jsx';
import BtnAdd from './components/create/BtnAdd';

const Clases = () => {
    const [clases, setClases] = useState([]);
    const [page, setPage] = useState(0);
    const [nextPage, setNextPage] = useState(false);

    const { getToken, cancan, verifyToken } = useGeneralContext();

    const nav = useNavigate()

    useEffect(() => {
        verifyToken()
        if (!cancan("Administrador de Colegio")) {
            nav("/")
        } else {
            //axios.get(`https://miapi.com/products?page=${page}&size=${size}`, {

            axios.get(`${APILINK}/api/clase/page/${1}/${page}`, {
                headers: {
                    Authorization: `Bearer ${getToken()}`,
                }
            })
                .then(response => {
                    setClases(response.data.dataList);
                    setNextPage(response.data.next);
                })
                .catch(error => {
                    console.error(error);
                });
        }
    }, [page, cancan, verifyToken, nav, getToken]);

    const doFetch = (clase) => {
        setClases([...clases, clase])
    };

    const handlePrevClick = () => {
        setPage(page - 1);
    };

    const handleNextClick = () => {
        setPage(page + 1);
    };

    const [show, setShow] = useState(false);


    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return <>
        <div className="page">
            <NavAdminClases pageName={"Clases"} />

            <PanelContainerBG>


                <div>
                    <table className="CustomTable">
                        <thead>
                            <tr>
                                <th>Nombre</th>
                                <th>Turno</th>
                                <th>Ciclo</th>
                                <th>Año</th>

                            </tr>
                        </thead>
                        <tbody>

                            {clases.map(clase => (
                                <tr key={clase.id}>
                                    <td>{clase.nombre}</td>
                                    <td>{clase.turno}</td>
                                    <td>{clase.ciclo}</td>
                                    <td>{clase.anho}</td>


                                </tr>
                            ))}
                        </tbody>
                    </table>
                    <div >



                    </div>


                    <nav aria-label="Page navigation example">
                        <ul className="pagination justify-content-end">
                            <li className="page-item disabled">


                                <button className="btn page-item btn-sm" onClick={handlePrevClick} disabled={page === 0}>Anterior</button>
                            </li>
                            <li className={`page-item ${!nextPage ? "disabled" : ""}`}>
                                <button className="btn page-item btn-sm" href="#" onClick={handleNextClick} disabled={!nextPage}>Siguiente</button>
                            </li>
                        </ul>
                    </nav>

                </div>

            </PanelContainerBG>
            <footer>
                <BtnAdd handleShowModal={handleShow} />


                <ModalCreateClase title="Agregar Clase" handleClose={handleClose} show={show}
                    triggerState={(profesor) => { doFetch(profesor) }}>
                </ModalCreateClase>



            </footer>












        </div>

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

            
            `}</style>
    </>
}

export default Clases