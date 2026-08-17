using Newtonsoft.Json;

namespace ClipSnip.Controllers
{
    public class FaceRatios
    {
        private string faceShape;
        public FaceRatios(string json)
        {
            Console.WriteLine(json);
            RatioAndClassifiedShape result;
            try
            {
                result = JsonConvert.DeserializeObject<RatioAndClassifiedShape>(json);
                faceShape = result.classifiedFaceShape;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not parse Json", ex.Message);
            }
        }

        public string FeedBack() => $"Your faceshape is {faceShape}";

        public class FaceRatioData
        {
            public double lengthToWidth { get; set; }
            public double foreheadToCheekbones { get; set; }
            public double jawToCheekbones { get; set; }
        }

        public class RatioAndClassifiedShape
        {
            public FaceRatioData ratios { get; set; }
            public string classifiedFaceShape { get; set; }
        }
    }
}
