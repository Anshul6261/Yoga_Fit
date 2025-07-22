import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';

const Ai_Exercises_plans = () => {
  const navigate = useNavigate();
  const [category, setCategory] = useState([]);
  const [focusArea, setFocusArea] = useState([]);
  const [experienceLevel, setExperienceLevel] = useState("");
  const [duration, setDuration] = useState("");
  const [medicalCondition, setMedicalCondition] = useState("");
  const [preferences, setPreferences] = useState("");
  const [loading, setLoading] = useState(false);

  const handleCategoryChange = (e) => {
    const value = e.target.value;
    setCategory((prev) =>
      prev.includes(value) ? prev.filter((cat) => cat !== value) : [...prev, value]
    );
  };

  const handleFocusAreaChange = (e) => {
    const value = e.target.value;
    setFocusArea((prev) =>
      prev.includes(value) ? prev.filter((area) => area !== value) : [...prev, value]
    );
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    navigate('/exercise/ai_suggestion', {
      state: { category, focusArea, experienceLevel, duration, medicalCondition, preferences }
    });
    setLoading(false);
  };

  return (
    <div className="min-h-screen flex justify-center items-start pt-16 bg-gray-100">
      <div className="bg-white p-10 rounded-3xl shadow-2xl w-full max-w-xl">
        <h2 className="text-3xl font-extrabold text-center text-blue-600 mb-8">Customize Your Ideal Workout!</h2>
        <form onSubmit={handleSubmit} className="space-y-6">
          {/* Category */}
          <div>
            <label className="font-semibold text-lg text-gray-800">Category:</label>
            <div className="grid grid-cols-2 gap-4 mt-3">
              {["Yoga Asanas", "Pranayama", "Dands and Baithaks", "Vyayam", "Talwarbazi", "Silambam", "Kalaripayattu", "Gatka"].map((option) => (
                <label key={option} className="flex items-center space-x-3 text-base">
                  <input type="checkbox" value={option} checked={category.includes(option)} onChange={handleCategoryChange} className="w-5 h-5" />
                  <span>{option}</span>
                </label>
              ))}
            </div>
          </div>

          {/* Focus Area */}
          <div>
            <label className="font-semibold text-lg text-gray-800">Focus Area:</label>
            <div className="grid grid-cols-2 gap-4 mt-3">
              {["relaxation", "digestion", "strength", "flexibility", "endurance"].map((option) => (
                <label key={option} className="flex items-center space-x-3 text-base">
                  <input type="checkbox" value={option} checked={focusArea.includes(option)} onChange={handleFocusAreaChange} className="w-5 h-5" />
                  <span>{option}</span>
                </label>
              ))}
            </div>
          </div>

          {/* Experience Level */}
          <div>
            <label className="font-semibold text-lg text-gray-800">Experience Level:</label>
            <select
              value={experienceLevel}
              onChange={(e) => setExperienceLevel(e.target.value)}
              required
              className="w-full mt-2 p-3 text-base border border-gray-300 rounded-xl"
            >
              <option value="">Select...</option>
              <option value="beginner">Beginner</option>
              <option value="intermediate">Intermediate</option>
              <option value="advanced">Advanced</option>
            </select>
          </div>

          {/* Duration */}
          <div>
            <label className="font-semibold text-lg text-gray-800">Duration (minutes):</label>
            <input
              type="number"
              value={duration}
              onChange={(e) => setDuration(e.target.value)}
              min="1"
              required
              className="w-full mt-2 p-3 text-base border border-gray-300 rounded-xl"
              placeholder="Enter duration in minutes"
            />
          </div>

          {/* Medical Condition */}
          <div>
            <label className="font-semibold text-lg text-gray-800">Medical Conditions (if any):</label>
            <textarea
              value={medicalCondition}
              onChange={(e) => setMedicalCondition(e.target.value)}
              rows="3"
              className="w-full mt-2 p-3 text-base border border-gray-300 rounded-xl"
              placeholder="e.g., asthma, back pain"
            />
          </div>

          {/* Preferences */}
          <div>
            <label className="font-semibold text-lg text-gray-800">Other Preferences:</label>
            <textarea
              value={preferences}
              onChange={(e) => setPreferences(e.target.value)}
              rows="3"
              className="w-full mt-2 p-3 text-base border border-gray-300 rounded-xl"
              placeholder="e.g., outdoor/indoor, equipment availability"
            />
          </div>

          {/* Submit Button */}
          <button
            type="submit"
            disabled={loading}
            className={`w-full text-lg font-bold py-4 rounded-xl transition duration-300 ${
              loading ? "bg-blue-300 cursor-not-allowed" : "bg-blue-600 hover:bg-blue-700 text-white"
            }`}
          >
            {loading ? "Generating..." : "Generate Your Plan!"}
          </button>
        </form>
      </div>
    </div>
  );
};

export default Ai_Exercises_plans;
