import "./index.css";
const LoadingScreen = () => {
  return (
    <div className="fixed h-screen w-screen inset-0 z-[9999] bg-white">
      <div className="absolute left-1/2 bottom-1/2 -translate-x-1/2">
        <div className="flex flex-col items-center">
          <div className="lds-circle">
            <div></div>
          </div>
          <span className="text-4xl font-thin">Loading...</span>
        </div>
      </div>
    </div>
  );
};

export default LoadingScreen;
