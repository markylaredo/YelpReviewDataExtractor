public class ReviewQueryDto
{
    /// <summary>
    /// The current page of the review data to retrieve. Default is 1.
    /// </summary>
    public int Offset { get; set; } = 1;

    /// <summary>
    /// The number of reviews to retrieve per page. Default is 20.
    /// </summary>
    public int Limit { get; set; } = 20;

    /// <summary>
    /// The criterion to use for sorting the reviews by "yelp_sort" and "newest". Default is "yelp_sort".
    /// </summary>
    public string SortBy { get; set; } = "yelp_sort";

}
