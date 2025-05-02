using AutoMapper;
using ExpertBooking.Core.Entities;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Contracts.DTOs.Dashboard.AdminDashboard;
using ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard;
using ExpertBooking.Contracts.DTOs.Dashboard.ClientDashboard;
using ExpertBooking.Contracts.DTOs.Website;
using ExpertBooking.Core.Enums;

namespace ExpertBooking.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ===== AdminDashboard DTOs =====
            CreateMap<Expert, ExpertDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<Admin, AdminDto>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<Booking, BookingDto>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            // ===== Shared DTOs =====
            CreateMap<ApplicationUser, UserProfileDto>();

            CreateMap<Expert, ExpertProfileDto>()
                .ForMember(dest => dest.Certifications, opt =>
                    opt.MapFrom(src => src.ExpertDocuments.Select(doc => doc.FileUrl)))
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            CreateMap<Client, ClientProfileDto>();

            // Expert Dashboard
            CreateMap<Expert, ExpertProfileDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.ExpertDocuments.Select(d => d.FileUrl)));

            CreateMap<Review, ReviewExpertDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.User.FullName));

            CreateMap<Booking, BookingExpertDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.User.FullName));

            CreateMap<Schedule, ScheduleDto>().ReverseMap();

            // Client Dashboard mappings
            CreateMap<Client, ClientProfileDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<Booking, BookingClientDto>()
                .ForMember(dest => dest.ExpertName, opt => opt.MapFrom(src => src.Expert.User.FirstName + " " + src.Expert.User.LastName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Review, ReviewClientDto>()
                .ForMember(dest => dest.ExpertName, opt => opt.MapFrom(src => src.Expert.User.FirstName + " " + src.Expert.User.LastName));
            
            // ================= Website =================
            CreateMap<Expert, ExpertCardDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                    src.Reviews != null && src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0));

            CreateMap<Expert, ExpertPublicProfileDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.ExpertDocuments))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                    src.Reviews != null && src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0));

            CreateMap<Schedule, ScheduleSlotDto>();
            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => BookingStatus.Pending))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        }
    }
}



