using SMSTelegram.Application.Models.Users;

namespace SMSTelegram.Application.Abstractions;

public interface IUserService
{
    Task<UserDto> GetByPhoneNumberAsync(string phoneNumber);
}