import React from "react";
import { ModularProvider } from "./context";
import BasePage from "./pages/base";

const ProfPages = () => {
    return(
        <>
            <ModularProvider>
                <BasePage />
            </ModularProvider>
        </>
    )
}

export default ProfPages