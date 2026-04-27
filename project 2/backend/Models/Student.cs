namespace CollegeLabEvalSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BatchId { get; set; }
        public Batch? Batch { get; set; }

        public ICollection<Marks> Marks { get; set; } = new List<Marks>();
    }
}
