import { ReactNode, createContext, useContext } from "react";
import bidRepository from "../services/repos/biddingRepository";
import biddingRepository from "../services/repos/biddingRepository";

export const BiddingRepositoryContext = createContext<
  bidRepository | undefined
>(undefined);

type ProviderProps = {
  children: ReactNode;
};

export const BidsRepositoryProvider = ({ children }: ProviderProps) => {
  const BiddingRepository = new biddingRepository();

  return (
    <BiddingRepositoryContext.Provider value={BiddingRepository}>
      {children}
    </BiddingRepositoryContext.Provider>
  );
};

export const useBidding = () => {
  return useContext(BiddingRepositoryContext);
};
