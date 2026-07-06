dotnetapp/Program.cs :
 
using dotnetapp.Data;
using dotnetapp.Services;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Services;
 
var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
 
builder.Services.AddControllers();
builder.Services.AddScoped<SurveyService>();
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"))
);
// Learn more about configuring Swagger/OpenAPI at Get started with Swashbuckle and ASP.NET Core
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
 
builder.Services.AddCors(options=>options.AddPolicy("AllowFrontend",policy=>{
    policy.WithOrigins("https://8081-ddfafcebedf351059707eadcbaafbcaone.premiumproject.examly.io")
    .AllowAnyHeader()
    .AllowAnyMethod();
})
 
);
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseCors("AllowFrontend");
 
app.UseHttpsRedirection();
 
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
 
Get started with Swashbuckle and ASP.NET Core
Learn how to add Swashbuckle to your ASP.NET Core web API project to integrate the Swagger UI.
 
reactapp/src/Components/NavBar.jsx :
 
import React from 'react'
import {Link} from 'react-router-dom'
 
const NavBar = () => {
  return (
    <div className='header'>
        <nav>
        <h1>Online Survey and Analysis Platform</h1>
        <Link to="/add"><button className='btn add'>Add Survey</button></Link>
        <Link to="/surveys"><button className='btn view'>Surveys</button></Link>
        </nav>
    </div>
  )
}
 
export default NavBar
 
reactapp/src/Components/SurveyForm.jsx :
 
 
import React, {useState} from 'react'
import {useNavigate} from 'react-router-dom'
import axios from "axios";
import { useParams } from 'react-router-dom';
const SurveyForm = ({mode}) => {
 
  const navigate = useNavigate();
 
  const [formData, setFormData] = useState({
    title: "",
    description: ""
  })
 
  const [errors, setErrors] = useState({})
 
  const [message, setMessage] = useState("")
 
  const HandleChange = (e) => {
    const {name,value} = e.target
    setFormData({...formData,[name]:value})
  }
 
  const validateForm = () =>{
    const error = {}
    if(!formData.title.trim())
    {
      error.title="Title is required"
    }
    if(!formData.description.trim())
    {
      error.description="Description is required"
    }
    return error;
  }
  const HandleSubmit = async (e) => {
    e.preventDefault()
    const submitErrors = validateForm()
    setErrors(submitErrors)
    if(Object.keys(submitErrors).length===0)
    {
      if(mode==="add"){
        await send();
        setMessage("Survey added successfully!")
      }
      else{
        await update();
        setMessage("Survey updated successfully!")
      }
 
      setFormData({
        title: "",
        description: ""
      })
     
     
      setTimeout(()=>{
        navigate("/")
      },3000)
 
       // axios //
 
     
 
    }
 
   
  }
 
  //axios//
 
  const send = async()=>{
    try{
      const res = await axios.post("https://8080-ddfafcebedf351059707eadcbaafbcaone.premiumproject.examly.io/api/surveys",{
        title:formData.title,
        description:formData.description,
      });
 
      console.log(res.data);
 
    }
    catch(err){
      console.error(err.message)
    }
  }
 
const {id} = useParams()
const update = async() => {
  try
  {
   
    const response = await axios.put(`https://8080-ddfafcebedf351059707eadcbaafbcaone.premiumproject.examly.io/api/surveys/${id}`, {
      title:formData.title,
      description:formData.description,
    });
    console.log(response.data);
  }
  catch(err){
    console.error(err.message)
  }
}
 
  return (
    <div className='form-card'>
      <button className='back' type="button" onClick={() => navigate("/")}>Back</button>
          <form onSubmit={HandleSubmit}>
          <h2>{mode === "add" ? "Add Survey" : "Edit Survey"}</h2>
          <div>
            <input type="text" placeholder='Survey Title' name="title" value={formData.title} onChange={HandleChange}/>
            {errors.title && <p className='error'>{errors.title}</p>}
            <br/>
          </div>
          <div>
            <textarea placeholder='Survey Description' name="description" value={formData.description} onChange={HandleChange}></textarea>
            {errors.description && <p className='error'>{errors.description}</p>}
            <br/>
          </div>
          <button className='success' type="submit">{mode === "add" ? "Save" : "Update"}</button>
          </form>
          {message && (<p>{message}</p>)}
    </div>
  )
}
export default SurveyForm
 
