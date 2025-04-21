using AutoMapper;
using ExpertBooking.Core.Entities;
using ExpertBooking.Contracts.DTOs.Dashboard;
using ExpertBooking.Contracts.DTOs.Shared;

namespace ExpertBooking.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ===== Administration DTOs =====
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
        }
    }
}



