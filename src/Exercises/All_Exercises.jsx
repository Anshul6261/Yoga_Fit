import React, { useState, useEffect } from 'react';

import './Exercises.css'; 
function All_Exercises() {
  return (
    <div>
      <div className="exercises-page">
        <div className="exercises-container">
          {/* Yoga Asanas Card */}
          <div className="exercises-card create-card">
          <img 
              src="/yoga-asanas.png" 
              alt="Yoga Asanas" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Yoga Asanas</h2>
            <p>Improve flexibility and strength with traditional Yoga poses.</p>
            <a href="/exercises/Yoga Asanas" className="exercises-btn">Explore Now</a>
          </div>

          {/* Pranayama Card */}
          <div className="exercises-card create-card">
          <img 
              src="/pranayama.png" 
              alt="Pranayama" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Pranayama</h2>
            <p>Enhance your breathing and lung capacity with Pranayama techniques.</p>
            <a href="/exercises/Pranayama" className="exercises-btn">Explore Now</a>
          </div>

          {/* Dands and Baithaks Card */}
          <div className="exercises-card create-card">
          <img 
              src="/baithak.jpg" 
              alt="Pranayama" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Dands and Baithaks</h2>
            <p>Explore traditional Indian fitness routines for endurance.</p>
            <a href="/exercises/Dands and Baithaks" className="exercises-btn">Explore Now</a>
          </div>

          {/* Vyayam Card */}
          <div className="exercises-card create-card">
          <img 
              src="/vya.jpeg" 
              alt="Pranayama" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Vyayam</h2>
            <p>Discover physical exercises rooted in ancient Indian traditions.</p>
            <a href="/exercises/Vyayam" className="exercises-btn">Explore Now</a>
          </div>

          {/* Talwarbazi Card */}
          <div className="exercises-card create-card">
          <img 
              src="/tal.png" 
              alt="Pranayama" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Talwarbazi</h2>
            <p>Learn the art of sword fighting with Talwarbazi.</p>
            <a href="/exercises/Talwarbazi" className="exercises-btn">Explore Now</a>
          </div>

          {/* Silambam Card */}
          <div className="exercises-card create-card">
          <img 
              src="/sil.png" 
              alt="Pranayama" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Silambam</h2>
            <p>Master the ancient martial art of Silambam using sticks and swords.</p>
            <a href="/exercises/Silambam" className="exercises-btn">Explore Now</a>
          </div>

          {/* Kalaripayattu Card */}
          <div className="exercises-card create-card">
          <img 
              src="/kal.jpeg" 
              alt="Pranayama" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Kalaripayattu</h2>
            <p>Engage in one of the oldest fighting systems in the world from Kerala.</p>
            <a href="/exercises/Kalaripayattu" className="exercises-btn">Explore Now</a>
          </div>

          {/* Gatka Card */}
          <div className="exercises-card create-card">
          <img 
              src="/gat.avif" 
              alt="Pranayama" 
              style={{ width: '100%', height: 'auto', borderRadius: '8px' }} // Inline styles
            />
            <h2>Gatka</h2>
            <p>Learn Gatka, a traditional martial art form from Punjab.</p>
            <a href="/exercises/Gatka" className="exercises-btn">Explore Now</a>
          </div>
        </div>
      </div>
    </div>
  );
}

export default All_Exercises;