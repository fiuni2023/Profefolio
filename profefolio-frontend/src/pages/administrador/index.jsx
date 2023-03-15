import React from "react";
import {Link, Outlet } from "react-router-dom";

const Administrador = () => {
    return (
        <>

             <Link to="/profesor">Profesores</Link>
               
            <Outlet />
        </>
    )
}

export default Administrador