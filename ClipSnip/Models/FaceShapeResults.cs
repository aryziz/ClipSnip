namespace ClipSnip.Models
{
    public class FaceShapeResult
    {
        public string FaceShape { get; set; }
        public int Score { get; set; }
        public List<string> Reasons { get; set; } = new List<string>();
    }
}