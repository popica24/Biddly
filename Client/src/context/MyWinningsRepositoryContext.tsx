import { ReactNode, createContext, useContext } from "react";
import MyWinningsRepository from "../services/repos/myWinningsRepository";

export const WinningsRepositoryContext = createContext<
  MyWinningsRepository | undefined
>(undefined);

type ProviderProps = {
  children: ReactNode;
};

export const WinningsRepositoryProvider = ({ children }: ProviderProps) => {
  const winningsRepository = new MyWinningsRepository();

  return (
    <WinningsRepositoryContext.Provider value={winningsRepository}>
      {children}
    </WinningsRepositoryContext.Provider>
  );
};

export const useMyWinnings = () => {
  return useContext(WinningsRepositoryContext);
};
