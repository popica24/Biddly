import { CiTrophy } from "react-icons/ci";
import { Link } from "react-router-dom";

type Props = {
  bidId: string;
  itemName: string;
  wonAt: string;
  highestBid: number;
  username: string;
};

const PastBidCard = (props: Props) => {
  const date = new Date(props.wonAt);
  const readableDate = date.toLocaleString("en-US", {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
    hour12: false,
  });
  return (
    <Link
      to={"/bid/" + props.bidId}
      className="w-full max-w-[17rem] bg-black border border-gray-200 rounded-lg cursor-pointer"
    >
      <img
        width={300}
        className="p-8 rounded-t-lg aspect-[3/4]"
        src="https://image.harrods.com/cartier-large-yellow-gold-tank-louis-cartier-watch-25-5mm_18803691_42076547_2048.jpg"
        alt="product image"
      />

      <div className="px-5 pb-5">
        <h5 className="text-xl font-semibold tracking-tight text-gray-900 dark:text-white">
          {props.itemName}
        </h5>
        <div className="inline-flex items-center">
          <CiTrophy size={"16px"} /> <p className="ms-1">{props.username}</p>
        </div>
        <div className="flex items-center justify-between flex-col">
          <span className="font-light text-gray-600 w-full my-2">
            <span className="inline-flex justify-between w-full">
              Winning bid :
              <p className="ms-auto float-righ text-gray-800 font-medium">
                {props.highestBid}RON
              </p>
            </span>
          </span>
        </div>
        <span className="font-light text-gray-600 w-full">
          <div className="flex flex-col items-start">
            <span>Bid ended</span>
            {readableDate}
          </div>
        </span>
      </div>
    </Link>
  );
};

export default PastBidCard;
