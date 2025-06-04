using ep_back_end.Entities;

namespace ep_back_end.Services
{
    public interface IAuthRepository
    {
        Task<bool> UserExists(string email);
        Task<User> Register(User user, string password);
        Task<User?> Login(string email, string password);
        string GenerateJwtToken(User user);
    }

}
