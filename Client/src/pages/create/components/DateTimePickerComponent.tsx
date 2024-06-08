type Props = {
  text: string;
  dateSetter: React.Dispatch<React.SetStateAction<string>>;
  timeSetter: React.Dispatch<React.SetStateAction<string>>;
};

const DateTimePickerComponent = (props: Props) => {
  return (
    <>
      <div className="flex flex-col items-start justify-start">
        <label htmlFor="">{props.text}</label>
        <div className="relative flex flex-row items-end">
          <input
            type="date"
            onChange={(e) => props.dateSetter(e.target.value)}
          />
          <input
            type="time"
            onChange={(e) => props.timeSetter(e.target.value)}
          />
        </div>
      </div>
    </>
  );
};

export default DateTimePickerComponent;
