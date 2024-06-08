import { ReactNode, createContext, useContext } from "react";
import bidRepository from "../services/repos/bidRepository";
export const BidRepositoryContext = createContext<bidRepository | undefined>(
  undefined
);

type ProviderProps = {
  children: ReactNode;
};

export const BidRepositoryProvider = ({ children }: ProviderProps) => {
  const BidRepository = new bidRepository();

  return (
    <BidRepositoryContext.Provider value={BidRepository}>
      {children}
    </BidRepositoryContext.Provider>
  );
};

export const useBid = () => {
  return useContext(BidRepositoryContext);
};
