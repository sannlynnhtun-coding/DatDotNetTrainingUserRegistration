using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Domain.Services;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DatDotNetTrainingUserRegistration.Domain.Features.Profile
{
    public class ProfileService
    {
        private readonly AppDbContext _db;
        private readonly UserSessionService _userSessionService;
        public ProfileService(AppDbContext db, UserSessionService userSessionService)
        {
            _db = db;
            _userSessionService = userSessionService;
        }

        public async Task<ProfileResponseDto> GetProfile(ProfileRequestDto request)
        {
            if (!_userSessionService.IsSessionValid(request.UserId, request.SessionId))
            {
                return new ProfileResponseDto
                {
                    IsSuccess = false,
                    IsValidationError = true,
                    Message = "Invalid or expired session."
                };
            }
            
            var userProfile = await _db.TblUsers
                .Select(u => new ProfileResponseDto
                {
                    IsSuccess = true,
                    IsValidationError = false,
                    Message = "Profile retrieved successfully.",
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FullName = u.FullName
                })
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (userProfile == null)
            {
                return new ProfileResponseDto
                {
                    IsSuccess = false,
                    IsValidationError = true,
                    Message = "User profile not found."
                };
            }

            return userProfile;
        }
    }
}
