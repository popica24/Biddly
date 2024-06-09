import { useState } from "react";
import { CgProfile } from "react-icons/cg";
import { Link } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

const AccountManager = () => {
  const [accountOpen, setAccountOpen] = useState(false);
  const { logout } = useAuth();
  return (
    <div className="relative">
      <CgProfile
        size={"24px"}
        onMouseEnter={() => setAccountOpen(true)}
        onMouseLeave={() => setAccountOpen(false)}
      />
      {accountOpen && (
        <div
          className="absolute p-4 left-1/2 -translate-x-1/2"
          onMouseEnter={() => setAccountOpen(true)}
          onMouseLeave={() => setAccountOpen(false)}
        >
          <div className="bg-[#F2F2F2] p-4 rounded-xl shadow font-thin">
            <ul>
              <Link
                className="text-gray-400 hover:text-black transition-colors"
                to={"/profile"}
              >
                Settings
              </Link>
              <li
                className="cursor-pointer mt-2 text-gray-400 hover:text-black transition-colors"
                onClick={logout}
              >
                Log out
              </li>
            </ul>
          </div>
        </div>
      )}
    </div>
  );
};

export default AccountManager;
