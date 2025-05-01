using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpertBooking.Application.Interfaces.Dashboard.ExpertDashboard;
using ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard;
using ExpertBooking.Core.Models;

namespace ExpertBooking.API.Controllers.Dashboard.ExpertDashboard
{
    [Authorize(Roles = "Expert")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertDashboardController : ControllerBase
    {
        private readonly IExpertDashboardService _expertDashboardService;

        public ExpertDashboardController(IExpertDashboardService expertDashboardService)
        {
            _expertDashboardService = expertDashboardService;
        }

        // ===== 1. Profile Management =====

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.GetExpertProfileAsync(userId);
            return Ok(response);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ExpertUpdateDto dto)
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.UpdateExpertProfileAsync(userId, dto);
            return Ok(response);
        }

        [HttpPost("upload-intro-video")]
        public async Task<IActionResult> UploadIntroductionVideo([FromForm] IFormFile video)
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.UploadIntroductionVideoAsync(userId, video);
            return Ok(response);
        }

        [HttpPost("upload-certifications")]
        public async Task<IActionResult> UploadCertifications([FromForm] List<IFormFile> certifications)
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.UploadCertificationAsync(userId, certifications);
            return Ok(response);
        }

        [HttpDelete("delete-certification/{docId}")]
        public async Task<IActionResult> DeleteCertification(Guid docId)
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.DeleteCertificationAsync(userId, docId);
            return Ok(response);
        }

        // ===== 2. Schedule Management =====

        [HttpGet("schedule")]
        public async Task<IActionResult> GetSchedule()
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.GetScheduleAsync(userId);
            return Ok(response);
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> AddScheduleSlot([FromBody] ScheduleDto dto)
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.AddScheduleSlotAsync(userId, dto);
            return Ok(response);
        }

        [HttpDelete("schedule/{slotId}")]
        public async Task<IActionResult> DeleteScheduleSlot(Guid slotId)
        {
            var response = await _expertDashboardService.DeleteScheduleSlotAsync(slotId);
            return Ok(response);
        }

        // ===== 3. Booking Management =====

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings([FromQuery] BookingFilter filter)
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.GetBookingsAsync(userId, filter);
            return Ok(response);
        }

        [HttpPost("bookings/confirm/{bookingId}")]
        public async Task<IActionResult> ConfirmBooking(Guid bookingId)
        {
            var response = await _expertDashboardService.ConfirmBookingAsync(bookingId);
            return Ok(response);
        }

        [HttpPost("bookings/reject/{bookingId}")]
        public async Task<IActionResult> RejectBooking(Guid bookingId)
        {
            var response = await _expertDashboardService.RejectBookingAsync(bookingId);
            return Ok(response);
        }

        [HttpPost("bookings/notes/{bookingId}")]
        public async Task<IActionResult> AddNotesToBooking(Guid bookingId, [FromBody] string notes)
        {
            var response = await _expertDashboardService.AddNotesToBookingAsync(bookingId, notes);
            return Ok(response);
        }

        // ===== 4. Review & Rating =====

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviews()
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.GetReviewsAsync(userId);
            return Ok(response);
        }

        // ===== 5. Statistics & Insights =====

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var userId = GetUserId();
            var response = await _expertDashboardService.GetExpertDashboardStatsAsync(userId);
            return Ok(response);
        }

        // ===== Helper to Get User ID from Token =====
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
        }
    }
}

