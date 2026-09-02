export const FACE_POINTS = {
    foreheadTop: 10,
    chin: 152,

    leftForehead: 103,
    rightForehead: 332,

    leftCheek: 234,
    rightCheek: 454,

    leftJaw: 172,
    rightJaw: 397
}

function toPixelFromNormalized(point, imageWidth, imageHeight) {
    return {
        x: point.x * imageWidth,
        y: point.y * imageHeight
    };
}

function calculateDistance(point1, point2) {
    const dx = point2.x - point1.x;
    const dy = point2.y - point1.y;
    return Math.hypot(dx, dy);
}

// New signature: accepts an object of normalized {x,y} points keyed by the FACE_POINTS names
export function calculateFaceMeasurements(pointsNormalized, imageWidth, imageHeight) {
    // Convert normalized points to pixel space
    const p = {};
    for (const key of Object.keys(pointsNormalized)) {
        p[key] = toPixelFromNormalized(pointsNormalized[key], imageWidth, imageHeight);
    }

    const faceLength = calculateDistance(p.foreheadTop, p.chin);
    const foreheadWidth = calculateDistance(p.leftForehead, p.rightForehead);
    const cheekboneWidth = calculateDistance(p.leftCheek, p.rightCheek);
    const jawWidth = calculateDistance(p.leftJaw, p.rightJaw);

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