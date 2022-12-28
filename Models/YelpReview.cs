public class YelpReview
{

    public string id { get; set; }
    public string text { get; set; }
    public int rating { get; set; }
    public YelpUser user { get; set; }

    public string joyLikelihood { get; set; }
    public string sorrowLikelihood { get; set; }
}

