using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace CourseRegistration.Application.Services
{
    public class StudentRegistrationService : IStudentRegistrationService
    {
        private readonly IStudentClassRegistrationRepository _registrationRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IHubContext<RegistrationHub> _hubContext;

        public StudentRegistrationService(
            IStudentClassRegistrationRepository registrationRepository,
            IStudentSubjectRepository studentSubjectRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository,
            IHubContext<RegistrationHub> hubContext)
        {
            _registrationRepository = registrationRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _hubContext = hubContext;
        }

        public async Task<StudentRegistrationResponseDto> RegisterStudentAsync(StudentRegistrationRequestDto request)
        {
            var response = new StudentRegistrationResponseDto
            {
                StudentId = request.StudentId,
                ClassId = request.ClassId
            };

            try
            {
                // Validate class exists
                var classExists = await _classRepository.ExistAsync(request.ClassId);
                if (!classExists)
                {
                    response.Errors.Add("Class not found");
                    return response;
                }

                // Validate all subjects exist
                foreach (var subjectId in request.SubjectIds)
                {
                    var subjectExists = await _subjectRepository.ExistAsync(subjectId);
                    if (!subjectExists)
                    {
                        response.Errors.Add($"Subject with ID {subjectId} not found");
                        return response;
                    }
                }

                // Check if student is already registered for this class
                var existingRegistration = await _registrationRepository.GetByStudentAndClassAsync(request.StudentId, request.ClassId);
                if (existingRegistration != null)
                {
                    response.Errors.Add("Student is already registered for this class");
                    return response;
                }

                // Create registrations for each subject
                foreach (var subjectId in request.SubjectIds)
                {
                    var registration = new StudentClassRegistration
                    {
                        StudentId = request.StudentId,
                        ClassId = request.ClassId,
                        SubjectId = subjectId,
                        IndexNumber = request.IndexNumber,
                        Status = RegistrationStatus.Pending,
                        RegisteredAt = DateTime.UtcNow
                    };

                    var savedRegistration = await _registrationRepository.AddAsync(registration);
                    response.RegistrationIds.Add(savedRegistration.StudentRegistrationId);
                }

                // Create student-subject mappings
                foreach (var subjectId in request.SubjectIds)
                {
                    var studentSubject = new StudentSubject
                    {
                        StudentId = request.StudentId,
                        SubjectId = subjectId,
                        EnrolledAt = DateTime.UtcNow,
                        IsActive = false // Will be activated after approval
                    };

                    await _studentSubjectRepository.AddAsync(studentSubject);
                }

                response.IsSuccess = true;
                response.Message = "Registration submitted successfully. Awaiting admin approval.";

                // Notify admins about new registration via SignalR
                await _hubContext.Clients.Group("Admins").SendAsync("NewRegistration", new
                {
                    StudentId = request.StudentId,
                    ClassId = request.ClassId,
                    SubjectIds = request.SubjectIds,
                    IndexNumber = request.IndexNumber,
                    RegisteredAt = DateTime.UtcNow
                });

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add($"Registration failed: {ex.Message}");
                return response;
            }
        }

        public async Task<bool> ApproveRegistrationAsync(int registrationId, int adminId)
        {
            try
            {
                var registration = await _registrationRepository.GetByIdAsync(registrationId);
                if (registration == null)
                    return false;

                registration.Status = RegistrationStatus.Approved;
                registration.ApprovedAt = DateTime.UtcNow;
                registration.ApprovedByAdminId = adminId;

                await _registrationRepository.UpdateAsync(registration);

                // Activate student-subject mapping
                var studentSubjects = await _studentSubjectRepository.GetByStudentIdAsync(registration.StudentId);
                foreach (var studentSubject in studentSubjects.Where(ss => ss.SubjectId == registration.SubjectId))
                {
                    studentSubject.IsActive = true;
                    await _studentSubjectRepository.UpdateAsync(studentSubject);
                }

                // Notify student about approval via SignalR
                await _hubContext.Clients.Group($"Student_{registration.StudentId}").SendAsync("RegistrationApproved", new
                {
                    RegistrationId = registrationId,
                    SubjectName = registration.Subject.Name,
                    ClassName = registration.Class.Name,
                    ApprovedAt = DateTime.UtcNow
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RejectRegistrationAsync(int registrationId, int adminId, string reason)
        {
            try
            {
                var registration = await _registrationRepository.GetByIdAsync(registrationId);
                if (registration == null)
                    return false;

                registration.Status = RegistrationStatus.Rejected;
                registration.ApprovedByAdminId = adminId;
                registration.Remarks = reason;

                await _registrationRepository.UpdateAsync(registration);

                // Notify student about rejection via SignalR
                await _hubContext.Clients.Group($"Student_{registration.StudentId}").SendAsync("RegistrationRejected", new
                {
                    RegistrationId = registrationId,
                    SubjectName = registration.Subject.Name,
                    ClassName = registration.Class.Name,
                    Reason = reason
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<StudentRegistrationDto>> GetStudentRegistrationsAsync(int studentId)
        {
            var registrations = await _registrationRepository.GetByStudentIdAsync(studentId);
            return registrations.Select(r => new StudentRegistrationDto
            {
                StudentRegistrationId = r.StudentRegistrationId,
                StudentId = r.StudentId,
                ClassId = r.ClassId,
                ClassName = r.Class.Name,
                SubjectId = r.SubjectId,
                SubjectName = r.Subject.Name,
                IndexNumber = r.IndexNumber,
                Status = r.Status,
                RegisteredAt = r.RegisteredAt,
                ApprovedAt = r.ApprovedAt,
                ApprovedByAdminId = r.ApprovedByAdminId,
                Remarks = r.Remarks
            });
        }

        public async Task<IEnumerable<StudentRegistrationDto>> GetPendingRegistrationsAsync()
        {
            var registrations = await _registrationRepository.GetPendingRegistrationsAsync();
            return registrations.Select(r => new StudentRegistrationDto
            {
                StudentRegistrationId = r.StudentRegistrationId,
                StudentId = r.StudentId,
                ClassId = r.ClassId,
                ClassName = r.Class.Name,
                SubjectId = r.SubjectId,
                SubjectName = r.Subject.Name,
                IndexNumber = r.IndexNumber,
                Status = r.Status,
                RegisteredAt = r.RegisteredAt,
                ApprovedAt = r.ApprovedAt,
                ApprovedByAdminId = r.ApprovedByAdminId,
                Remarks = r.Remarks
            });
        }

        public async Task<StudentRegistrationDto?> GetRegistrationByIdAsync(int registrationId)
        {
            var registration = await _registrationRepository.GetByIdAsync(registrationId);
            if (registration == null)
                return null;

            return new StudentRegistrationDto
            {
                StudentRegistrationId = registration.StudentRegistrationId,
                StudentId = registration.StudentId,
                ClassId = registration.ClassId,
                ClassName = registration.Class.Name,
                SubjectId = registration.SubjectId,
                SubjectName = registration.Subject.Name,
                IndexNumber = registration.IndexNumber,
                Status = registration.Status,
                RegisteredAt = registration.RegisteredAt,
                ApprovedAt = registration.ApprovedAt,
                ApprovedByAdminId = registration.ApprovedByAdminId,
                Remarks = registration.Remarks
            };
        }
    }
}
