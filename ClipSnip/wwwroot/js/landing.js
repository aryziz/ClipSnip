import * as THREE from "three";

const canvas = document.getElementById("hero-canvas");
const visual = document.querySelector(".landing-visual");

if (!canvas || !visual) {
    throw new Error("Could not find hero canvas or landing visual");
}

// ============================================================
// Scene
// ============================================================

const scene = new THREE.Scene();

const camera = new THREE.PerspectiveCamera(
    38,
    1,
    0.1,
    100
);

camera.position.set(0, 0, 9);

// ============================================================
// Renderer
// ============================================================

const renderer = new THREE.WebGLRenderer({
    canvas,
    antialias: true,
    alpha: true
});


renderer.setPixelRatio(
    Math.min(window.devicePixelRatio, 2)
);

renderer.outputColorSpace = THREE.SRGBColorSpace;

// ============================================================
// Helpers
// ============================================================

function roundedRect(ctx, x, y, width, height, radius) {
    ctx.beginPath();

    ctx.roundRect(
        x,
        y,
        width,
        height,
        radius
    );

    ctx.closePath();
}

// ============================================================
// Create card texture
// ============================================================

function createCardTexture() {
    const cardCanvas = document.createElement("canvas");

    cardCanvas.width = 1200;
    cardCanvas.height = 760;

    const ctx = cardCanvas.getContext("2d");

    // --------------------------------------------------------
    // Background
    // --------------------------------------------------------

    const gradient = ctx.createLinearGradient(
        0,
        0,
        1200,
        760
    );

    gradient.addColorStop(0, "#17171c");
    gradient.addColorStop(1, "#09090b");

    roundedRect(
        ctx,
        0,
        0,
        1200,
        760,
        55
    );

    ctx.fillStyle = gradient;
    ctx.fill();

    // --------------------------------------------------------
    // Subtle purple glow
    // --------------------------------------------------------

    const glow = ctx.createRadialGradient(
        850,
        160,
        20,
        850,
        160,
        480
    );

    glow.addColorStop(
        0,
        "rgba(139, 92, 246, .22)"
    );

    glow.addColorStop(
        1,
        "rgba(139, 92, 246, 0)"
    );

    ctx.fillStyle = glow;

    ctx.fillRect(
        0,
        0,
        1200,
        760
    );

    // --------------------------------------------------------
    // Header
    // --------------------------------------------------------

    ctx.fillStyle = "#a78bfa";

    ctx.font =
        "600 25px Arial, sans-serif";

    ctx.letterSpacing = "6px";

    ctx.fillText(
        "CLIPSNIP",
        70,
        80
    );

    // Ready badge

    roundedRect(
        ctx,
        965,
        42,
        180,
        58,
        29
    );

    ctx.fillStyle =
        "rgba(139, 92, 246, .15)";

    ctx.fill();

    ctx.fillStyle = "#c4b5fd";

    ctx.font =
        "600 21px Arial, sans-serif";

    ctx.fillText(
        "✓ READY",
        995,
        79
    );

    // --------------------------------------------------------
    // Hairstyle preview container
    // --------------------------------------------------------

    roundedRect(
        ctx,
        65,
        135,
        380,
        390,
        35
    );

    ctx.fillStyle = "#202027";
    ctx.fill();

    // --------------------------------------------------------
    // Fake portrait / hairstyle preview
    // --------------------------------------------------------

    // neck

    ctx.fillStyle = "#8c6b5b";

    roundedRect(
        ctx,
        207,
        370,
        95,
        130,
        30
    );

    ctx.fill();

    // ears

    ctx.beginPath();

    ctx.arc(
        175,
        315,
        30,
        0,
        Math.PI * 2
    );

    ctx.arc(
        335,
        315,
        30,
        0,
        Math.PI * 2
    );

    ctx.fill();

    // head

    ctx.beginPath();

    ctx.ellipse(
        255,
        305,
        95,
        130,
        0,
        0,
        Math.PI * 2
    );

    ctx.fill();

    // hair silhouette

    ctx.fillStyle = "#171717";

    ctx.beginPath();

    ctx.moveTo(165, 290);

    ctx.bezierCurveTo(
        160,
        175,
        215,
        150,
        270,
        160
    );

    ctx.bezierCurveTo(
        355,
        170,
        360,
        230,
        345,
        285
    );

    ctx.bezierCurveTo(
        320,
        250,
        310,
        215,
        280,
        230
    );

    ctx.bezierCurveTo(
        255,
        195,
        230,
        230,
        210,
        205
    );

    ctx.bezierCurveTo(
        180,
        220,
        175,
        260,
        165,
        290
    );

    ctx.fill();

    // --------------------------------------------------------
    // Hairstyle information
    // --------------------------------------------------------

    ctx.fillStyle = "#a1a1aa";

    ctx.font =
        "500 21px Arial, sans-serif";

    ctx.fillText(
        "YOUR STYLE",
        505,
        165
    );

    ctx.fillStyle = "#ffffff";

    ctx.font =
        "700 54px Arial, sans-serif";

    ctx.fillText(
        "Textured Crop",
        505,
        225
    );

    ctx.fillStyle = "#a1a1aa";

    ctx.font =
        "400 26px Arial, sans-serif";

    ctx.fillText(
        "Low taper · Natural texture",
        505,
        270
    );

    // --------------------------------------------------------
    // Time
    // --------------------------------------------------------

    roundedRect(
        ctx,
        505,
        320,
        290,
        95,
        22
    );

    ctx.fillStyle =
        "rgba(255,255,255,.055)";

    ctx.fill();

    ctx.fillStyle = "#a78bfa";

    ctx.font =
        "600 20px Arial, sans-serif";

    ctx.fillText(
        "ESTIMATED TIME",
        535,
        352
    );

    ctx.fillStyle = "#ffffff";

    ctx.font =
        "700 31px Arial, sans-serif";

    ctx.fillText(
        "30–40 min",
        535,
        393
    );

    // --------------------------------------------------------
    // Face shape
    // --------------------------------------------------------

    roundedRect(
        ctx,
        800,
        320,
        305,
        95,
        22
    );

    ctx.fillStyle =
        "rgba(255,255,255,.055)";

    ctx.fill();

    ctx.fillStyle = "#a78bfa";

    ctx.font =
        "600 20px Arial, sans-serif";

    ctx.fillText(
        "FACE SHAPE",
        830,
        352
    );

    ctx.fillStyle = "#ffffff";

    ctx.font =
        "700 31px Arial, sans-serif";

    ctx.fillText(
        "Oval",
        830,
        393
    );

    // --------------------------------------------------------
    // Barber notes
    // --------------------------------------------------------

    ctx.fillStyle = "#71717a";

    ctx.font =
        "600 18px Arial, sans-serif";

    ctx.fillText(
        "BARBER NOTES",
        505,
        475
    );

    ctx.fillStyle = "#e4e4e7";

    ctx.font =
        "400 24px Arial, sans-serif";

    ctx.fillText(
        "Keep length on top.",
        505,
        515
    );

    ctx.fillText(
        "Low taper around the sides.",
        505,
        550
    );

    ctx.fillText(
        "Natural textured finish.",
        505,
        585
    );

    // --------------------------------------------------------
    // Footer
    // --------------------------------------------------------

    ctx.strokeStyle =
        "rgba(255,255,255,.1)";

    ctx.lineWidth = 2;

    ctx.beginPath();

    ctx.moveTo(
        65,
        640
    );

    ctx.lineTo(
        1135,
        640
    );

    ctx.stroke();

    ctx.fillStyle = "#71717a";

    ctx.font =
        "400 20px Arial, sans-serif";

    ctx.fillText(
        "Generated for your next haircut",
        65,
        695
    );

    ctx.fillStyle = "#c4b5fd";

    ctx.font =
        "600 22px Arial, sans-serif";

    //ctx.fillText(
    //    "SHOW TO YOUR BARBER  →",
    //    825,
    //    695
    //);

    // --------------------------------------------------------

    const texture =
        new THREE.CanvasTexture(cardCanvas);

    texture.colorSpace =
        THREE.SRGBColorSpace;

    texture.anisotropy =
        renderer.capabilities.getMaxAnisotropy();

    return texture;
}

