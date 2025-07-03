using CourseManagementService.Data;
using CourseManagementService.Models.Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementService.Repositories
{
    public class SQLTeacherRepository : ITeacherRepository
    {
        private readonly LmsContext _context;

        public SQLTeacherRepository(LmsContext Context)
        {
            _context = Context;
        }

        public async Task<Teacher> CreateAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        public async Task<Teacher?> DeleteAsync(Guid Id)
        {
            var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.Id == Id);

            if (existingTeacher is null)
                return null;

            _context.Remove(existingTeacher);
            await _context.SaveChangesAsync();

            return existingTeacher;
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid Id)
        {
            var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.Id == Id);

            if (existingTeacher == null)
            {
                Console.WriteLine($"Teacher with ID {Id} not found in the database.");
            }

            return existingTeacher;
        }


        public async Task<Teacher?> UpdateAsync(Guid Id, Teacher updateTeacher)
        {
            var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.Id == Id);

            if (existingTeacher is null)
                return null;

            existingTeacher.FullName = updateTeacher.FullName;
            existingTeacher.Birthday = updateTeacher.Birthday;
            existingTeacher.Email = updateTeacher.Email;
            existingTeacher.ContactNo = updateTeacher.ContactNo;
            existingTeacher.Address = updateTeacher.Address;

            await _context.SaveChangesAsync();

            return existingTeacher;
        }
    }
}
