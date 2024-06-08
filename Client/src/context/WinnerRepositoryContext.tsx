import { ReactNode, createContext, useContext } from "react";
import WinnerRepository from "../services/repos/winnerRepository";

export const WinnerRepositoryContext = createContext<
  WinnerRepository | undefined
>(undefined);

type ProviderProps = {
  children: ReactNode;
};

export const WinnerRepositoryProvider = ({ children }: ProviderProps) => {
  const winnerRepository = new WinnerRepository();

  return (
    <WinnerRepositoryContext.Provider value={winnerRepository}>
      {children}
    </WinnerRepositoryContext.Provider>
  );
};

export const useWinnerBids = () => {
  return useContext(WinnerRepositoryContext);
};
