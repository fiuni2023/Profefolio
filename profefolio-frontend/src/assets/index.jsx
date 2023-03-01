import React from "react";

export const LogoNavBar = ({
    width = "100%",
    height = "100%"
}) => {
    const navbarLogoImage = require('./images/BigLogo.png')
    return(
        <>
            <div className="imageContainer">
                <img src={navbarLogoImage} alt="Logo" />
            </div>
            <style jsx="true">{`
                .imageContainer{
                    width: 100%;
                    height: 100%;
                }
                .image{
                    width: ${width};
                    height: ${height};
                }
            `}</style>
        </>
    )
}