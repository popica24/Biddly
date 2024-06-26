type Props = {
  username?: string | undefined;
  itemName: string | undefined;
  highestBid: number;
  wonAt: string;
  image: string;
};

const WonBid = (props: Props) => {
  return (
    <div className="flex justify-center mt-[68.8px] w-full">
      <div className="flex flex-row items-center justify-center">
        <img width={400} className="aspect-[3/4]" src={props.image} alt="" />
        <div className="flex flex-col items-start">
          <h1 className="text-4xl font-thin my-8">{props.itemName}</h1>
          <h2>Winner : {props.username}</h2>
          <h3>Bid ended : {props.wonAt}</h3>
          <h4>Winner bid : {props.highestBid} RON</h4>
        </div>
      </div>
    </div>
  );
};

export default WonBid;
