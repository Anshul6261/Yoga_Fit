// import  { useEffect, useState } from 'react';
// import { useNavigate, Link } from 'react-router-dom';
// import axios from 'axios';
// import React from 'react';
// import { set } from 'mongoose';
// const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
// console.log("API_BASE_URL", API_BASE_URL);
// function Blogs() {
// const [currentUserName, setCurrentUserName] = useState(null);
// const [user, setUser] = useState(null);
// const [userID, setUserID] = useState(null);


// useEffect(() => {
//   const getCurrentUserName = () => {
//     const token = localStorage.getItem('token');
//     if (!token) return;

//     try {
//       const user = JSON.parse(atob(token.split('.')[1]));
//       setUser(user);
//       setUserID(user.id);
//       setCurrentUserName(user.fullName || user.username || null);
//     } catch (error) {
//       console.error('Error decoding token:', error);
//     }
//   };

//   getCurrentUserName();
// }, []);
  
// console.log('Decoded User:', user);

//   console.log("userID", userID);
//   const [blogs, setBlogs] = useState([]);
//   const navigate = useNavigate();

//   // console.log("User", user);

//   // Function to navigate to start a discussion
//   const handleStartDiscussion = () => {
//     navigate('/blogs/new');
//   };

//   // Function to fetch blogs from the backend
//   useEffect(() => {
//     fetchBlogs();
//   }, []);

//   const fetchBlogs = async () => {
//     try {
//       const response = await axios.get(`${API_BASE_URL}/blogs`);
//       setBlogs(response.data);
//     } catch (err) {
//       console.error('Error while fetching blogs:', err);
//     }
//   };
//   const handleLike = (blogId ,userID) => {

//     const blog = blogs.find(blog => blog._id === blogId);
//     if (!blog) return;

//     const userAlreadyLiked = blog.likes.find(like => like.userId === userID);
//     if (userAlreadyLiked) {
//       return;
//     }else{
 
//           setBlogs(blogs.map(blog => {
//       if (blog._id === blogId) {
//         return {
//           ...blog,
//           likes: [...blog.likes, { userId: userID, count: 1 }]
//         };
//       }
//       return blog;
//     }));

//     // Then call the server to update the database
//     axios.put(`${API_BASE_URL}/blogs/${blogId}/like`, {userId: userID})
//       .catch(err => console.error('Error while liking the blog:', err));
//     }
    
//   };
  

//   return (
//     <>
//       <div className="community bg-white p-6 rounded-lg shadow-lg w-full max-w-7xl mx-auto mt-10">
//         {/* Community Header with "Start A Discussion" button */}
//         <div className="flex justify-between items-center mb-6 w-full">
//           <p className="text-2xl font-bold text-gray-800">Community</p>
//           <button
//             className="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition"
//             onClick={handleStartDiscussion}
//           >
//             Start A Discussion
//           </button>
//         </div>

//         {/* Blog List */}
//         <div className="space-y-6 w-full">
//         <div className="w-full p-4 bg-gray-50 rounded-lg shadow-sm border border-gray-200">
//   {/* Section Title */}

//   <div className="flex justify-between">
//     <h2 className="text-2xl font-bold text-gray-900 mb-4">Discussions</h2>
//     <h2 className="text-2xl font-bold text-gray-900 mb-4"style={{ paddingRight: '10px' }}>Likes</h2>
//   </div>

//   {blogs.map((blog) => (
//     <div
//       key={blog._id}
//       className="flex items-center p-4 bg-gray-50 rounded-lg shadow-sm border border-gray-200 w-full mb-4"
//       style={{ minHeight: '150px' }}
//     >
//       {/* Blog Image */}
//       {blog.imageUrl && (
//         <div className="w-12 h-12 mr-4">
//           <img
//             src={blog.imageUrl}
//             alt={blog.title}
//             className="w-12 h-12 object-cover rounded-full"
//             style={{ width: '50px', height: '50px' }}
//           />
//         </div>
//       )}

