using CollegeLabEvalSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollegeLabEvalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMarksService _marksService;

        public StudentsController(IMarksService marksService)
        {
            _marksService = marksService;
        }

        [HttpGet("by-batch/{batchId}")]
        public async Task<IActionResult> GetByBatch(int batchId)
        {
            var students = await _marksService.GetStudentsByBatchAsync(batchId);
            return Ok(students);
        }
    }
}
