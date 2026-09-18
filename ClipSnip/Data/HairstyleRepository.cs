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
                },

                new Hairstyle
                {
                    Id = 6,
                    Name = "French Crop",
                    Subtitle = "Short fringe · Textured top",
                    EstimatedTime = "25–35 min",
                    BarberNotes = new List<string>
                    {
                        "Keep a short, textured fringe.",
                        "Taper tightly around the temples.",
                        "Add texture without excessive height."
                    },
                    SuitableFaceShapes = new List<string>
                    {
                        "Diamond"
                    },
                    Description =
                        "A French crop adds controlled texture while softening the angles of a diamond face."
                },

                new Hairstyle
                {
                    Id = 7,
                    Name = "Modern Quiff",
                    Subtitle = "Lifted front · Tapered sides",
                    EstimatedTime = "35–45 min",
                    BarberNotes = new List<string>
                    {
                        "Leave length through the front.",
                        "Blend the sides into the top.",
                        "Use moderate volume for a balanced shape."
                    },
                    SuitableFaceShapes = new List<string>
                    {
                        "Oval"
                    },
                    Description =
                        "A modern quiff complements the balanced proportions of an oval face with controlled height."
                },

                // ADDITIONAL FACE SHAPES
                new Hairstyle
                {
                    Id = 8,
                    Name = "Textured Caesar",
                    Subtitle = "Short crop · Defined texture",
                    EstimatedTime = "25–35 min",
                    BarberNotes = new List<string>
                    {
                        "Keep the top short and textured.",
                        "Use a low taper around the sides.",
                        "Keep the fringe soft rather than blunt."
                    },
                    SuitableFaceShapes = new List<string>
                    {
                        "Round"
                    },
                    Description =
                        "A textured Caesar adds definition and subtle height to help visually lengthen a round face."
                },

                new Hairstyle
                {
                    Id = 9,
                    Name = "Ivy League",
                    Subtitle = "Tapered classic · Side swept",
                    EstimatedTime = "30–40 min",
                    BarberNotes = new List<string>
                    {
                        "Keep moderate length on top.",
                        "Use a clean taper instead of harsh edges.",
                        "Style the top slightly to one side."
                    },
                    SuitableFaceShapes = new List<string>
                    {
                        "Square"
                    },
                    Description =
                        "An Ivy League style keeps a square face structured while softening the overall outline."
                },

                new Hairstyle
                {
                    Id = 10,
                    Name = "Side Swept Fringe",
                    Subtitle = "Soft fringe · Natural movement",
                    EstimatedTime = "35–45 min",
                    BarberNotes = new List<string>
                    {
                        "Leave length across the front.",
                        "Sweep the fringe diagonally across the forehead.",
                        "Keep the sides softly blended."
                    },
                    SuitableFaceShapes = new List<string>
                    {
                        "Heart"
                    },
                    Description =
                        "A side swept fringe balances a wider forehead and adds softness to a heart-shaped face."
                },

                new Hairstyle
                {
                    Id = 11,
                    Name = "Long Layers",
                    Subtitle = "Flowing layers · Balanced volume",
                    EstimatedTime = "45–60 min",
                    BarberNotes = new List<string>
                    {
                        "Keep length below the chin.",
                        "Begin layers around the cheekbones.",
                        "Use light texture to avoid excess width at the sides."
                    },
                    SuitableFaceShapes = new List<string>
                    {
                        "Oblong"
                    },
                    Description =
                        "Long layers add width and movement to help balance the length of an oblong face."
                }
            };
        }
    }
}