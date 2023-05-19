import React from "react";
import { useModularContext } from "../../context";

const BasePage = () => {

    const { currentPage } = useModularContext()

    return(
        <>
            {currentPage}
        </>
    )
}

export default BasePage