import React from "react";
import styles from './index.module.css'
export const LogoNavBar = ({
    width = "100%",
    height = "100%"
}) => {
    const navbarLogoImage = require('./images/BigLogo.png')
    return(
        <>
            <div className="imageContainer">
                <img className={styles.image} src={navbarLogoImage} alt="Logo" />
            </div>
            <style jsx="true">{`
                .imageContainer{
                    width: ${width};
                    height: ${height};
                }
            `}</style>
        </>
    )
}

export const Logo = ({
    height = "100%",
    className=""
}) => {
    const navbarLogoImage = require('./images/Logo.png')
    return(
        <>
            <div className={`imageContainer ${className}`}>
                <img className={styles.image} src={navbarLogoImage} alt="Logo" />
            </div>
            <style jsx="true">{`
                .imageContainer{
                    height: ${height};
                    margin-left: 30px;
                    margin-right: 30px;
                }
            `}</style>
        </>
    )
}