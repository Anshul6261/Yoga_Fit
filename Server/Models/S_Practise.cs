import React, { useState } from 'react'
import NavBar from './NavBar'
 
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
  const handleSubmit = (e)=>{
    e.preventDefault();
    let allError = validation();
    setError(allError);
    if(Object.keys(error).length===0)
    {
      setIsSub(true);
 
      setFormData({
        title:"",
        description:""
      })
 
      setTimeout(()=>{
        setIsSub(false);
      },3000)
    }
  }
 
  return (
    <div>
        <NavBar/>
        <form onSubmit={handleSubmit}>
          {isSub && <p>Survey added sucessfully!</p>}
        <div>
            <button type="button">Back</button>
            <h1>Add Survey</h1>
            <input type="text" placeholder="Survey Title" value={formData.title} onChange={handleChange}/>
            {error.title&& <p>{error.title}</p>}
        </div>
        <div>
          <textarea placeholder='Survey Description' value={formData.description} onChange={handleChange}></textarea>
          {error.description && <p>{error.description}</p>}
        </div>
        <button type='submit' >Save</button>
        </form>
    </div>
  )
}
 
export default SurveyForm
 
