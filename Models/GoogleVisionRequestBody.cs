namespace YelpReviewDataExtractor.Models
{
    public class GoogleVisionRequestBody
    {
        public GoogleVisionFeature[] features { get; set; }
        public GoogleVisionSource image { get; set; }
    }
}