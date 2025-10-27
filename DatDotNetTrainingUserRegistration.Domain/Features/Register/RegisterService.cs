using Azure.Core;
using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotNetTrainingUserRegistration.Domain.Features.Register;

public class RegisterService
{
    private readonly AppDbContext _db;

    // constructor injection
    // method injection
    // property injection
    public RegisterService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
    {
        if (await _db.TblUsers.AnyAsync(u => u.UserName == request.UserName))
        {
            return new RegisterResponseDto
            {
                IsSuccess = false,
                IsValidationError = true,
                Message = "Username already exists."
            };
        }

        var newUser = new TblUser
        {
            UserId = Guid.NewGuid(),
            UserName = request.UserName,
            FullName = request.FullName,
            Password = request.Password
        };

        _db.TblUsers.Add(newUser);
        await _db.SaveChangesAsync();

        return new RegisterResponseDto
        {
            IsSuccess = true,
            IsValidationError = false,
            Message = "User registered successfully."
        };
    }
}
