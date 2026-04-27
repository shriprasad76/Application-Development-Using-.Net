using CollegeLabEvalSystem.DTOs;

namespace CollegeLabEvalSystem.Services
{
    public interface IMarksService
    {
        Task<List<StudentDto>> GetStudentsByBatchAsync(int batchId);
        Task<List<MarksResponseDto>> SaveMarksAsync(MarksSaveRequest request);
    }
}
