import { useEffect, useState } from 'react';
import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';

function BlogDetails() {
  const [blog, setBlog] = useState(null);
  const [comments, setComments] = useState([]);
  const [newComment, setNewComment] = useState('');
  const [newReply, setNewReply] = useState('');
  const [activeReplyBox, setActiveReplyBox] = useState(null);
  const [user, setUser] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();
  const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

  useEffect(() => {
    const fetchBlogAndComments = async () => {
      try {
        const [blogResponse, commentsResponse] = await Promise.all([
          axios.get(`${API_BASE_URL}/blogs/${id}`),
          axios.get(`${API_BASE_URL}/blogs/${id}/comments`)
        ]);

        setBlog(blogResponse.data);
        setComments(commentsResponse.data);
      } catch (err) {
        console.error('Error fetching blog or comments:', err);
      }
    };

    fetchBlogAndComments();
  }, [id]);

  const [currentUserName, setCurrentUserName] = useState(null);
  useEffect(() => {
    const getCurrentUserName = () => {
      const token = localStorage.getItem('token');
      if (!token) return null;

      try {
        const user = JSON.parse(atob(token.split('.')[1]));
        setUser(user);
        return user.fullName || user.username || null;
      } catch (error) {
        console.error('Error decoding token:', error);
        return null;
      }
    };

    const userName = getCurrentUserName();
    setCurrentUserName(userName);
  }, []);

  const handleDelete = async () => {
    const confirmed = window.confirm('Are you sure you want to delete this blog post?');
    if (confirmed) {
      try {
        await axios.delete(`${API_BASE_URL}/blogs/${blog._id}`);
        alert('Blog post deleted successfully!');
        navigate('/blogs');
      } catch (err) {
        console.error('Error deleting the blog post:', err);
        alert('There was an error deleting the blog post. Please try again.');
      }
    }
  };

  const handleAddComment = async () => {
    if (!newComment) return;

    try {
      const response = await axios.post(`${API_BASE_URL}/blogs/${id}/comments`, {
        author: currentUserName,
        content: newComment,
        imageUrl: user.profilePhoto || 'https://via.placeholder.com/50'
      });
      setComments(prevComments => [...prevComments, response.data]);
      setNewComment('');
    } catch (err) {
      console.error('Error adding comment:', err);
    }
  };

  const handleAddReply = async (commentId) => {
    if (!newReply) return;

    try {
      const response = await axios.post(`${API_BASE_URL}/blogs/${id}/comments/${commentId}/replies`, {
        author: currentUserName,
        content: newReply,
        imageUrl: user.profilePhoto || 'https://via.placeholder.com/50'
      });

      setComments(prevComments =>
        prevComments.map(comment => {
          if (comment._id === commentId) {
            const updatedReplies = Array.isArray(comment.replies)
              ? [...comment.replies, response.data]
              : [response.data];
            return { ...comment, replies: updatedReplies };
          }
          return comment;
        })
      );

      setNewReply('');
      setActiveReplyBox(null);
    } catch (err) {
      console.error('Error adding reply:', err);
    }
  };

// console.log(localStorage.getItem('token'));

  
const handleDeleteComment = async (commentId) => {
  const token = localStorage.getItem('token');

  try {
    await axios.delete(`${API_BASE_URL}/blogs/${id}/comments/${commentId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    setComments(prev => prev.filter(c => c._id !== commentId));
  } catch (err) {
    console.error('Error deleting comment:', err);
  }
};


const handleDeleteReply = async (commentId, replyId) => {
  const token = localStorage.getItem('token');
  try {
    await axios.delete(`${API_BASE_URL}/blogs/${id}/comments/${commentId}/replies/${replyId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    setComments(prevComments =>
      prevComments.map(comment => {
        if (comment._id === commentId) {
          const updatedReplies = comment.replies.filter(reply => reply._id !== replyId);
          return { ...comment, replies: updatedReplies };
        }
        return comment;
      })
    );
  } catch (err) {
    console.error('Error deleting reply:', err);
  }
};


  if (!blog) {
    return <div className="text-center text-gray-500 py-10">Loading...</div>;
  }

  const isAuthor = user && (currentUserName === blog.author);

  return (
    <div className="max-w-3xl mx-auto bg-white shadow-md rounded-2xl p-6 mt-10">
      <div className="flex justify-between items-start">
        <h1 className="text-3xl font-bold text-gray-800 leading-snug">{blog.title}</h1>

      </div>

<div className="flex justify-between items-center mt-4 text-gray-500">
  <div className="flex items-center">
    <img src={blog.imageUrl || 'https://via.placeholder.com/50'} alt={blog.title} className="w-10 h-10 rounded-full object-cover mr-3" />
    <span className="text-sm">By <strong>{blog.author}</strong> • {new Date(blog.datePosted).toLocaleDateString()}</span>
  </div>

  {isAuthor && (
    <button
      onClick={handleDelete}
      className="bg-red-500 hover:bg-red-600 text-white text-sm px-4 py-2 rounded-md shadow ml-4"
    >
      Delete
    </button>
  )}
</div>

      <div className="mt-6 text-gray-700 leading-relaxed whitespace-pre-line">{blog.content}</div>


      <div className="mt-8">
        <h3 className="text-xl font-semibold mb-4">💬 Comments</h3>
        {comments.map(comment => (
          <div key={comment._id} className="mb-6 border-t pt-4">
            <div className="flex items-center">
              <img src={comment.imageUrl || 'https://via.placeholder.com/50'} alt={comment.author} className="w-10 h-10 rounded-full object-cover mr-3" />
              <div>
                <p className="font-semibold text-gray-800">{comment.author}</p>
                <span className="text-xs text-gray-500">{new Date(comment.createdAt).toLocaleString()}</span>
              </div>
            </div>
            {/* <p className="mt-2 text-gray-700">{comment.content}</p> */}
            <p className="mt-2 text-gray-700">{comment.content}</p>


  <div className="flex justify-between items-center mt-4">
 <button onClick={() => setActiveReplyBox(comment._id)} className="bg-blue-500 hover:bg-blue-600 text-white text-sm px-4 py-2 rounded-md shadow ml-4">Reply</button>
          
{currentUserName === comment.author && (
  <button
    onClick={() => handleDeleteComment(comment._id)}
    className="bg-red-500 hover:bg-red-600 text-white text-sm px-4 py-2 rounded-md shadow ml-4"
  >
    Delete Comment
  </button>
)}        
  </div>
            {activeReplyBox === comment._id && (
              <div className="mt-4">
                <input
                  type="text"
                  value={newReply}
                  onChange={(e) => setNewReply(e.target.value)}
                  placeholder="Write a reply..."
                  className="border border-gray-300 rounded px-3 py-2 w-full"
                />
                <button
                  onClick={() => handleAddReply(comment._id)}
                  className="bg-blue-600 text-white px-4 py-2 mt-2 rounded-md"
                >
                  Submit Reply
                </button>
              </div>
            )}

            {comment.replies?.map(reply => (
<div key={reply._id || reply.date} className="ml-6 mt-4 p-3 bg-gray-100 rounded-lg">

  <div className="flex justify-between items-start">
    <div className="flex items-center">
      <img
        src={reply.imageUrl || 'https://via.placeholder.com/50'}
        alt={reply.author}
        className="w-8 h-8 rounded-full object-cover mr-2"
      />
      <div>
        <p className="text-sm font-semibold">{reply.author}</p>
        <span className="text-xs text-gray-500 block">
          {new Date(reply.date).toLocaleString()}
        </span>
      </div>
    </div>

    {currentUserName === reply.author && (
      <button
        onClick={() => handleDeleteReply(comment._id, reply._id)}
        className="bg-red-500 hover:bg-red-600 text-white text-sm px-4 py-2 rounded-md shadow ml-4"
      >
        Delete Reply
      </button>
    )}
  </div>

  <p className="mt-2 text-sm text-gray-700">{reply.content}</p>
</div>

            ))}
          </div>
        ))}

        <div className="mt-6">
          <input
            type="text"
            value={newComment}
            onChange={(e) => setNewComment(e.target.value)}
            placeholder="Add a comment..."
            className="border border-gray-300 rounded p-3 w-full"
          />
          <button
            onClick={handleAddComment}
            className="bg-blue-600 text-white px-4 py-2 mt-2 rounded-md"
          >
            Submit Comment
          </button>
        </div>
      </div>
    </div>
  );
}

export default BlogDetails;
