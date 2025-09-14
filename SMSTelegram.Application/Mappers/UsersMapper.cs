using SMSTelegram.Application.Models.Users;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Application.Mappers;

public static class UsersMapper
{
    public static UserDto ToDto(this UserEntity entity)
    {
        return new UserDto
        {
            UserId = entity.UserId,
            UserTelegramId = entity.UserTelegramId,
            UserName = entity.UserName,
            UserPhone = entity.UserPhone,
            UserInn = entity.UserInn,
            AuthCode = entity.AuthCode,
            AuthCodeExpiry = entity.AuthCodeExpiry
        };
    }
}