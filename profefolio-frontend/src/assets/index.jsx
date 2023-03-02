import React from "react";

export const LogoNavBar = ({
    width = "100%",
    height = "100%"
}) => {
    const navbarLogoImage = require('./images/BigLogo.png')
    return(
        <>
            <div className="imageContainer">
                <img className="image" src={navbarLogoImage} alt="Logo" />
            </div>
            <style jsx="true">{`
                .imageContainer{
                    width: ${width};
                    height: ${height};
                }
                .image{
                    width: 100%;
                    height: 100%;
                }
            `}</style>
        </>
    )
}