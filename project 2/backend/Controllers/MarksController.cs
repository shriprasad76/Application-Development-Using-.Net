using CollegeLabEvalSystem.DTOs;
using CollegeLabEvalSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollegeLabEvalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly IMarksService _marksService;

        public MarksController(IMarksService marksService)
        {
            _marksService = marksService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveMarks([FromBody] MarksSaveRequest request)
        {
            if (request == null || request.SubjectId <= 0 || request.BatchId <= 0)
            {
                return BadRequest("SubjectId and BatchId are required.");
            }

            if (request.Entries == null || request.Entries.Count == 0)
            {
                return BadRequest("At least one student mark entry is required.");
            }

            var results = await _marksService.SaveMarksAsync(request);
            return Ok(results);
        }
    }
}
