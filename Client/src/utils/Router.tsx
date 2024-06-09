import { createBrowserRouter } from "react-router-dom";
import Root from "../layout/Root";
import BidPage from "../pages/bidPage/BidPage";
import CreateBid from "../pages/create/CreateBid";
import Homepage from "../pages/homepage/Homepage";
import Authenticate from "../pages/authenticate/Authenticate";
import MyWinnings from "../pages/myWinnings/MyWinnings";
import Profile from "../pages/profile/Profile";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    children: [
      {
        index: true,
        element: <Homepage />,
      },
      {
        path: "bid/:itemName",
        element: <BidPage />,
      },
      {
        path: "create",
        element: <CreateBid />,
      },
      {
        path: "wins",
        element: <MyWinnings />,
      },
      {
        path: "authenticate",
        element: <Authenticate />,
      },
      {
        path: "profile",
        element: <Profile />,
      },
    ],
  },
]);
