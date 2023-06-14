import { Modal, Button } from "react-bootstrap";
import styles from  './Modal.module.css';
function ModalConfirmacion(props) {
  const { show, onHide, onConfirm, message } = props;

  return (


    <Modal show={show} onHide={onHide} centered>
      <Modal.Header closeButton className={styles.footerModal}>
        <Modal.Title style={{fontSize: "16px"}}>Confirmar Eliminación</Modal.Title>
      </Modal.Header>
      <Modal.Body className={styles.footerModal}>{message}</Modal.Body>
      <Modal.Footer className={styles.footerModal}>
        <Button variant="secondary" onClick={onHide}>
          Cancelar
        </Button>
        <Button variant="danger" onClick={onConfirm}>
          Eliminar
        </Button>
      </Modal.Footer>
    </Modal>
  );
}


export default ModalConfirmacion;