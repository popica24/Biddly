import { ReactNode, createContext, useContext } from "react";
import LatestBidsRepository from "../services/repos/latestBidsRepository";

export const LatestBidsRepositoryContext = createContext<
  LatestBidsRepository | undefined
>(undefined);

type ProviderProps = {
  children: ReactNode;
};

export const LatestBidsRepositoryProvider = ({ children }: ProviderProps) => {
  const latestBidsRepository = new LatestBidsRepository();

  return (
    <LatestBidsRepositoryContext.Provider value={latestBidsRepository}>
      {children}
    </LatestBidsRepositoryContext.Provider>
  );
};

export const useLatestBids = () => {
  return useContext(LatestBidsRepositoryContext);
};
