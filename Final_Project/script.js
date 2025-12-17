document.getElementById("regForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Prevent page reload

    // 1. Get Values
    const name = document.getElementById("name").value.trim();
    const email = document.getElementById("email").value.trim();
    const course = document.getElementById("course").value;
    const terms = document.getElementById("terms").checked;

    // For Radio Buttons (Gender)
    let gender = "";
    const genderRadios = document.getElementsByName("gender");
    for (let i = 0; i < genderRadios.length; i++) {
        if (genderRadios[i].checked) {
            gender = genderRadios[i].value;
            break;
        }
    }

    // 2. Reset Errors
    document.querySelectorAll('.error-msg').forEach(el => el.style.display = 'none');

    // 3. Validation Logic (Sequential)

    // Validate Name
    if (name === "") {
        document.getElementById("nameError").style.display = "block";
        document.getElementById("name").focus();
        return; // Stop execution
    }

    // Validate Email
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (email === "" || !emailPattern.test(email)) {
        document.getElementById("emailError").style.display = "block";
        document.getElementById("email").focus();
        return; // Stop execution
    }

    // Validate Gender
    if (gender === "") {
        document.getElementById("genderError").style.display = "block";
        return; // Stop execution
    }

    // Validate Course
    if (course === "") {
        document.getElementById("courseError").style.display = "block";
        document.getElementById("course").focus();
        return; // Stop execution
    }

    // Validate Terms
    if (!terms) {
        document.getElementById("termsError").style.display = "block";
        document.getElementById("terms").focus();
        return; // Stop execution
    }

    // 4. On Successful Submission
    // If we reached here, everything is valid
    const displayDiv = document.getElementById("displayData");
    displayDiv.style.display = "block";
    displayDiv.innerHTML = `
        <h4>Registration Successful!</h4>
        <p><strong>Name:</strong> ${name}</p>
        <p><strong>Email:</strong> ${email}</p>
        <p><strong>Gender:</strong> ${gender}</p>
        <p><strong>Course:</strong> ${course}</p>
    `;

    // Optional: Scroll to bottom to show result
    displayDiv.scrollIntoView({ behavior: 'smooth' });
});
