namespace YelpReviewDataExtractor.Services
{
    public interface IYelpService
    {
        /// <summary>
        /// Retrieves reviews from the Yelp API using the specified query parameters.
        /// </summary>
        /// <param name="query">The query parameters to use for filtering and sorting the reviews.</param>
        /// <returns>A collection of <see cref="YelpReview"/> objects representing the reviews retrieved from the Yelp API.</returns>
        Task<IEnumerable<YelpReview>> GetReviewsAsync(ReviewQueryDto query);
    }
}