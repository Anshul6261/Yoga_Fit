<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>BMI Calculator</title>

    <style>
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
            box-sizing: border-box;
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
    </style>
</head>
<body>

    <div class="container">
        <h1>BMI Calculator</h1>

        <form id="bmi-form">
            <label for="gender">Gender:</label>

            <select id="gender">
                <option value="">Select Gender</option>
                <option value="male">male</option>
                <option value="female">female</option>
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

            const bmi = (
                parseFloat(weight) /
                Math.pow(parseFloat(height) / 100, 2)
            ).toFixed(2);

            let category = "";
            let color = "";
            let message = "";

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

            if (gender === "male") {
                message = "Healthy BMI range for men is 18.5 - 24.9";
            } else {
                message = "Healthy BMI range for women is 18.5 - 24.9";
            }

            result.innerHTML =
                "BMI: " + bmi +
                "<br>" +
                category +
                "<br>" +
                message;

            result.style.color = color;
        }
    </script>

</body>
</html>
