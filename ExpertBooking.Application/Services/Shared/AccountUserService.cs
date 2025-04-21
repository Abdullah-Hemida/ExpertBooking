using AutoMapper;
using ExpertBooking.Application.Interfaces.Shared;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Enums;
using ExpertBooking.Core.IRepositories.Shared;
using Microsoft.AspNetCore.Identity;

namespace ExpertBooking.Application.Services.Shared
{
    public class AccountUserService : IAccountUserService
    {
        private readonly IAccountUserRepository _userRepository;
        private readonly IExpertUserRepository _expertRepository;
        private readonly IClientUserRepository _clientRepository;
        private readonly IFileStorageService _fileStorage;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public AccountUserService(
            IAccountUserRepository userRepository,
            IExpertUserRepository expertRepository,
            IClientUserRepository clientRepository,
            IFileStorageService fileStorage,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _expertRepository = expertRepository;
            _clientRepository = clientRepository;
            _fileStorage = fileStorage;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task SelectRoleAsync(Guid userId, UserType userType)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            if (user.UserType != UserType.ApplicationUser)
                throw new InvalidOperationException("User role already selected.");

            user.UserType = userType;

            await _userRepository.UpdateAsync(user);

            var identityRole = userType.ToString(); // "Client" or "Expert"
            var result = await _userManager.AddToRoleAsync(user, identityRole);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to assign role: {errors}");
            }
        }

        public async Task CompleteExpertProfileAsync(Guid userId, ExpertProfileFormDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user.UserType != UserType.Expert) throw new UnauthorizedAccessException();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsProfileCompleted = true;

            var expert = new Expert
            {
                UserId = userId,
                JobTitle = dto.JobTitle,
                Bio = dto.Bio,
                ExperienceYears = dto.ExperienceYears,
                HourlyRate = dto.HourlyRate,
                CategoryId = dto.CategoryId,
                Status = ExpertStatus.Pending
            };

            if (dto.ProfileImage != null)
                user.ProfileImageUrl = await _fileStorage.SaveFileAsync(dto.ProfileImage, "ProfilImages");

            if (dto.IdentificationDocument != null)
                expert.IdentificationDocumentUrl = await _fileStorage.SaveFileAsync(dto.IdentificationDocument, "IdentificationDocuments");

            if (dto.IntroductionVideo != null)
                expert.IntroductionVideoUrl = await _fileStorage.SaveFileAsync(dto.IntroductionVideo, "IntroductionVideos");

            if (dto.Certifications != null)
            {
                expert.ExpertDocuments = new List<ExpertDocument>();
                foreach (var cert in dto.Certifications)
                {
                    var url = await _fileStorage.SaveFileAsync(cert, "Certifications");
                    expert.ExpertDocuments.Add(new ExpertDocument
                    {
                        FileUrl = url,
                        IsVerified = false
                    });
                }
            }

            await _userRepository.UpdateAsync(user);
            await _expertRepository.CreateAsync(expert);
        }

        public async Task CompleteClientProfileAsync(Guid userId, ClientProfileFormDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user.UserType != UserType.Client) throw new UnauthorizedAccessException();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsProfileCompleted = true;

            var client = new Client
            {
                UserId = userId,
                Preferences = dto.Preferences,
                Bio = dto.Bio,
            };

            if (dto.ProfileImage != null)
                user.ProfileImageUrl = await _fileStorage.SaveFileAsync(dto.ProfileImage, "ProfilImages");

            await _userRepository.UpdateAsync(user);
            await _clientRepository.CreateAsync(client);
        }

        public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var dto = _mapper.Map<UserProfileDto>(user);

            if (user.UserType == UserType.Expert)
            {
                var expert = await _expertRepository.GetByIdWithCategoryAndDocumentsAsync(userId);
                dto.ExpertProfile = _mapper.Map<ExpertProfileDto>(expert);
            }
            else if (user.UserType == UserType.Client)
            {
                var client = await _clientRepository.GetByIdAsync(userId);
                dto.ClientProfile = _mapper.Map<ClientProfileDto>(client);
            }
            return dto;
        }
    }
}









