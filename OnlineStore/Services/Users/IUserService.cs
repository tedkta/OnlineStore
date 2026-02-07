using OnlineStore.Data.Models.User;

namespace OnlineStore.Services.Users;

public interface IUserService
{
    void Create(CreateUserDto user);
    void Login(LoginUserDto user);
    void Logout();

}