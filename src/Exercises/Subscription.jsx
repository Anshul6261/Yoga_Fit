import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Subscription.css";

function Subscription() {
    const navigate = useNavigate();
    const [subscribingPlan, setSubscribingPlan] = useState(null);
    const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

    const plans = [
        { name: "monthly", price: 200, duration: 1 },
        { name: "six-months", price: 600, duration: 6 },
        { name: "yearly", price: 1200, duration: 12 }
    ];

    const handleSubscribe = async (plan) => {
        setSubscribingPlan(plan.name);
        const token = localStorage.getItem("token");
          console.log("Current token:", token); // Add this line
        console.log("Subscribing to plan:", plan.name); // Add this line

        if (!token) {
            alert("Please log in to subscribe.");
            setSubscribingPlan(null);
            return;
        }

        try {
            const response = await fetch(`${API_BASE_URL}/subscribe`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({ 
                    plan: plan.name  // Send just the plan name as string
                })
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || "Subscription failed");
            }

            const data = await response.json();
            
            if (data.success) {
                alert("Subscription Successful! 🎉");
                localStorage.setItem("isSubscribed", "true");
                navigate("/exercises/Ai-plans");
            } else {
                alert(data.message || "Subscription failed. Try again.");
            }
        } catch (error) {
            console.error("Subscription error:", error);
            alert(error.message || "Error processing subscription. Please try again.");
        } finally {
            setSubscribingPlan(null);
        }
    };

    return (
        <div className="subscription-page">
            <h2>Subscribe to AI-Powered Plans</h2>
            <p>Get access to personalized AI-generated exercise and yoga plans tailored to your needs.</p>
            <div className="subscriptions">
                {plans.map((plan) => (
                    <div key={plan.name} className="subscription-card">
                        <h3>AI Plan - ₹{plan.price} ({plan.name.replace("-", " ")})</h3>
                        <p>✔ Personalized AI-generated workouts</p>
                        <p>✔ Tailored Yoga & Fitness plans</p>
                        <p>✔ Unlimited Access to AI Features</p>
                        <button 
                            onClick={() => handleSubscribe(plan)}
                            disabled={subscribingPlan !== null}
                            className="subscribe-btn"
                        >
                            {subscribingPlan === plan.name ? "Processing..." : "Subscribe Now"}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Subscription;