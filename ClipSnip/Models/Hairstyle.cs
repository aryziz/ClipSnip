namespace ClipSnip.Models
{
    public class Hairstyle
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Subtitle { get; set; } = String.Empty;
        public string EstimatedTime { get; set; } = String.Empty;
        public List<string> BarberNotes { get; set; } = new List<string>();
        public string ImagePath { get; set; } = "/img/hair-silhouette.jpg";
        public List<string> SuitableFaceShapes { get; set; }
            = new List<string>();
        public string Description { get; set; } = String.Empty;
    }
}