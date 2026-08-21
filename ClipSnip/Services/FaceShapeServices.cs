using ClipSnip.Models;
namespace ClipSnip.Services
{
    public class FaceShapeServices
    {
        public List<FaceShapeResult> Analyse(FaceMeasurements face)
        {
            var results = new List<FaceShapeResult>();

            results.Add(CalculateDiamond(face));
            results.Add(CalculateOval(face));

            return results
                .OrderByDescending(x => x.Score)
                .ToList();
        }

        private FaceShapeResult CalculateDiamond(FaceMeasurements face)
        {
            int score = 0;
            var reasons = new List<string>();

            if (face.ForeheadToCheekbone < 0.90)
            {
                score += 30;
                reasons.Add("Cheekbones are wider than the forehead.");
            }

            if (face.JawToCheekbone < 0.90)
            {
                score += 30;
                reasons.Add("Cheekbones are wider than the jaw.");
            }

            if (face.LengthToWidth >= 1.10 &&
                face.LengthToWidth <= 1.50)
            {
                score += 25;
                reasons.Add("The face is longer than it is wide.");
            }

            if (face.ForeheadToJaw >= 0.85 &&
                face.ForeheadToJaw <= 1.15)
            {
                score += 15;
                reasons.Add("The forehead and jaw are relatively balanced.");
            }

            return new FaceShapeResult
            {
                FaceShape = "Diamond",
                Score = score,
                Reasons = reasons
            };
        }

        private FaceShapeResult CalculateOval(FaceMeasurements face)
        {
            int score = 0;
            var reasons = new List<string>();

            if (face.LengthToWidth >= 1.20 &&
                face.LengthToWidth <= 1.60)
            {
                score += 40;
                reasons.Add("The face is moderately longer than it is wide.");
            }

            if (face.ForeheadToCheekbone >= 0.85 &&
                face.ForeheadToCheekbone <= 1.05)
            {
                score += 20;
                reasons.Add("The forehead and cheekbones are relatively balanced.");
            }

            if (face.JawToCheekbone >= 0.75 &&
                face.JawToCheekbone <= 0.95)
            {
                score += 20;
                reasons.Add("The jaw is slightly narrower than the cheekbones.");
            }

            if (face.ForeheadToJaw >= 0.90 &&
                face.ForeheadToJaw <= 1.15)
            {
                score += 20;
                reasons.Add("The forehead and jaw are relatively balanced.");
            }

            return new FaceShapeResult
            {
                FaceShape = "Oval",
                Score = score,
                Reasons = reasons
            };
        }
    }
}