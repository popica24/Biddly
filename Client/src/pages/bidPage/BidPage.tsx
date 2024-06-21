import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { BidModel } from "../../utils/types";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useBid } from "../../context/BidRepositoryContext";
import { useBidding } from "../../context/BiddingRepositoryContext";
import { useAuth } from "../../context/AuthContext";
import LoadingOverlay from "./components/LoadingOverlay";
import { useWinnerBids } from "../../context/WinnerRepositoryContext";
import ErrorScreen from "./components/ErrorScreen";
import WonBid from "./components/WonBid";
import { imageDb } from "../../utils/firebase";
import { getDownloadURL, ref } from "firebase/storage";

const BidPage = () => {
  const [bid, setBid] = useState<BidModel>();
  const [image, setImage] = useState("");
  const [winner, setWinner] = useState<string | undefined>(undefined);
  const [bidValue, setBidValue] = useState<number | undefined>(undefined);
  const [timerExpired, setTimerExpired] = useState(false);
  const [error, setError] = useState(false);
  const bidParams = useParams();
  const bidRepo = useBid();
  const biddingRepo = useBidding();
  const winnerRepo = useWinnerBids();
  const { user } = useAuth();
  const bidId = bidParams.itemName;

  if (!bidId) {
    return <ErrorScreen />;
  }
  useEffect(() => {
    if (!bidId) return;
    const imagePath = ref(imageDb, `${bidId}/hero.jpg`);
    getDownloadURL(imagePath).then((url) => setImage(url));
  }, [bidId]);

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

  const handleBidPlace = async () => {
    if (!user) {
      return;
    }
    const bidderId = user.id;
    if (bidValue !== undefined) {
      await bidRepo?.create({
        bidId,
        bidderId,
        ammount: bidValue,
      });
    }
  };

  useEffect(() => {
    fetchBid(bidId);
  }, [bidId]);

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
    if (!bid) return;
    const endAt = new Date(bid?.wonAt).getTime();
    const now = new Date().getTime();
    const difference = endAt - now;

    const timer = setTimeout(() => {
      setTimerExpired(true);
    }, difference);

    return () => clearTimeout(timer);
  }, [bid]);

  if (error || !bid) {
    return <ErrorScreen />;
  }

  if (bid.username) {
    return <WonBid {...bid} image={image} />;
  }
  const date = new Date(bid.wonAt);
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
    <>
      {timerExpired && <LoadingOverlay winner={winner} />}

      <div className="flex flex-col justify-center mt-[68.8px] w-full container xl:px-36">
        <h1 data-aos="slide-right" className="text-4xl font-thin my-8">
          {bid?.itemName}
        </h1>

        <div className="xl:grid flex flex-col grid-cols-2 place-items-center">
          <div className="col-span-1">
            <img
              width={400}
              className="aspect-[3/4] rounded-xl shadow-lg"
              src={image}
              alt=""
            />
          </div>
          <div className="col-span-1">
            <div className="flex flex-col justify-start items-start xl:px-32 py-4 text-nowrap">
              <div className="text-start">
                {bid?.highestBid > 0 ? (
                  <span className="inline-flex items-center">
                    <p className="font-medium text-[#666]">Current bid </p>
                    <p className=" ms-4 text-xl font-bold">
                      {bid?.highestBid} RON
                    </p>
                  </span>
                ) : (
                  <span className="inline-flex items-center">
                    <p className="font-medium text-[#666]">Opening bid </p>
                    <p className=" ms-4 text-xl font-bold">
                      {bid?.startingFrom} RON
                    </p>
                  </span>
                )}
                {bid?.createdBy !== user?.id && (
                  <div className="flex flex-row items-center my-5">
                    <p className="font-medium text-[#666]">Your bid </p>
                    <div className="bg-white inline-flex items-center border hover:border-black transition-colors ms-2">
                      <input
                        value={bidValue}
                        onChange={(e) => setBidValue(Number(e.target.value))}
                        type="number"
                        placeholder="Enter your bid"
                        className="h-[30px] w-[150px] xl:w-[220px] border-0 active:outline-none active:border-0"
                      />

                      <span className="font-bold pe-2">RON</span>
                    </div>
                    <button
                      className="bg-blue-400 hover:bg-blue-500 transition-colors h-[30px] px-4 text-white"
                      onClick={handleBidPlace}
                    >
                      Place Bid
                    </button>
                  </div>
                )}
                {bid?.createdBy !== user?.id && (
                  <span className="text-center">
                    (bid {Math.max(bid.startingFrom, bid.highestBid) + 10} or
                    more)
                  </span>
                )}
              </div>
              <span className="inline-flex items-start">
                Timed auction:{" "}
                <p className="ms-4 flex flex-col items-start">
                  <span className="font-medium">Bidding ends:</span>
                  <span className="text-red-600 font-medium">
                    {readableDate}
                  </span>
                </p>
              </span>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default BidPage;
