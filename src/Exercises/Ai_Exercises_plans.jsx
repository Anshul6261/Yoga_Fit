import React, { useState } from "react";
import { useNavigate } from 'react-router-dom'; // Use useNavigate
const Ai_Exercises_plans = () => {
    const navigate = useNavigate(); // Create navigate variable
    const [category, setCategory] = useState([]);
    const [focusArea, setFocusArea] = useState([]);
    const [experienceLevel, setExperienceLevel] = useState(""); 
    const [duration, setDuration] = useState("");
    const [medicalCondition, setMedicalCondition] = useState("");
    const [preferences, setPreferences] = useState("");
    const [loading, setLoading] = useState(false);
    const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
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

        // Navigate to the ExerciseSuggestions component and pass the details as state
        navigate('/exercise/ai_suggestion', { state: { category, focusArea, experienceLevel, duration, medicalCondition, preferences } });

        setLoading(false);
    };

    return (
        <div>
            <div style={{ 
                display: 'flex', 
                justifyContent: 'center', 
                alignItems: 'flex-start', 
                minHeight: '100vh', 
                paddingTop: '20px', 
                backgroundColor: '#f5f5f5' 
            }}>
                <div style={{
                    backgroundColor: '#fff',
                    padding: '30px',
                    borderRadius: '10px',
                    boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
                    maxWidth: '500px',
                    width: '100%',
                    margin: '20px 0'
                }}>
                    <h2 style={{ textAlign: 'center', color: '#007BFF' }}>Customize Your Ideal Workout!</h2>
                    <form onSubmit={handleSubmit}>
                        {/* Category */}
                        <label style={{ fontWeight: 'bold' }}>Category:</label>
                        <div style={{ margin: '10px 0' }}>
                            {["Yoga Asanas", "Pranayama", "Dands and Baithaks", "Vyayam", "Talwarbazi", "Silambam", "Kalaripayattu", "Gatka"].map((option) => (
                                <div key={option}>
                                    <input 
                                        type="checkbox" 
                                        value={option} 
                                        checked={category.includes(option)} 
                                        onChange={handleCategoryChange} 
                                    />
                                    <label>{option}</label>
                                </div>
                            ))}
                        </div>

                        {/* Focus Area */}
                        <label style={{ fontWeight: 'bold' }}>Focus Area:</label>
                        <div style={{ margin: '10px 0' }}>
                            {["relaxation", "digestion", "strength", "flexibility", "endurance"].map((option) => (
                                <div key={option}>
                                    <input 
                                        type="checkbox" 
                                        value={option} 
                                        checked={focusArea.includes(option)} 
                                        onChange={handleFocusAreaChange} 
                                    />
                                    <label>{option}</label>
                                </div>
                            ))}
                        </div>

                        {/* Experience Level */}
                        <label style={{ fontWeight: 'bold' }} htmlFor="experienceLevel">Experience Level:</label>
                        <select
                            id="experienceLevel"
                            value={experienceLevel}
                            onChange={(e) => setExperienceLevel(e.target.value)}
                            required
                            style={{
                                width: '100%',
                                padding: '10px',
                                margin: '10px 0',
                                borderRadius: '5px',
                                border: '1px solid #ddd'
                            }}
                        >
                            <option value="">Select...</option>
                            <option value="beginner">Beginner</option>
                            <option value="intermediate">Intermediate</option>
                            <option value="advanced">Advanced</option>
                        </select>

                        {/* Duration */}
                        <label style={{ fontWeight: 'bold' }} htmlFor="duration">Duration (minutes):</label>
                        <input
                            type="number"
                            id="duration"
                            value={duration}
                            onChange={(e) => setDuration(e.target.value)}
                            min="1"
                            required
                            style={{
                                width: '100%',
                                padding: '10px',
                                margin: '10px 0',
                                borderRadius: '5px',
                                border: '1px solid #ddd'
                            }}
                        />

                        {/* Medical Condition */}
                        <label style={{ fontWeight: 'bold' }} htmlFor="medicalCondition">Do you have any medical conditions we should know about?</label>
                        <textarea
                            id="medicalCondition"
                            value={medicalCondition}
                            onChange={(e) => setMedicalCondition(e.target.value)}
                            rows="3"
                            placeholder="e.g., asthma, back pain"
                            style={{
                                width: '100%',
                                padding: '10px',
                                margin: '10px 0',
                                borderRadius: '5px',
                                border: '1px solid #ddd'
                            }}
                        />

                        {/* Additional Questions */}
                        <label style={{ fontWeight: 'bold' }} htmlFor="preferences">Any other preferences (e.g., outdoor/indoor, equipment availability):</label>
                        <textarea
                            id="preferences"
                            value={preferences}
                            onChange={(e) => setPreferences(e.target.value)}
                            rows="3"
                            placeholder="e.g., prefer outdoor exercises, have access to weights"
                            style={{
                                width: '100%',
                                padding: '10px',
                                margin: '10px 0',
                                borderRadius: '5px',
                                border: '1px solid #ddd'
                            }}
                        />

                        {/* Submit Button */}
                        <button
                            type="submit"
                            disabled={loading}
                            style={{
                                width: '100%',
                                backgroundColor: '#007BFF',
                                color: '#FFFFFF',
                                border: 'none',
                                borderRadius: '5px',
                                padding: '12px',
                                cursor: loading ? 'not-allowed' : 'pointer',
                                opacity: loading ? 0.7 : 1,
                                transition: 'background-color 0.3s'
                            }}
                            onMouseOver={(e) => e.currentTarget.style.backgroundColor = '#0056b3'}
                            onMouseOut={(e) => e.currentTarget.style.backgroundColor = '#007BFF'}
                        >
                            {loading ? "Loading..." : "Generate Your Plan!"}
                        </button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default Ai_Exercises_plans;
