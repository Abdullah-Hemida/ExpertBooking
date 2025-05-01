using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpertBooking.Application.Interfaces.Dashboard.ClientDashboard;
using ExpertBooking.Contracts.DTOs.Dashboard.ClientDashboard;
using ExpertBooking.Core.Models;

namespace ExpertBooking.API.Controllers.Dashboard.ClientDashboard
{
    [Authorize(Roles = "Client")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientDashboardController : ControllerBase
    {
        private readonly IClientDashboardService _clientDashboardService;

        public ClientDashboardController(IClientDashboardService clientDashboardService)
        {
            _clientDashboardService = clientDashboardService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var clientId = Guid.Parse(User.FindFirst("nameid")!.Value);
            var response = await _clientDashboardService.GetClientProfileAsync(clientId);
            return Ok(response);
        }

        [HttpPost("profile/update")]
        public async Task<IActionResult> UpdateProfile([FromBody] ClientUpdateDto dto)
        {
            var clientId = Guid.Parse(User.FindFirst("nameid")!.Value);
            var response = await _clientDashboardService.UpdateClientProfileAsync(clientId, dto);
            return Ok(response);
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings([FromQuery] BookingFilter filter)
        {
            var clientId = Guid.Parse(User.FindFirst("nameid")!.Value);
            var response = await _clientDashboardService.GetMyBookingsAsync(clientId, filter);
            return Ok(response);
        }

        [HttpPost("booking/cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking(Guid bookingId)
        {
            var response = await _clientDashboardService.CancelBookingAsync(bookingId);
            return Ok(response);
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetMyReviews()
        {
            var clientId = Guid.Parse(User.FindFirst("nameid")!.Value);
            var response = await _clientDashboardService.GetMyReviewsAsync(clientId);
            return Ok(response);
        }

        [HttpPost("review/add")]
        public async Task<IActionResult> AddReview([FromBody] ReviewClientCreateDto dto)
        {
            var clientId = Guid.Parse(User.FindFirst("nameid")!.Value);
            var response = await _clientDashboardService.AddReviewAsync(clientId, dto);
            return Ok(response);
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetClientStats()
        {
            var clientId = Guid.Parse(User.FindFirst("nameid")!.Value);
            var response = await _clientDashboardService.GetClientStatsAsync(clientId);
            return Ok(response);
        }
    }
}

