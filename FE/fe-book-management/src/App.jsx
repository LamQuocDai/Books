import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./LoginPage";
import HomePage from "./HomePage";
import UserPage from "./UserPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/admin" element={<HomePage />} />
        <Route path="/" element={<LoginPage />} />
        <Route path="/home" element={<UserPage />} />
      </Routes>
    </Router>
  );
}

export default App;
