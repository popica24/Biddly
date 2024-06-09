import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";
import { User } from "../utils/types";
import { useUsers } from "./UserRepositoryContext";
import LoadingScreen from "../components/LoadingScreen";
import axios from "axios";
const BASE_URL = import.meta.env.VITE_API_LOCAL_URL + "/auth/";
type AuthContextType = {
  user: User | undefined;
  loading: boolean;
  logout: () => void;
  changeUsername: (username: string) => Promise<string>;
  changePassword: (oldPassword: string, newPassword: string) => void;
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

  const logout = async () => {
    setLoading(true);
    const token = localStorage.getItem("token");
    await axios
      .post(
        "https://localhost:7174/api/logout",
        {},
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
          withCredentials: true,
        }
      )
      .then(() => {
        localStorage.removeItem("token");
        setTimeout(() => {
          window.location.href = "/";
          setLoading(false);
        }, 500);
      });
  };

  const changeUsername = async (username: string): Promise<string> => {
    if (!user) return "";
    const response = await axios.post(BASE_URL + user.id, null, {
      params: {
        username: username,
      },
    });

    return response.data as string;
  };
  const changePassword = async (oldPassword: string, newPassword: string) => {
    if (!user) return "";
    const response = await axios.put(BASE_URL + user.id, null, {
      params: {
        oldPassword,
        newPassword,
      },
    });

    return response.data as string;
  };

  useEffect(() => {
    fetchUser();
    window.addEventListener("tokenChanged", fetchUser);
    return () => {
      window.removeEventListener("tokenChanged", fetchUser);
    };
  }, []);

  return (
    <AuthContext.Provider
      value={{
        user: user,
        loading: loading,
        logout: logout,
        changeUsername,
        changePassword,
      }}
    >
      {children}
      {loading && <LoadingScreen />}
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
