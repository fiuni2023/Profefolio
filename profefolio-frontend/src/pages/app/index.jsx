import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Navbar from "../../components/Navbar.jsx";
import SideBar from "../../components/Sidebar.jsx";
import { GeneralProvider} from "../../context/GeneralContext";
import Partidos from "../partidos";
import CreatePartidos from "../partidos/components/create";
import PartidosEdit from "../partidos/components/edit";
import PartidosList from "../partidos/components/list";

const App = () => {

    return (
        <>
                <BrowserRouter>
            <GeneralProvider>
                    <div className="page">
                        <Navbar />
                        <div className="content">
                            <SideBar />
                            <div className="relativeContainer">
                                    <Routes>
                                        <Route path="/" element={<>Home</>}/>
                                        <Route path="/pagina1" element={<Partidos />}>
                                            <Route path="list" element={<PartidosList />} />
                                            <Route path='create' element={<CreatePartidos />} />
                                            <Route path="edit" element={<PartidosEdit />} />
                                        </Route>
                                    </Routes>
                            </div>
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
                width:100%;
                height: 100%;
            }
            `}</style>
        </>
    )
}

export default App