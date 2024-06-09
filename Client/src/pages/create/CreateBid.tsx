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
    <section className="bg-white mt-28 container mx-auto xl:px-36">
      <h2
        className="mb-4 text-4xl font-thin text-gray-900 dark:text-white xl:text-start text-center"
        data-aos="slide-right"
      >
        Schedule a bid
      </h2>
      <form onSubmit={(e) => handleSubmit(e)}>
        <div className="flex flex-col max-w-4xl mx-auto">
          <div className="w-full max-w-[290px] xl:max-w-none mx-auto">
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
          <div className="w-full max-w-[290px] xl:max-w-none mx-auto">
            <label
              className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              htmlFor="file_input"
            >
              Product Picture
            </label>
            <input
              className="block w-full text-sm text-gray-400 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-400 dark:border-gray-400 dark:placeholder-gray-400"
              id="file_input"
              type="file"
            />
          </div>
          <div className="flex  flex-col xl:flex-row items-center justify-between w-full">
            <div className="w-full max-w-[290px] xl:max-w-none">
              <label
                htmlFor="price"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Opening bid
              </label>
              <input
                value={startingFrom}
                onChange={(e) => setStartingFrom(e.target.value)}
                type="number"
                name="price"
                id="price"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                placeholder="2999RON"
                required
              />
            </div>
            <DateTimePickerComponent
              dateSetter={setStartDate}
              timeSetter={setStartTime}
              text="Starting At"
            />
            <DateTimePickerComponent
              dateSetter={setEndDate}
              timeSetter={setEndTime}
              text="Ending At"
            />
          </div>
        </div>

        <div className=" w-full flex items-center justify-center xl:justify-start">
          <button
            type="submit"
            className="bg-[#202936] hover:bg-[#384150] inline-flex items-center px-5 py-2.5 mt-4 sm:mt-6 text-sm font-medium text-center text-white bg-primary-700 rounded-lg focus:ring-4 focus:ring-primary-200 dark:focus:ring-primary-900 hover:bg-primary-800 w-full xl:w-fit max-w-[290px] xl:max-w-none"
          >
            Add product
          </button>
        </div>
      </form>
    </section>
  );
};

export default CreateBid;
