import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { BidModel } from "../../utils/types";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { Button } from "flowbite-react";
import Counter from "./components/Counter";
import { useBid } from "../../context/BidRepositoryContext";
import { useBidding } from "../../context/BiddingRepositoryContext";
import { useAuth } from "../../context/AuthContext";
import LoadingOverlay from "./components/LoadingOverlay";
import { useWinnerBids } from "../../context/WinnerRepositoryContext";

const BidPage = () => {
  const [error, setError] = useState(false);
  const [bid, setBid] = useState<BidModel>();
  const [winner, setWinner] = useState<string | undefined>(undefined);
  const [bidValue, setBidValue] = useState<number | undefined>(undefined);
  const [timerExpired, setTimerExpired] = useState(false);
  const bidParams = useParams();
  const bidRepo = useBid();
  const biddingRepo = useBidding();
  const winnerRepo = useWinnerBids();
  const { user } = useAuth();
  const bidId = bidParams.itemName;

  if (!bidId) {
    return <>Error</>;
  }

  const fetchBid = async (id: string) => {
    try {
      var response = await biddingRepo?.get(id);
      var bid = response?.data as BidModel;
      setBid(bid);
      if (bid.highestBid !== undefined && bid.startingFrom !== undefined) {
        setBidValue(Math.max(bid.highestBid, bid.startingFrom));
      } else {
        setBidValue(response?.data?.startingFrom);
      }
    } catch (err: any) {
      setError(true);
    }
  };

  const fetchWinner = async (id: string) => {
    var response = await winnerRepo?.get(id);
    if (response?.data) setWinner(response?.data);
    else setWinner("A winner was not chosed");
  };

  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7174/biddingHub")
      .build();

    connection.on("UpdateHighestBid", async () => {
      fetchBid(bidId);
    });
    connection.on("AnnounceWinner", async () => {
      fetchWinner(bidId);
    });
    connection.start().catch((err) => console.error("Connection error: ", err));
    return () => {
      connection.stop();
    };
  }, [bidId]);

  useEffect(() => {
    fetchBid(bidId);
  }, [bidId]);

  const handleBidPlace = async () => {
    if (!user) {
      return;
    }
    const bidderId = user.id;
    if (bidValue !== undefined) {
      await bidRepo?.create({ bidId, bidderId, ammount: bidValue });
    }
  };

  const handleIncreaseBidValue = () => {
    if (bidValue !== undefined) {
      setBidValue(bidValue + 5);
    }
  };

  const handleDecreaseBidValue = () => {
    if (bidValue !== undefined) {
      setBidValue(bidValue - 5);
    }
  };

  if (error) {
    return (
      <div className="flex flex-col justify-center  mt-auto items-center w-full">
        <h1 className="text-3xl font-thin">
          This bid is not available or has been moved
        </h1>
        <img src="/no-content.gif" alt="" width={500} />
      </div>
    );
  }

  if (bid?.username) {
    return (
      <>
        <div className="flex justify-center mt-[68.8px] w-full">
          <div className="flex flex-row items-center justify-center">
            <img
              width={400}
              className="aspect-[3/4]"
              src="https://media.richardmille.com/wp-content/uploads/2022/09/21141757/RM-88view.png?dpr=3&width=187.5"
              alt=""
            />
            <div className="flex flex-col items-start">
              <h1 className="text-4xl font-thin my-8">{bid?.itemName}</h1>
              <h2>Winner : {bid.username}</h2>
            </div>
          </div>
        </div>
      </>
    );
  }

  return (
    <>
      {timerExpired && <LoadingOverlay winner={winner} />}
      <div className="flex justify-center mt-[68.8px] w-full">
        <div className="flex flex-col items-center justify-center">
          <img
            width={400}
            className="aspect-[3/4]"
            src="https://media.richardmille.com/wp-content/uploads/2022/09/21141757/RM-88view.png?dpr=3&width=187.5"
            alt=""
          />
          <h1 className="text-4xl font-thin my-8">{bid?.itemName}</h1>
        </div>
      </div>
      <div className="container grid grid-cols-2 mx-auto place-content-center ">
        <div className="col-span-1">
          <div className="flex flex-row items-center justify-center">
            <Button
              className="text-black bg-[#ffed4b]"
              onClick={handleBidPlace}
            >
              Post Bid
            </Button>
          </div>
        </div>
        <div className="col-span-1">
          <div className="flex flex-col justify-center">
            <span className="text-center">
              {timerExpired ? "Bid Ended" : "Bid Ends in:"}
            </span>
            {bid && (
              <Counter
                timerExpired={timerExpired}
                setTimerExpired={setTimerExpired}
                endsAt={bid.wonAt}
              />
            )}

            <div className="border-b-2 border-t-2 px-44 my-6">
              <div className="flex flex-row my-4 text-[#8d8d8d]">
                <span className="float-left">Starting Bid</span>
                <span className="ms-auto float-right">
                  {bid?.startingFrom}RON
                </span>
              </div>
              <div className="flex flex-row mb-4 font-medium">
                <span className="float-left">Current Bid</span>
                <span className="ms-auto float-right">
                  {Math.max(bid?.highestBid!, bid?.startingFrom!)}RON
                </span>
              </div>
            </div>
            {bid?.createdBy !== user?.id ? (
              <div className="flex flex-row items-stretch justify-center">
                <span className="border border-[#114D58] flex items-center justify-evenly w-1/3 rounded-md me-4">
                  <button onClick={handleIncreaseBidValue}>+</button>
                  <span>{bidValue} RON</span>
                  <button onClick={handleDecreaseBidValue}>-</button>
                </span>
                <Button
                  className="text-white bg-[#114D58]"
                  onClick={handleBidPlace}
                >
                  Post Bid
                </Button>
              </div>
            ) : (
              <></>
            )}
          </div>
        </div>
      </div>
    </>
  );
};

export default BidPage;
