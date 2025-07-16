// components/ProfilePhotoUpload.js
import React, { useState } from 'react';
import axios from 'axios';

const PhotoUpload = ({ currentPhoto, onUpload }) => {
    const [isUploading, setIsUploading] = useState(false);
    const [error, setError] = useState('');
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
    const handleFileChange = async (e) => {
        const file = e.target.files[0];
        if (!file) return;

        try {
            setIsUploading(true);
            setError('');
            
            const formData = new FormData();
            formData.append('photo', file);

            const response = await axios.put(
                `${API_BASE_URL}/auth/profile-photo`,
                formData,
                {
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'Authorization': `Bearer ${localStorage.getItem('token')}`
                    }
                }
            );

            onUpload(response.data.profilePhoto);
        } catch (err) {
            setError(err.response?.data?.error || 'Failed to upload photo');
        } finally {
            setIsUploading(false);
        }
    };

    return (
        <div className="profile-photo-upload">
            <div className="current-photo">
                <img 
                    src={currentPhoto} 
                    alt="Profile" 
                    className="profile-image"
                />
            </div>
            
            <label className="upload-button">
                {isUploading ? 'Uploading...' : 'Change Photo'}
                <input 
                    type="file" 
                    accept="image/*"
                    onChange={handleFileChange}
                    disabled={isUploading}
                    style={{ display: 'none' }}
                />
            </label>
            
            {error && <p className="error-message">{error}</p>}
        </div>
    );
};

export default PhotoUpload;