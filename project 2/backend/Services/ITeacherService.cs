using CollegeLabEvalSystem.DTOs;
using CollegeLabEvalSystem.Models;

namespace CollegeLabEvalSystem.Services
{
    public interface ITeacherService
    {
        Task<Subject> CreateSubjectAsync(CreateSubjectRequest request);
        Task<Student> CreateStudentAsync(CreateStudentRequest request);
        Task<Batch> CreateBatchAsync(CreateBatchRequest request);
    }
}
