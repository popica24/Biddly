import axios from "axios";

const authService = (() => {
  const BASE_URL = import.meta.env.VITE_API_LOCAL_URL;

  const Register = async (username:string, email:string, password:string) => {
    const data = {
      username,
      email,
      password,
    };
    await axios
      .post(`${BASE_URL}/auth?authType=register`, data, {
        withCredentials: true,
      })
      .then((response) => localStorage.setItem("token", response.data));
  };

  const Login = async (email:string, password:string) => {
    const data = {
      email,
      password,
    };
    await axios
      .post(`${BASE_URL}/auth?authType=credential`, data, {
        withCredentials: true,
      })
      .then((response) => localStorage.setItem("token", response.data));
  };

  return {
    Login,
    Register,
  };
})();

export default authService;
