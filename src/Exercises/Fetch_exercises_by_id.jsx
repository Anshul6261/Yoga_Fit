import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './ExerciseDetails.css'; 


const Fetch_exercises_by_id = () => {
  const { _id } = useParams(); // Get exercise ID from the route parameters
  const [exercise, setExercise] = useState(null);
  const [loading, setLoading] = useState(true);
  const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

  useEffect(() => {
    const fetchExercise = async () => {
      try {
        const response = await fetch(`${API_BASE_URL}/exercises/_id/${_id}`);
        const data = await response.json();
        // console.log(data);
        setExercise(data);
      } catch (error) {
        console.error('Error fetching exercise:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchExercise();
  }, [_id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!exercise) {
    return <div>No exercise found!</div>;
  }

  return (
    <div>
      <div className="exercise-details-page">
        {/* Name Card */}
        <div className="exercise-name-card">
          <h1>{exercise.name}</h1>
        </div>

        {/* Exercise details container */}
        <div className="exercise-details-container">
          <div className="exercise-image image-container">
            <img src={exercise.image_link} alt={exercise.name} />
          </div>

          <div className="exercise-info">
            <div className="about-card">
              <h4 className='bold-heading'>About</h4>
              <p>{exercise.description}</p>
            </div>

            <div className="steps-card">
              <h4 className='bold-heading'>Steps:</h4>
              <ol>
                {exercise.steps.map((step, index) => (
                  <li key={index}>{step}</li>
                ))}
              </ol>
            </div>
          </div>
        </div>

        {/* Lower section */}
        <div className="lower-section">
          <div className="dos-donts-card">
            <h4 className='bold-heading'>Do's:</h4>
            <ul>
              {exercise.dos.map((doItem, index) => (
                <li key={index}>{doItem}</li>
              ))}
            </ul>
            <h4 className='bold-heading'>Dont's:</h4>
            {exercise.donts.map((dontItem, index) => (
                <li key={index}>{dontItem}</li>
              ))}
          </div>

          <div className="benefits-card">
            <h4 className='bold-heading'>Benefits:</h4>
            <ul>
              {exercise.benefits.map((benefit, index) => (
                <li key={index}>{benefit}</li>
              ))}
            </ul>
          </div>

          <div className="video-card">
            <p className='bold-heading'>Watch the Video:</p>
            <a href={exercise.video_link} target="_blank" rel="noreferrer">
              Watch Video
            </a>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Fetch_exercises_by_id;
