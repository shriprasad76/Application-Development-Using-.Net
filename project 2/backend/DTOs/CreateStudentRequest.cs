namespace CollegeLabEvalSystem.DTOs
{
    public class CreateStudentRequest
    {
        public string Name { get; set; } = string.Empty;
        public int BatchId { get; set; }
    }
}
