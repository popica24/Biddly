import { useParams } from "react-router-dom";
import { useBids } from "../../context/BidsRepositoryContext";
import { useEffect, useState } from "react";
import { BidModel } from "../../utils/types";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useBidding } from "../../context/BidRepositoryContext";
import { Button } from "flowbite-react";
import Counter from "./components/Counter";

const BidPage = () => {
  const [bid, setBid] = useState<BidModel>();
  const [bidValue, setBidValue] = useState<number | undefined>(undefined);
  const bidParams = useParams();
  const bidRepo = useBids();
  const biddingRepo = useBidding();
  const bidId = bidParams.itemName;

  if (!bidId) {
    return <>Error</>;
  }

  const fetchBid = async (id: string) => {
    var response = await bidRepo?.get(id);
    setBid(response?.data);
    if (
      response?.data?.highestBid !== undefined &&
      response.data.startsFrom !== undefined
    ) {
      setBidValue(Math.max(response.data.highestBid, response.data.startsFrom));
    } else {
      setBidValue(response?.data?.startsFrom);
    }
  };

  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7174/biddingHub")
      .build();

    connection.on("UpdateHighestBid", async () => {
      fetchBid(bidId);
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
    if (bidValue !== undefined) {
      await biddingRepo?.create({ bidId, ammount: bidValue });
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

  console.log(bidValue);

  return (
    <>
      <div className="flex justify-center mt-[68.8px] w-full">
        <div className="flex flex-col items-center justify-center">
          <img
            width={400}
            className="aspect-[3/4]"
            src="https://media.richardmille.com/wp-content/uploads/2022/09/21141757/RM-88view.png?dpr=3&width=187.5"
            alt=""
          />
          <h1 className="text-4xl font-thin my-8">{bid?.bidName}</h1>
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
            <span className="text-center">Bid Ends in:</span>
            {bid && <Counter endsAt={bid.endsAt} />}

            <div className="border-b-2 border-t-2 px-44 my-6">
              <div className="flex flex-row my-4 text-[#8d8d8d]">
                <span className="float-left">Starting Bid</span>
                <span className="ms-auto float-right">
                  {bid?.startsFrom}RON
                </span>
              </div>
              <div className="flex flex-row mb-4 font-medium">
                <span className="float-left">Current Bid</span>
                <span className="ms-auto float-right">
                  {Math.max(bid?.highestBid!, bid?.startsFrom!)}RON
                </span>
              </div>
            </div>
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
          </div>
        </div>
      </div>
    </>
  );
};

export default BidPage;
