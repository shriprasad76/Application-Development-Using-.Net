using CollegeLabEvalSystem.Data;
using CollegeLabEvalSystem.DTOs;
using CollegeLabEvalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeLabEvalSystem.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(LoginRequest request)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);
        }

        public async Task SeedDefaultTeacherAsync()
        {
            if (!await _context.Users.AnyAsync(u => u.Username == "teacher1"))
            {
                _context.Users.Add(new User
                {
                    Username = "teacher1",
                    Password = "Password123!",
                    Role = "Teacher"
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
