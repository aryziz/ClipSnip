namespace ClipSnip.Models
{
    public class Hairstyle
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<string> SuitableFaceShapes { get; set; }
            = new List<string>();
        public string Description { get; set; } = String.Empty;
    }
}