import {
    FaceLandmarker, FilesetResolver
} from "https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision/vision_bundle.mjs"
import {
    calculateFaceMeasurements,
    calculateFaceRatios,
} from "./faceMeasurements.js";
import {
    classify
} from "./faceClassifier.js";

let faceLandmarker;

async function initializeMediaPipe() {
    const vision = await FilesetResolver.forVisionTasks(
        "https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision@latest/wasm"
    );

    faceLandmarker = await FaceLandmarker.createFromOptions(
        vision,
        {
            baseOptions: {
                modelAssetPath:
                    "https://storage.googleapis.com/mediapipe-models/face_landmarker/face_landmarker/float16/1/face_landmarker.task"
            },
            runningMode: "IMAGE",
            numFaces: 1
        }
    );

    console.log("MediaPipe initialized");
}

await initializeMediaPipe();

function detectFace(image) { return faceLandmarker.detect(image); }


const imageInput = document.getElementById("imageInput");

const imagePreview = document.getElementById("imagePreview");

const status = document.getElementById("status");

function getFaceLandmarks(result) {
    if (!result.faceLandmarks || result.faceLandmarks.length === 0) {
        throw new Error("No face detected.");
    }

    if (result.faceLandmarks.length > 1) {
        throw new Error("Multiple faces detected. Please provide an image with a single face.");
    }
    return result.faceLandmarks[0];
}


imageInput.addEventListener("change", async () => {
    const file = imageInput.files?.[0];
    if (!file) {
        return;
    }

    const imageUrl = URL.createObjectURL(file);

    imagePreview.src = imageUrl;
    imagePreview.hidden = false;

    await imagePreview.decode();

    status.textContent = "Analyzing...";

    const result = detectFace(imagePreview);

    try
    {
        const landmarks = getFaceLandmarks(result);

        const measurements = calculateFaceMeasurements(landmarks, imagePreview.naturalWidth, imagePreview.naturalHeight);
        const ratios = calculateFaceRatios(measurements);
        const faceClassifier = classify(ratios);
        console.log("Data: ", ratios);
        console.log(`Detected face shape: ${faceClassifier}`);
        status.textContent = `Detected face shape: ${faceClassifier}`;
        const data = { data: ratios, result: faceClassifier }

        const response = await fetch("/hairstyle-finder/analyze", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

        if (!response.ok) {
            throw new Error(`Failed to save data: ${response.statusText}`);
        }

        // Server returns an HTML partial. Insert it into the results container.
        const html = await response.text();
        const results = document.getElementById("results");
        if (results) {
            results.hidden = false;
            results.innerHTML = html;
        }
    }
    catch (error)
    {
        status.textContent = error.message;
    }
});