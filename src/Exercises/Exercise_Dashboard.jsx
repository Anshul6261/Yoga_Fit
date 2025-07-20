import './Exercises.css'
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
function Exercises() {
    const navigate = useNavigate();
    const [isSubscribed, setIsSubscribed] = useState(false);
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
    useEffect(() => {
        // Initial check
        checkAuthStatus();
        
        // Listen for storage changes
        const handleStorageChange = () => {
            checkAuthStatus();
        };

        window.addEventListener('storage', handleStorageChange);
        
        return () => {
            window.removeEventListener('storage', handleStorageChange);
        };
    }, []);

const checkAuthStatus = async () => {
    const token = localStorage.getItem("token");
    setIsLoggedIn(!!token);

    if (token) {
        try {
            const res = await fetch(`${API_BASE_URL}/check-subscription`, {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            const data = await res.json();
            setIsSubscribed(data.subscribed);
        } catch (error) {
            console.error("Failed to check subscription", error);
            setIsSubscribed(false);
        }
    } else {
        setIsSubscribed(false);
    }
};

const handleAiPlanClick = async () => {
    await checkAuthStatus(); // Make sure subscription is freshly verified

    if (!isLoggedIn) {
        navigate("/Login", { state: { from: location.pathname } });
    } else if (!isSubscribed) {
        navigate("/subscription");
    } else {
        navigate("/exercises/Ai-plans");
    }
};


    return (
     <div>
    <div className="exercises-page">
            <div className="exercises-container">
                {/* Card to explore all exercises and yoga */}
                <div className="exercises-card create-card">
                    <h2>Explore All Exercises and Yogas</h2>
                   
                    <img src="/all_exercises(4).png" alt="photo of student" />
                    <p>Discover a variety of exercises and yoga asanas we have in store for you.</p>
                    <a href="/all_exercises" className="exercises-btn">Explore Now</a>
                </div>
                
                {/* Card to get exercises from a curated plan */}
                <div className="exercises-card create-card">
                    <h2>Get Exercises from a Curated Plan</h2>
                    <img src='/schedule.png'></img>
                    <p>Receive tailored exercise plans based on your preferences and goals.</p>
                    <a href="/exercises/curated-plans" className="exercises-btn">Get Started</a>
                </div>
                
                {/* Card for AI-generated yoga suggestions */}
                <div className="exercises-card join-card">
                    <h2>Personalized Yoga with AI Magic</h2>
                    <img src='/ai_image.png' alt="AI Yoga" />
                    <p>Generate personalized yoga routines based on your requirements using AI.</p>
                    <button onClick={handleAiPlanClick} className="exercises-btn">Try Now</button>
                </div>
            </div>
        </div>
        </div>
    );
}

export default Exercises;

