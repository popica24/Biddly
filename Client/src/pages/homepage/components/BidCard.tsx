import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

type Props = {
  bidId: string;
  name: string;
  startsAt: string;
  highestBid: number;
};

const BidCard = (props: Props) => {
  const calculateTimeLeft = () => {
    const startDate = new Date(props.startsAt).getTime();
    const currentTime = new Date().getTime();
    const difference = startDate - currentTime;
    let timeLeft = {
      hours: 0,
      minutes: 0,
      seconds: 0,
    };

    if (difference > 0) {
      timeLeft = {
        hours: Math.floor(difference / (1000 * 60 * 60)),
        minutes: Math.floor((difference / 1000 / 60) % 60),
        seconds: Math.floor((difference / 1000) % 60),
      };
    }

    return timeLeft;
  };

  const [timeLeft, setTimeLeft] = useState(calculateTimeLeft());
  const [timerExpired, setTimerExpired] = useState(false);

  useEffect(() => {
    const timer = setInterval(() => {
      const newTimeLeft = calculateTimeLeft();
      setTimeLeft(newTimeLeft);

      if (
        newTimeLeft.hours === 0 &&
        newTimeLeft.minutes === 0 &&
        newTimeLeft.seconds === 0
      ) {
        setTimerExpired(true);
        clearInterval(timer);
      }
    }, 1000);

    // Check if timer should be expired immediately on component mount/update
    if (
      timeLeft.hours === 0 &&
      timeLeft.minutes === 0 &&
      timeLeft.seconds === 0
    ) {
      setTimerExpired(true);
    }

    return () => clearInterval(timer);
  }, [props.startsAt]);
  return (
    <div className="w-full max-w-xs bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 m-4">
      <img
        width={300}
        className={`p-8 rounded-t-lg aspect-[3/4] ${
          !timerExpired ? "cursor-not-allowed" : "cursor-pointer"
        }`}
        src="https://media.richardmille.com/wp-content/uploads/2022/09/21141757/RM-88view.png?dpr=3&width=187.5"
        alt="product image"
      />

      <div className="px-5 pb-5">
        <h5 className="text-xl font-semibold tracking-tight text-gray-900 dark:text-white">
          {props.name}
        </h5>
        <div className="flex items-center justify-between">
          <span className="text-xl font-bold text-gray-900 dark:text-white">
            {timerExpired && props.highestBid !== 0 && (
              <span>Highest bid : {props.highestBid}</span>
            )}
          </span>
        </div>
        {!timerExpired ? (
          <div className="text-sm font-thin">
            <span>{timeLeft.hours || "00"} hours </span>
            <span>{timeLeft.minutes || "00"} minutes </span>
            <span>{timeLeft.seconds || "00"} seconds</span>
          </div>
        ) : (
          <div className="my-6 flex items-center flex-row justify-evenly">
            <Link
              to={"/bid/" + props.bidId}
              className=" text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
            >
              Join Bidding !
            </Link>
            <a
              href="#"
              className=" text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
            >
              Quick Bid
            </a>
          </div>
        )}
      </div>
    </div>
  );
};

export default BidCard;
