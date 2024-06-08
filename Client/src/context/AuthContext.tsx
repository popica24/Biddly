import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";
import { User } from "../utils/types";
import { useUsers } from "./UserRepositoryContext";

// Extend the context type to include loading
type AuthContextType = {
  user: User | undefined;
  loading: boolean;
};

export const AuthContext = createContext<AuthContextType | undefined>(
  undefined
);

type ProviderProps = {
  children: ReactNode;
};

export const AuthProvider = ({ children }: ProviderProps) => {
  const [user, setUser] = useState<User | any>(undefined);
  const [loading, setLoading] = useState(true);
  const userRepo = useUsers();

  const fetchUser = async () => {
    try {
      const result = await userRepo?.getMany();
      if (result && result.data) {
        setUser(result.data);
      } else {
        setUser(undefined);
      }
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUser();
    window.addEventListener("tokenChanged", fetchUser);
    return () => {
      window.removeEventListener("tokenChanged", fetchUser);
    };
  }, []);

  return (
    <AuthContext.Provider value={{ user: user, loading: loading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
