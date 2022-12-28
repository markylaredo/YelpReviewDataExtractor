using Microsoft.Extensions.Options;
using RestSharp;
using YelpReviewDataExtractor.Dtos;
using YelpReviewDataExtractor.Models;

namespace YelpReviewDataExtractor.Services
{
    public class YelpService : IYelpService
    {

        private const string baseUrl = "https://api.yelp.com/v3/businesses/hog-island-oyster-san-francisco-2/reviews";

        private readonly IOptionsMonitor<YelpSetting> _yelpSetting;
        private readonly IOptionsMonitor<GoogleVisionSetting> _googleVisionSetting;

        public YelpService(IOptionsMonitor<YelpSetting> yelpSetting,
            IOptionsMonitor<GoogleVisionSetting> googleVisionSetting)
        {
            _yelpSetting = yelpSetting;
            _googleVisionSetting = googleVisionSetting;
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

            var reviews = response.Data?.Reviews ?? Enumerable.Empty<YelpReview>();

            await ProcessReviewerAvatarEmotions(reviews);


            return response.Data?.Reviews ?? Enumerable.Empty<YelpReview>();
        }


        private async Task ProcessReviewerAvatarEmotions(IEnumerable<YelpReview> reviews)
        {
            // Process the review data further by running the reviewer avatar images through the Google Vision API
            foreach (var review in reviews)
            {

                var client = new RestClient();
                var request = new RestRequest
                {
                    Resource = $"https://vision.googleapis.com/v1/images:annotate?key={_googleVisionSetting.CurrentValue.ApiKey}",
                    Method = Method.Post,
                };
                request.AddHeader("accept", "application/json");

                var features = new[] {
                    new GoogleVisionFeature {
                        maxResults = 10,
                        type = "FACE_DETECTION"
                    }
                };

                var gVisionRequest = new GoogleVisionRequest
                {
                    requests = new[] {
                        new GoogleVisionRequestBody
                    {
                        features = features,
                        image = new GoogleVisionSource
                        {
                           Source= new GoogleVisionImageSource
                           {
                                imageUri = review.user.image_url
                           }
                        }
                    }}
                };


                // Add the JsonBody object to the request
                request.AddJsonBody(gVisionRequest);


                var executeResponse = await client.ExecuteAsync<GoogleVisionResponseData>(request);

                if (executeResponse.StatusCode != System.Net.HttpStatusCode.OK) continue;

                if (executeResponse.Data is null) continue;

                var response = executeResponse.Data.responses.FirstOrDefault();
                if (response is null) continue;

                review.joyLikelihood = response.faceAnnotations.FirstOrDefault()?.joyLikelihood;
                review.sorrowLikelihood = response.faceAnnotations.FirstOrDefault()?.sorrowLikelihood;
            }
        }

    }
}

