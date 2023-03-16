import React from "react";
import { useGeneralContext } from "../../context/GeneralContext";
import styles from "./index.module.css"

const Home = () => {

    const {getUserName} = useGeneralContext()

    return(
        <div className={styles.HomeDiv}>
            <h3>Bienvenido, {getUserName()}!</h3>
            <label>Profefolio version 0.0.1</label>
        </div>
    )
}

export default Home