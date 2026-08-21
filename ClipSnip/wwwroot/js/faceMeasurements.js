const FACE_POINTS = {
    foreheadTop: 10,
    chin: 152,

    leftForehead: 103,
    rightForehead: 332,

    leftCheek: 234,
    rightCheek: 454,

    leftJaw: 172,
    rightJaw: 397
}

function toPixel(landmark, imageWidth, imageHeight) {
    return {
        x: landmark.x * imageWidth,
        y: landmark.y * imageHeight
    }
}

function calculateDistance(point1, point2) {
    const dx = point2.x - point1.x;
    const dy = point2.y - point1.y;
    return Math.hypot(dx, dy);
}

function distanceBetweenLandmarks(landmarks, index1, index2, imageWidth, imageHeight) {
    const first = toPixel(landmarks[index1], imageWidth, imageHeight);
    const second = toPixel(landmarks[index2], imageWidth, imageHeight);

    return calculateDistance(first, second);
}

export function calculateFaceMeasurements(
    landmarks,
    imageWidth,
    imageHeight
) {
    const faceLength = distanceBetweenLandmarks(
        landmarks,
        FACE_POINTS.foreheadTop,
        FACE_POINTS.chin,
        imageWidth,
        imageHeight
    );

    const foreheadWidth = distanceBetweenLandmarks(
        landmarks,
        FACE_POINTS.leftForehead,
        FACE_POINTS.rightForehead,
        imageWidth,
        imageHeight
    );

    const cheekboneWidth = distanceBetweenLandmarks(
        landmarks,
        FACE_POINTS.leftCheek,
        FACE_POINTS.rightCheek,
        imageWidth,
        imageHeight
    );

    const jawWidth = distanceBetweenLandmarks(
        landmarks,
        FACE_POINTS.leftJaw,
        FACE_POINTS.rightJaw,
        imageWidth,
        imageHeight
    );


    return {
        faceLength,
        foreheadWidth,
        cheekboneWidth,
        jawWidth
    };
}

export function calculateFaceRatios(measurements) {
    const { faceLength, foreheadWidth, cheekboneWidth, jawWidth } = measurements;

    return {
        lengthToWidth: faceLength / cheekboneWidth,
        foreheadToCheekbone: foreheadWidth / cheekboneWidth,
        jawToCheekbone: jawWidth / cheekboneWidth,
        foreheadToJaw: foreheadWidth / jawWidth
    };
}