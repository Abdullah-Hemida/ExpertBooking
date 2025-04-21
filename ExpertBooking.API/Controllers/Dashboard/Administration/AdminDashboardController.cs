using ExpertBooking.Application.Interfaces.Dashboard.Administration;
using ExpertBooking.Contracts.DTOs.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminDashboardController : ControllerBase
{
    private readonly IAdminDashboardService _adminDashboardService;

    public AdminDashboardController(IAdminDashboardService adminDashboardService)
    {
        _adminDashboardService = adminDashboardService;
    }

    [HttpGet("counts")]
    public async Task<IActionResult> GetDashboardCounts()
    {
        var response = await _adminDashboardService.GetDashboardCountsAsync();
        return Ok(response);
    }

    [HttpGet("experts")]
    public async Task<IActionResult> GetExperts([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _adminDashboardService.GetAllExpertsAsync(search, page, pageSize);
        return Ok(response);
    }

    [HttpGet("clients")]
    public async Task<IActionResult> GetClients([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _adminDashboardService.GetAllClientsAsync(search, page, pageSize);
        return Ok(response);
    }

    [HttpGet("admins")]
    public async Task<IActionResult> GetAdmins([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _adminDashboardService.GetAllAdminsAsync(search, page, pageSize);
        return Ok(response);
    }

    [HttpGet("experts/{userId}")]
    public async Task<IActionResult> GetExpertById(Guid userId)
    {
        var response = await _adminDashboardService.GetExpertByIdAsync(userId);
        return Ok(response);
    }

    [HttpGet("clients/{userId}")]
    public async Task<IActionResult> GetClientById(Guid userId)
    {
        var response = await _adminDashboardService.GetClientByIdAsync(userId);
        return Ok(response);
    }

    [HttpGet("admins/{userId}")]
    public async Task<IActionResult> GetAdminById(Guid userId)
    {
        var response = await _adminDashboardService.GetAdminByIdAsync(userId);
        return Ok(response);
    }

    [HttpPut("experts/{userId}/approve")]
    public async Task<IActionResult> ApproveExpert(Guid userId)
    {
        var response = await _adminDashboardService.ApproveExpertAsync(userId);
        return Ok(response);
    }

    [HttpPut("experts/{userId}/unapprove")]
    public async Task<IActionResult> UnapproveExpert(Guid userId)
    {
        var response = await _adminDashboardService.UnapproveExpertAsync(userId);
        return Ok(response);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var response = await _adminDashboardService.GetAllCategoriesAsync();
        return Ok(response);
    }

    [HttpGet("categories/{categoryId}")]
    public async Task<IActionResult> GetCategoryById(Guid categoryId)
    {
        var response = await _adminDashboardService.GetCategoryByIdAsync(categoryId);
        return Ok(response);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto dto)
    {
        var response = await _adminDashboardService.CreateCategoryAsync(dto);
        return Ok(response);
    }

    [HttpPut("categories/{categoryId}")]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] CategoryUpdateDto dto)
    {
        var response = await _adminDashboardService.UpdateCategoryAsync(categoryId, dto);
        return Ok(response);
    }

    [HttpDelete("categories/{categoryId}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var response = await _adminDashboardService.DeleteCategoryAsync(categoryId);
        return Ok(response);
    }

    [HttpGet("bookings")]
    public async Task<IActionResult> GetAllBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _adminDashboardService.GetAllBookingsAsync(page, pageSize);
        return Ok(response);
    }

    [HttpGet("stats/bookings-per-category")]
    public async Task<IActionResult> GetBookingStatsByCategory()
    {
        var response = await _adminDashboardService.GetBookingStatsByCategoryAsync();
        return Ok(response);
    }

    [HttpGet("stats/top-rated-experts")]
    public async Task<IActionResult> GetTopRatedExperts([FromQuery] int topN = 5)
    {
        var response = await _adminDashboardService.GetTopRatedExpertsAsync(topN);
        return Ok(response);
    }
}

