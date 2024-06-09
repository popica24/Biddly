import { useEffect, useState } from "react";
import { CiClock2, CiNoWaitingSign } from "react-icons/ci";
import { Link } from "react-router-dom";

type Props = {
  i: number;
  bidId: string;
  itemName: string;
  startingAt: string;
  highestBid: number;
  startingFrom: number;
  wonAt: string;
};

const imgLib = [
  "https://washingtonwatchgroup.com/wp-content/uploads/2023/04/m126506-0001_collection_upright_portrait.jpg",
  "https://media.richardmille.com/wp-content/uploads/2022/09/21141757/RM-88view.png?dpr=3&width=187.5",
  "https://www.audemarspiguet.com/content/dam/ap/com/products/watches/MTR010402AA/importer/watch.png.transform.appdpmain.png",
];

const BidCard = (props: Props) => {
  const calculateTimeLeft = () => {
    const startDate = new Date(props.startingAt).getTime();

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
  }, [props.startingAt]);
  const date = new Date(props.wonAt);
  const readableDate = date.toLocaleString("en-US", {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "numeric",
    minute: "numeric",
    hour12: false,
  });

  return (
    <Link
      to={timerExpired ? `${"/bid/" + props.bidId}` : "#"}
      className={`bg-white border border-gray-200 rounded-lg ${
        !timerExpired ? "cursor-not-allowed" : "cursor-pointer"
      }`}
    >
      <img
        width={300}
        className="mx-auto p-8 rounded-t-lg aspect-[3/4]"
        src={imgLib[props.i]}
        alt="product image"
      />

      <div className="px-5 pb-5">
        <h5 className="text-xl font-semibold tracking-tight text-gray-900 dark:text-white">
          {props.itemName}
        </h5>
        <div className="inline-flex items-center">
          {timerExpired ? (
            <>
              <CiClock2 size={"16px"} /> <p className="ms-1">Live</p>
            </>
          ) : (
            <>
              <CiNoWaitingSign />
              <p className="ms-1">Timed</p>
            </>
          )}
        </div>
        <div className="flex items-center justify-between flex-col">
          <span className="font-light text-gray-600 w-full my-2">
            {timerExpired && props.highestBid !== 0 ? (
              <span className="inline-flex justify-between w-full">
                Highest bid :
                <p className="ms-auto float-righ text-gray-800 font-medium">
                  {props.highestBid}RON
                </p>
              </span>
            ) : (
              <span className="inline-flex justify-between w-full">
                Opening bid :
                <p className="ms-auto float-righ text-gray-800 font-medium">
                  {props.startingFrom}RON
                </p>
              </span>
            )}
          </span>
        </div>
        {!timerExpired ? (
          <div className="text-sm font-thin">
            <span>Bid starts in</span>
            <div>
              <span>{timeLeft.hours || "00"} hours </span>
              <span>{timeLeft.minutes || "00"} minutes </span>
              <span>{timeLeft.seconds || "00"} seconds</span>
            </div>
          </div>
        ) : (
          <span className="font-light text-gray-600 w-full">
            <div className="flex flex-col items-start">
              <span>Bid ends</span>
              {readableDate}
            </div>
          </span>
        )}
      </div>
    </Link>
  );
};

export default BidCard;
