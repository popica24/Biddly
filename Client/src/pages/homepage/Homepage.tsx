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
    <div className="mt-28 container mx-auto xl:px-36">
      <div data-aos="fade-up">
        <h1 className="text-6xl text-center font-thin">Biddly</h1>
      </div>
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
      <h2
        className="mb-4 text-4xl font-thin text-gray-900 dark:text-white"
        data-aos="slide-right"
      >
        Live Bids
      </h2>
      <section className="flex xl:flex-row flex-col flex-wrap xl:justify-start justify-center items-center">
        {latestBids?.map((l, i) => {
          return (
            <div data-aos-delay={`${100 + i * 200}`} className="xl:me-12">
              <BidCard {...l} i={i} />
            </div>
          );
        })}
      </section>
      <div className="mt-16">
        <h2
          className="mb-4 text-4xl font-thin text-gray-900 dark:text-white"
          data-aos="slide-right"
        >
          Past Bids
        </h2>
        <section className="flex xl:flex-row flex-col flex-wrap xl:justify-start justify-center items-center">
          {pastBids?.map((l, i) => {
            return (
              <div
                data-aos="flip-left"
                data-aos-delay={`${100 + i * 200}`}
                className="xl:me-12"
              >
                <PastBidCard {...l} i={i}/>
              </div>
            );
          })}
        </section>
      </div>
    </div>
  );
};

export default Homepage;
