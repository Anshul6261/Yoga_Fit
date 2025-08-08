import { useState } from 'react'
import './App.css'
import React from 'react';
import { Navigate, Outlet } from 'react-router-dom'
import Navbar from "./Customs/navbar";
import Footer from "./Customs/footer";
function App() {
  const [count, setCount] = useState(0)

  return (
    <>
  <Navbar/>
  <Outlet/>
  <Footer/>
    </>
  )
}

export default App
