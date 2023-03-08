import React from "react";
import { Modal } from "react-bootstrap";

const ModalContainer = ({children, show = false, handleClose=()=>{}, title=""}) =>{
    return(
        <>
            <Modal backdropClassName="Backdrop" contentClassName="ModalCustom" show={show} onHide={handleClose} centered>
                {children}
            </Modal>
            <style jsx="true">{`
                .Backdrop{
                    background-color: white;
                    opacity: 0.25;
                }
                .ModalCustom{
                    background-color: #BBBBBB;
                    box-shadow: 0px 0px 10px 0px #00000040;
                }
                .Header{
                    text-align: center;
                }
            `}</style>
        </>
    )
}

export default ModalContainer