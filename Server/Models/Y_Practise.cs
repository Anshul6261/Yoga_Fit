
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>HTML Form</title>
</head>
<body>
    <h1>HTML Form</h1>
    <form>
    <label for="firstName">First Name:</label>
    <input type="text" id="firstName" placeholder="First Name">
 
    <label for="lastName">Last Name:</label>
    <input type="text" id="lastName" placeholder="Last Name">
 
    <label for="age">Age</label>
    <input type="number" id="age" placeholder="Age">
 
    <label for="email">Email</label>
    <input type="text" id="email" placeholder="Email">
 
    <label for="password">Password</label>
    <input type="password" id="password" placeholder="Password">
 
    <h3>Gender:</h3>
    <input type="radio" id="male" name="gender" value="male">
    <label for="male">Male</label>
   
    <input type="radio" id="female" name="gender" value="female">
    <label for="female">Female</label>
 
    <h3>Select your device:</h3>
 
    <input type="checkbox" id="macbook" name="device" value="macbook">
    <label for="macbook">Macbook</label>
   
    <input type="checkbox" id="iphone" name="device" value="iphone">
    <label for="iphone">iphone</label>
 
    <input type="checkbox" id="ipad" name="device" value="ipad">
    <label for="ipad">ipad</label>
    </br>
 
    <label for id="aboutus">How did you hear about us?</label>
    <select type="select" id="aboutus">
        <option>Search Engine</option>
        <option>Option 1</option>
        <option>Option 2</option>
    </select>
    </br>
   
    <input type="checkbox" id="news" name="news" value="news">
    <label for="news">Sign me up for News</label>
    </br>
 
    <label for="choose">Upload your photo</label>
    <input type="button" id="choose" value="Choose">
    </br>
 
    <label for="dob">Date of Birth:</label>
    <input type="date" id="dob">
 
    </br>
    <button type="button">Reset</button>
 
    <button type="button">Sign up</button>
    </br>
 
    </form>
</body>
</html>
 
