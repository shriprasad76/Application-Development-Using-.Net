namespace CollegeLabEvalSystem.Models
{
    public class Marks
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int Lab1 { get; set; }
        public int Lab2 { get; set; }
        public int Lab3 { get; set; }
        public int Lab4 { get; set; }
        public int Lab5 { get; set; }
        public int Lab6 { get; set; }
        public int Lab7 { get; set; }
        public int Lab8 { get; set; }
        public int Lab9 { get; set; }
        public int Lab10 { get; set; }
        public int Lab11 { get; set; }
        public int Lab12 { get; set; }

        public int CIE1 { get; set; }
        public int CIE2 { get; set; }
        public int CIE3 { get; set; }

        public int Total { get; set; }
        public int FinalOutOf50 { get; set; }
    }
}
