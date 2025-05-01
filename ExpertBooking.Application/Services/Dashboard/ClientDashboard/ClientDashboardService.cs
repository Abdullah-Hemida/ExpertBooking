using AutoMapper;
using ExpertBooking.Application.Helper;
using ExpertBooking.Application.Interfaces.Dashboard.ClientDashboard;
using ExpertBooking.Contracts.DTOs.Dashboard.ClientDashboard;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Models;
using ExpertBooking.Core.IRepositories.Dashboard.ClientDashboard;

namespace ExpertBooking.Application.Services.Dashboard.ClientDashboard
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly IClientDashboardRepository _repository;
        private readonly IMapper _mapper;

        public ClientDashboardService(IClientDashboardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ClientProfileDto>> GetClientProfileAsync(Guid clientId)
        {
            var client = await _repository.GetClientWithProfileAsync(clientId);
            if (client == null)
                return ServiceResponse<ClientProfileDto>.Fail("Client not found");

            var dto = _mapper.Map<ClientProfileDto>(client);
            return ServiceResponse<ClientProfileDto>.Success(dto);
        }

        public async Task<ServiceResponse<bool>> UpdateClientProfileAsync(Guid clientId, ClientUpdateDto dto)
        {
            var client = await _repository.GetClientWithProfileAsync(clientId);
            if (client == null)
                return ServiceResponse<bool>.Fail("Client not found");

            // Map manually or use AutoMapper if configured
            client.Preferences = dto.Preferences;
            client.Bio = dto.Bio;
            if (client.User != null)
            {
                client.User.FirstName = dto.FirstName;
                client.User.LastName = dto.LastName;
            }

            var success = await _repository.UpdateClientProfileAsync(client);
            return success
                ? ServiceResponse<bool>.Success(true)
                : ServiceResponse<bool>.Fail("Update failed");
        }

        public async Task<ServiceResponse<List<BookingClientDto>>> GetMyBookingsAsync(Guid clientId, BookingFilter filter)
        {
            var bookings = await _repository.GetBookingsAsync(clientId, filter);
            return ServiceResponse<List<BookingClientDto>>.Success(_mapper.Map<List<BookingClientDto>>(bookings));
        }

        public async Task<ServiceResponse<bool>> CancelBookingAsync(Guid bookingId)
        {
            var success = await _repository.CancelBookingAsync(bookingId);
            return success
                ? ServiceResponse<bool>.Success(true)
                : ServiceResponse<bool>.Fail("Cancel failed");
        }

        public async Task<ServiceResponse<List<ReviewClientDto>>> GetMyReviewsAsync(Guid clientId)
        {
            var reviews = await _repository.GetMyReviewsAsync(clientId);
            return ServiceResponse<List<ReviewClientDto>>.Success(_mapper.Map<List<ReviewClientDto>>(reviews));
        }

        public async Task<ServiceResponse<bool>> AddReviewAsync(Guid clientId, ReviewClientCreateDto dto)
        {
            var review = new Review
            {
                ClientId = clientId,
                ExpertId = dto.ExpertId,
                BookingId = dto.BookingId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            var success = await _repository.AddReviewAsync(review);
            return success
                ? ServiceResponse<bool>.Success(true)
                : ServiceResponse<bool>.Fail("Add review failed");
        }

        public async Task<ServiceResponse<ClientDashboardStatsDto>> GetClientStatsAsync(Guid clientId)
        {
            var (totalBookings, reviewsCount) = await _repository.GetClientStatsAsync(clientId);
            var dto = new ClientDashboardStatsDto
            {
                TotalBookings = totalBookings,
                ReviewsCount = reviewsCount
            };

            return ServiceResponse<ClientDashboardStatsDto>.Success(dto);
        }
    }
}


