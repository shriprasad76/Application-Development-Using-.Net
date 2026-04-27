using CollegeLabEvalSystem.Data;
using CollegeLabEvalSystem.DTOs;
using CollegeLabEvalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeLabEvalSystem.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;

        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subject> CreateSubjectAsync(CreateSubjectRequest request)
        {
            var subject = new Subject { Name = request.Name };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<Student> CreateStudentAsync(CreateStudentRequest request)
        {
            var batch = await _context.Batches.FindAsync(request.BatchId);
            if (batch == null)
            {
                throw new InvalidOperationException("Batch not found.");
            }

            var student = new Student
            {
                Name = request.Name,
                BatchId = request.BatchId
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Batch> CreateBatchAsync(CreateBatchRequest request)
        {
            var batch = new Batch { Name = request.Name };
            _context.Batches.Add(batch);
            await _context.SaveChangesAsync();
            return batch;
        }
    }
}
