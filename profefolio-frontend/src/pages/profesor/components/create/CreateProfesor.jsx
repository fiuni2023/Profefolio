

import { Form, InputGroup, Row, Button } from 'react-bootstrap';

import NavCreate  from '../NavCreate.jsx';

function CreateProfesor() {
  return (

    <>
    <div>

      <NavCreate/>
        <form>
      <div class="row">

      <div class="form-group row">
        <label for="inputEmail3" class="col-sm-2 col-form-label">Email</label>
        <div class="col-sm-10">
          <input type="email" class="form-control" id="inputEmail3" placeholder="Email"/>
        </div>
      </div>


    <div class="row">
      <input type="text" class="form-control" placeholder="Nombre"/>
    </div>
    <div class="row">
      <input type="text" class="form-control" placeholder="Ci"/>
    </div>
    <div class="row">
      <input type="text" class="form-control" placeholder="Telefono"/>
    </div>
    <div class="row">
      <input type="text" class="form-control" placeholder="Correo electronico"/>
    </div>
    <div class="row">
      <input type="text" class="form-control" placeholder="Direccion"/>
    </div>
    <div class="row">
      <input type="date" class="form-control" placeholder="Fecha de Nacimiento"/>
    </div>
    <div class="row">
      <input type="text" class="form-control" placeholder="Genero"/>
    </div>
    <div class="row">
      <input type="text" class="form-control" placeholder="Ci"/>
    </div>

  </div>
</form>

    
</div>

</>

  );
}

export default CreateProfesor;