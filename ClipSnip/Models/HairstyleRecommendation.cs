namespace ClipSnip.Models
{
    public class HairstyleRecommendation
    {
        public Hairstyle HairStyle { get; set; }
        public int Score { get; set; }
        public List<string> reasons { get; set; }

    }
}
