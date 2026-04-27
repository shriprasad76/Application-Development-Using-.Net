namespace CollegeLabEvalSystem.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
