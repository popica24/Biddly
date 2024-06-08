type Props = {
  winner: string | undefined;
};

const LoadingOverlay = (props: Props) => {
  return (
    <div className="fixed inset-0 bg-black opacity-70 z-40">
      <div className="absolute left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2 z-40">
        <div className="flex flex-col items-center justify-center z-40">
          <img src="/loading.gif" alt="" width={200} />
          <span className="text-white">
            {props.winner ? props.winner : "Chosing winner"}
          </span>
        </div>
      </div>
    </div>
  );
};

export default LoadingOverlay;
