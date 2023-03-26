import React from "react";

function Modal(props) {
  return (
    <div className="modal">
      <div className="modal-content">
        {props.children}
        <button onClick={props.onClose}>Cerrar</button>
      </div>
    </div>
  );
}

export default Modal;