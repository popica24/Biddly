import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { router } from "./utils/Router";
import "./index.css";
import { LatestBidsRepositoryProvider } from "./context/LatestBidsRepositoryContext";
import { BidsRepositoryProvider } from "./context/BidsRepositoryContext";
import { BidRepositoryProvider } from "./context/BidRepositoryContext";
ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <LatestBidsRepositoryProvider>
      <BidsRepositoryProvider>
        <BidRepositoryProvider>
          <RouterProvider router={router} />
        </BidRepositoryProvider>
      </BidsRepositoryProvider>
    </LatestBidsRepositoryProvider>
  </React.StrictMode>
);
