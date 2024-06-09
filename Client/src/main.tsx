import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { router } from "./utils/Router";
import "./index.css";
import { LatestBidsRepositoryProvider } from "./context/LatestBidsRepositoryContext";
import { BidsRepositoryProvider } from "./context/BiddingRepositoryContext";
import { BidRepositoryProvider } from "./context/BidRepositoryContext";
import { CookiesProvider } from "react-cookie";
import { UserRepositoryProvider } from "./context/UserRepositoryContext";
import { AuthProvider } from "./context/AuthContext";
import "flowbite/dist/datepicker";
import { WinnerRepositoryProvider } from "./context/WinnerRepositoryContext";
import { PastBidsRepositoryProvider } from "./context/PastBidsRepositoryContext";
import { WinningsRepositoryProvider } from "./context/MyWinningsRepositoryContext";
ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <LatestBidsRepositoryProvider>
      <BidsRepositoryProvider>
        <CookiesProvider>
          <WinnerRepositoryProvider>
            <UserRepositoryProvider>
              <BidRepositoryProvider>
                <PastBidsRepositoryProvider>
                  <WinningsRepositoryProvider>
                    <AuthProvider>
                      <RouterProvider router={router} />
                    </AuthProvider>
                  </WinningsRepositoryProvider>
                </PastBidsRepositoryProvider>
              </BidRepositoryProvider>
            </UserRepositoryProvider>
          </WinnerRepositoryProvider>
        </CookiesProvider>
      </BidsRepositoryProvider>
    </LatestBidsRepositoryProvider>
  </React.StrictMode>
);
