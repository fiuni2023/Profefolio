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

import ListarColegios from "../colegios/ListarColegios.jsx";
import CreateProfesor from "../profesor/components/create/CreateModal.jsx";
import ListarMaterias from "../materias/list/ListarMaTerias.jsx"
import Alumnos from "../alumnos/index.jsx";
import ListAlumnos from "../alumnos/pages/ListAlumnos.jsx";
import 'bootstrap/dist/css/bootstrap.min.css';


import Profesor from "../profesor/index.jsx";
import Clases from "../clases/index.jsx";
import ShowClase from "../clases/pages/view/ShowClase.jsx";
import Home from "../home/index.jsx";
import TestPage from "../../components/TestPage.jsx";
import { ClaseProvider } from "../clases/context/ClaseContext.jsx";

const App = () => {

    return (
        <>
            <BrowserRouter>
                <GeneralProvider>
                    <div className="page">
                        <Navbar />
                        <ClaseProvider>
                            <div className="content">
                                <SideBar />
                                <Routes>

                                    <Route path="/" element={<Home />} />
                                    <Route path="/test" element={<TestPage />} />
                                    <Route path="/pagina1" element={<Partidos />}>
                                        <Route path="list" element={<PartidosList />} />
                                        <Route path='create' element={<CreatePartidos />} />
                                        <Route path="edit" element={<PartidosEdit />} />
                                    </Route>

                                    <Route path="/administrador" element={<Administrador />}>
                                        <Route path="list" element={<ListAdministrador />} />
                                    </Route>

                                    <Route path="/colegios/list" element={<ListarColegios />}>

                                    </Route>


                                    <Route path="/profesor" element={<Profesor />}>
                                        <Route path="list" element={<ProfesorList />} />
                                    </Route>

                                    <Route path="/profesor/create" element={<CreateProfesor />}>
                                        <Route path="list" element={<CreateProfesor />} />
                                    </Route>


                                    <Route path="/clases" element={<Clases />}>
                                    </Route>
                                    <Route path="/clases/view/:idClase" element={<ShowClase />} />

                                    <Route path="/materias" element={<ListarMaterias />}>
                                        <Route path="list" element={<ListarMaterias />} />
                                    </Route>
                                    <Route path="/alumnos" element={<Alumnos />} >
                                        <Route path="list" element={<ListAlumnos />} />
                                    </Route>
                                </Routes>
                            </div>
                        </ClaseProvider>
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