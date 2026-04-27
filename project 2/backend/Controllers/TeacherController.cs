using CollegeLabEvalSystem.DTOs;
using CollegeLabEvalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeLabEvalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost("subjects")]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Subject name is required.");
            }

            var subject = await _teacherService.CreateSubjectAsync(request);
            return Ok(subject);
        }

        [HttpPost("batches")]
        public async Task<IActionResult> CreateBatch([FromBody] CreateBatchRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Batch name is required.");
            }

            var batch = await _teacherService.CreateBatchAsync(request);
            return Ok(batch);
        }

        [HttpPost("students")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.BatchId <= 0)
            {
                return BadRequest("Student name and BatchId are required.");
            }

            try
            {
                var student = await _teacherService.CreateStudentAsync(request);
                return Ok(student);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
