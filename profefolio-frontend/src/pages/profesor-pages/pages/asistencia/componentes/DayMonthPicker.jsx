const DayMonthPicker = () => {
  const currentDate = new Date();
  const currentYear = currentDate.getFullYear();
  
  return (
    <input
      id= "fechaSeleccionada"
      type="date"
      style={{ width: '90px' , borderRadius: '10px', border: 'solid 1px'}} 
      min={`${currentYear}-01-01`}
      max={`${currentYear}-12-31`}
      defaultValue={currentDate.toISOString().split('T')[0]}
    />
  );
};

export default DayMonthPicker;
