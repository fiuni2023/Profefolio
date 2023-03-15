import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Navbar from "../../components/Navbar.jsx";
import SideBar from "../../components/Sidebar.jsx";
import { GeneralProvider } from "../../context/GeneralContext";
import Administrador from "../administrador/index.jsx";
import ListAdministrador from "../administrador/pages/list/index.jsx";

import ProfesorList from "../profesor/list/index.jsx";
import Partidos from "../partidos";
import CreatePartidos from "../partidos/components/create";
import PartidosEdit from "../partidos/components/edit";
import PartidosList from "../partidos/components/list";
import ListarColegios from "../administradorMaster/colegios/ListarColegios";
import CreateProfesor from "../profesor/components/create/CreateProfesor.jsx";
import 'bootstrap/dist/css/bootstrap.min.css';
import Login from "../login/index.jsx";
import ModalAgregarColegios from "../administradorMaster/colegios/ModalAgregarColegios"


import Profesor from "../profesor/index.jsx";

const App = () => {



    return (
        <>
            <BrowserRouter>
                <GeneralProvider>
                    <div className="page">
                        <Navbar />
                        <div className="content">
                            <SideBar />
                            <Routes>
                                <Route path="/" element={<>Home</>} />
                                <Route path="/pagina1" element={<Partidos />}>
                                    <Route path="list" element={<PartidosList />} />
                                    <Route path='create' element={<CreatePartidos />} />
                                    <Route path="edit" element={<PartidosEdit />} />
                                </Route>
                                <Route path="/administrador" element={<Administrador />}>
                                    <Route path="list" element={<ListAdministrador />} />
                                    <Route path="listColegios" element={<ListarColegios />} />
                                    <Route path="modalColegios" element={<ModalAgregarColegios />} />
                                </Route>


                                <Route path="/profesor" element={<Profesor />}>
                                    <Route path="list" element={<ProfesorList />} />
                                </Route>

                                <Route path="/profesor/create" element={<CreateProfesor />}>
                                    <Route path="list" element={<CreateProfesor />} />
                                </Route>
                            </Routes>
                        </div>
                    </div>
                    
                </GeneralProvider>
            </BrowserRouter>
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
            `}</style>
        </>
    )
}

export default App