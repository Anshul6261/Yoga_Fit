import "./HomePage.css";
import React from "react";
const HomePage = () => {
  return (
    <div>

      <main>
        <section className="upper-portion">
        <div className="quote flexible">
  <h1 className="quote-heading">Fitness That Fits Into Your Lifestyle.</h1>
  <h2 className="quote-subheading">Friendly fitness plans that suit your schedule and goals.</h2>
  <button 
    className="btn quote-button" 
    onClick={() => window.location.href = '/profile'}>
    Try us free for 14 days
  </button>
</div>
<div className="cards fexible">

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
          <div className="sec-content-div flexible">
            <p>
              The FitQuest is designed to support users in achieving their goals while prioritizing their physical health...
            </p>
            <img src="/people-practicing-exercise-athletes-characters-free-vector.jpg" alt="People playing basketball" />
          </div>
        </section>
        <section id="varieties" className="sec-padding" style={{ backgroundColor: "rgba(224, 239, 237, 1)" }}>
          <h3 className="section-heading">WORKOUT PLANS</h3>
          <div className="sec-content-div flexible">
            <div className="tile">
              <img src="/student.jpg" alt="photo of student" />
              <h4>STUDENT</h4>
              <ul>
                <li>Bodyweight Exercises</li>
                <li>Time-Efficient</li>
                <li>Stress Relief</li>
                <li>Variety and Fun</li>
              </ul>
            </div>
            <div className="tile">
              <img src="/employee.jpg" alt="Corporate Employee" />
              <h4>Corporate Employee</h4>
              <ul>
                <li>Effective Time Management</li>
                <li>Interval Training</li>
                <li>Office-Friendly Exercises</li>
                <li>Stress Reduction</li>
              </ul>
            </div>
            <div className="tile">
              <img src="/remote.png" alt="Remote Workers" />
              <h4>Remote Workers</h4>
              <ul>
                <li>Home Workouts</li>
                <li>Ergonomics</li>
                <li>Balanced Routines</li>
                <li>Digital Detox</li>
              </ul>
            </div>
          </div>
        </section>
        <section id="our-service" className="sec-padding" style={{ backgroundColor: "rgba(224, 239, 237, 1)" }}>
          <h3 className="section-heading" >OUR SERVICE</h3>
          <div className="sec-content-div">
            <div className="bars">
              <div className="icon-container">
                <img src="/effectiveworkout.gif" alt="Effective Workouts" />
              </div>
              <div className="txt-container">
                <h5>Effective Fitness plans</h5>
                <p>We provide satisfactory workout plans that users can customize.</p>
              </div>
            </div>
            <div className="bars">
              <div className="icon-container">
                <img src="/huddle.gif" alt="Community" />
              </div>
              <div className="txt-container">
                <h5>COMMUNITY</h5>
                <p>Connect with people with similar interests and work out in groups.</p>
              </div>
            </div>
            <div className="bars">
              <div className="icon-container">
                <img src="/events.gif" alt="Events" />
              </div>
              <div className="txt-container">
                <h5>EVENTS</h5>
                <p>Join or create events and invite friends and families to enjoy.</p>
              </div>
            </div>
          </div>
        </section>

      </main>

    </div>
  );
};

export default HomePage;