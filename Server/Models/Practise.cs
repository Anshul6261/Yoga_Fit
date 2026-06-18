<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>BMI Calculator</title>
</head>
<body>

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
            const weight = parseFloat(document.getElementById("weight").value);
            const height = parseFloat(document.getElementById("height").value);
            const result = document.getElementById("result");

            if (!gender || !weight || !height) {
                result.innerHTML = "Please fill out all fields.";
                result.style.color = "black";
                return;
            }

            const bmi = (weight / Math.pow(height / 100, 2)).toFixed(2);

            let category = "";
            let color = "";

            if (bmi < 18.5) {
                category = "Underweight";
                color = "blue";
            } else if (bmi >= 18.5 && bmi < 24.9) {
                category = "Normal Weight";
                color = "green";
            } else if (bmi >= 25 && bmi < 29.9) {
                category = "Overweight";
                color = "orange";
            } else {
                category = "Obese";
                color = "red";
            }

            let message = "";

            if (gender === "male") {
                message = "Healthy BMI range for men is 18.5 - 24.9";
            } else {
                message = "Healthy BMI range for women is 18.5 - 24.9";
            }

            result.innerHTML =
                "BMI: " + bmi +
                "<br>Category: " + category +
                "<br>" + message;

            result.style.color = color;
        }
    </script>

</body>
</html>
