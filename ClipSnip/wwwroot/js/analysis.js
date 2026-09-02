import {
    FaceLandmarker, FilesetResolver
} from "https://cdn.jsdelivr.net/npm/@mediapipe/tasks-vision/vision_bundle.mjs"
import {
    calculateFaceMeasurements,
    calculateFaceRatios,
    FACE_POINTS
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

const faceEditor = document.getElementById("faceEditor");

const status = document.getElementById("status");

let imageRequestId = 0;
let currentImageUrl;
let resultsUpdateId = 0;
let resultsUpdateTimer;
let resultsAbortController;

function clearEditableOverlay() {
    const parent = imagePreview.parentElement;
    if (!parent) return;

    parent.querySelector('#face-overlay')?.remove();
    parent.querySelectorAll('.face-controls').forEach(control => control.remove());
}

function scheduleResultsUpdate(points) {
    const results = document.getElementById('results');
    if (!results) return;

    clearTimeout(resultsUpdateTimer);
    const updateId = ++resultsUpdateId;
    results.hidden = false;
    results.innerHTML = '<p class="text-muted mb-0">Updating recommendations...</p>';

    resultsUpdateTimer = setTimeout(async () => {
        resultsAbortController?.abort();
        resultsAbortController = new AbortController();

        try {
            const measurements = calculateFaceMeasurements(
                points,
                imagePreview.naturalWidth,
                imagePreview.naturalHeight);
            const ratios = calculateFaceRatios(measurements);
            const faceClassifier = classify(ratios);
            const response = await fetch('/hairstyle-finder/analyze', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ data: ratios, result: faceClassifier }),
                signal: resultsAbortController.signal
            });

            if (!response.ok) {
                throw new Error(`Failed to save data: ${response.statusText}`);
            }

            const html = await response.text();
            if (updateId === resultsUpdateId) {
                results.innerHTML = html;
            }
        }
        catch (error) {
            if (error.name !== 'AbortError' && updateId === resultsUpdateId) {
                results.innerHTML = `<p class="text-danger mb-0">${error.message}</p>`;
            }
        }
    }, 100);
}

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

    const requestId = ++imageRequestId;
    const imageUrl = URL.createObjectURL(file);

    clearEditableOverlay();
    if (currentImageUrl) {
        URL.revokeObjectURL(currentImageUrl);
    }
    currentImageUrl = imageUrl;

    imagePreview.src = imageUrl;
    imagePreview.hidden = false;
    faceEditor.hidden = false;

    try {
        const image = new Image();
        image.src = imageUrl;
        await image.decode();

        if (requestId !== imageRequestId) {
            return;
        }

        status.textContent = "Detecting face...";
        const result = detectFace(image);
        const landmarks = getFaceLandmarks(result);

        if (requestId !== imageRequestId) {
            return;
        }

        await imagePreview.decode();
        if (requestId !== imageRequestId) {
            return;
        }

        const initialPoints = createEditableFacePointsFromLandmarks(landmarks);

        setupEditableOverlay(initialPoints);

        status.textContent = "Adjust the markers if needed, then Confirm measurements.";
    }
    catch (error) {
        if (requestId !== imageRequestId) {
            return;
        }
        status.textContent = error.message;
    }
});

// Convert selected landmark indices into normalized {x,y} points keyed by FACE_POINTS names
function createEditableFacePointsFromLandmarks(landmarks) {
    // landmarks are in normalized coordinates from MediaPipe already
    return {
        foreheadTop: { x: landmarks[FACE_POINTS.foreheadTop].x, y: landmarks[FACE_POINTS.foreheadTop].y },
        chin: { x: landmarks[FACE_POINTS.chin].x, y: landmarks[FACE_POINTS.chin].y },

        leftForehead: { x: landmarks[FACE_POINTS.leftForehead].x, y: landmarks[FACE_POINTS.leftForehead].y },
        rightForehead: { x: landmarks[FACE_POINTS.rightForehead].x, y: landmarks[FACE_POINTS.rightForehead].y },

        leftCheek: { x: landmarks[FACE_POINTS.leftCheek].x, y: landmarks[FACE_POINTS.leftCheek].y },
        rightCheek: { x: landmarks[FACE_POINTS.rightCheek].x, y: landmarks[FACE_POINTS.rightCheek].y },

        leftJaw: { x: landmarks[FACE_POINTS.leftJaw].x, y: landmarks[FACE_POINTS.leftJaw].y },
        rightJaw: { x: landmarks[FACE_POINTS.rightJaw].x, y: landmarks[FACE_POINTS.rightJaw].y }
    };
}

