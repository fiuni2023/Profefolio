import { Modal } from "react-bootstrap";
import TextButton from '../../../components/TextButton';

function ModalConfirmacion(props) {
  const { show, onHide,onConfirm } = props;

  return (
    <Modal show={show} onHide={onHide} centered >
        <Modal.Header closeButton>
          <Modal.Title>Confirmación de eliminación</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          ¿Está seguro de que desea eliminar?
        </Modal.Body>
        <Modal.Footer>
        <TextButton enabled={true} buttonType='cancel' onClick={() => onHide()}/>
        <TextButton enabled={true} buttonType='accept' onClick={() => onConfirm()} />
        </Modal.Footer>
    </Modal>
    
  );
}

export default ModalConfirmacion;