!st one

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>HTML PRACTICE</title>
</head>
<body>
    <h1>One</h1>
    <hr>

    <h2>Two</h2>
    <hr>

    <h3>Three</h3>
    <hr>

    <h4>Four</h4>
    <hr>

    <h5>Five</h5>
    <hr>

    <h6>Six</h6>
    <hr>
</body>
</html>


2nd One
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
        <label for="fname">First Name:</label>
        <input type="text" id="fname" name="fname" placeholder="First Name">

        <label for="lname">Last Name:</label>
        <input type="text" id="lname" name="lname" placeholder="Last Name">

        <label for="age">Age</label>
        <input type="number" id="age" name="age" placeholder="Age">

        <br><br>

        <label for="email">Email</label>
        <input type="email" id="email" name="email" placeholder="Email">

        <label for="password">Password</label>
        <input type="password" id="password" name="password" placeholder="Password">

        <br><br><br>

        <h3>Gender:</h3>

        <input type="radio" id="male" name="gender" value="Male">
        <label for="male">Male</label>

        <input type="radio" id="female" name="gender" value="Female">
        <label for="female">Female</label>

        <br><br><br>

        <h3>Select your device:</h3>

        <input type="checkbox" id="macbook" name="device" value="Macbook">
        <label for="macbook">Macbook</label>

        <input type="checkbox" id="iphone" name="device" value="iphone">
        <label for="iphone">iphone</label>

        <input type="checkbox" id="ipad" name="device" value="ipad" checked>
        <label for="ipad">ipad</label>

        <br><br>

        <label for="source">How did you hear about us?</label>

        <select id="source" name="source">
            <option selected>Search Engine</option>
            <option>Social Media</option>
            <option>Friend</option>
            <option>Advertisement</option>
        </select>

        <br><br>

        <input type="checkbox" id="news" name="news" checked>
        <label for="news">Sign me up for News</label>

        <br><br>

        <label for="photo">Upload your photo</label>
        <input type="file" id="photo" name="photo">

        <br><br>

        <label for="dob">Date of Birth:</label>
        <input type="date" id="dob" name="dob">

        <br><br>

        <input type="reset" value="Reset">
        <input type="submit" value="Sign up">

    </form>

</body>
</html>

