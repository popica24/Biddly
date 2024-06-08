import { ReactNode, createContext, useContext } from "react";
import PastBidsRepository from "../services/repos/pastBidsRepository";

export const PastBidsRepositoryContext = createContext<
  PastBidsRepository | undefined
>(undefined);

type ProviderProps = {
  children: ReactNode;
};

export const PastBidsRepositoryProvider = ({ children }: ProviderProps) => {
  const pastBidsRepository = new PastBidsRepository();

  return (
    <PastBidsRepositoryContext.Provider value={pastBidsRepository}>
      {children}
    </PastBidsRepositoryContext.Provider>
  );
};

export const usePastBids = () => {
  return useContext(PastBidsRepositoryContext);
};
