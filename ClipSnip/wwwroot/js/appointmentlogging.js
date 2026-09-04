const submitButton = document.getElementById("submitButton");
const mins = document.getElementById("durationInMinutes");
const date = document.getElementById("dateOfAppointment");
const hairstyle = document.getElementById("hairstyle");

//console.log(ViewBag.hairstyles);
//const hairstyleoptions = [ViewData["test"]];
//console.log(hairstyleoptions);

//hairstyleoptions.forEach((hairstring) => { 
//    const hairoption = document.createElement("option");
//    hairoption.value = hairstring;
//    hairoption.textContent = hairstring;
//    hairstyle.appendChild(hairoption);
//    console.log(hairstring);
//});



submitButton.addEventListener('click', async () => {
    const data = {
        duration: mins.value,
        date: date.value,
        hairstyle: hairstyle.value
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