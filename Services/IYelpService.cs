namespace YelpReviewDataExtractor.Services
{
    public interface IYelpService
    {
        Task<IEnumerable<YelpReview>> GetReviewsAsync();
    }
}