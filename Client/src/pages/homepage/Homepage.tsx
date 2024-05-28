import { HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { useLatestBids } from "../../context/LatestBidsRepositoryContext";
import { BidModel } from "../../utils/types";
import BidCard from "./components/BidCard";

const Homepage = () => {
  const latestBidsRepo = useLatestBids();

  const [latestBids, setLatestBids] = useState<BidModel[]>();
  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7174/biddingHub")
      .build();

    connection.on("UpdateLatestBids", async () => {
      fetchBids();
    });
    connection.start().catch((err) => console.error("Connection error: ", err));
    return () => {
      connection.stop();
    };
  });

  useEffect(() => {
    fetchBids();
  }, []);
  const fetchBids = async () => {
    var response = await latestBidsRepo?.getMany();
    setLatestBids(response?.data);
  };

  return (
    <div className="mt-32">
      <div data-aos="fade-up">
        <h1 className="text-6xl text-center font-thin">Biddly</h1>
        <section
          className="text-center mt-4"
          data-aos="fade-up"
          data-aos-delay="200"
        >
          <p className="text-lg font-light">
            A premier platform for live and online bidding. <br /> Discover and
            bid on exclusive items from the comfort of your home.
          </p>
        </section>
        <section className="flex flex-row flex-wrap justify-center">
          {latestBids?.map((l) => {
            return (
              <BidCard
                name={l.bidName}
                startsAt={l.startsAt}
                highestBid={l.highestBid}
                bidId={l.bidId}
              />
            );
          })}
        </section>
      </div>
    </div>
  );
};

export default Homepage;
