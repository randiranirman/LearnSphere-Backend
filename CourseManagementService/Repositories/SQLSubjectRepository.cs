using CourseManagementService.Data;
using CourseManagementService.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementService.Repositories
{
    public class SQLSubjectRepository : ISubjectRepository
    {
        private readonly LmsContext _context;
        public SQLSubjectRepository(LmsContext context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetSubjectAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> CreateSubjectAsync(Subject subject, Guid teacherId)
        {
            // add new subject to thr Subject table
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();

            var newTeacherSubject = new TeacherSubject
            {
                TeacherId = teacherId,
                SubjectCode = subject.Code
            };

            // add a teacher to the new subject
            await _context.TeacherSubjects.AddAsync(newTeacherSubject);
            await _context.SaveChangesAsync();

            return subject;
        }

        // this must be remove subject (in subject table) and assigned teacher (in teacherSubject table)
        public async Task<TeacherSubject?> DeleteSubjectAsync(Guid Code)
        {
            var existingSubject = await _context.Subjects.FirstOrDefaultAsync(subject => subject.Code == Code);

            if (existingSubject is null)
                return null;

            var existingTeacherSubject = await _context.TeacherSubjects.FirstOrDefaultAsync(teacherSubject => teacherSubject.SubjectCode == Code);
            Console.WriteLine("This is teacherId and subjectCode values =  " + existingTeacherSubject.SubjectCode + " " + existingTeacherSubject.TeacherId);

            var returnTeacherSubject = new TeacherSubject { };


            if (existingTeacherSubject is not null)
            {
                returnTeacherSubject = existingTeacherSubject;

                _context.TeacherSubjects.Remove(existingTeacherSubject);
                await _context.SaveChangesAsync();
            }

            _context.Subjects.Remove(existingSubject);
            await _context.SaveChangesAsync();

            return returnTeacherSubject;
        }


        public async Task<Subject?> UpdateSubjectAsync(Guid Code, Subject updatedSubject, Guid assignedTeacherId)
        {
            var existingSubject = await _context.Subjects.FirstOrDefaultAsync(subject => subject.Code == Code);

            var existingTeacherSubject = await _context.TeacherSubjects.FirstOrDefaultAsync(teacherSubject => teacherSubject.SubjectCode == Code);

            if (existingSubject == null)
                return null;

            existingSubject.Title = updatedSubject.Title;
            existingSubject.Grade = updatedSubject.Grade;

            await _context.SaveChangesAsync();

            if (existingTeacherSubject != null)
            {
                // Remove the existing teacher-subject relation
                _context.TeacherSubjects.Remove(existingTeacherSubject);
                await _context.SaveChangesAsync();
            }


            var newTeacherSubject = new TeacherSubject
            {
                TeacherId = assignedTeacherId,
                SubjectCode = Code
            };

            await _context.TeacherSubjects.AddAsync(newTeacherSubject);
            await _context.SaveChangesAsync();

            return existingSubject;
        }

        public async Task<Subject?> GetSubjectByIdAsync(Guid Code)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(item => item.Code == Code);

            if (subject is null)
                return null;

            return subject;
        }
    }
}
