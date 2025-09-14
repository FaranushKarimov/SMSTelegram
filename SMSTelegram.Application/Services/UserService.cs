using Microsoft.EntityFrameworkCore;
using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Mappers;
using SMSTelegram.Application.Models.Users;
using SMSTelegram.Domain;
using SMSTelegram.Domain.Exceptions;

namespace SMSTelegram.Application.Services;

public class UserService(SmsDbContext smsDbContext) : IUserService
{
    public async Task<UserDto> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = await smsDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserPhone == phoneNumber);

        if (user == null)
        {
            throw new EntityNotFoundException($"User entity with phoneNumber {phoneNumber} not found");
        }

        return user.ToDto();
    }
}