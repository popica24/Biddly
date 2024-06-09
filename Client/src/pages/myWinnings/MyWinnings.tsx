import { useEffect, useState } from "react";
import { PastBid } from "../../utils/types";
import { useMyWinnings } from "../../context/MyWinningsRepositoryContext";
import PastBidCard from "../homepage/components/PastBidCard";

const MyWinnings = () => {
  const [winnings, setWinnings] = useState<Array<PastBid>>();
  const winningsRepo = useMyWinnings();
  useEffect(() => {
    fetchWins();
  }, []);
  const fetchWins = async () => {
    var response = await winningsRepo?.getMany();
    setWinnings(response?.data);
  };
  return (
    <div className="mt-28 container mx-auto px-36">
      <h2
        className="mb-4 text-4xl font-thin text-gray-900 dark:text-white"
        data-aos="slide-right"
      >
        My Winnings
      </h2>
      <section className="flex flex-row flex-wrap justify-start items-center">
        {winnings?.map((l, i) => {
          return (
            <div
              data-aos="flip-left"
              data-aos-delay={`${100 + i * 200}`}
              className="me-12"
            >
              <PastBidCard {...l} />
            </div>
          );
        })}
      </section>
    </div>
  );
};

export default MyWinnings;
