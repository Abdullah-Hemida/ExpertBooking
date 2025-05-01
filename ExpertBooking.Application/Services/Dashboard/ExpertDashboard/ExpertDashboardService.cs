using AutoMapper;
using ExpertBooking.Application.Helper;
using ExpertBooking.Application.Interfaces.Dashboard.ExpertDashboard;
using ExpertBooking.Application.Interfaces.Shared;
using ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Enums;
using ExpertBooking.Core.IRepositories.Dashboard.ExpertDashboard;
using ExpertBooking.Core.Models;
using Microsoft.AspNetCore.Http;

namespace ExpertBooking.Application.Services.Dashboard.ExpertDashboard
{
    public class ExpertDashboardService : IExpertDashboardService
    {
        private readonly IExpertDashboardRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public ExpertDashboardService(
            IExpertDashboardRepository repository,
            IMapper mapper,
            IFileStorageService fileStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        // 1. Profile Management

        public async Task<ServiceResponse<ExpertProfileDto>> GetExpertProfileAsync(Guid userId)
        {
            var expert = await _repository.GetExpertWithProfileAsync(userId);
            if (expert == null) return ServiceResponse<ExpertProfileDto>.Fail("Expert not found");

            return ServiceResponse<ExpertProfileDto>.Success(_mapper.Map<ExpertProfileDto>(expert));
        }

        public async Task<ServiceResponse<bool>> UpdateExpertProfileAsync(Guid userId, ExpertUpdateDto dto)
        {
            var expert = await _repository.GetExpertAsync(userId);
            if (expert == null) return ServiceResponse<bool>.Fail("Expert not found");

            _mapper.Map(dto, expert);
            await _repository.UpdateExpertAsync(expert);
            return ServiceResponse<bool>.Success(true);
        }

        public async Task<ServiceResponse<string>> UploadIntroductionVideoAsync(Guid userId, IFormFile video)
        {
            var expert = await _repository.GetExpertAsync(userId);
            if (expert == null) return ServiceResponse<string>.Fail("Expert not found");

            var url = await _fileStorageService.SaveFileAsync(video, "IntroductionVideos");
            expert.IntroductionVideoUrl = url;
            await _repository.UpdateExpertAsync(expert);
            return ServiceResponse<string>.Success(url);
        }

        public async Task<ServiceResponse<List<string>>> UploadCertificationAsync(Guid userId, List<IFormFile> certifications)
        {
            var expert = await _repository.GetExpertAsync(userId);
            if (expert == null) return ServiceResponse<List<string>>.Fail("Expert not found");

            var urls = new List<string>();
            foreach (var cert in certifications)
            {
                var url = await _fileStorageService.SaveFileAsync(cert, "Certifications");
                expert.ExpertDocuments.Add(new ExpertDocument { FileUrl = url });
                urls.Add(url);
            }
            await _repository.UpdateExpertAsync(expert);
            return ServiceResponse<List<string>>.Success(urls);
        }

        public async Task<ServiceResponse<bool>> DeleteCertificationAsync(Guid expertId, Guid docId)
        {
            var result = await _repository.DeleteCertificationAsync(expertId, docId);
            return result ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Fail("Document not found");
        }

        // 2. Schedule Management

        public async Task<ServiceResponse<List<ScheduleDto>>> GetScheduleAsync(Guid expertId)
        {
            var slots = await _repository.GetScheduleAsync(expertId);
            return ServiceResponse<List<ScheduleDto>>.Success(_mapper.Map<List<ScheduleDto>>(slots));
        }

        public async Task<ServiceResponse<bool>> AddScheduleSlotAsync(Guid expertId, ScheduleDto dto)
        {
            var slot = _mapper.Map<Schedule>(dto);
            slot.ExpertId = expertId;
            await _repository.AddScheduleSlotAsync(slot);
            return ServiceResponse<bool>.Success(true);
        }

        public async Task<ServiceResponse<bool>> DeleteScheduleSlotAsync(Guid slotId)
        {
            var result = await _repository.DeleteScheduleSlotAsync(slotId);
            return result ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Fail("Slot not found");
        }

        // 3. Booking Management

        public async Task<ServiceResponse<List<BookingExpertDto>>> GetBookingsAsync(Guid expertId, BookingFilter filter)
        {
            var bookings = await _repository.GetBookingsAsync(expertId, filter);
            return ServiceResponse<List<BookingExpertDto>>.Success(_mapper.Map<List<BookingExpertDto>>(bookings));
        }

        public async Task<ServiceResponse<bool>> ConfirmBookingAsync(Guid bookingId)
        {
            var result = await _repository.UpdateBookingStatusAsync(bookingId, BookingStatus.Confirmed);
            return result ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Fail("Booking not found");
        }

        public async Task<ServiceResponse<bool>> RejectBookingAsync(Guid bookingId)
        {
            var result = await _repository.UpdateBookingStatusAsync(bookingId, BookingStatus.Cancelled);
            return result ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Fail("Booking not found");
        }

        public async Task<ServiceResponse<bool>> AddNotesToBookingAsync(Guid bookingId, string notes)
        {
            var result = await _repository.AddNotesToBookingAsync(bookingId, notes);
            return result ? ServiceResponse<bool>.Success(true) : ServiceResponse<bool>.Fail("Booking not found");
        }

        // 4. Reviews

        public async Task<ServiceResponse<List<ReviewExpertDto>>> GetReviewsAsync(Guid expertId)
        {
            var reviews = await _repository.GetReviewsAsync(expertId);
            return ServiceResponse<List<ReviewExpertDto>>.Success(_mapper.Map<List<ReviewExpertDto>>(reviews));
        }

        // 5. Statistics

        public async Task<ServiceResponse<ExpertDashboardStatsDto>> GetExpertDashboardStatsAsync(Guid expertId)
        {
            var stats = new ExpertDashboardStatsDto
            {
                TotalBookings = await _repository.GetTotalBookingsAsync(expertId),
                AverageRating = await _repository.GetAverageRatingAsync(expertId)
            };
            return ServiceResponse<ExpertDashboardStatsDto>.Success(stats);
        }
    }
}

