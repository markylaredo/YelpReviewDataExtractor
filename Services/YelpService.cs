using Microsoft.Extensions.Options;
using RestSharp;

namespace YelpReviewDataExtractor.Services
{
    public class YelpService : IYelpService
    {

        private const string baseUrl = "https://api.yelp.com/v3/businesses/hog-island-oyster-san-francisco-2/reviews?locale=en_PH&offset=20&limit=20&sort_by=yelp_sort";

        private readonly IOptionsMonitor<YelpSetting> _yelpSetting;

        public YelpService(IOptionsMonitor<YelpSetting> yelpSetting)
        {
            _yelpSetting = yelpSetting;
        }

        public async Task<IEnumerable<YelpReview>> GetReviewsAsync()
        {
            var client = new RestClient();
            var request = new RestRequest
            {
                Resource = baseUrl,
                Method = Method.Get
            };

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {_yelpSetting.CurrentValue.ApiKey}");

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