namespace ClipSnip.Models;

public class FaceAnalysisRequest
{
    public FaceRatios Data { get; set; } = null!;

    public string Result { get; set; } = string.Empty;
}

public class FaceRatios
{
    public double LengthToWidth { get; set; }
    public double ForeheadToCheekbone { get; set; }
    public double JawToCheekbone { get; set; }
}