import React from "react";
import { Modal } from "react-bootstrap";
import ModalContainer from "../../../components/Modals";
import styles from './CreateModal.module.css'

const LACreateModal = ({ 
    show = false, 
    handleClose = () => {}, 
}) => {
    return(
        <>
            <ModalContainer show={show} handleClose={handleClose} >
                <Modal.Body>
                    <div className={styles.Header}>
                        <div className={styles.Htext}>
                            <h5>agregar administrador</h5>
                        </div>
                        <div className={styles.ExitContainer}>
                            
                        </div>
                    </div>
                    <div className={styles.Divisor}></div>
                </Modal.Body>
            </ModalContainer>
        </>
    )
}

export default LACreateModal