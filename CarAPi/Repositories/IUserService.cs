using CarAPi.Entities;

namespace CarAPi.Repositories
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<(bool Success, string Token)> Login(User user);
    }
}
