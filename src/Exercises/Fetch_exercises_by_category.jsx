import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './Exercises.css'; 

const Fetch_exercises_by_category = () => {
  const { category } = useParams(); // Get category from the route parameters
  const [exercises, setExercises] = useState([]);
  const [loading, setLoading] = useState(true);
//   const apiUrl = import.meta.env.VITE_API_BASE_URL;
  const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
  useEffect(() => {
    const fetchExercises = async () => {
      try {
        // Fetch exercises for the specific category
        const response = await fetch(`${API_BASE_URL}/exercises/category/${category}`);
        const data = await response.json();
        // console.log(data);
        setExercises(data);
      } catch (error) {
        console.error('Error fetching exercises:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchExercises();
  }, [category]);

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <div className="exercises-page">
        <h2>Exercises in {category}</h2>
        <div className="exercises-container">
          {exercises.length > 0 ? (
            exercises.map((exercise) => (
              <div key={exercise._id} className="exercises-card">
                <img 
                  src={exercise.image_link} 
                  alt={exercise.name} 
                  // style={{ width: '100%', height: 'auto', borderRadius: '8px' }} 
                />
                <h3>{exercise.name}</h3>
                <p>{exercise.description}</p>
                <a href={`/exercises/_id/${exercise._id}`} className="exercises-btn">View Details</a>
              </div>
            ))
          ) : (
            <p>No exercises found in this category.</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default Fetch_exercises_by_category;