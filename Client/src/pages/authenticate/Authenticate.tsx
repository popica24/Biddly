import React, { useState } from "react";
import Login from "./components/Login";

const Authenticate = () => {
  const [panel, setPanel] = useState(1);
  if (panel == 1) {
    return <Login />;
  }
  return <div>Authenticate</div>;
};

export default Authenticate;
