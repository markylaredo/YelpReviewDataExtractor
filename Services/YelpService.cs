using Microsoft.Extensions.Options;
using RestSharp;

namespace YelpReviewDataExtractor.Services
{
    public class YelpService : IYelpService
    {

        private const string baseUrl = "https://api.yelp.com/v3/businesses/hog-island-oyster-san-francisco-2/reviews";

        private readonly IOptionsMonitor<YelpSetting> _yelpSetting;

        public YelpService(IOptionsMonitor<YelpSetting> yelpSetting)
        {
            _yelpSetting = yelpSetting;
        }

        /// <summary>
        /// Retrieves reviews from the Yelp API using the specified query parameters.
        /// </summary>
        /// <param name="query">The query parameters to use for filtering and sorting the reviews.</param>
        /// <returns>A collection of <see cref="YelpReview"/> objects representing the reviews retrieved from the Yelp API.</returns>
        public async Task<IEnumerable<YelpReview>> GetReviewsAsync(ReviewQueryDto query)
        {
            var client = new RestClient();
            var request = new RestRequest
            {
                Resource = baseUrl,
                Method = Method.Get
            };

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {_yelpSetting.CurrentValue.ApiKey}");

            request.AddQueryParameter("offset", query.Offset);
            request.AddQueryParameter("limit", query.Limit);
            request.AddQueryParameter("sort_by", query.SortBy);

            var response = await client.ExecuteAsync<YelpData>(request);

            return response.Data?.Reviews ?? Enumerable.Empty<YelpReview>();
        }

    }
}


public class YelpData
{
    public List<YelpReview> Reviews { get; set; } = new();
}


public class YelpReview
{
    public string id { get; set; }
    public string text { get; set; }
    public int rating { get; set; }
    public YelpUser user { get; set; }
}

public class YelpUser
{
    public string id { get; set; }
    public string profile_url { get; set; }
    public string image_url { get; set; }
    public string name { get; set; }
}

public class YelpSetting
{
    public string ApiKey { get; set; }
}



