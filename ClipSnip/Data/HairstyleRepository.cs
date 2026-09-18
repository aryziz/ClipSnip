using ClipSnip.Models;

namespace ClipSnip.Data
{
    public class HairstyleRepository
    {
        public List<Hairstyle> GetHairstyles()
        {
            return new List<Hairstyle>
            {
                // DIAMOND
                new Hairstyle
                {
                    Id = 1,
                    Name = "Textured Crop",
                    Subtitle = "Low taper · Natural texture",
                    EstimatedTime = "30–40 min",
                    BarberNotes = new List<string>
                    {
                        "Keep length on top.",
                        "Low taper around the sides.",
                        "Natural textured finish."
                    },

                    SuitableFaceShapes = new List<string>
                    {
                        "Diamond"
                    },

                    Description =
                        "A textured crop can add volume on top and help balance wider cheekbones."
                },

                new Hairstyle
                {
                    Id = 2,
                    Name = "Messy Fringe",
                    Subtitle = "Textured fringe · Soft taper",
                    EstimatedTime = "30–40 min",
                    BarberNotes = new List<string>
                    {
                        "Leave movement through the fringe.",
                        "Keep the sides softly tapered.",
                        "Finish with natural separation."
                    },

                    SuitableFaceShapes = new List<string>
                    {
                        "Diamond"
                    },

                    Description =
                        "A messy fringe adds texture around the forehead and can balance prominent cheekbones."
                },

                // OVAL
                new Hairstyle
                {
                    Id = 3,
                    Name = "Side Part",
                    Subtitle = "Classic part · Clean finish",
                    EstimatedTime = "35–45 min",
                    BarberNotes = new List<string>
                    {
                        "Keep enough length for the part.",
                        "Taper the sides cleanly.",
                        "Style with a controlled natural finish."
                    },

                    SuitableFaceShapes = new List<string>
                    {
                        "Oval"
                    },

                    Description =
                        "A side part works well with the balanced proportions of an oval face."
                },

                new Hairstyle
                {
                    Id = 4,
                    Name = "Slick Back",
                    Subtitle = "Brushed back · Refined shape",
                    EstimatedTime = "35–45 min",
                    BarberNotes = new List<string>
                    {
                        "Preserve length through the top.",
                        "Keep the sides neat and blended.",
                        "Leave a smooth brushed-back finish."
                    },

                    SuitableFaceShapes = new List<string>
                    {
                        "Oval"
                    },

                    Description =
                        "A slick back style can complement the naturally balanced proportions of an oval face."
                },

                // BOTH
                new Hairstyle
                {
                    Id = 5,
                    Name = "Medium Layers",
                    Subtitle = "Layered shape · Natural movement",
                    EstimatedTime = "45–60 min",
                    BarberNotes = new List<string>
                    {
                        "Build balanced layers through the top.",
                        "Keep the outline soft and natural.",
                        "Finish with lightweight movement."
                    },

                    SuitableFaceShapes = new List<string>
                    {
                        "Diamond",
                        "Oval"
                    },

                    Description =
                        "Medium layers can work with both diamond and oval face shapes by adding movement and balance."
                }
            };
        }
    }
}