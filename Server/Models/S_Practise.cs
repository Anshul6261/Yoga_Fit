navbar.jsx
 
import React from 'react'
import {Link} from 'react-router-dom'
 
const NavBar = () => {
  return (
    <div>
        <nav>
            <h1>Online Survey and Analysis Platform</h1>
            <Link to ="/add"><button>Add Survey</button></Link>
            <Link to  = "/surveys"><button>Surveys</button></Link>
        </nav>
    </div>
  )
}
 
export default NavBar
 
surveylist.jxs
 
import React from 'react'
import axios from "axios";
import { useState } from 'react';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
const SurveyList = () => {
 
    const[Data, SetData] = useState([]);
    const[msg,setMsg] = useState("");
    const[err,setErr] = useState("");
    const nav = useNavigate();
 
    const fetchData = async()=>{
      try{
        const response = await axios.get("https://8080-bcecbbaaacaed351060496eadcbaafbcaone.premiumproject.examly.io/api/surveys");
        console.log(response);
        SetData(response.data);
      }catch(err){
          console.log(err);
      }
     
   
    }
    useEffect(()=>{
      const fetch = async()=>{
          await fetchData();
      }
      fetch();
    },[]);
 
    const handleDelete = async(surveyId)=>{
      try{
        await axios.delete(`https://8080-bcecbbaaacaed351060496eadcbaafbcaone.premiumproject.examly.io/api/surveys/${surveyId}`);
        setMsg("Successfully deleted.")
        await fetchData()
      }catch{
          setErr("Error deleting survey")
      }
 
    }
    if(err) return <p>{err}</p>
  return (
    <div>
      {/* <h1>Online Survey and Analysis Platform</h1> */}
      {msg &&<p>{msg}</p>}
      {Data.length===0?(<p>No surveys available</p>):(Data.map((survey)=>(
      <div className='card key' key={survey.surveyId}>
          <h3>{survey.title}</h3>
          <p>{survey.description}</p>
          <div>
            <button className='edit' onClick={()=>
              nav(`/edit/${survey.surveyId}`)
            }>Edit
            </button>
 
            <button className='delete' onClick={()=>
              handleDelete(survey.surveyId)
            }>Delete
            </button>
          </div>
      </div>)))}
 
      {/* <label htmlFor="Survey 1">Survey 1</label>
      <label htmlFor="Survey 2">Survey 2</label> */}
    </div>
  )
}
 
export default SurveyList
 
program.cs
using dotnetapp.Data;
using dotnetapp.Services;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("myconnstring"))
);
builder.Services.AddScoped<SurveyService>();
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
 
builder.Services.AddCors(options=>options.AddPolicy("AllowFrontend",policy=>{
    policy
    .WithOrigins("https://8081-bcecbbaaacaed351060496eadcbaafbcaone.premiumproject.examly.io")
    .AllowAnyHeader()
    .AllowAnyMethod();
}));
 
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
 
Get started with Swashbuckle and ASP.NET Core | Microsoft Learn
Learn how to add Swashbuckle to your ASP.NET Core web API project to integrate the Swagger UI.
 
surveyform.jsx
 
import React, { useState } from 'react'
import NavBar from './NavBar'
import axios from "axios";
import { useParams } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
 
const SurveyForm = ({mode}) => {
  const[formData, setFormData]= useState({
    title:"",
    description:""
  })
  const handleChange =(e)=>{
    const {name,value} = e.target;
    setFormData({...formData, [name]:value})
  }
  const[isSub, setIsSub] = useState(false);
  const[error,setError] = useState({});
  const [msg,setMsg] = useState("");
  const nav = useNavigate();
 
  const validation = ()=>{
    let errors = {};
    if(!formData.title.trim())
    {
      errors.title = "Title is required"
    }
    if(!formData.description.trim())
    {
      errors.description = "Description is required"
    }
    return errors;
  }
  const handleSubmit = async(e)=>{
    e.preventDefault();
    let allError = validation();
    setError(allError);
    if(Object.keys(allError).length===0)
    {
      if(mode==="add"){
        await send()
        setMsg("Survey added successfully!")
      }
      else{
        await update();
        setMsg("Survey updated successfully!")
      }
      setIsSub(true);
 
      setFormData({
        title:"",
        description:""
      })
 
      setTimeout(()=>{
        setIsSub(false);
        nav("/")
 
      },3000)
    }
  }
  const send  =  async()=>{
    try{
      const res =  await axios
      .post
      ("https://8080-bcecbbaaacaed351060496eadcbaafbcaone.premiumproject.examly.io/api/surveys",{
        title:formData.title,
        description:formData.description
      });
      console.log(res.data);
    }catch(error){
      console.error(error);
    }
  }
  const{id} = useParams();
 
  const update = async()=>{
    try{
      const res =  
      await axios
      .put
      (`https://8080-bcecbbaaacaed351060496eadcbaafbcaone.premiumproject.examly.io/api/surveys/${id}`,{
        title:formData.title,
        description:formData.description
      })
      console.log(res.data);
    }catch(error){
      console.error(error);
    }
  }
 
  return (
    <div>
        <button type="button" onClick={()=>nav("/")}>Back</button>
 
        <form onSubmit={handleSubmit}>
          <h2>{mode==="add"?"Add Survey":"Edit Survey"}</h2>
        <div>
            <input type="text" placeholder="Survey Title" name='title' value={formData.title} onChange={handleChange}/>
            {error.title&& <p>{error.title}</p>}
        </div>
        <div>
          <textarea placeholder='Survey Description' name='description' value={formData.description} onChange={handleChange}></textarea>
          {error.description && <p>{error.description}</p>}
        </div>
        <button type='submit'className='sucess' >{mode==="add"?"Save":"Update"}</button>
        </form>
        {msg && <p>{msg}</p>}
    </div>
  )
}
 
export default SurveyForm

app.js
import './App.css';
import NavBar from './Components/NavBar';
import SurveyForm from './Components/SurveyForm';
import SurveyList from './Components/SurveyList';
import { BrowserRouter as Router,Routes,Route } from 'react-router-dom';
 
function App() {
  return (
    <div >
      <Router>
        <NavBar/>
        <Routes>
          <Route path='/add' element={<SurveyForm mode="add"/>}></Route>
          <Route path='/surveys' element={<SurveyList/>}></Route>
          <Route path='/edit/:id' element={<SurveyForm mode="edit"/>}></Route>        
        </Routes>
      </Router>
     
    </div>
  );
}
 
export default App;                           


                           
 
