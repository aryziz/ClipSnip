using ClipSnip.Models;

namespace ClipSnip.Services
{
    public class HairstyleRecommendationService
    {
        public List<Hairstyle> Recommend(
            string faceShape,
            List<Hairstyle> hairstyles)
        {
            return hairstyles
                .Where(h => h.SuitableFaceShapes
                    .Any(shape => shape.Equals(
                        faceShape,
                        StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}