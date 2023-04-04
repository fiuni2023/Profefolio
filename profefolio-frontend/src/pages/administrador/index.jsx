import React from "react";
import { Outlet } from "react-router-dom";
import { AdminProvider } from "./context/AdminContext";

const Administrador = () => {
    return (
        <>     
            <AdminProvider>
                <Outlet />
            </AdminProvider>
        </>
    )
}

export default Administrador