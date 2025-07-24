using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories;

public class SubjectRepository : ISubjectRepository, IRepository<Subject>
{
    private readonly CourseRegistrationDbcontext _context;

    public DbContext Context => _context;

    public SubjectRepository(CourseRegistrationDbcontext context)
    {
        _context = context;
    }

    public async Task<Subject> AddAsync(Subject subject)
    {
        var existingSubject = await _context.Subjects.FirstOrDefaultAsync(s => s.Code == subject.Code);
        if (existingSubject != null)
        {
            throw new Exception("Subject with this code already exists.");
        }
        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();
        return subject;
    }

    public async Task DeleteAsync(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject != null)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistAsync(int id)
    {
        return await _context.Subjects.AnyAsync(s => s.SubjectId == id);
    }

    public async Task<IEnumerable<Subject>> GetAllAsync()
    {
        return await _context.Subjects
            .Include(s => s.StudentSubjects)
            .Include(s => s.TeacherSubjects)
            .Include(s => s.Classes)
            .ToListAsync();
    }

    public async Task<Subject?> GetByCodeAsync(string code)
    {
        return await _context.Subjects.FirstOrDefaultAsync(s => s.Code == code);
    }

    public async Task<Subject?> GetByIdAsync(int id)
    {
        return await _context.Subjects
            .Include(s => s.StudentSubjects)
            .Include(s => s.TeacherSubjects)
            .Include(s => s.Classes)
            .FirstOrDefaultAsync(s => s.SubjectId == id);
    }

    public async Task<IEnumerable<Subject>> GetSubjectByStudentIdAsync(int studentId)
    {
        var subjects = await _context.StudentSubjects
            .Where(ss => ss.StudentId == studentId && ss.IsActive)
            .Include(ss => ss.Subject)
            .Select(ss => ss.Subject)
            .ToListAsync();
        return subjects;
    }

    public Task<IEnumerable<Subject>> GetSubjectsByGradeIdAsync(int grade)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
    {
        var subjects = await _context.TeacherSubjects.Where(ss => ss.TeacherId == teacherId).Include(ss => ss.Subject).Select(ss => ss.Subject).ToListAsync();
        return subjects;
    }

    public async Task<Subject> UpdateAsync(Subject entity)
    {
        var existingSubject = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == entity.SubjectId);

        if (existingSubject == null)
        {
            throw new Exception("Invalid Id");
        }

        // Update the properties  
        existingSubject.Name = entity.Name;
        existingSubject.Code = entity.Code;
        existingSubject.Description = entity.Description;
        existingSubject.TeacherSubjects = entity.TeacherSubjects;
        existingSubject.StudentSubjects = entity.StudentSubjects;
        existingSubject.Classes = entity.Classes;

        _context.Subjects.Update(existingSubject);
        await _context.SaveChangesAsync();

        return existingSubject;
    }

    public async Task<IEnumerable<GetAllSubjectsDetailsWithStudentCountByTeacherIdResponseDTO>> GetAllSubjectsDeatilsWithStudentCountByTeacherId(int teacherId)
    {
        var result = await _context.Subjects
        .Include(s => s.TeacherSubjects)
        .Include(s => s.StudentSubjects)
        .Where(s => s.TeacherSubjects.Any(ts => ts.TeacherId == teacherId))
        .Select(s => new GetAllSubjectsDetailsWithStudentCountByTeacherIdResponseDTO
        {
            SubjectId = s.SubjectId,
            SubjectTitle = s.Name,
            Code = s.Code,
            NoOfRegisteredStudents = s.StudentSubjects.Count()
        })
        .ToListAsync();

        return result;

    }

}
