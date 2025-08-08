import React from 'react';
const Footer = () => {
  return (
    <footer className="text-black py-12 px-6" style={{ backgroundColor: "#187e89" }}>
      <div className="max-w-7xl mx-auto flex flex-col items-center gap-10">

        {/* Logo */}
        <div className="logo">
                  <img src="/logo4.png" alt="Logo" className="cursor-pointer " />
        </div>


        {/* Footer Links */}
        <div className="w-full flex flex-wrap justify-center gap-16 text-center md:text-left">
          {/* Classes Column */}
          <div>
            <h4 className="text-lg font-semibold mb-3">Classes</h4>
            <ul className="space-y-2 text-sm">
              <li>Classes with Esther</li>
              <li>20 - 30 minutes</li>
              <li>Morning yoga</li>
              <li>Evening yoga</li>
              <li>Yin</li>
              <li>Strength and fitness</li>
              <li>Women's health</li>
              <li>Meditation</li>
            </ul>
          </div>

          {/* Magazine Column */}
          <div>
            <h4 className="text-lg font-semibold mb-3">Magazine</h4>
            <ul className="space-y-2 text-sm">
              <li>New in Yoga Magazine</li>
              <li>Practice</li>
              <li>Meditation</li>
              <li>Anatomy</li>
              <li>Wellbeing</li>
              <li>Philosophy</li>
              <li>Recipes</li>
              <li>News</li>
            </ul>
          </div>

          {/* Others Column */}
          <div>
            <h4 className="text-lg font-semibold mb-3">Others</h4>
            <ul className="space-y-2 text-sm">
              <li>Teachers</li>
              <li>Programs</li>
              <li>Prices</li>
              <li>Academy</li>
            </ul>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
