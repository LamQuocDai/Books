import { useNavigate } from "react-router-dom";

function LoginPage() {
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const email = e.target.elements.email.value;
    const password = e.target.elements.password.value;

    try {
      const response = await fetch("https://localhost:7109/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });

      const data = await response.json();
      if (response.ok) {
        if(data.role === "Admin"){
          navigate("/admin", { state: { userLogin: data } });
        }else{
          navigate("/home", { state: { userLogin: data } });
        }
      } else {
        alert("Login failed");
      }
    } catch (error) {
      alert("Email hoặc mật khẩu không đúng");
      console.error(error);
    }
  };

  return (
    <div className="w-screen h-screen flex items-center justify-center">
      <form
        onSubmit={handleSubmit}
        className="flex flex-col items-center border-2 p-10 rounded-3xl h-96 w-96"
      >
        <h1 className="text-center text-5xl">Login</h1>
        <input
          className="p-2 m-2 mt-6 w-80 border-2"
          type="text"
          placeholder="Email"
          name="email"
        />
        <input
          className="p-2 m-2 mt-6 w-80 border-2"
          name="password"
          type="password"
          placeholder="Password"
        />
        <button
          className="m-4 bg-blue-500 h-10 w-36 rounded-3xl mt-10"
          type="submit"
        >
          Login
        </button>
      </form>
    </div>
  );
}

export default LoginPage;
