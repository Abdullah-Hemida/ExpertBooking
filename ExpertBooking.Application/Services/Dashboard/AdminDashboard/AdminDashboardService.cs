using AutoMapper;
using ExpertBooking.Application.Interfaces.Dashboard.AdminDashboard;
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Enums;
using ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard;
using ExpertBooking.Application.Helper;
using ExpertBooking.Contracts.DTOs.Dashboard.AdminDashboard;
using ExpertBooking.Core.Models;

namespace ExpertBooking.Application.Services.Dashboard.AdminDashboard
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IExpertRepository _expertRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public AdminDashboardService(
            IExpertRepository expertRepository,
            IClientRepository clientRepository,
            IAdminRepository adminRepository,
            ICategoryRepository categoryRepository,
            IBookingRepository bookingRepository,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _expertRepository = expertRepository;
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
            _categoryRepository = categoryRepository;
            _bookingRepository = bookingRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DashboardCountsDto>> GetDashboardCountsAsync()
        {
            var expertCount = await _expertRepository.CountAsync();
            var clientCount = await _clientRepository.CountAsync();
            var bookingCount = await _bookingRepository.CountAsync();

            return ServiceResponse<DashboardCountsDto>.Success(new DashboardCountsDto
            {
                TotalExperts = expertCount,
                TotalClients = clientCount,
                TotalBookings = bookingCount
            });
        }
        public async Task<ServiceResponse<List<ExpertDto>>> GetAllExpertsAsync(string? search, int page, int pageSize)
        {
            var experts = await _expertRepository.GetPagedAsync(search, page, pageSize);
            return ServiceResponse<List<ExpertDto>>.Success(_mapper.Map<List<ExpertDto>>(experts));
        }

        public async Task<ServiceResponse<List<ClientDto>>> GetAllClientsAsync(string? search, int page, int pageSize)
        {
            var clients = await _clientRepository.GetPagedAsync(search, page, pageSize);
            return ServiceResponse<List<ClientDto>>.Success(_mapper.Map<List<ClientDto>>(clients));
        }

        public async Task<ServiceResponse<List<AdminDto>>> GetAllAdminsAsync(string? search, int page, int pageSize)
        {
            var admins = await _adminRepository.GetPagedAsync(search, page, pageSize);
            return ServiceResponse<List<AdminDto>>.Success(_mapper.Map<List<AdminDto>>(admins));
        }

        public async Task<ServiceResponse<ExpertDto>> GetExpertByIdAsync(Guid userId)
        {
            var expert = await _expertRepository.GetByIdAsync(userId);
            if (expert == null) return ServiceResponse<ExpertDto>.Fail("Expert not found");
            return ServiceResponse<ExpertDto>.Success(_mapper.Map<ExpertDto>(expert));
        }

        public async Task<ServiceResponse<ClientDto>> GetClientByIdAsync(Guid userId)
        {
            var client = await _clientRepository.GetByIdAsync(userId);
            if (client == null) return ServiceResponse<ClientDto>.Fail("Client not found");
            return ServiceResponse<ClientDto>.Success(_mapper.Map<ClientDto>(client));
        }

        public async Task<ServiceResponse<AdminDto>> GetAdminByIdAsync(Guid userId)
        {
            var admin = await _adminRepository.GetByIdAsync(userId);
            if (admin == null) return ServiceResponse<AdminDto>.Fail("Admin not found");
            return ServiceResponse<AdminDto>.Success(_mapper.Map<AdminDto>(admin));
        }

        public async Task<ServiceResponse<bool>> ApproveExpertAsync(Guid userId)
        {
            var expert = await _expertRepository.GetByIdAsync(userId);
            if (expert == null) return ServiceResponse<bool>.Fail("Expert not found");
            expert.Status = ExpertStatus.Approved;
            await _expertRepository.UpdateAsync(expert);
            return ServiceResponse<bool>.Success(true);
        }

        public async Task<ServiceResponse<bool>> UnapproveExpertAsync(Guid userId)
        {
            var expert = await _expertRepository.GetByIdAsync(userId);
            if (expert == null) return ServiceResponse<bool>.Fail("Expert not found");
            expert.Status = ExpertStatus.Unapproved;
            await _expertRepository.UpdateAsync(expert);
            return ServiceResponse<bool>.Success(true);
        }

        public async Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return ServiceResponse<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
        }

        public async Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null) return ServiceResponse<CategoryDto>.Fail("Category not found");
            return ServiceResponse<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
        }

        public async Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            return ServiceResponse<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
        }

        public async Task<ServiceResponse<CategoryDto>> UpdateCategoryAsync(Guid categoryId, CategoryUpdateDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null) return ServiceResponse<CategoryDto>.Fail("Category not found");

            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateAsync(category);
            return ServiceResponse<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null) return ServiceResponse<bool>.Fail("Category not found");
            await _categoryRepository.DeleteAsync(category);
            return ServiceResponse<bool>.Success(true);
        }

        public async Task<ServiceResponse<List<BookingDto>>> GetAllBookingsAsync(int page = 1, int pageSize = 10)
        {
            var bookings = await _bookingRepository.GetPagedAsync(page, pageSize);
            return ServiceResponse<List<BookingDto>>.Success(_mapper.Map<List<BookingDto>>(bookings));
        }

        public async Task<ServiceResponse<List<CategoryBookingStat>>> GetBookingStatsByCategoryAsync()
        {
            var stats = await _bookingRepository.GetBookingStatsByCategoryAsync();
            return ServiceResponse<List<CategoryBookingStat>>.Success(stats);
        }

        public async Task<ServiceResponse<List<TopRatedExpert>>> GetTopRatedExpertsAsync(int topN = 5)
        {
            var experts = await _reviewRepository.GetTopRatedExpertsAsync(topN);
            return ServiceResponse<List<TopRatedExpert>>.Success(experts);
        }
    }
}
