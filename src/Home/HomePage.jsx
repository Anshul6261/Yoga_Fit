import "./HomePage.css";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";  
import About from "../About/AboutUs.jsx";

const HomePage = () => {
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

          

if (res.status === 401) {
  // Token expired
  localStorage.removeItem("token");

  navigate("/login", { state: { from: location.pathname } });
}
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

      <main>
        <section className="upper-portion">
        <div className="quote flexible">
  <h1 className="quote-heading">Fitness That Fits Into Your Lifestyle.</h1>
  <h2 className="quote-subheading">Friendly fitness plans that suit your schedule and goals.</h2>
  
    <button 
    className="btn quote-button" 
    onClick={handleAiPlanClick}>
    Try Our AI Plan
  </button>
 

</div>
<div className="cards fexible front-cards">

<a href="/exercises/Pranayama" className="max-w-sm rounded overflow-hidden shadow-lg box fexible block">
  <img className="w-full" src="IMG_3803-scaled.jpg" alt="Sunset in the mountains"/>
  <div className="px-6 py-4">
    <div className="font-bold text-xl mb-2">Breathe Into Calmness</div>
    <p className="text-gray-700 text-base">
      Master your breath to unlock inner peace, reduce stress, and enhance lung health with the power of Pranayama.
    </p>
  </div>
</a>


<a href="/exercises/Yoga Asanas" className="max-w-sm rounded overflow-hidden shadow-lg box fexible block">
  <img className="w-full" src="IMG_0106_small-scaled.jpg" alt="Sunset in the mountains"/>
  <div className="px-6 py-4">
    <div className="font-bold text-xl mb-2">Flow Into Flexibility</div>
    <p className="text-gray-700 text-base">
      Rejuvenate your body and mind through graceful yoga postures that build strength, balance, and flexibility.
    </p>
  </div>
</a>

<a href="/exercises/Vyayam" className="max-w-sm rounded overflow-hidden shadow-lg box fexible block">
 <img className="w-full" src="IMG_5978-scaled.jpg" alt="Sunset in the mountains"/>
  <div className="px-6 py-4">
    <div className="font-bold text-xl mb-2">Strength the Indian Way</div>
    <p className="text-gray-700 text-base">
     Rediscover traditional Indian fitness with Vyayam – a blend of movement, endurance, and holistic strength.
    </p>
  </div>
</a>

<a href="/exercises/Dands and Baithaks" className="max-w-sm rounded overflow-hidden shadow-lg box fexible block">
 <img className="w-full" src="images (5).jpeg" alt="Sunset in the mountains"/>
  <div className="px-6 py-4">
    <div className="font-bold text-xl mb-2">Power Through Tradition</div>
    <p className="text-gray-700 text-base">
     Train like warriors of the past with Dand Baithak – the timeless secret to agility, power, and discipline.
    </p>
  </div>
</a>
</div>
        </section>
        <section id="about" className="sec-padding">
          <h3 className="section-heading">ABOUT US</h3>
          {/* <div className="sec-content-div flexible">
            <p>
              The FitQuest is designed to support users in achieving their goals while prioritizing their physical health...
            </p>
            <img src="/people-practicing-exercise-athletes-characters-free-vector.jpg" alt="People playing basketball" />
          </div> */}
          <About />
        </section>

<section className="academy-section">
  {/* Banner Section */}
  <div className="academy-banner">
    <div className="academy-text">
      <h2>Take your fitness to the next level with the YOGAFIT </h2>
      <p>
        Explore certified programs, short-term challenges, and deep-dive courses on yoga, bodyweight training, pranayama, and holistic wellness.
      </p>
    </div>
    
    <div className="academy-image">
      <img
        src="/academy-desktop.webp"
        alt="Notebook and pen showing planning"
      />
    </div>
  </div>

  {/* Testimonials Section */}
  <div className="testimonials">
    {[
      {
        title: 'A HEALTHY CHOICE',
        quote: '“YOGAFIT has been the best for my mental, spiritual, and physical balance.”',
        author: 'Melissa'
      },
      {
        title: 'WHAT REALLY MATTERS',
        quote: '“I feel supported even when I wobble — YOGAFIT  reminds me why I love mindful movement.”',
        author: 'Sam'
      },
      {
        title: 'CAN’T STOP RECOMMENDING',
        quote: '“I’ve recommended YOGAFIT to countless friends — it’s a complete wellness experience.”',
        author: 'Carole'
      }
    ].map((t, index) => (
      <div className="testimonial-card" key={index}>
        <div className="stars">★★★★★</div>
        <h4>{t.title}</h4>
        <p className="quote">{t.quote}</p>
        <p className="author">{t.author}</p>
      </div>
    ))}
  </div>
</section>

  



<section id="our-service" className="services-section" style={{ backgroundColor: "rgba(224, 239, 237, 1)" }}>
  <h3 className="section-heading">OUR SERVICES</h3>
  <div className="card-container">
    <div className="service-card">
      <img src="/ai_fitness.jpeg" alt="AI Fitness Plans" className="card-icon" />
      <h5>AI-Personalized Workouts</h5>
      <p>Get exercise recommendations tailored to your body, goals, and any issues you face, using AI-driven assessments.</p>
       
    </div>
    <div className="service-card">
      <img src="/all_library.jpeg" alt="All Exercises" className="card-icon" />
      <h5>Complete Exercise Library</h5>
      <p>Explore a wide range of exercises including yoga, bodyweight workouts, and breathing techniques—all in one place.</p>
    </div>
    <div className="service-card">
      <img src="/blogs_img.jpeg" alt="Community and Blogs" className="card-icon" />
      <h5>Community & Blogs</h5>
      <p>Share experiences, post queries, and connect with like-minded people through our interactive blog and community board.</p>
    </div>
  </div>
</section>


      </main>

    </div>
  );
};

export default HomePage;

