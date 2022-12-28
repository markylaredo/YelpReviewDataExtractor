namespace YelpReviewDataExtractor.Models
{
    public class GoogleVisionResponseData
    {
        public Response[] responses { get; set; }
    }

    public class Response
    {
        public Faceannotation[] faceAnnotations { get; set; }
    }

    public class Faceannotation
    {
        public Boundingpoly boundingPoly { get; set; }
        public Fdboundingpoly fdBoundingPoly { get; set; }
        public Landmark[] landmarks { get; set; }
        public float rollAngle { get; set; }
        public float panAngle { get; set; }
        public float tiltAngle { get; set; }
        public float detectionConfidence { get; set; }
        public float landmarkingConfidence { get; set; }
        public string joyLikelihood { get; set; }
        public string sorrowLikelihood { get; set; }
        public string angerLikelihood { get; set; }
        public string surpriseLikelihood { get; set; }
        public string underExposedLikelihood { get; set; }
        public string blurredLikelihood { get; set; }
        public string headwearLikelihood { get; set; }
    }

    public class Boundingpoly
    {
        public Vertex[] vertices { get; set; }
    }

    public class Vertex
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Fdboundingpoly
    {
        public Vertex1[] vertices { get; set; }
    }

    public class Vertex1
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Landmark
    {
        public string type { get; set; }
        public Position position { get; set; }
    }

    public class Position
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

}
