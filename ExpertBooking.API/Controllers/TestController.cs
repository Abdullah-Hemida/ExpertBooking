using Microsoft.AspNetCore.Mvc;

namespace ExpertBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }
    }
}

// public string FullName => $"{FirstName} {LastName}";
//public bool IsAvailable { get; set; }
//