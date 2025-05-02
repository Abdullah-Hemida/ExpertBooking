using AutoMapper;
using ExpertBooking.Application.Helper;
using ExpertBooking.Application.Interfaces.Website;
using ExpertBooking.Contracts.DTOs.Dashboard.AdminDashboard;
using ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Contracts.DTOs.Website;
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Website;


namespace ExpertBooking.Application.Services.Website
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IWebsiteRepository _repository;
        private readonly IMapper _mapper;

        public WebsiteService(IWebsiteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ExpertCardDto>>> GetFeaturedExpertsAsync()
        {
            var experts = await _repository.GetFeaturedExpertsAsync(6);
            var dtos = _mapper.Map<List<ExpertCardDto>>(experts);
            return ServiceResponse<List<ExpertCardDto>>.Success(dtos);
        }

        public async Task<ServiceResponse<List<ExpertCardDto>>> SearchExpertsAsync(string? keyword, Guid? categoryId)
        {
            var experts = await _repository.SearchExpertsAsync(keyword, categoryId);
            var dtos = _mapper.Map<List<ExpertCardDto>>(experts);
            return ServiceResponse<List<ExpertCardDto>>.Success(dtos);
        }

        public async Task<ServiceResponse<ExpertProfileDto>> GetExpertProfileAsync(Guid expertId)
        {
            var expert = await _repository.GetExpertByIdAsync(expertId);
            if (expert == null) return ServiceResponse<ExpertProfileDto>.Fail("Expert not found");

            return ServiceResponse<ExpertProfileDto>.Success(_mapper.Map<ExpertProfileDto>(expert));
        }

        public async Task<ServiceResponse<List<ScheduleDto>>> GetExpertScheduleAsync(Guid expertId)
        {
            var slots = await _repository.GetExpertScheduleAsync(expertId);
            return ServiceResponse<List<ScheduleDto>>.Success(_mapper.Map<List<ScheduleDto>>(slots));
        }

        public async Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            return ServiceResponse<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
        }
        public async Task<ServiceResponse<List<ExpertCardDto>>> GetExpertsByCategoryAsync(Guid categoryId)
        {
            var experts = await _repository.GetExpertsByCategoryAsync(categoryId);
            var dtos = _mapper.Map<List<ExpertCardDto>>(experts);
            return ServiceResponse<List<ExpertCardDto>>.Success(dtos);
        }
        public async Task<ServiceResponse<List<ScheduleDto>>> GetAvailableSlotsAsync(Guid expertId)
        {
            var slots = await _repository.GetAvailableSlotsAsync(expertId);
            var dtos = _mapper.Map<List<ScheduleDto>>(slots);
            return ServiceResponse<List<ScheduleDto>>.Success(dtos);
        }
        public async Task<ServiceResponse<bool>> CreateBookingAsync(Guid clientId, CreateBookingDto dto)
        {
            var expert = await _repository.GetExpertByIdAsync(dto.ExpertId);
            if (expert == null) return ServiceResponse<bool>.Fail("Expert not found");

            var slotExists = await _repository.IsSlotAvailableAsync(dto.ExpertId, dto.ScheduledDateTime, dto.Duration);
            if (!slotExists) return ServiceResponse<bool>.Fail("Selected slot is not available");

            var booking = _mapper.Map<Booking>(dto);
            booking.ClientId = clientId;

            var created = await _repository.CreateBookingAsync(booking);
            return created
                ? ServiceResponse<bool>.Success(true)
                : ServiceResponse<bool>.Fail("Failed to create booking");
        }
    }
}
