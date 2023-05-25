const DayMonthPicker = ({ selectedDate = () => new Date(), handleDateChange = () => { } }) => {
  const currentDate = new Date();
  const currentYear = currentDate.getFullYear();
  
  const formatDate = (date) => {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  };

  return (
    <input
      type="date"
      min={`${currentYear}-01-01`}
      max={`${currentYear}-12-31`}
      value={formatDate(selectedDate)}
      onChange={handleDateChange}
    />
  );
};

export default DayMonthPicker;
