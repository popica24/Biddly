import { HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { useLatestBids } from "../../context/LatestBidsRepositoryContext";
import { BidModel, PastBid } from "../../utils/types";
import BidCard from "./components/BidCard";
import { usePastBids } from "../../context/PastBidsRepositoryContext";
import PastBidCard from "./components/PastBidCard";

const Homepage = () => {
  const latestBidsRepo = useLatestBids();
  const pastBidsRepo = usePastBids();

  const [latestBids, setLatestBids] = useState<BidModel[]>();
  const [pastBids, setPastBids] = useState<PastBid[]>();
  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7174/biddingHub")
      .build();

    connection.on("UpdateLatestBids", async () => {
      fetchLatestBids();
      fetchPastBids();
    });
    connection.start().catch((err) => console.error("Connection error: ", err));
    return () => {
      connection.stop();
    };
  });

  useEffect(() => {
    fetchLatestBids();
    fetchPastBids();
  }, []);
  const fetchLatestBids = async () => {
    var response = await latestBidsRepo?.getMany();
    setLatestBids(response?.data);
  };
  const fetchPastBids = async () => {
    var response = await pastBidsRepo?.getMany();
    setPastBids(response?.data);
  };

  return (
    <div className="mt-32">
      <div data-aos="fade-up">
        <h1 className="text-6xl text-center font-thin">Biddly</h1>
        <section
          className="text-center my-4"
          data-aos="fade-up"
          data-aos-delay="200"
        >
          <p className="text-lg font-light">
            A premier platform for live and online bidding. <br /> Discover and
            bid on exclusive items from the comfort of your home.
          </p>
        </section>
        <span className="container px-28 font-thin text-4xl">Live bids</span>
        <section className="flex flex-row flex-wrap justify-center items-start">
          {latestBids?.map((l, i) => {
            return (
              <div data-aos="flip-left" data-aos-delay={`${100 + i * 200}`}>
                <BidCard {...l} />
              </div>
            );
          })}
        </section>
        <div className="mt-16">
          <span className="container px-28 font-thin text-4xl">Past bids</span>
          <section className="flex flex-row flex-wrap justify-center items-center">
            {pastBids?.map((l, i) => {
              return (
                <div
                  className="w-full max-w-[15rem]"
                  data-aos="flip-left"
                  data-aos-delay={`${100 + i * 200}`}
                >
                  <PastBidCard {...l} />
                </div>
              );
            })}
          </section>
        </div>
      </div>
    </div>
  );
};

export default Homepage;