// ============================================================
// 3D card
// ============================================================

const cardGroup = new THREE.Group();

scene.add(cardGroup);

// ------------------------------------------------------------
// Card body
// ------------------------------------------------------------

const cardShape = new THREE.Shape();

const width = 5;
const height = 3.17;
const radius = 0.22;

cardShape.moveTo(
    -width / 2 + radius,
    -height / 2
);

cardShape.lineTo(
    width / 2 - radius,
    -height / 2
);

cardShape.quadraticCurveTo(
    width / 2,
    -height / 2,
    width / 2,
    -height / 2 + radius
);

cardShape.lineTo(
    width / 2,
    height / 2 - radius
);

cardShape.quadraticCurveTo(
    width / 2,
    height / 2,
    width / 2 - radius,
    height / 2
);

cardShape.lineTo(
    -width / 2 + radius,
    height / 2
);

cardShape.quadraticCurveTo(
    -width / 2,
    height / 2,
    -width / 2,
    height / 2 - radius
);

cardShape.lineTo(
    -width / 2,
    -height / 2 + radius
);

cardShape.quadraticCurveTo(
    -width / 2,
    -height / 2,
    -width / 2 + radius,
    -height / 2
);

// ------------------------------------------------------------

const bodyGeometry =
    new THREE.ExtrudeGeometry(
        cardShape,
        {
            depth: 0.12,
            bevelEnabled: true,
            bevelSegments: 4,
            bevelSize: 0.035,
            bevelThickness: 0.035
        }
    );

