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
using UserManagement.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Application.Services
{
    public class YearlyRegistrationService : IYearlyRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CourseRegistrationDbcontext _context;
        private readonly IStudentClassRegistrationRepository _studentClassRegistrationRepository;
        private readonly ITeacherClassRegistrationRepository _teacherClassRegistrationRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly ITeacherSubjectRepository _teacherSubjectRepository;

        public YearlyRegistrationService(
            IUnitOfWork unitOfWork,
            CourseRegistrationDbcontext context,
            IStudentClassRegistrationRepository studentClassRegistrationRepository,
            ITeacherClassRegistrationRepository teacherClassRegistrationRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository,
            IStudentSubjectRepository studentSubjectRepository,
            ITeacherSubjectRepository teacherSubjectRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _studentClassRegistrationRepository = studentClassRegistrationRepository;
            _teacherClassRegistrationRepository = teacherClassRegistrationRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
        }

        public async Task<int> RegisterStudentAsync(StudentYearlyRegistrationRequest request)
        {
            // Validate the request
            if (!await ValidateStudentRegistrationAsync(request))
            {
                throw new InvalidOperationException("Student registration validation failed.");
            }

            Student student;
            
            // Check if this is an existing student or new student
            if (request.ExistingStudentId.HasValue)
            {
                student = await _context.Students.FindAsync(request.ExistingStudentId.Value)
                    ?? throw new ArgumentException("Student not found.");
                
                // Update student information
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.IndexNumber = request.IndexNumber;
                student.Email = request.Email;
                student.ContactNumber = request.ContactNumber;
                student.Address = request.Address;
                student.DateOfBirth = request.DateOfBirth;
                student.ParentContactNumber = request.ParentContactNumber;
                student.ParentName = request.ParentName;
                student.Grade = request.Grade;
                student.UpdatedAt = DateTime.UtcNow;
                
                _context.Students.Update(student);
            }
            else
            {
                // Create new student
                student = new Student
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IndexNumber = request.IndexNumber,
                    Email = request.Email,
                    ContactNumber = request.ContactNumber,
                    Address = request.Address,
                    DateOfBirth = request.DateOfBirth,
                    ParentContactNumber = request.ParentContactNumber,
                    ParentName = request.ParentName,
                    Grade = request.Grade
                };
                
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync(); // Save to get the ID
            }

            // Register for the selected class
            var classEntity = await _classRepository.GetByIdAsync(request.ClassId)
                ?? throw new ArgumentException("Class not found.");

            var studentClassRegistration = new StudentClassRegistration
            {
                StudentId = student.Id,
                ClassId = request.ClassId,
                SubjectId = classEntity.SubjectId,
                IndexNumber = request.IndexNumber,
                Status = RegistrationStatus.Pending
            };

            await _studentClassRegistrationRepository.AddAsync(studentClassRegistration);

            // Register for selected subjects
            foreach (var subjectId in request.SubjectIds)
            {
                var studentSubject = new StudentSubject
                {
                    StudentId = student.Id,
                    SubjectId = subjectId
                };
                await _studentSubjectRepository.AddAsync(studentSubject);
            }

            await _unitOfWork.SaveChangesAsync();
            
            return studentClassRegistration.Id;
        }

        public async Task<int> RegisterTeacherAsync(TeacherYearlyRegistrationRequest request)
        {
            // Validate the request
            if (!await ValidateTeacherRegistrationAsync(request))
            {
                throw new InvalidOperationException("Teacher registration validation failed.");
            }

            Teacher teacher;
            
            // Check if this is an existing teacher or new teacher
            if (request.ExistingTeacherId.HasValue)
            {
                teacher = await _context.Teachers.FindAsync(request.ExistingTeacherId.Value)
                    ?? throw new ArgumentException("Teacher not found.");
                
                // Update teacher information
                teacher.FirstName = request.FirstName;
                teacher.LastName = request.LastName;
                teacher.Email = request.Email;
                teacher.ContactNumber = request.ContactNumber;
                teacher.Address = request.Address;
                teacher.DateOfBirth = request.DateOfBirth;
                teacher.EmployeeId = request.EmployeeId;
                teacher.Qualification = request.Qualification;
                teacher.HireDate = request.HireDate;
                teacher.UpdatedAt = DateTime.UtcNow;
                
                _context.Teachers.Update(teacher);
            }
            else
            {
                // Create new teacher
                teacher = new Teacher
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    ContactNumber = request.ContactNumber,
                    Address = request.Address,
                    DateOfBirth = request.DateOfBirth,
                    EmployeeId = request.EmployeeId,
                    Qualification = request.Qualification,
                    HireDate = request.HireDate
                };
                
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync(); // Save to get the ID
            }

            var registrationIds = new List<int>();
            
            // Register for selected classes
            foreach (var classId in request.ClassIds)
            {
                var classEntity = await _classRepository.GetByIdAsync(classId)
                    ?? throw new ArgumentException($"Class with ID {classId} not found.");

                var teacherClassRegistration = new TeacherClassRegistration
                {
                    TeacherId = teacher.Id,
                    ClassId = classId,
                    SubjectId = classEntity.SubjectId,
                    EmployeeId = request.EmployeeId ?? string.Empty,
                    Status = RegistrationStatus.Pending
                };

                await _teacherClassRegistrationRepository.AddAsync(teacherClassRegistration);
                registrationIds.Add(teacherClassRegistration.Id);
            }

            // Register for selected subjects
            foreach (var subjectId in request.SubjectIds)
            {
                var teacherSubject = new TeacherSubject
                {
                    TeacherId = teacher.Id,
                    SubjectId = subjectId
                };
                await _teacherSubjectRepository.AddAsync(teacherSubject);
            }

            await _unitOfWork.SaveChangesAsync();
            
            return registrationIds.FirstOrDefault(); // Return first registration ID
        }

        public async Task<bool> ValidateStudentRegistrationAsync(StudentYearlyRegistrationRequest request)
        {
            // Check if index number is unique
            var existingStudentWithIndex = await _context.Students
                .Where(s => s.IndexNumber == request.IndexNumber && 
                           (!request.ExistingStudentId.HasValue || s.Id != request.ExistingStudentId.Value))
                .FirstOrDefaultAsync();
            if (existingStudentWithIndex != null)
            {
                return false;
            }

            // Check if email is unique
            var existingStudentWithEmail = await _context.Students
                .Where(s => s.Email == request.Email && 
                           (!request.ExistingStudentId.HasValue || s.Id != request.ExistingStudentId.Value))
                .FirstOrDefaultAsync();
            if (existingStudentWithEmail != null)
            {
                return false;
            }

            // Check if class exists and has capacity
            var classEntity = await _classRepository.GetByIdAsync(request.ClassId);
            if (classEntity == null)
            {
                return false;
            }

            // Check class capacity
            var registeredCount = await _studentClassRegistrationRepository.GetRegisteredStudentCountAsync(request.ClassId);
            if (registeredCount >= classEntity.MaxStudents)
            {
                return false;
            }

            // Validate that all subjects exist
            foreach (var subjectId in request.SubjectIds)
            {
                var subject = await _subjectRepository.GetByIdAsync(subjectId);
                if (subject == null)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> ValidateTeacherRegistrationAsync(TeacherYearlyRegistrationRequest request)
        {
            // Check if email is unique
            var existingTeacherWithEmail = await _context.Teachers
                .Where(t => t.Email == request.Email && 
                           (!request.ExistingTeacherId.HasValue || t.Id != request.ExistingTeacherId.Value))
                .FirstOrDefaultAsync();
            if (existingTeacherWithEmail != null)
            {
                return false;
            }

            // Check if employee ID is unique (if provided)
            if (!string.IsNullOrEmpty(request.EmployeeId))
            {
                var existingTeacherWithEmployeeId = await _context.Teachers
                    .Where(t => t.EmployeeId == request.EmployeeId && 
                               (!request.ExistingTeacherId.HasValue || t.Id != request.ExistingTeacherId.Value))
                    .FirstOrDefaultAsync();
                if (existingTeacherWithEmployeeId != null)
                {
                    return false;
                }
            }

            // Validate that all classes exist
            foreach (var classId in request.ClassIds)
            {
                var classEntity = await _classRepository.GetByIdAsync(classId);
                if (classEntity == null)
                {
                    return false;
                }
            }

            // Validate that all subjects exist
            foreach (var subjectId in request.SubjectIds)
            {
                var subject = await _subjectRepository.GetByIdAsync(subjectId);
                if (subject == null)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> ApproveStudentRegistrationAsync(RegistrationApprovalRequest request)
        {
            var registration = await _studentClassRegistrationRepository.GetByIdAsync(request.RegistrationId);
            if (registration == null || registration.Status != RegistrationStatus.Pending)
            {
                return false;
            }

            registration.Status = request.IsApproved ? RegistrationStatus.Approved : RegistrationStatus.Rejected;
            registration.ApprovedAt = DateTime.UtcNow;
            registration.ApprovedByAdminId = request.AdminId;
            registration.Remarks = request.Remark;

            await _studentClassRegistrationRepository.UpdateAsync(registration);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ApproveTeacherRegistrationAsync(RegistrationApprovalRequest request)
        {
            var registration = await _teacherClassRegistrationRepository.GetByIdAsync(request.RegistrationId);
            if (registration == null || registration.Status != RegistrationStatus.Pending)
            {
                return false;
            }

            registration.Status = request.IsApproved ? RegistrationStatus.Approved : RegistrationStatus.Rejected;
            registration.ApprovedAt = DateTime.UtcNow;
            registration.ApprovedByAdminId = request.AdminId;
            registration.Remarks = request.Remark;

            await _teacherClassRegistrationRepository.UpdateAsync(registration);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectStudentRegistrationAsync(RegistrationApprovalRequest request)
        {
            request.IsApproved = false;
            return await ApproveStudentRegistrationAsync(request);
        }

        public async Task<bool> RejectTeacherRegistrationAsync(RegistrationApprovalRequest request)
        {
            request.IsApproved = false;
            return await ApproveTeacherRegistrationAsync(request);
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetStudentRegistrationsAsync(int studentId)
        {
            return await _studentClassRegistrationRepository.GetByStudentIdAsync(studentId);
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetTeacherRegistrationsAsync(int teacherId)
        {
            return await _teacherClassRegistrationRepository.GetByTeacherIdAsync(teacherId);
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetPendingStudentRegistrationsAsync()
        {
            return await _studentClassRegistrationRepository.GetPendingRegistrationsAsync();
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetPendingTeacherRegistrationsAsync()
        {
            return await _teacherClassRegistrationRepository.GetPendingRegistrationsAsync();
        }

        public async Task<bool> CanStudentRegisterForClassAsync(int studentId, int classId)
        {
            var existingRegistration = await _studentClassRegistrationRepository.GetByStudentAndClassAsync(studentId, classId);
            if (existingRegistration != null)
            {
                return false; // Already registered
            }

            var classEntity = await _classRepository.GetByIdAsync(classId);
            if (classEntity == null)
            {
                return false;
            }

            var registeredCount = await _studentClassRegistrationRepository.GetRegisteredStudentCountAsync(classId);
            return registeredCount < classEntity.MaxStudents;
        }

        public async Task<bool> CanTeacherRegisterForClassAsync(int teacherId, int classId)
        {
            var existingRegistration = await _teacherClassRegistrationRepository.GetByTeacherAndClassAsync(teacherId, classId);
            if (existingRegistration != null)
            {
                return false; // Already registered
            }

            var classEntity = await _classRepository.GetByIdAsync(classId);
            return classEntity != null;
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetAllStudentRegistrationsAsync()
        {
            return await _studentClassRegistrationRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetAllTeacherRegistrationsAsync()
        {
            return await _teacherClassRegistrationRepository.GetAllAsync();
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetStudentRegistrationsByStatusAsync(RegistrationStatus status)
        {
            return await _studentClassRegistrationRepository.GetByStatusAsync(status);
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetTeacherRegistrationsByStatusAsync(RegistrationStatus status)
        {
            return await _teacherClassRegistrationRepository.GetByStatusAsync(status);
        }
    }
}

