import { createBrowserRouter } from "react-router-dom";
import Root from "../layout/Root";
import BidPage from "../pages/bidPage/BidPage";
import CreateBid from "../pages/create/CreateBid";
import Homepage from "../pages/homepage/Homepage";
import Authenticate from "../pages/authenticate/Authenticate";

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
        path: "authenticate",
        element: <Authenticate />,
      },
    ],
  },
]);
