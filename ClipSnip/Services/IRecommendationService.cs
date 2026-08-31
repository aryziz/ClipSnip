using ClipSnip.Models;

namespace ClipSnip.Services;

public interface IRecommendationService
{
    HairstyleRecommendationsViewModel GetRecommendations(FaceAnalysisRequest request);
}
