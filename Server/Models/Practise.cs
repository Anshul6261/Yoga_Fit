<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tip Calculator</title>

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }

        .container {
            max-width: 600px;
            margin: 50px auto;
            background-color: #fff;
            padding: 20px;
        }

        h1 {
            text-align: center;
        }

        #tipForm {
            display: grid;
            gap: 10px;
        }

        input,
        select {
            padding: 10px;
            font-size: 16px;
        }

        button {
            background-color: #4caf50;
            color: #fff;
            border: none;
            padding: 10px;
            cursor: pointer;
        }

        #result {
            margin-top: 20px;
        }
    </style>
</head>
<body>

    <div class="container">
        <h1>Tip Calculator</h1>

        <form id="tipForm">
            <label for="billAmount">Bill Amount:</label>
            <input type="number" id="billAmount">

            <label for="tipPercentage">Tip Percentage:</label>
            <select id="tipPercentage">
                <option value="5">5%</option>
                <option value="10">10%</option>
                <option value="15">15%</option>
                <option value="20">20%</option>
            </select>

            <button type="button" onclick="calculateTip()">Calculate Tip</button>
        </form>

        <div id="result">
            <p>Tip Amount: <span id="tipAmount">0</span></p>
            <p>Total Amount: <span id="totalAmount">0</span></p>
        </div>
    </div>

    <script>
        function calculateTip() {
            const billAmount = parseFloat(document.getElementById("billAmount").value);
            const tipPercentage = parseFloat(document.getElementById("tipPercentage").value);

            if (isNaN(billAmount) || billAmount <= 0) {
                alert("Please enter a valid bill amount.");
                return;
            }

            const tipAmount = (billAmount * tipPercentage) / 100;
            const totalAmount = billAmount + tipAmount;

            document.getElementById("tipAmount").textContent =
                tipAmount.toFixed(2);

            document.getElementById("totalAmount").textContent =
                totalAmount.toFixed(2);
        }
    </script>

</body>
</html>
