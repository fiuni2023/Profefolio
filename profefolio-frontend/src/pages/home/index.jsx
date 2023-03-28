import React from "react";
import { useGeneralContext } from "../../context/GeneralContext";
import AdminHome from './dashboards/adminHome';
import ProfeHome from "./dashboards/profeHome";
import CAdminHome from './dashboards/cAdminHome'


const Home = () => {
    
    const { cancan } = useGeneralContext()

    const getDashBoard = () => {
        if(cancan("Master")) return <AdminHome />
        if(cancan("Administrador de Colegio")) return <CAdminHome />
        if(cancan("Profesor")) return <ProfeHome />
    }

    return <>
        {getDashBoard()}
    </>
}

export default Home