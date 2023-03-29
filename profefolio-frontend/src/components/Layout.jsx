import React from "react";

export const PanelContainer = ({ children, className }) => {
    return <>
        <div className={`panel-container ${className}`}>
            {children}
        </div>
        <style jsx="true">{`
            .panel-container{
                background-color: white;
                border-radius: 10px;
                border: 1px solid black;
            }
        `}</style>
    </>
}
// color original #363636
export const PanelContainerBG = ({ children, className }) => {
    return <>
        <PanelContainer className={`PanelBig ${className}`}>
            {children}
        </PanelContainer>
        <style jsx='true'>{`
                .PanelBig{
                    height: 100%;
                    width: 100%;
                    padding: 2vh;
                }
            `}</style>
    </>
}

export const MainLayout = ({
    titleText = "",

}) => {

}
