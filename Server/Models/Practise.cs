    import React, {useState} from 'react'
 
    const ContactForm = () => {
 
        const [formData, setformData]= useState(
            {
                firstName: '',
                lastName: '',
                age: '',
                email: '',
                phone: '',
                address: '',
                pincode: '',
                message: ''
            }
        )
        const [errors, setError]=useState({});
        const [isSubmitted, setIsSubmitted]=useState(false)
 
        const handleChange= (e)=>{
            const {name, value}= e.target
            setformData({...formData, [name]: value})
        }
 
        const validateForm=()=>{
            const errors={}
 
            if(!formData.firstName.trim()){
                errors.firstName="First name is required"
            }
 
            if(!formData.lastName.trim()){
                errors.lastName="Last name is required"
            }
 
            if(!formData.age.trim()){
                errors.age="Age is required"
            }
            else if(isNaN(formData.age)|| formData.age<=0){
                errors.age="Invalid age"
            }
 
            if(!formData.email.trim()){
                errors.email="Email is required"
            }
            else if(!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)){
                errors.email="Invalid email format"
            }
 
            if(!formData.phone.trim()){
                errors.phone="Phone number is required"
            }
            else if(!/^\d{10}$/.test(formData.phone)){
                errors.phone="Invalid phone number"
            }
 
            if(!formData.address.trim()){
                errors.address="Address is required"
            }
 
            if(!formData.pincode.trim()){
                errors.pincode="Pincode is required"
            }
            else if(!/^\d{6}$/.test(formData.pincode)){
                errors.pincode="Invalid pincode"
            }
 
            if(!formData.message.trim()){
                errors.message="Message is required"
            }
            return errors
        }
 
        const handleSubmit=(e)=>{
            e.preventDefault()
            const valErrors= validateForm()
            setError(valErrors)
 
            if(Object.keys(valErrors).length===0){
                setIsSubmitted(true)
                console.log("Form submitted successfully", formData)
                setformData({
                firstName: '',
                lastName: '',
                age: '',
                email: '',
                phone: '',
                address: '',
                pincode: '',
                message: ''
                })
 
            }
                setTimeout(()=>{
                    setIsSubmitted(false)
                }, 3000 )
           
        }
 
 
    return (
        <div>
            <h2>Contact Form</h2>
            {isSubmitted && <p>Form submitted successfully!</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="firstName">First Name:</label>
                    <input type="text" id="firstName" name="firstName" value={formData.firstName} onChange={handleChange}/>
                    {errors.firstName && <p>{errors.firstName}</p>}
                </div>
                <div>
                    <label htmlFor="lastName">Last Name:</label>
                    <input type="text" id="lastName" name="lastName" value={formData.lastName} onChange={handleChange}/>
                    {errors.lastName && <p>{errors.lastName}</p>}
                </div>
                <div>
                    <label htmlFor="age">Age:</label>
                    <input type="text" id="age" name="age" value={formData.age} onChange={handleChange}/>
                    {errors.age && <p>{errors.age}</p>}
                </div>
                <div>
                    <label htmlFor="email">Email:</label>
                    <input type="text" id="email" name="email" value={formData.email} onChange={handleChange}/>
                    {errors.email && <p>{errors.email}</p>}
                </div>
                <div>
                    <label htmlFor="phone">Phone:</label>
                    <input type="text" id="phone" name="phone" value={formData.phone} onChange={handleChange}/>
                    {errors.phone && <p>{errors.phone}</p>}
                </div>
                <div>
                    <label htmlFor="address">Address:</label>
                    <input type="text" id="address" name="address" value={formData.address} onChange={handleChange}/>
                    {errors.address && <p>{errors.address}</p>}
                </div>
                <div>
                    <label htmlFor="pincode">Pincode:</label>
                    <input type="text" id="pincode" name="pincode" value={formData.pincode} onChange={handleChange}/>
                    {errors.pincode && <p>{errors.pincode}</p>}
                </div>
                <div>
                    <label htmlFor="message">Message:</label>
                    <input type="text" id="message" name="message" value={formData.message} onChange={handleChange}/>
                    {errors.message && <p>{errors.message}</p>}
                </div>
                <button type="submit">Submit</button>
            </form>
        </div>
    )
    }
 
    export default ContactForm
 
