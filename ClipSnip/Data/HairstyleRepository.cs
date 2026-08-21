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