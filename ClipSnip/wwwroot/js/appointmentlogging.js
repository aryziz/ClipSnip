const submitButton = document.getElementById("submitButton");
const mins = document.getElementById("durationInMinutes");
const date = document.getElementById("dateOfAppointment");
const hairstyle = document.getElementById("hairstyle");

date.valueAsDate = new Date();

submitButton.addEventListener('click', async () => {
    const data = {
        duration: mins.value,
        date: date.value,
        hairstyle: hairstyle.value
    };
    console.log(data);

    var message = "";
    try {
        const response = await fetch("/log-appointment/log", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
        message = "Your appointment was succesfully logged";
    }
    catch (error) {
        status.textContent = error.message;
        message = "Log failed:\n" + error.message;
    }
    alert(message);
});