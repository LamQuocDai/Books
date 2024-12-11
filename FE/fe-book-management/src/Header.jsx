import { useLocation, useNavigate } from "react-router-dom";



function Header() {

  const location = useLocation();
  const {userLogin} = location.state || {};
  const navigate = useNavigate();

  const actionButton = () =>{
    navigate("/");
  }

  return (
    <header className="w-full h-16 bg-yellow-300">
      <h1>Hello, {userLogin.name}</h1>
      <button className="bg-red-400 rounded-md w-20 mt-2" onClick={actionButton}>Logout</button>
    </header>
  );
}

export default Header;