const bodyMaterial =
    new THREE.MeshStandardMaterial({
        color: 0x16161a,
        roughness: 0.32,
        metalness: 0.18
    });

const cardBody =
    new THREE.Mesh(
        bodyGeometry,
        bodyMaterial
    );

cardBody.position.z = -0.06;

cardGroup.add(cardBody);

// ============================================================
// Card front texture
// ============================================================

const cardTexture =
    createCardTexture();

const frontGeometry =
    new THREE.PlaneGeometry(
        4.91,
        3.08
    );

const frontMaterial =
    new THREE.MeshBasicMaterial({
        map: cardTexture,
        transparent: true
    });

const cardFront =
    new THREE.Mesh(
        frontGeometry,
        frontMaterial
    );

cardFront.position.z = 0.105;

cardGroup.add(cardFront);

// ============================================================
// Accent edge
// ============================================================

const edgeGeometry =
    new THREE.EdgesGeometry(
        bodyGeometry
    );

const edgeMaterial =
    new THREE.LineBasicMaterial({
        color: 0x6d5aa8,
        transparent: true,
        opacity: 0.25
    });

const edges =
    new THREE.LineSegments(
        edgeGeometry,
        edgeMaterial
    );

edges.position.z = -0.06;

cardGroup.add(edges);

// ============================================================
// Lighting
// ============================================================

scene.add(
    new THREE.AmbientLight(
        0xffffff,
        1.8
    )
);

const keyLight =
    new THREE.DirectionalLight(
        0xffffff,
        3
    );

keyLight.position.set(
    4,
    5,
    8
);

scene.add(keyLight);

const purpleLight =
    new THREE.PointLight(
        0x8b5cf6,
        14,
        15
    );

purpleLight.position.set(
    -3,
    1,
    4
);

scene.add(purpleLight);

// ============================================================
// Initial position
// ============================================================

cardGroup.position.set(
    1.5,
    0.5,
    0
);

cardGroup.rotation.set(
    -0.12,
    -0.28,
    -0.03
);

// ============================================================
// Mouse movement
// ============================================================

const mouse = {
    x: 0,
    y: 0
};

window.addEventListener(
    "pointermove",
    event => {

        mouse.x =
            event.clientX /
            window.innerWidth -
            0.5;

        mouse.y =
            event.clientY /
            window.innerHeight -
            0.5;
    }
);

// ============================================================
// Responsive / Resize
// ============================================================

function updateLayout() {
    const viewportWidth = window.innerWidth;

    cardGroup.position.set(0, 0, 0);

    if (viewportWidth < 600) {
        camera.position.z = 10.5;
        cardGroup.scale.setScalar(0.82);
    }
    else if (viewportWidth < 900) {
        camera.position.z = 9.7;
        cardGroup.scale.setScalar(0.9);
    }
    else {
        camera.position.z = 9;
        cardGroup.scale.setScalar(0.95);
    }
}

function resizeScene() {
    const width = visual.clientWidth;
    const height = visual.clientHeight;

    if (!width || !height) {
        return;
    }

    camera.aspect = width / height;
    camera.updateProjectionMatrix();

    renderer.setSize(width, height, false);

    renderer.setPixelRatio(
        Math.min(window.devicePixelRatio, 2)
    );
}

function updateScene() {
    updateLayout();
    resizeScene();
}

window.addEventListener("resize", updateScene);

const resizeObserver = new ResizeObserver(resizeScene);
resizeObserver.observe(visual);

updateScene();

// ============================================================
// Animation
// ============================================================

const clock = new THREE.Clock();

const reducedMotion =
    window.matchMedia(
        "(prefers-reduced-motion: reduce)"
    ).matches;

function animate() {
    requestAnimationFrame(animate);

    const time = clock.getElapsedTime();

    if (!reducedMotion) {

        // Gentle floating
        cardGroup.position.y =
            Math.sin(time * 0.75) * 0.08;

        const isMobile =
            window.innerWidth < 900;

        const mouseTiltY =
            isMobile ? 0 : mouse.x * 0.18;

        const mouseTiltX =
            isMobile ? 0 : mouse.y * 0.10;

        const targetRotationY =
            -0.16 +
            mouseTiltY +
            Math.sin(time * 0.25) * 0.025;

        const targetRotationX =
            -0.06 -
            mouseTiltX;

        const targetRotationZ =
            -0.015;

        cardGroup.rotation.y +=
            (
                targetRotationY -
                cardGroup.rotation.y
            ) * 0.035;

        cardGroup.rotation.x +=
            (
                targetRotationX -
                cardGroup.rotation.x
            ) * 0.035;

        cardGroup.rotation.z +=
            (
                targetRotationZ -
                cardGroup.rotation.z
            ) * 0.035;
    }

    renderer.render(scene, camera);
}

animate();