//       {/* Blog Content */}
//       <div className="w-full">
//         <Link to={`/blogs/${blog._id}`} className="no-underline">
//           <h3 className="text-lg font-bold text-gray-900 hover:text-blue-500">
//             {blog.title}
//           </h3>
//         </Link>
//         <p className="text-gray-600">
//           By {blog.author} • {new Date(blog.datePosted).toLocaleDateString()}
//         </p>
//         <p className="text-gray-700">
//           {blog.content.length > 100
//             ? `${blog.content.substring(0, 100)}...`
//             : blog.content}
//           <Link
//             to={`/blogs/${blog._id}`}
//             className="text-blue-500 hover:underline"
//           >
//             {" "}
//             Read More
//           </Link>
//         </p>
//       </div>

//       {/* Like Section */}
//       <div className=" flex flex-col items-center ml-4 rounded-md p-1">
//         <button
//           onClick={() => handleLike(blog._id ,userID) }
//           className=" hover:bg-gray-200 transition text-sm"
//           style={{backgroundColor: 'gray'}}
//         >
//           👍{blog.likes.length}
//         </button>
      
//       </div>
//     </div>
//   ))}
// </div>


//         </div>
//       </div>
//     </>
//   );
// }

// export default Blogs;


import { useEffect, useState } from 'react';
import React from 'react';

import { useNavigate, Link } from 'react-router-dom';
import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

function Blogs() {
  const [currentUserName, setCurrentUserName] = useState(null);
  const [user, setUser] = useState(null);
  const [userID, setUserID] = useState(null);
  const [blogs, setBlogs] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const getCurrentUserName = () => {
      const token = localStorage.getItem('token');
      if (!token) return;
      try {
        const user = JSON.parse(atob(token.split('.')[1]));
        setUser(user);
        setUserID(user.id);
        setCurrentUserName(user.fullName || user.username || null);
      } catch (error) {
        console.error('Error decoding token:', error);
      }
    };
    getCurrentUserName();
  }, []);

  useEffect(() => {
    fetchBlogs();
  }, []);

  const fetchBlogs = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/blogs`);
      setBlogs(response.data);
    } catch (err) {
      console.error('Error while fetching blogs:', err);
    }
  };

  const handleLike = (blogId, userID) => {
    const blog = blogs.find(blog => blog._id === blogId);
    if (!blog) return;

    const userAlreadyLiked = blog.likes.find(like => like.userId === userID);
    if (userAlreadyLiked) {
      return;
    } else {
      setBlogs(blogs.map(blog => {
        if (blog._id === blogId) {
          return {
            ...blog,
            likes: [...blog.likes, { userId: userID, count: 1 }]
          };
        }
        return blog;
      }));

      axios.put(`${API_BASE_URL}/blogs/${blogId}/like`, { userId: userID })
        .catch(err => console.error('Error while liking the blog:', err));
    }
  };

  return (
    <div className="w-full max-w-4xl mx-auto px-4 mt-10" style={{marginBottom:'50px'}}>
      <div className="flex justify-between items-center mb-6">
        <p className="text-2xl font-bold text-gray-800">Community</p>
        <button
          onClick={() => navigate('/blogs/new')}
          className="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition"
        >
          Start A Discussion
        </button>
      </div>

      <div className="space-y-8" >
        {blogs.map(blog => (
          <div
            key={blog._id}
            className="bg-white p-5 rounded-xl shadow-md hover:shadow-lg transition flex gap-4"
          >
            <img
              src={blog.imageUrl || '/default_profile.png'}
              alt={blog.title}
              className="w-14 h-14 rounded-full object-cover"
            />

            <div className="flex-1">
              <div className="flex justify-between items-center">
                <div>
                  <h3 className="font-semibold text-lg text-gray-900">{blog.title}</h3>
                  <p className="text-sm text-gray-600">
                    By {blog.author} • {new Date(blog.datePosted).toLocaleDateString()}
                  </p>
                </div>
                <button
                  onClick={() => handleLike(blog._id, userID)}
                  className="text-gray-600 hover:text-black text-sm flex items-center"
                   style={{backgroundColor: 'white'}}
                >
                  ❤️ {blog.likes.length}
                </button>
              </div>

              <p className="text-gray-700 mt-2 text-sm">
                {blog.content.length > 100
                  ? `${blog.content.substring(0, 100)}...`
                  : blog.content}
                <Link
                  to={`/blogs/${blog._id}`}
                  className="text-blue-500 hover:underline ml-1"
                >
                  Read More
                </Link>
              </p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Blogs;
