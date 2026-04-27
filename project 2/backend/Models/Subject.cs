namespace CollegeLabEvalSystem.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Marks> Marks { get; set; } = new List<Marks>();
    }
}
