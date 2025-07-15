import React, { Children } from 'react';
import { StrictMode } from 'react'
import ReactDOM from "react-dom/client";
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import './index.css'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
// import Navbar from './Customs/navbar.jsx';
import All_Exercises from './Exercises/All_Exercises.jsx'
import Blogs from './Blogs/Blogs.jsx'
import About from './About/AboutUs.jsx'
import Signup from './Registration/Signup.jsx'
import Login from './Registration/Login.jsx'
import HomePage from './Home/HomePage.jsx'
import Exercises_Dashboard from './Exercises/Exercise_Dashboard.jsx'
import Fetch_exercises_by_category from './Exercises/fetch_exercises_by_category.jsx'
import Fetch_exercises_by_id from './Exercises/Fetch_exercises_by_id.jsx';
import Ai_Exercises_plans from './Exercises/Ai_Exercises_plans.jsx';
import Fetch_exercises_by_ai  from './Exercises/Fetch_exercises_by_ai.jsx';
import Subscription from './Exercises/Subscription.jsx';
import New_blog from './Blogs/New_blog.jsx'
import BlogDetails from './Blogs/BlogDetails.jsx'
import PhotoUpload from './Registration/PhotoUpload.jsx';   
const router = createBrowserRouter(
  [
    {
      element:<App/>,
      children:[
        {
          path:'/',
          element:<HomePage/>
        }
    ,    
        {
          path:'/all_exercises',
          element:<All_Exercises/>
        },
        {
          path:'/blogs',
          element:<Blogs/>
        },
        {
          path:'/about',
          element:<About/>
        },
        {
          path:'/Signup',
          element:<Signup/>
        },
        {
          path:'/Login',
          element:<Login/>
        },
        {
          path:'exercises_dashboard',
          element:<Exercises_Dashboard/>
        },
        {
          path:'/exercises/:category',
          element:<Fetch_exercises_by_category/>
        },
        {
          path:'/exercises/_id/:_id',
          element:<Fetch_exercises_by_id/>
        },
        {
          path:'/exercises/Ai-plans',
          element:<Ai_Exercises_plans/>
        },
        {
          path:'/exercise/ai_suggestion',
          element:<Fetch_exercises_by_ai/>
        },
        {
          path:'/subscription',
          element:<Subscription/>
        },
        {
          path:'/blogs/new',
          element:<New_blog/>
          },
          {
          path:'/blogs/:id',
          element: <BlogDetails/>
          },
          {
          path: '/blogs',
          element: <Blogs />  // Add route for Blogs
          },
          {
          path:'photo_upload',
          element:<PhotoUpload/>
          }
      ],
    },

  ]
);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router}></RouterProvider>
  </StrictMode>,
  // <RouterProvider router={router}></RouterProvider>
);
