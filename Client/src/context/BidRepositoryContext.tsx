import { ReactNode, createContext, useContext } from "react";
import biddingRepository from "../services/repos/biddingRepository";

export const BidRepositoryContext = createContext<
  biddingRepository | undefined
>(undefined);

type ProviderProps = {
  children: ReactNode;
};

export const BidRepositoryProvider = ({ children }: ProviderProps) => {
  const bidsRepository = new biddingRepository();

  return (
    <BidRepositoryContext.Provider value={bidsRepository}>
      {children}
    </BidRepositoryContext.Provider>
  );
};

export const useBidding = () => {
  return useContext(BidRepositoryContext);
};
