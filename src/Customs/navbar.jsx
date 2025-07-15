import { Link, useNavigate } from 'react-router-dom';
import React, { useState, useEffect } from "react";
import "./navbar.css";

function Navbar() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();

  // Check login status on component mount and when token changes
  useEffect(() => {
    const token = localStorage.getItem("token");
    setIsLoggedIn(!!token); // Convert to boolean
  }, [localStorage.getItem("token")]); // Re-run when token changes

  // Listen for storage changes (e.g., logging out from another tab)
  useEffect(() => {
    const handleStorageChange = (event) => {
      if (event.key === "token") {
        const token = localStorage.getItem("token");
        setIsLoggedIn(!!token);
      }
    };

    window.addEventListener("storage", handleStorageChange);

    return () => {
      window.removeEventListener("storage", handleStorageChange);
    };
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("token"); // Remove token on logout
    setIsLoggedIn(false); // Update login state
    navigate("/"); // Navigate to home page or desired route
  };

  return (
    <div style={{backgroundColor:" rgba(224, 239, 237, 1)"}}className="flex size-20 justify-between p-4 shadow-md w-full">
      {/* Logo on the top left */}
      <Link to="/">
        <img src="/logo.png" alt="Logo" width={100} height={100} style={{backgroundColor:"#08656E"}} className="cursor-pointer " />
      </Link>

      {/* Links aligned to the right */}
      <div className="flex space-x-6 nav-links">
        <Link to="/exercises_dashboard" className="link">
          Exercises
        </Link>
        <Link to="/blogs" className="link">
          Blogs
        </Link>
        <Link to="/about" className="link">
          About
        </Link>

        {/* Conditional rendering based on login status */}
        {!isLoggedIn ? (
          <div className="space-x-4">
            <Link 
              to="/login" 
              className="text-white hover:underline" 
              style={{ 
                backgroundColor: "#08656E", 
                borderRadius: "6rem",
                padding: "0.5rem 1rem",
                fontSize: "16px", 
                fontWeight: "600",
                color: "white"
              }}
            >
              Login
            </Link>
            <Link 
              to="/signup" 
              className="text-white hover:underline" 
              style={{ 
                backgroundColor: "#08656E", 
                borderRadius: "6rem",
                padding: "0.5rem 1rem",
                fontSize: "16px", 
                fontWeight: "600" ,
                color: "white"
              }}
            >
              Register
            </Link>
          </div>
        ) : (
          <button 
            onClick={handleLogout} 
            className="text-white hover:underline" 
              style={{ 
                backgroundColor: "#08656E", 
                borderRadius: "6rem",
                padding: "0.5rem 1rem",
                fontSize: "16px", 
                fontWeight: "600" ,
                color: "white"
              }}
          >
            Logout
          </button>
        )}
      </div>
    </div>
  );
}

export default Navbar;