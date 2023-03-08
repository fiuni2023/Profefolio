import React, { useState } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Navbar from "../../components/Navbar.jsx";
import SideBar from "../../components/Sidebar.jsx";
import { GeneralProvider} from "../../context/GeneralContext";
import Administrador from "../administrador/index.jsx";
import ListAdministrador from "../administrador/pages/list/index.jsx";
import Partidos from "../partidos";
import CreatePartidos from "../partidos/components/create";
import PartidosEdit from "../partidos/components/edit";
import PartidosList from "../partidos/components/list";
import ListarColegios from "../administradorMaster/colegios/ListarColegios";
import 'bootstrap/dist/css/bootstrap.min.css';
import Login from "../login/index.jsx";

const App = () => {
    const [isLogged, setIsLogged] = useState(false)

    return (
        <>
            <BrowserRouter>
                <GeneralProvider>
                    {
                        isLogged?
                        <div className="page">
                            <Navbar />
                            <div className="content">
                                <SideBar handleLogOut = {()=>{setIsLogged(!isLogged)}} />
                                        <Routes>
                                            <Route path="/" element={<>Home</>}/>
                                            <Route path="/pagina1" element={<Partidos />}>
                                                <Route path="list" element={<PartidosList />} />
                                                <Route path='create' element={<CreatePartidos />} />
                                                <Route path="edit" element={<PartidosEdit />} />
                                            </Route>
                                            <Route path="/administrador" element={<Administrador />}>
                                                <Route path="list" element={<ListAdministrador />}/>
                                                <Route path="listColegios" element={<ListarColegios />}/>
                                            </Route>
                                        </Routes>
                            </div>

                        </div>
                        :
                        <>
                            <Login handleLogin={()=>{setIsLogged(true)}}/>
                        </>
                    }
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