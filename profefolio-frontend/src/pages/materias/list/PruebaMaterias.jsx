import CreateModalDeleteMateria from "../delete/CreateModalDeleteMateria.jsx";

function PruebaMaterias() {
  const handleDelete = () => {
  
    // código para eliminar el elemento aquí
  };

  return (
    <div>
      <h1>Mi componente</h1>
      <CreateModalDeleteMateria onDelete={handleDelete} />
    </div>
  );
}

export default PruebaMaterias;
