const THRESHOLDS = {
    similarWidthTolerance: 0.08,

    shortFaceMax: 1.20,

    ovalMin: 1.30,
    ovalMax: 1.55,

    taperedMin: 1.25,
    taperedMax: 1.55,

    oblongMin: 1.55,

    narrowForeheadMax: 0.93,
    narrowJawMax: 0.88,

    wideForeheadMin: 0.98
}

function approximatelyEqual(
    value,
    target,
    tolerance = THRESHOLDS.similarWidthTolerance
) {
    return Math.abs(value - target) <= tolerance;
}

export function classify(ratios) {
    const {
        lengthToWidth,
        foreheadToCheekbone,
        jawToCheekbone,
    } = ratios;

    if (
        lengthToWidth >= THRESHOLDS.oblongMin &&
        approximatelyEqual(foreheadToCheekbone, 1) &&
        approximatelyEqual(jawToCheekbone, 1)
    ) {
        return "oblong";
    }

    if (
        lengthToWidth >= THRESHOLDS.taperedMin &&
        lengthToWidth <= THRESHOLDS.taperedMax &&
        foreheadToCheekbone >= THRESHOLDS.wideForeheadMin &&
        jawToCheekbone <= THRESHOLDS.narrowJawMax
    ) {
        return "heart";
    }

    if (
        lengthToWidth <= THRESHOLDS.shortFaceMax &&
        approximatelyEqual(foreheadToCheekbone, 1) &&
        approximatelyEqual(jawToCheekbone, 1)
    ) {

        return "square";
    }

    if (
        lengthToWidth >= THRESHOLDS.ovalMin &&
        lengthToWidth <= THRESHOLDS.ovalMax &&
        foreheadToCheekbone < 1 && jawToCheekbone < 1
    ) {
        
        return "oval";
    }

    if (
        lengthToWidth >= THRESHOLDS.taperedMin &&
        lengthToWidth <= THRESHOLDS.taperedMax &&
        foreheadToCheekbone <= THRESHOLDS.narrowForeheadMax &&
        jawToCheekbone <= THRESHOLDS.narrowJawMax
    ) {
        return "diamond";
    }

    if (
        lengthToWidth <= THRESHOLDS.shortFaceMax &&
        foreheadToCheekbone < 1 &&
        jawToCheekbone < 1
    ) {
        return "round";
    }

    return "unknown";
}