// Manage the SVG overlay, draggable points, and confirm/reset controls
function setupEditableOverlay(initialPointsNormalized) {
    const parent = imagePreview.parentElement;
    if (!parent) return;

    if (getComputedStyle(parent).position === 'static') {
        parent.style.position = 'relative';
    }

    clearEditableOverlay();

    const svgNS = 'http://www.w3.org/2000/svg';
    const svg = document.createElementNS(svgNS, 'svg');
    svg.setAttribute('id', 'face-overlay');
    svg.setAttribute('viewBox', `0 0 ${imagePreview.naturalWidth} ${imagePreview.naturalHeight}`);
    svg.style.position = 'absolute';
    svg.style.left = '0';
    svg.style.top = '0';
    svg.style.width = `${imagePreview.offsetWidth}px`;
    svg.style.height = `${imagePreview.offsetHeight}px`;
    svg.style.zIndex = '20';
    svg.style.touchAction = 'none';

    parent.appendChild(svg);

    const pairs = [
        ['foreheadTop', 'chin'],
        ['leftForehead', 'rightForehead'],
        ['leftCheek', 'rightCheek'],
        ['leftJaw', 'rightJaw']
    ];

    const lines = {};
    for (const [a, b] of pairs) {
        const line = document.createElementNS(svgNS, 'line');
        line.setAttribute('stroke', 'rgba(0,150,255,0.9)');
        line.setAttribute('stroke-width', '4');
        line.setAttribute('stroke-linecap', 'round');
        svg.appendChild(line);
        lines[`${a}_${b}`] = line;
    }

    const circles = {};
    const points = JSON.parse(JSON.stringify(initialPointsNormalized));

    function updateOverlayFromPoints() {
        for (const key of Object.keys(points)) {
            const c = circles[key];
            const px = points[key].x * imagePreview.naturalWidth;
            const py = points[key].y * imagePreview.naturalHeight;
            c.setAttribute('cx', px);
            c.setAttribute('cy', py);
        }

        for (const [a, b] of pairs) {
            const line = lines[`${a}_${b}`];
            const ax = points[a].x * imagePreview.naturalWidth;
            const ay = points[a].y * imagePreview.naturalHeight;
            const bx = points[b].x * imagePreview.naturalWidth;
            const by = points[b].y * imagePreview.naturalHeight;
            line.setAttribute('x1', ax);
            line.setAttribute('y1', ay);
            line.setAttribute('x2', bx);
            line.setAttribute('y2', by);
        }
    }

    // Create draggable circles
    for (const key of Object.keys(points)) {
        const circle = document.createElementNS(svgNS, 'circle');
        circle.setAttribute('r', '10');
        circle.setAttribute('fill', 'rgba(255,255,255,0.95)');
        circle.setAttribute('stroke', 'rgba(0,150,255,1)');
        circle.setAttribute('stroke-width', '3');
        circle.style.cursor = 'move';
        svg.appendChild(circle);
        circles[key] = circle;

        let dragging = false;

        circle.addEventListener('pointerdown', (ev) => {
            ev.preventDefault();
            circle.setPointerCapture(ev.pointerId);
            dragging = true;
        });

        circle.addEventListener('pointerup', (ev) => {
            ev.preventDefault();
            try { circle.releasePointerCapture(ev.pointerId); } catch { }
            dragging = false;
        });

        // Move handler on svg, but only act when dragging this circle
        svg.addEventListener('pointermove', (ev) => {
            if (!dragging) return;
            const bbox = svg.getBoundingClientRect();
            const x = ((ev.clientX - bbox.left) / bbox.width) * imagePreview.naturalWidth;
            const y = ((ev.clientY - bbox.top) / bbox.height) * imagePreview.naturalHeight;
            const nx = Math.min(Math.max(x / imagePreview.naturalWidth, 0), 1);
            const ny = Math.min(Math.max(y / imagePreview.naturalHeight, 0), 1);
            points[key].x = nx;
            points[key].y = ny;
            updateOverlayFromPoints();
            scheduleResultsUpdate(points);
        });
    }

    updateOverlayFromPoints();

    // Create controls: Reset
    const controls = document.createElement('div');
    controls.className = 'face-controls';
    controls.style.marginTop = '8px';
    controls.style.display = 'flex';
    controls.style.gap = '8px';

    const resetBtn = document.createElement('button');
    resetBtn.type = 'button';
    resetBtn.className = 'btn btn-secondary';
    resetBtn.textContent = 'Reset markers';
    resetBtn.addEventListener('click', () => {

        for (const k of Object.keys(initialPointsNormalized)) {
            points[k].x = initialPointsNormalized[k].x;
            points[k].y = initialPointsNormalized[k].y;
        }
        updateOverlayFromPoints();
        scheduleResultsUpdate(points);
        status.textContent = 'Markers reset. Recommendations updated.';
    });

    controls.appendChild(resetBtn);
    parent.appendChild(controls);

    scheduleResultsUpdate(points);
}
