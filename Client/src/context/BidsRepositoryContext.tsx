import { ReactNode, createContext, useContext } from "react";
import bidRepository from "../services/repos/bidRepository";

export const BidsRepositoryContext = createContext<bidRepository | undefined>(
  undefined
);

type ProviderProps = {
  children: ReactNode;
};

export const BidsRepositoryProvider = ({ children }: ProviderProps) => {
  const bidsRepository = new bidRepository();

  return (
    <BidsRepositoryContext.Provider value={bidsRepository}>
      {children}
    </BidsRepositoryContext.Provider>
  );
};

export const useBids = () => {
  return useContext(BidsRepositoryContext);
};
