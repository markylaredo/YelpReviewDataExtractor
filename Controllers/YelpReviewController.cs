using Microsoft.AspNetCore.Mvc;
using YelpReviewDataExtractor.Services;

namespace YelpReviewDataExtractor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YelpReviewController : ControllerBase
    {
        private readonly IYelpService _yelpService;

        public YelpReviewController(IYelpService yelpService)
        {
            _yelpService = yelpService;
        }


        [HttpGet]
        public async Task<IActionResult> Reviews([FromQuery] ReviewQueryDto query)
        {
            var reviews = await _yelpService.GetReviewsAsync(query);

            return Ok(reviews);
        }
    }
}
