using CollegeLabEvalSystem.Data;
using CollegeLabEvalSystem.DTOs;
using CollegeLabEvalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeLabEvalSystem.Services
{
    public class MarksService : IMarksService
    {
        private readonly AppDbContext _context;
        private const int MaxTotal = 210;

        public MarksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDto>> GetStudentsByBatchAsync(int batchId)
        {
            return await _context.Students
                .Where(s => s.BatchId == batchId)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    BatchId = s.BatchId
                })
                .ToListAsync();
        }

        public async Task<List<MarksResponseDto>> SaveMarksAsync(MarksSaveRequest request)
        {
            if (request.Entries == null || request.Entries.Count == 0)
            {
                return new List<MarksResponseDto>();
            }

            var responses = new List<MarksResponseDto>();
            foreach (var entry in request.Entries)
            {
                var total = CalculateTotal(entry);
                var finalOutOf50 = ConvertToOutOf50(total);

                var existing = await _context.Marks
                    .FirstOrDefaultAsync(m => m.StudentId == entry.StudentId && m.SubjectId == request.SubjectId);

                if (existing is null)
                {
                    existing = new Marks
                    {
                        StudentId = entry.StudentId,
                        SubjectId = request.SubjectId,
                    };
                    _context.Marks.Add(existing);
                }

                existing.Lab1 = entry.Lab1;
                existing.Lab2 = entry.Lab2;
                existing.Lab3 = entry.Lab3;
                existing.Lab4 = entry.Lab4;
                existing.Lab5 = entry.Lab5;
                existing.Lab6 = entry.Lab6;
                existing.Lab7 = entry.Lab7;
                existing.Lab8 = entry.Lab8;
                existing.Lab9 = entry.Lab9;
                existing.Lab10 = entry.Lab10;
                existing.Lab11 = entry.Lab11;
                existing.Lab12 = entry.Lab12;
                existing.CIE1 = entry.CIE1;
                existing.CIE2 = entry.CIE2;
                existing.CIE3 = entry.CIE3;
                existing.Total = total;
                existing.FinalOutOf50 = finalOutOf50;

                responses.Add(new MarksResponseDto
                {
                    StudentId = entry.StudentId,
                    Total = total,
                    FinalOutOf50 = finalOutOf50
                });
            }

            await _context.SaveChangesAsync();
            return responses;
        }

        private static int CalculateTotal(MarksEntryDto entry)
        {
            var labTotal = entry.Lab1 + entry.Lab2 + entry.Lab3 + entry.Lab4 + entry.Lab5 + entry.Lab6 +
                           entry.Lab7 + entry.Lab8 + entry.Lab9 + entry.Lab10 + entry.Lab11 + entry.Lab12;
            var cieTotal = entry.CIE1 + entry.CIE2 + entry.CIE3;
            return labTotal + cieTotal;
        }

        private static int ConvertToOutOf50(int total)
        {
            if (total <= 0)
            {
                return 0;
            }

            return (int)Math.Round(total * 50.0 / MaxTotal, MidpointRounding.AwayFromZero);
        }
    }
}
