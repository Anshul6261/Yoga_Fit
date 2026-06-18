1st question:
header {
    text-align: center;
    background-color: #5ed33a;
    padding: 20px;
}

h2 {
    text-align: center;
    color: #0779e4;
}

img {
    border-radius: 5px;
}

.image-container {
    display: flex;
    flex-direction: row;
    gap: 20px;
    align-items: flex-start;
}

.image-container p {
    margin: 0;
}




2nd question:
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>SkillUp Login Form</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>

    <h1>Welcome to SkillUp - Learn, Grow, Succeed!</h1>

    <h2>LOGIN FORM</h2>

    <div class="login-form">
        <form>
            <label for="username">Username:</label><br><br>
            <input type="text" id="username"><br><br>

            <label for="password">Password:</label><br><br>
            <input type="password" id="password"><br><br>

            <input type="submit" value="Login">
        </form>
    </div>

</body>
</html>
`

h1 {
    color: rgb(255, 0, 0);
    text-align: center;
}

h2 {
    text-align: center;
}

.login-form {
    width: 400px;
    margin: 0 auto;
    border: 1px solid lightgray;
    border-radius: 5px;
    padding: 30px;
}

#username,
#password {
    width: 100%;
    padding: 10px;
    border-radius: 5px;
    box-sizing: border-box;
}

input[type="submit"] {
    width: 100%;
    padding: 10px;
    background-color: rgb(0, 0, 255);
    color: white;
    border: none;
    border-radius: 5px;
}
