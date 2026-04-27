using CollegeLabEvalSystem.DTOs;
using CollegeLabEvalSystem.Models;

namespace CollegeLabEvalSystem.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(LoginRequest request);
        Task SeedDefaultTeacherAsync();
    }
}