reactapp/src/Components/SurveyList.jsx :
 
 
import React, {useState, useEffect} from 'react'
import axios from 'axios'
import {useNavigate} from "react-router-dom"
 
const SurveyList = () => {
  const [data,setData] = useState([])
  const [message, setMessage] = useState("")
  const [error, setError] = useState("")
 
  const navigate = useNavigate()
 
  const fetchData = async () => {
    try
    {
      const response = await axios.get(`https://8080-ddfafcebedf351059707eadcbaafbcaone.premiumproject.examly.io/api/surveys`)
      setData(response.data)
    }
    catch(err)
    {
      setError("Failed to load surveys.")
    }
  }
 
  useEffect(()=>{
    const sunny = async()=>{
      await fetchData()
    }
    sunny();
  },[])
 
  const HandleDelete = async (surveyId) => {
    try{
      await axios.delete(`https://8080-ddfafcebedf351059707eadcbaafbcaone.premiumproject.examly.io/api/surveys/${surveyId}`)
      setMessage("Successfully deleted.")
      await fetchData()
    }
    catch{
      setError("Error deleting survey")
    }
  }
 
  if (error) return <p>{error}</p>
 
  return(
    <div className='container'>
      <h1 className="header">Online Survey and Analysis Platform</h1>
      {message && <p>{message}</p>}
    {data.length === 0 ? (<p>No surveys available</p>) : (data.map((survey) => (<div className='card' key={data.surveyId}>
      <h3>{survey.title} </h3>
      <p>{survey.description}</p>
      <div className='actions'>
      <button className="edit" onClick={()=>navigate(`/edit/${survey.surveyId}`)}>Edit</button>
      <button className='delete' onClick={()=>HandleDelete(survey.surveyId)}>Delete</button>
      </div>
    </div>
    ))
    )}
     
      <label htmlFor='Survey 1'>Survey 1</label>
      <label htmlFor='Survey 2'>Survey 2</label>
    </div>
  )
}
export default SurveyList
 
reactapp/src/App.css :
 
 
.App {
  text-align: center;
}
 
.App-logo {
  height: 40vmin;
  pointer-events: none;
}
 
@media (prefers-reduced-motion: no-preference) {
  .App-logo {
    animation: App-logo-spin infinite 20s linear;
  }
}
 
.App-header {
  background-color: #282c34;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  font-size: calc(10px + 2vmin);
  color: white;
}
 
.App-link {
  color: #61dafb;
}
 
@keyframes App-logo-spin {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}
 
body {
  font-family: Arial, Helvetica, sans-serif;
  background: #f6f6f6;
}
 
.header {
  text-align: center;
  margin-bottom: 20px;
}
 
.btn {
  margin: 10px;
  padding: 10px 15px;
}
 
.add {
  background: #ff6b5c;
  color: white;
}
 
.view {
  background:#007bff;
  color: white;
}
 
.container {
  width: 70%;
  margin: auto;
}
 
.card {
  background: white;
  padding: 15px;
  margin: 10px 0;
  border-radius: 8px;
}
 
.actions {
  float: right;
}
 
.edit {
  background: green;
  color: white;
  margin-right: 5px;
}
 
.delete {
  background: red;
  color: white;
}
 
.form-card {
  width: 500px;
  margin: 60px auto;
  background: white;
  padding: 25px 30px;
  border-radius: 12px;
  text-align: center;
}
input,
textarea {
  width: 100%;
  margin: 10px 0;
  padding: 8px;
}
 
.submit{
  background: #ff6b5c;
  color: white;
  padding: 10px;
  width: 100%;
}
 
.back {
  float: left;
}
 
.error {
  color: red;
}
 
.success {
  color: green;
}
 
Get started with Swashbuckle and ASP.NET Core
Learn how to add Swashbuckle to your ASP.NET Core web API project to integrate the Swagger UI.
 
App.js:
 
import React from 'react'
import NavBar from './Components/NavBar'
import SurveyForm from './Components/SurveyForm'
import {BrowserRouter as Router,Routes,Route} from 'react-router-dom'
import SurveyList from './Components/SurveyList'
 
const App = () => {
  return (
    <div>
      <Router>
      <NavBar/>
        <Routes>
          <Route path="/add" element={<SurveyForm/>}></Route>
          <Route path="/surveys" element={<SurveyList/>}></Route>
        </Routes>
      </Router>
    </div>
  )
}
export default App
 
<Route path="/edit/:id" element={<SurveyForm mode="edit"/>}/>
