import React from 'react';
import { useLocation } from 'react-router-dom';

import { useEffect, useState } from 'react';
import { GoogleGenerativeAI } from "@google/generative-ai";
import './Exercises.css'; 
const ExerciseSuggestions = () => {
    const location = useLocation();
    const { category, focusArea, experienceLevel, duration, medicalCondition, preferences } = location.state;

    const [, setSuggestions] = useState("");
    const [exerciseDetails, setExerciseDetails] = useState([]);
    const [loading, setLoading] = useState(true);
    const apiUrl = import.meta.env.VITE_API_BASE_URL;
    useEffect(() => {
        const genAI = new GoogleGenerativeAI("AIzaSyBJXzJwXyv5FO9pT7YG8oZxgOjLZKXl16w");

        const model = genAI.getGenerativeModel({ model: "gemini-1.5-flash" });

        const prompt = `Please provide only 5 most suitable IDs of the exercises that match the categories of ${category.join(", ")}, focus on ${focusArea.join(", ")}, for a ${experienceLevel} user, limit number of  exercises to  ${duration} . Consider medical conditions: ${medicalCondition}, and preferences: ${preferences}. The available exercises in form id->name are: 
66f536c4b02a495ec3a498df -> Tadasana
66f536c4b02a495ec3a498e0 -> Vrikshasana
66f536c4b02a495ec3a498e1 -> Padmasana
66f536c4b02a495ec3a498e2 -> Bhujangasana
66f536c4b02a495ec3a498e3 -> Anulom Vilom
66f536c4b02a495ec3a498e4 -> Bhastrika
66f5382cb02a495ec3a498e5 -> Dhanurasana
66f5382cb02a495ec3a498e6 -> Sarvangasana
66f5382cb02a495ec3a498e7 -> Sirsasana
66f5382cb02a495ec3a498e8 -> Bakasana
66f5382cb02a495ec3a498ea -> Kapalbhati
66f5382cb02a495ec3a498eb -> Ujjayi
66f5382cb02a495ec3a498ec -> Dand
66f5382cb02a495ec3a498ed -> Baithak
66f53917b02a495ec3a498ee -> Paschimottanasana
66f53917b02a495ec3a498f0 -> Chakrasana
66f53917b02a495ec3a498f1 -> Naukasana
66f53917b02a495ec3a498f2 -> Pawanmuktasana
66f53917b02a495ec3a498f3 -> Talwarbazi
66f53917b02a495ec3a498f4 -> Kuttu Varisai
66f53917b02a495ec3a498f5 -> Kolthari
66f53917b02a495ec3a498f6 -> Single Stick Fighting
66f53917b02a495ec3a498f7 -> Dand Baithak
66f539f5b02a495ec3a498f8 -> Malasana
66f539f5b02a495ec3a498f9 -> Anjaneyasana
66f539f5b02a495ec3a498fa -> Baddha Konasana
66f539f5b02a495ec3a498fc -> Utthita Trikonasana
66f539f5b02a495ec3a498fe -> Adho Mukha Svanasana
66f539f5b02a495ec3a498ff -> Utkatasana
66f539f5b02a495ec3a49900 -> Marichyasana
66f53a8bb02a495ec3a49901 -> Garudasana
66f53a8bb02a495ec3a49902 -> Marichyasana
66f53a8bb02a495ec3a49903 -> Ananda Balasana
66f53a8bb02a495ec3a49905 -> Adho Mukha Svanasana
66f53a8bb02a495ec3a49906 -> Navasana
66f53a8bb02a495ec3a49909 -> Lolasana
66f53b6ab02a495ec3a4990b -> Kakasana
66f53b6ab02a495ec3a4990c -> Vrksasana
66f53b6ab02a495ec3a4990d -> Setu Bandhasana
66f53b6ab02a495ec3a4990e -> Brahmasana
66f53b6ab02a495ec3a4990f -> Parivrtta Trikonasana
66f53b6ab02a495ec3a49910 -> Supta Baddha Konasana
66f53b6ab02a495ec3a49911 -> Halasana
66f53b6ab02a495ec3a49912 -> Viparita Karani
66f53b6ab02a495ec3a49913 -> Chaturanga Dandasana
66f53c6bb02a495ec3a49914 -> Ardha Matsyendrasana
66f53c6bb02a495ec3a49916 -> Pashasana
66f53c6bb02a495ec3a49917 -> Makarasana
66f53c6bb02a495ec3a49918 -> Gomukhasana
66f53c6bb02a495ec3a49919 -> Savasana
66f53c6bb02a495ec3a4991b -> Krounchasana
66f54413b02a495ec3a4991e -> Ustrasana
66f54413b02a495ec3a49920 -> Simhasana
66f54413b02a495ec3a49922 -> Hanumanasana
66f54413b02a495ec3a49923 -> Urdhva Mukha Svanasana
66f54413b02a495ec3a49924 -> Tittibhasana
66f54413b02a495ec3a49925 -> Astavakrasana
66f54413b02a495ec3a49927 -> Paripurna Navasana
66f546c4b02a495ec3a49928 -> Agnistambhasana
66f546c4b02a495ec3a4992a -> Matsyasana
66f546c4b02a495ec3a4992b -> Parighasana
66f546c4b02a495ec3a4992c -> Upavistha Konasana
66f546c4b02a495ec3a4992e -> Malasana
66f546c4b02a495ec3a4992f -> Prasarita Padottanasana
66f546c4b02a495ec3a49930 -> Eka Pada Rajakapotasana
66f546c4b02a495ec3a49931 -> Koundinyasana
66f548c2b02a495ec3a49934 -> Karnapidasana
66f548c2b02a495ec3a49935 -> Supta Virasana
66f548c2b02a495ec3a49938 -> Padangusthasana
66f548c2b02a495ec3a49939 -> Ardha Chandrasana
66f548c2b02a495ec3a4993b -> Parivrtta Trikonasana
66f54acdb02a495ec3a4993e -> Ardha Matsyendrasana
66f54acdb02a495ec3a49940 -> Shalabhasana
66f54acdb02a495ec3a49941 -> Mandukasana
66f54acdb02a495ec3a49943 -> Anantasana
66f54acdb02a495ec3a49945 -> Utthita Parsvakonasana
66f54f90b02a495ec3a49946 -> Chaturanga Dandasana
66f54f90b02a495ec3a49948 -> Parsvottanasana
66f54f90b02a495ec3a49949 -> Uttanasana
66f54f90b02a495ec3a4994a -> Parighasana
66f54f90b02a495ec3a4994c -> Vajrasana
66f54fbcb02a495ec3a49950 -> Salamba Sarvangasana
66f54fbcb02a495ec3a49951 -> Urdhva Dhanurasana
66f54fbcb02a495ec3a49953 -> Brahma Mudra
66f5870cb02a495ec3a4995a -> Utkata Konasana
66f591a2b02a495ec3a49962 -> Setu Bandhasana
66f591a2b02a495ec3a49963 -> Utthita Parsvakonasana
66f59bc4b02a495ec3a49969 -> Sukhasana (Easy Pose)
66f59bc4b02a495ec3a4996a -> Paschimottanasana (Seated Forward Bend)
66f59bc4b02a495ec3a4996f -> Bakasana (Crow Pose)
66f59bc4b02a495ec3a49970 -> Natarajasana (Dancer Pose)
66f59bc4b02a495ec3a49971 -> Anulom Vilom (Alternate Nostril Breathing)
66f59bc4b02a495ec3a49972 -> Kapalbhati (Skull Shining Breath)
66f59bc4b02a495ec3a49973 -> Bhramari (Bee Breath)
66f59bc4b02a495ec3a49976 -> Sheetali (Cooling Breath)
670198ded112c1dbf69869d4 -> Pungu Mayurasana
670198ded112c1dbf69869d3 -> Katyayani Asana
670198ded112c1dbf69869da -> Ashtanga Namaskara
670198ded112c1dbf69869db -> Anjaneyasana
670198ded112c1dbf69869dc -> Vasisthasana
670198ded112c1dbf69869d6 -> Chaturanga Dandasana
670199c7d112c1dbf69869e2 -> Crocodile Dand
670199c7d112c1dbf69869e6 -> Superman Dand
670199c7d112c1dbf69869de -> Tiger Dand
670199c7d112c1dbf69869e3 -> Spider Dand
670199c7d112c1dbf69869e5 -> Archer Dand
670199c7d112c1dbf69869dd -> Hindu Dand
670199c7d112c1dbf69869e4 -> Plyometric Dand
670199c7d112c1dbf69869e0 -> Scorpion Dand
670199c7d112c1dbf69869df -> Reverse Dand
670199c7d112c1dbf69869e1 -> Diamond Dand
67019ae6d112c1dbf69869e8 -> Rope Climb
67019ae6d112c1dbf69869ee -> Double Hand Support
67019ae6d112c1dbf69869e9 -> Hanging Vertical Split
67019ae6d112c1dbf69869ed -> Hanging Twist
67019ae6d112c1dbf69869eb -> Backward Bend
67019ae6d112c1dbf69869ea -> Lotus Hold
67019ae6d112c1dbf69869e7 -> Pole Climb
67019ae6d112c1dbf69869ef -> Rope Skipping
67019ae6d112c1dbf69869f0 -> Inverted Lotus
67019ae6d112c1dbf69869ec -> Handstand on Pole
67019f33d112c1dbf69869fa -> Double-Handed Block and Strike
67019f33d112c1dbf69869f1 -> Basic Dand Patta Strikes
67019f33d112c1dbf69869f3 -> Double-Handed Guard
67019f33d112c1dbf69869f6 -> Low Guard Block
67019f33d112c1dbf69869f9 -> Feint and Counter
67019f33d112c1dbf69869f7 -> Reverse Grip Strike
67019f33d112c1dbf69869f8 -> Flourish and Flow
67019f33d112c1dbf69869f2 -> Circular Strikes
67019f33d112c1dbf69869f4 -> Overhead Strike
67019f33d112c1dbf69869f5 -> Side Swipe
67019fc9d112c1dbf69869fe -> Chakkar Spin
67019fc9d112c1dbf69869fb -> Single Stick Basic Strikes
67019fc9d112c1dbf6986a04 -> Circular Attack with Shield
67019fc9d112c1dbf69869fc -> Double Stick Sparring
67019fc9d112c1dbf6986a00 -> Single Sword Flow
67019fc9d112c1dbf6986a02 -> Low Stance Defense
67019fc9d112c1dbf6986a03 -> Reverse Grip Stick Defense
67019fc9d112c1dbf69869fd -> Sword and Shield Technique
67019fc9d112c1dbf69869ff -> Shield Block and Strike
67019fc9d112c1dbf6986a01 -> Double Stick Spin

Please provide the exercise IDs in a strict JSON format. The response should be an array of strings, where each string is an ID. For example:
["id1", "id2", "id3", ..., "idN"]. Ensure there are no additional descriptions or explanations. Just provide the array of IDs and not even the name of array .
.`;

const fetchSuggestions = async () => {
    try {
        const response = await model.generateContent(prompt);
        const rawResponseText = response.response.text();
        
        // Clean the response to remove any extra code block formatting, like "```json"
        const cleanedResponseText = rawResponseText.replace(/```json|\n|\`\`\`/g, '').trim();

        // Attempt to parse the cleaned response
        const generatedid = JSON.parse(cleanedResponseText);

        console.log(generatedid); // Debugging output for the generated IDs

        await fetchExerciseDetails(generatedid); // Proceed to fetch the exercise details
    } catch (error) {
        console.error("Error generating content:", error);
        setSuggestions("Sorry, there was an error generating exercise suggestions.");
    } finally {
        setLoading(false);
    }
};


// const fetchExerciseDetails = async (exerciseIDs) => {
//     try {
//         const exercises = await Promise.all(
//             exerciseIDs.map(async (_id) => {
//                 const response = await fetch(`http://localhost:5000/exercises/_id/${_id}`);
//                 const data = await response.json();
//                 return data;
//             })
//         );
//         setExerciseDetails(exercises); // <-- Set the state with the exercise details
//     } catch (error) {
//         console.error("Error fetching exercise details:", error);
//     }
// };
const fetchExerciseDetails = async (exerciseIDs) => {
    try {
        const exercises = await Promise.all(
            exerciseIDs.map(async (_id) => {
                const response = await fetch(`http://localhost:5000/exercises/_id/${_id}`);
                if (!response.ok) return null; // Ignore exercises not found
                const data = await response.json();
                return data;
            })
        );

        // Filter out null values (exercises not found in the database)
        const validExercises = exercises.filter(exercise => exercise !== null);
        setExerciseDetails(validExercises);
    } catch (error) {
        console.error("Error fetching exercise details:", error);
    }
};


fetchSuggestions();
}, [category, focusArea, experienceLevel, duration, medicalCondition, preferences]);




return (
    <div>
        <h2>Exercise Suggestions</h2>
        {loading ? (
            <p>Loading...</p>
        ) : (
            <div className="exercises-container">
                {exerciseDetails.length > 0 ? (
                    exerciseDetails.map((exercise) => (
                        <div key={exercise._id} className="exercises-card">
                            <img 
                                src={exercise.image_link} 
                                alt={exercise.name} 
                                style={{ width: '100%', height: 'auto', borderRadius: '8px' }} 
                            />
                            <h3>{exercise.name}</h3>
                            <p>{exercise.description}</p>
                            <a href={`/exercises/_id/${exercise._id}`} className="exercises-btn">View Details</a>
                        </div>
                    ))
                ) : (
                    <p>No exercises found</p>
                )}
            </div>
        )}
    </div>

//     <div>
//     <h2>Exercise Suggestions</h2>
//     {loading ? (
//         <p>Loading...</p>
//     ) : (
//         <div className="exercises-container">
//             {exerciseDetails.length > 0 ? (
//                 exerciseDetails.map((exercise, index) => (
//                     <div key={exercise._id || index} className="exercises-card">
//                         <img 
//                             src={exercise.image_link} 
//                             alt={exercise.name} 
//                             style={{ width: '100%', height: 'auto', borderRadius: '8px' }} 
//                         />
//                         <h3>{exercise.name}</h3>
//                         <p>{exercise.description}</p>
//                         <a href={`/exercises/${exercise._id}`} className="exercises-btn">View Details</a>
//                     </div>
//                 ))
//             ) : (
//                 <p>No exercises found</p>
//             )}
//         </div>
//     )}
// </div>

);
};

export default ExerciseSuggestions;