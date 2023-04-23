import React from "react";
import { Outlet } from "react-router-dom";
import { StudentProvider } from "./context/StudentContext";

const Alumnos = () => {
    return (
        <>     
            <StudentProvider>
                <Outlet />
            </StudentProvider>
        </>
    )
}

export default Alumnos