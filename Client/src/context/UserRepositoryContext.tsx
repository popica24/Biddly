import { ReactNode, createContext, useContext } from "react";
import UserRepository from "../services/repos/userRepository";

export const UserRepositoryContext = createContext<UserRepository | undefined>(
  undefined
);

type ProviderProps = {
  children: ReactNode;
};

export const UserRepositoryProvider = ({ children }: ProviderProps) => {
  const userRepository = new UserRepository();

  return (
    <UserRepositoryContext.Provider value={userRepository}>
      {children}
    </UserRepositoryContext.Provider>
  );
};

export const useUsers = () => {
  return useContext(UserRepositoryContext);
};
