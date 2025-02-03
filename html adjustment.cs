<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>File Download</title>
    <style>
        /* Basic styling for the page */
        body {
            font-family: Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f0f0f0;
            margin: 0;
        }

        .container {
            text-align: center;
        }

        button {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #4CAF50;
            color: white;
            border: none;
            cursor: pointer;
        }

        button:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>

    <div class="container">
        <h1>Download File</h1>
        <!-- Button to trigger download -->
        <button id="download-btn">Download File</button>
    </div>

    <script>
        document.getElementById('download-btn').addEventListener('click', function() {
            const fileId = 1; // Example file ID. Replace with dynamic value if needed.

            // Send request to backend to fetch file info
            fetch(`/Download/GetFile?id=${fileId}`)
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        // Create a temporary link to start downloading the file
                        const link = document.createElement('a');
                        link.href = data.fileUrl;
                        link.download = data.fileName;
                        link.click();  // Trigger the download
                    } else {
                        alert(data.message || 'Error fetching the file!');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('Something went wrong!');
                });
        });
    </script>

</body>
</html>
