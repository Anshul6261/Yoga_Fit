1st qustion:
<div class="container">
    <h1>BMI Calculator</h1>

    <form id="bmi-form">
        <label for="gender">Gender:</label>
        <select id="gender">
            <option value="">Select Gender</option>
            <option value="male">Male</option>
            <option value="female">Female</option>
        </select>

        <label for="weight">Weight (kg):</label>
        <input type="number" id="weight">

        <label for="height">Height (cm):</label>
        <input type="number" id="height">

        <button type="button" onclick="calculateBMI()">Calculate</button>
    </form>

    <div id="result" class="result"></div>
</div>

<script>
function calculateBMI() {
    const gender = document.getElementById("gender").value;
    const weight = document.getElementById("weight").value;
    const height = document.getElementById("height").value;
    const result = document.getElementById("result");

    if (!gender || !weight || !height) {
        result.innerHTML = "Please fill out all fields.";
        result.style.color = "black";
        return;
    }

    const h = height / 100;
    const bmi = (weight / (h * h)).toFixed(2);

    let category = "";
    let color = "";
    let message = "";

    if (bmi < 18.5) {
        category = "Underweight";
        color = "blue";
    } else if (bmi < 25) {
        category = "Normal Weight";
        color = "green";
    } else if (bmi < 30) {
        category = "Overweight";
        color = "orange";
    } else {
        category = "Obese";
        color = "red";
    }

    if (gender === "male") {
        message = "Healthy BMI range for men is 18.5 - 24.9";
    } else {
        message = "Healthy BMI range for women is 18.5 - 24.9";
    }

    result.innerHTML =
        `BMI: ${bmi}<br>Category: ${category}<br>${message}`;
    result.style.color = color;
}
</script>

body {
    font-family: Arial, sans-serif;
    background: #f2f2f2;
}

.container {
    width: 400px;
    margin: 40px auto;
    padding: 20px;
    background: white;
    border-radius: 10px;
}

h1 {
    text-align: center;
}

label,
select,
input,
button {
    display: block;
    width: 100%;
    margin-bottom: 15px;
}

select,
input {
    padding: 10px;
}

button {
    padding: 10px;
    background: #0d6efd;
    color: white;
    border: none;
    cursor: pointer;
}

.result {
    text-align: center;
    font-size: 20px;
    margin-top: 15px;
}
