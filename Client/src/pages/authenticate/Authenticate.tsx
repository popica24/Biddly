import { useState } from "react";
import Login from "./components/Login";
import Register from "./components/Register";

const Authenticate = () => {
  const [panel, setPanel] = useState(1);
  if (panel == 1) {
    return <Login setPanel={setPanel} />;
  }
  if (panel == 2) {
    return <Register setPanel={setPanel} />;
  }
  return <div>Authenticate</div>;
};

export default Authenticate;
