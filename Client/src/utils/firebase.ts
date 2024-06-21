// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getStorage } from "firebase/storage";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyBLowR49xP8lTxpaf4r97uQQlpOKOhlYFg",
  authDomain: "biddly-76b18.firebaseapp.com",
  projectId: "biddly-76b18",
  storageBucket: "biddly-76b18.appspot.com",
  messagingSenderId: "682321011887",
  appId: "1:682321011887:web:e679eec35c6ddfef40205a"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
export const imageDb = getStorage(app);
export default app;