document.getElementById("cvForm").addEventListener("submit", async function (e) {
  e.preventDefault();

  const fileInput = document.getElementById("cvFile");
  const file = fileInput.files[0];
  const status = document.getElementById("status");

  if (!file || file.type !== "application/pdf") {
    status.textContent = "Please upload a valid PDF file.";
    return;
  }

  const formData = new FormData();
  formData.append("cv", file);

  status.textContent = "Uploading...";

  try {
    const response = await fetch("https://localhost:7160/api/cv/upload", {
      method: "POST",
      body: formData
    });

    if (response.ok) {
      status.textContent = "CV uploaded and sent to Langflow successfully!";
      result = await response.json();
      console.log(result);
      var resultMessage = result.outputs[0].outputs[0].messages[0].message
      document.getElementById("result").innerText = resultMessage;
      document.getElementById("resultContainer").style.removeProperty("visibility");
    } else {
      status.textContent = "Failed to upload CV.";
    }
  } catch (error) {
  }
});