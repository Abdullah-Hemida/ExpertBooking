using Microsoft.AspNetCore.Mvc;
using ExpertBooking.Application.Interfaces.Website;

namespace ExpertBooking.API.Controllers.Website
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebsiteController : ControllerBase
    {
        private readonly IWebsiteService _websiteService;

        public WebsiteController(IWebsiteService websiteService)
        {
            _websiteService = websiteService;
        }

        // GET: api/website/featured-experts
        [HttpGet("featured-experts")]
        public async Task<IActionResult> GetFeaturedExperts()
        {
            var result = await _websiteService.GetFeaturedExpertsAsync();
            return Ok(result);
        }

        // GET: api/website/search-experts?keyword=xyz&categoryId=guid
        [HttpGet("search-experts")]
        public async Task<IActionResult> SearchExperts([FromQuery] string? keyword, [FromQuery] Guid? categoryId)
        {
            var result = await _websiteService.SearchExpertsAsync(keyword, categoryId);
            return Ok(result);
        }

        // GET: api/website/expert/{expertId}
        [HttpGet("expert/{expertId}")]
        public async Task<IActionResult> GetExpertProfile(Guid expertId)
        {
            var result = await _websiteService.GetExpertProfileAsync(expertId);
            return Ok(result);
        }

        // GET: api/website/expert/{expertId}/schedule
        [HttpGet("expert/{expertId}/schedule")]
        public async Task<IActionResult> GetExpertSchedule(Guid expertId)
        {
            var result = await _websiteService.GetExpertScheduleAsync(expertId);
            return Ok(result);
        }

        // GET: api/website/categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _websiteService.GetAllCategoriesAsync();
            return Ok(result);
        }
        [HttpGet("experts/category/{categoryId}")]
        public async Task<IActionResult> GetExpertsByCategory(Guid categoryId)
        {
            var result = await _websiteService.GetExpertsByCategoryAsync(categoryId);
            return Ok(result);
        }

        [HttpGet("experts/{expertId}/available-slots")]
        public async Task<IActionResult> GetAvailableSlots(Guid expertId)
        {
            var result = await _websiteService.GetAvailableSlotsAsync(expertId);
            return Ok(result);
        }
    }
}
