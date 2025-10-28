using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DatDotNetTrainingUserRegistration.Domain.Features.Login
{
    public class LoginService
    {
        private readonly AppDbContext _db;

        public LoginService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var user = await _db.TblUsers
                .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password);

            if (user == null)
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid username or password."
                };

            var newSession = new TblLogin
            {
                LoginId = Guid.NewGuid(),
                UserId = user.UserId,
                SessionId = Guid.NewGuid(),
                SessionTime = DateTime.UtcNow.AddMinutes(2)
            };

            _db.TblLogins.Add(newSession); 
            await _db.SaveChangesAsync(); 

            return new LoginResponseDto
            {
                IsSuccess = true,
                Message = "Login successful.",
                UserId = user.UserId,
                SessionId = newSession.SessionId
            };
        }
    }
}