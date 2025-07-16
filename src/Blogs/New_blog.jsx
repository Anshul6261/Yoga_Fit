import { useState,useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import React from 'react';
function New_blog() {
  const [currentUserName, setCurrentUserName] = useState(null);
  const [profilePhoto, setprofilePhoto] = useState(null);
  const [user, setUser] = useState(null);
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
useEffect(() => {
  const getUser = () => {
    const token = localStorage.getItem('token');
    console.log("Token", token);
    if (!token) return null;
    
    try {
      // Decode the token payload
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload; // Return the entire payload object
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  };

  const userData = getUser();
  if (userData) {
    setUser(userData);
    setCurrentUserName(userData.username);
    setprofilePhoto(userData.profilePhoto);
    
    // For debugging
    console.log("UserName", userData.username);
    console.log("profilephoto", userData.profilePhoto);
  }
}, []);
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    title: '',
    content: ''
  });

  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState(null);

  // Handle input changes
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError(null);

    try {
      // Get current timestamp
      const timestamp = new Date().toISOString();

      // Get author's name and image URL from Clerk
      const authorName = currentUserName || 'Anonymous';
      console.log("Author Name", authorName);
      const imageUrl = profilePhoto || 'https://via.placeholder.com/50'; // Fallback image URL

      // Prepare the payload
      const payload = {
        title: formData.title,
        content: formData.content,
        author: authorName,
        timestamp: timestamp,
        imageUrl: imageUrl // Include the image URL in the payload
      };

      // Send POST request to /api/blogs
      const response = await axios.post(`${API_BASE_URL}/blogs`, payload);

      if (!response.data) {
        throw new Error('Failed to post the discussion.');
      }

      // Optionally, handle the response data
      console.log('Post successful:', response.data);

      // Redirect to the blogs page or show a success message
      navigate('/blogs');
    } catch (err) {
      console.error(err);
      setError(err.message || 'Something went wrong.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <>
     
      <div className="max-w-3xl mx-auto p-6 bg-white rounded-lg shadow-lg mt-10">
        <h2 className="text-3xl font-bold text-gray-800 mb-6">Start a New Discussion</h2>
        <form onSubmit={handleSubmit} className="space-y-4">
          
          {/* Title Input */}
          <div>
            <label htmlFor="title" className="block text-lg font-medium text-gray-700 mb-2">
              Title
            </label>
            <input
              type="text"
              name="title"
              id="title"
              value={formData.title}
              onChange={handleChange}
              className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              placeholder="Enter the title here"
              required
            />
          </div>

          {/* Content Textarea */}
          <div>
            <label htmlFor="content" className="block text-lg font-medium text-gray-700 mb-2">
              Content
            </label>
            <textarea
              name="content"
              id="content"
              value={formData.content}
              onChange={handleChange}
              rows="6"
              className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              placeholder="Write your discussion content here"
              required
            />
          </div>

          {/* Error Message */}
          {error && (
            <div className="text-red-500">
              {error}
            </div>
          )}

          {/* Submit Button */}
          <div>
            <button
              type="submit"
              disabled={isSubmitting}
              className={`w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 ${
                isSubmitting ? 'opacity-50 cursor-not-allowed' : ''
              }`}
            >
              {isSubmitting ? 'Posting...' : 'Post Discussion'}
            </button>
          </div>
        </form>
      </div>
    </>
  );
}

export default New_blog;
