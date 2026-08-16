import {
    FaceLandmarker, FilesetResolver
} from "https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision/vision_bundle.mjs"

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


const imageInput = document.getElementById("imageInput");

const imagePreview = document.getElementById("imagePreview");

const status = document.getElementById("status");