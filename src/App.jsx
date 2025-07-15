import { useState } from 'react'
import './App.css'
import React from 'react';
import { Navigate, Outlet } from 'react-router-dom'
import Navbar from "./Customs/navbar";

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
  <Navbar/>
  <Outlet/>
    </>
  )
}

export default App
