import { Link } from "react-router-dom";

type Props = {
  bidId: string;
  itemName: string;
  wonAt: string;
  highestBid: number;
  username: string;
};

const PastBidCard = (props: Props) => {
  return (
    <Link
      to={"/bid/" + props.bidId}
      className="w-full max-w-[15rem] bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 m-4"
    >
      <img
        className="p-8 rounded-t-lg aspect-[3/4]"
        src="https://media.richardmille.com/wp-content/uploads/2022/09/21141757/RM-88view.png?dpr=3&width=187.5"
        alt="product image"
      />

      <div className="px-5 pb-5">
        <h5 className="text-xl text-center font-semibold tracking-tight text-gray-900 dark:text-white">
          {props.itemName}
        </h5>
        <span className="font-thin text-sm">
          Won by {props.username} at {props.highestBid}RON
        </span>
      </div>
    </Link>
  );
};

export default PastBidCard;
