import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

// No CSS import is needed when using Tailwind CSS utility classes.

function Subscription() {
    const navigate = useNavigate();
    const [subscribingPlan, setSubscribingPlan] = useState(null);
    // State to track the currently selected plan, defaults to 'six-months'
    const [selectedPlan, setSelectedPlan] = useState('six-months');
    const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

    const plans = [
        { name: "monthly", price: 200, duration: 1 },
        { name: "six-months", price: 600, duration: 6 },
        { name: "yearly", price: 1200, duration: 12 }
    ];

    const handleSubscribe = async (plan) => {
        setSubscribingPlan(plan.name);
        const token = localStorage.getItem("token");
        console.log("Current token:", token); // Log the current token for debugging
        console.log("Subscribing to plan:", plan.name);

        if (!token) {
            alert("Please log in to subscribe.");
            navigate("/Login");
            // setSubscribingPlan(null);
            return;
        }

        try {
            const response = await fetch(`${API_BASE_URL}/subscribe`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({ plan: plan.name })
            });
if (response.status === 401) {
  // Token expired
  localStorage.removeItem("token");
 navigate("/login", { state: { from: location.pathname } });
}
            const data = await response.json();
            //             if (response.status === 401 && data.message === "Access token expired") {
            //     // 🔁 Redirect to login
            //     // window.location.href = "/login"; 
            //     navigate("/Login" );
            //     return;
            // }

            if (!response.ok) {
                throw new Error(data.message || "Subscription failed");
            }

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
        // Main container with the custom background color
        <div className="bg-[rgba(214,229,227,255)] min-h-screen w-full flex flex-col items-center justify-center p-4 sm:p-8 font-sans">
            {/* Header Section */}
            <div className="text-center mb-10 sm:mb-16">
                <h1 className="text-3xl sm:text-4xl md:text-5xl font-bold text-teal-900 mb-3">
                    Select The Best Plan For Your Needs
                </h1>
                <p className="text-base sm:text-lg text-gray-700 max-w-2xl mx-auto">
                    Get access to personalized AI-generated exercise and yoga plans tailored to your needs.
                </p>
            </div>

            {/* Subscription Cards Wrapper */}
            <div className="flex flex-col lg:flex-row items-center justify-center gap-8 w-full">
                {plans.map((plan) => {
                    const isSelected = selectedPlan === plan.name;

                    return (
                        <div
                            key={plan.name}
                            onClick={() => setSelectedPlan(plan.name)}
                            // Conditional classes for selected vs. non-selected cards
                            className={`
                                relative p-8 rounded-2xl w-full max-w-md lg:max-w-sm cursor-pointer transition-all duration-300 ease-in-out
                                ${isSelected
                                    ? 'bg-teal-600 text-white shadow-2xl scale-105'
                                    : 'bg-white text-gray-800 shadow-lg hover:shadow-xl hover:-translate-y-2'
                                }
                            `}
                        >
                            {/* Recommended Badge for the 'six-months' plan */}
                            {plan.name === 'six-months' && (
                                <span className={`absolute top-4 right-4 text-xs font-bold py-1 px-3 rounded-full ${isSelected ? 'bg-white/20 text-white' : 'bg-teal-100 text-teal-800'}`}>
                                    Recommended
                                </span>
                            )}
                            
                            {/* Plan Details */}
                            <h2 className={`text-2xl font-semibold capitalize mb-4 ${isSelected ? 'text-white' : 'text-teal-600'}`}>
                                {plan.name.replace("-", " ")}
                            </h2>
                            <div className="mb-6">
                                <span className="text-5xl font-bold">₹{plan.price}</span>
                                <span className={`ml-1 text-lg font-medium ${isSelected ? 'text-teal-100' : 'text-gray-500'}`}>
                                    /{plan.duration === 1 ? 'mo' : `${plan.duration} mos`}
                                </span>
                            </div>

                            {/* Features List */}
                            <ul className="space-y-4 mb-8">
                                {['Personalized AI-generated workouts', 'Tailored Yoga & Fitness plans', 'Unlimited Access to AI Features', 'Track your progress'].map(feature => (
                                    <li key={feature} className="flex items-center">
                                        <svg className={`w-6 h-6 mr-3 ${isSelected ? 'text-white' : 'text-teal-500'}`} fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 13l4 4L19 7"></path></svg>
                                        <span className={isSelected ? 'text-teal-50' : 'text-gray-600'}>{feature}</span>
                                    </li>
                                ))}
                            </ul>

                            {/* Subscribe Button */}
                            <button
                                onClick={(e) => {
                                    e.stopPropagation(); // Prevent card's onClick from firing
                                    handleSubscribe(plan);
                                }}
                                disabled={subscribingPlan !== null}
                                className={`
                                    w-full py-3 px-6 rounded-lg font-semibold text-center transition-colors duration-300
                                    disabled:opacity-50 disabled:cursor-not-allowed
                                    ${isSelected
                                        ? 'bg-white text-teal-600 hover:bg-gray-100'
                                        : 'bg-teal-600 text-white hover:bg-teal-700'
                                    }
                                `}
                            >
                                {subscribingPlan === plan.name ? "Processing..." : "Subscribe Now"}
                            </button>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}

export default Subscription;
