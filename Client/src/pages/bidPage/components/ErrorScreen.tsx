const ErrorScreen = () => {
  return (
    <div className="flex flex-col justify-center  mt-auto items-center w-full">
      <h1 className="text-3xl font-thin">
        This bid is not available or has been moved
      </h1>
      <img src="/no-content.gif" alt="" width={500} />
    </div>
  );
};

export default ErrorScreen;
