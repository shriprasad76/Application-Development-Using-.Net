namespace CollegeLabEvalSystem.DTOs
{
    public class MarksSaveRequest
    {
        public int SubjectId { get; set; }
        public int BatchId { get; set; }
        public List<MarksEntryDto> Entries { get; set; } = new List<MarksEntryDto>();
    }
}
