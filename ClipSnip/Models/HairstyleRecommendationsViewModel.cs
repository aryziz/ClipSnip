using System.Collections.Generic;

namespace ClipSnip.Models
{
    public class HairstyleRecommendationsViewModel
    {
        public string FaceShape { get; set; } = string.Empty;
        public List<string> Reasons { get; set; } = new List<string>();
        public List<Hairstyle> Recommendations { get; set; } = new List<Hairstyle>();
    }
}
