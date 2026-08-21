const submitButton = document.getElementById("submitButton");
const mins = document.getElementById("durationInMinutes");
const date = document.getElementById("dateOfAppointment");
const hairstyle = document.getElementById("hairstyle");

submitButton.addEventListener('click', async () => {
    const data = {
        duration: mins.value,
        date: date.value
    };

    try {
        const response = await fetch("/log-appointment/log", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
    }
    catch (error) {
        status.textContent = error.message;
    }
});