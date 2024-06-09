import { useEffect, useState } from "react";
import { useAuth } from "../../context/AuthContext";

const Profile = () => {
  const { user, changeUsername, changePassword } = useAuth();
  const [username, setUsername] = useState("");
  const [newUsername, setNewUsername] = useState("");
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");

  useEffect(() => {
    if (!user) return;
    setUsername(user.username);
    setNewUsername(user.username);
  }, [user]);

  const handleUsernameChange = (e: any) => {
    setUsername(e.target.value);
  };
  const handleOldPasswordChange = (e: any) => {
    setOldPassword(e.target.value);
  };
  const handleNewPasswordChange = (e: any) => {
    setNewPassword(e.target.value);
  };
  const confirmChanges = async () => {
    if (!username) return;
    if (
      username !== user?.username &&
      username !== "" &&
      username?.length > 4
    ) {
      var newUsername = await changeUsername(username);
      setUsername(newUsername);
      setNewUsername(newUsername);
    }
    if (oldPassword != "" && newPassword != "") {
      await changePassword(oldPassword, newPassword);
    }
  };
  return (
    <div className="flex flex-col justify-center mt-[68.8px] w-full container xl:px-36">
      <h1 data-aos="slide-right" className="text-4xl font-thin my-8">
        Hello, {newUsername}
      </h1>
      <div className="max-w-[500px] w-full mx-auto flex flex-col">
        <label htmlFor="username">Username</label>
        <input
          id="username"
          type="text"
          value={username}
          onChange={handleUsernameChange}
        />
        <div className="flex flex-col mt-8">
          <label htmlFor="oldPassword">Old Password</label>
          <input
            value={oldPassword}
            id="oldPassword"
            type="password"
            placeholder="&#9679;&#9679;&#9679;&#9679;&#9679;"
            onChange={handleOldPasswordChange}
          />
        </div>
        <label htmlFor="newPassword">New Password</label>
        <input
          id="newPassword"
          type="password"
          value={newPassword}
          placeholder="&#9679;&#9679;&#9679;&#9679;&#9679;"
          onChange={handleNewPasswordChange}
        />
      </div>
      <button
        onClick={confirmChanges}
        type="submit"
        className="bg-[#202936] hover:bg-[#384150] inline-flex items-center px-5 py-2.5 mt-4 sm:mt-6 text-sm font-medium text-center text-white bg-primary-700 rounded-lg focus:ring-4 focus:ring-primary-200 dark:focus:ring-primary-900 hover:bg-primary-800 w-full xl:w-fit max-w-[290px] xl:max-w-none"
      >
        Confirm update
      </button>
    </div>
  );
};

export default Profile;
