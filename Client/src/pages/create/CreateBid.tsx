import { useState } from "react";
import DateTimePickerComponent from "./components/DateTimePickerComponent";
import { CreateBidRequest } from "../../utils/types";
import { useAuth } from "../../context/AuthContext";
import { useBidding } from "../../context/BiddingRepositoryContext";

const CreateBid = () => {
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");

  const [startingFrom, setStartingFrom] = useState("");

  const [itemName, setItemName] = useState("");

  const { user } = useAuth();
  const bidRepo = useBidding();

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(user);

    const startingFromNumber = Number(startingFrom);
    if (!user) return;
    const bid: CreateBidRequest = {
      createdBy: user?.id,
      startingFrom: startingFromNumber,
      itemName,
      startingAt: `${startDate} ${startTime}`,
      wonAt: `${endDate} ${endTime}`,
    };
    bidRepo?.create(bid);
  };

  return (
    <section className="bg-white dark:bg-gray-900">
      <div className="py-8 px-4 mx-auto max-w-2xl lg:py-16">
        <h2 className="mb-4 text-xl font-bold text-gray-900 dark:text-white">
          Schedule a bid
        </h2>
        <form onSubmit={(e) => handleSubmit(e)}>
          <div className="grid gap-4 sm:grid-cols-2 sm:gap-6">
            <div className="sm:col-span-2">
              <label
                htmlFor="name"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Item Name
              </label>
              <input
                value={itemName}
                onChange={(e) => setItemName(e.target.value)}
                type="text"
                name="name"
                id="name"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                placeholder="Type product name"
                required
              />
            </div>

            <div className="w-full">
              <DateTimePickerComponent
                dateSetter={setStartDate}
                timeSetter={setStartTime}
                text="Starting At"
              />
            </div>
            <div className="w-full">
              <DateTimePickerComponent
                dateSetter={setEndDate}
                timeSetter={setEndTime}
                text="Ending At"
              />
            </div>
          </div>
          <div className="w-full">
            <label
              htmlFor="price"
              className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
            >
              Starting Price
            </label>
            <input
              value={startingFrom}
              onChange={(e) => setStartingFrom(e.target.value)}
              type="number"
              name="price"
              id="price"
              className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
              placeholder="$2999"
              required
            />
          </div>
          <button
            type="submit"
            className="bg-blue-700 hover:bg-blue-800 inline-flex items-center px-5 py-2.5 mt-4 sm:mt-6 text-sm font-medium text-center text-white bg-primary-700 rounded-lg focus:ring-4 focus:ring-primary-200 dark:focus:ring-primary-900 hover:bg-primary-800"
          >
            Add product
          </button>
        </form>
      </div>
    </section>
  );
};

export default CreateBid;
