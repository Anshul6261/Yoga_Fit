import './AboutUs.css';
import React from 'react';
import { useNavigate } from 'react-router-dom';

const AboutUs = () => {
  const navigate = useNavigate();

  const missionItems = [
    { title: "Empowering Wellness", description: "Empower individuals with personalized fitness journeys." },
    { title: "Evolving Together", description: "Blend ancient wisdom with modern techniques." },
    { title: "Inclusive Community", description: "Welcoming everyone, no matter the background." },
    { title: "Growth Mindset", description: "Encourage lifelong learning and positivity." },
  ];

  const valueItems = [
    { title: "Integrity First", description: "Honest and transparent in every interaction." },
    { title: "Sustainable Fitness", description: "Focus on long-term, balanced health." },
    { title: "Passion-Driven", description: "Inspired by passion, fueled by purpose." },
    { title: "Supportive Community", description: "Stronger together on every journey." },
  ];

  return (
    <div className="about-page">
<section className="hero-section">

</section>


      <div className="two-column-section">
        <div className="column">
          <h2>Our Mission</h2>
          <div className="cards-grid">
            {missionItems.map((item, index) => (
              <div key={index} className="info-card">
                <h3>{item.title}</h3>
                <p>{item.description}</p>
              </div>
            ))}
          </div>
        </div>

        <div className="column">
          <h2>Our Values</h2>
          <div className="cards-grid">
            {valueItems.map((item, index) => (
              <div key={index} className="info-card">
                <h3>{item.title}</h3>
                <p>{item.description}</p>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default AboutUs;
