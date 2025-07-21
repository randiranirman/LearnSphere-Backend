using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace CourseRegistration.Application.Services
{
    public class TeacherRegistrationService : ITeacherRegistrationService
    {
        private readonly ITeacherClassRegistrationRepository _teacherClassRegistrationRepository;
        private readonly ITeacherSubjectRepository _teacherSubjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherHttpService _teacherHttpService;
        private readonly IHubContext<RegistrationHub> _hubContext;

        public TeacherRegistrationService(
            ITeacherClassRegistrationRepository teacherClassRegistrationRepository,
            ITeacherSubjectRepository teacherSubjectRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository,
            ITeacherHttpService teacherHttpService,
            IHubContext<RegistrationHub> hubContext)
        {
            _teacherClassRegistrationRepository = teacherClassRegistrationRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _teacherHttpService = teacherHttpService;
            _hubContext = hubContext;
        }

        public async Task<TeacherRegistrationResponseDto> RegisterTeacherAsync(TeacherRegistrationRequestDto request)
        {
            var response = new TeacherRegistrationResponseDto
            {
                TeacherId = request.TeacherId,
                IsSuccess = false
            };

            try
            {
                // Validate teacher exists
                var teacher = await _teacherHttpService.GetTeacherByIdAsync(request.TeacherId);
                if (teacher == null)
                {
                    response.Errors.Add("Teacher not found");
                    response.Message = "Teacher registration failed: Teacher not found";
                    return response;
                }

                // Validate classes exist
                var validationErrors = new List<string>();
                foreach (var classId in request.ClassIds)
                {
                    var classExists = await _classRepository.ExistAsync(classId);
                    if (!classExists)
                    {
                        validationErrors.Add($"Class with ID {classId} does not exist");
                    }
                }

                // Validate subjects exist
                foreach (var subjectId in request.SubjectIds)
                {
                    var subjectExists = await _subjectRepository.ExistAsync(subjectId);
                    if (!subjectExists)
                    {
                        validationErrors.Add($"Subject with ID {subjectId} does not exist");
                    }
                }

                if (validationErrors.Any())
                {
                    response.Errors.AddRange(validationErrors);
                    response.Message = "Teacher registration failed: Invalid classes or subjects";
                    return response;
                }

                // Register teacher for classes and subjects (no transaction needed)
                var classRegistrationIds = new List<int>();
                var subjectRegistrationIds = new List<int>();

                // Register for each class-subject combination
                foreach (var classId in request.ClassIds)
                {
                    foreach (var subjectId in request.SubjectIds)
                    {
                        // Check if teacher is already registered for this class-subject combination
                        var existingClassRegistration = await _teacherClassRegistrationRepository
                            .GetByTeacherAndClassAsync(request.TeacherId, classId);

                        if (existingClassRegistration == null)
                        {
                            var classRegistration = new TeacherClassRegistration
                            {
                                TeacherId = request.TeacherId,
                                ClassId = classId,
                                SubjectId = subjectId,
                                EmployeeId = request.EmployeeId,
                                Status = RegistrationStatus.Pending,
                                RegisteredAt = DateTime.UtcNow,
                                Remarks = request.Remarks
                            };

                            await _teacherClassRegistrationRepository.AddAsync(classRegistration);
                            classRegistrationIds.Add(classRegistration.TeacherRegistrationId);
                        }
                    }
                }

                // Register for subjects separately
                foreach (var subjectId in request.SubjectIds)
                {
                    var existingSubjectRegistration = await _teacherSubjectRepository
                        .GetByTeacherAndSubjectAsync(request.TeacherId, subjectId);

                    if (existingSubjectRegistration == null)
                    {
                        var subjectRegistration = new TeacherSubject
                        {
                            TeacherId = request.TeacherId,
                            SubjectId = subjectId,
                            EmployeeId = request.EmployeeId,
                            Status = RegistrationStatus.Pending,
                            RegisteredAt = DateTime.UtcNow,
                            Remarks = request.Remarks,
                            IsActive = true
                        };

                        await _teacherSubjectRepository.AddAsync(subjectRegistration);
                        subjectRegistrationIds.Add(subjectRegistration.Id);
                    }
                }

                // Send SignalR notification to admin
                await _hubContext.Clients.Group("Admins").SendAsync("NewTeacherRegistration", new
                {
                    TeacherId = request.TeacherId,
                    TeacherName = teacher.FullName,
                    EmployeeId = request.EmployeeId,
                    ClassCount = request.ClassIds.Count,
                    SubjectCount = request.SubjectIds.Count,
                    ClassRegistrationIds = classRegistrationIds,
                    SubjectRegistrationIds = subjectRegistrationIds,
                    RegisteredAt = DateTime.UtcNow,
                    Message = "New teacher registration awaiting approval"
                });

                response.ClassRegistrationIds = classRegistrationIds;
                response.SubjectRegistrationIds = subjectRegistrationIds;
                response.IsSuccess = true;
                response.Message = "Teacher registration submitted successfully. Waiting for admin approval.";
                response.RegisteredAt = DateTime.UtcNow;

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add($"An error occurred during registration: {ex.Message}");
                response.Message = "Teacher registration failed due to an internal error";
                return response;
            }
        }

        public async Task<bool> ApproveRegistration(RegistrationApprovalRequestDto request)
        {
            try
            {
                // This method can handle both class and subject registrations
                // For now, we'll assume it's for class registrations
                var registration = await _teacherClassRegistrationRepository.GetByIdAsync(request.RegistrationId);
                
                if (registration == null)
                {
                    // Try subject registration
                    var subjectRegistration = await _teacherSubjectRepository.GetByIdAsync(request.RegistrationId);
                    if (subjectRegistration == null)
                    {
                        return false;
                    }

                    subjectRegistration.Status = request.Status;

                    subjectRegistration.ApprovedAt = DateTime.UtcNow;
                    subjectRegistration.ApprovedByAdminId = request.AdminId;
                    subjectRegistration.Remarks = request.Remarks ?? subjectRegistration.Remarks;

                    await _teacherSubjectRepository.UpdateAsync(subjectRegistration);

                    // Get subject details for notification
                    var subjectDetails = await _subjectRepository.GetByIdAsync(subjectRegistration.SubjectId);
                    
                    // Send appropriate SignalR notification to teacher
                    if (request.Status == RegistrationStatus.Approved)
                    {
                        await _hubContext.Clients.Group($"Teacher_{subjectRegistration.TeacherId}")
                            .SendAsync("TeacherRegistrationApproved", new
                            {
                                RegistrationId = request.RegistrationId,
                                Type = "Subject",
                                SubjectName = subjectDetails?.Name ?? "Unknown Subject",
                                EmployeeId = subjectRegistration.EmployeeId,
                                ApprovedAt = DateTime.UtcNow,
                                ApprovedByAdminId = request.AdminId,
                                Message = $"Your registration for subject '{subjectDetails?.Name}' has been approved"
                            });
                    }
                    else if (request.Status == RegistrationStatus.Rejected)
                    {
                        await _hubContext.Clients.Group($"Teacher_{subjectRegistration.TeacherId}")
                            .SendAsync("TeacherRegistrationRejected", new
                            {
                                RegistrationId = request.RegistrationId,
                                Type = "Subject",
                                SubjectName = subjectDetails?.Name ?? "Unknown Subject",
                                EmployeeId = subjectRegistration.EmployeeId,
                                RejectedAt = DateTime.UtcNow,
                                RejectedByAdminId = request.AdminId,
                                Reason = request.Remarks ?? "No reason provided",
                                Message = $"Your registration for subject '{subjectDetails?.Name}' has been rejected"
                            });
                    }

                    return true;
                }

                registration.Status = request.Status;
                registration.ApprovedAt = DateTime.UtcNow;
                registration.ApprovedByAdminId = request.AdminId;
                registration.Remarks = request.Remarks ?? registration.Remarks;

                await _teacherClassRegistrationRepository.UpdateAsync(registration);

                // Get class and subject details for notification
                var classInfo = await _classRepository.GetByIdAsync(registration.ClassId);
                var subject = await _subjectRepository.GetByIdAsync(registration.SubjectId);
                
                // Send appropriate SignalR notification to teacher
                if (request.Status == RegistrationStatus.Approved)
                {
                    await _hubContext.Clients.Group($"Teacher_{registration.TeacherId}")
                        .SendAsync("TeacherRegistrationApproved", new
                        {
                            RegistrationId = request.RegistrationId,
                            Type = "Class",
                            ClassName = classInfo?.Name ?? "Unknown Class",
                            SubjectName = subject?.Name ?? "Unknown Subject",
                            EmployeeId = registration.EmployeeId,
                            ApprovedAt = DateTime.UtcNow,
                            ApprovedByAdminId = request.AdminId,
                            Message = $"Your registration for class '{classInfo?.Name}' teaching subject '{subject?.Name}' has been approved"
                        });
                }
                else if (request.Status == RegistrationStatus.Rejected)
                {
                    await _hubContext.Clients.Group($"Teacher_{registration.TeacherId}")
                        .SendAsync("TeacherRegistrationRejected", new
                        {
                            RegistrationId = request.RegistrationId,
                            Type = "Class",
                            ClassName = classInfo?.Name ?? "Unknown Class",
                            SubjectName = subject?.Name ?? "Unknown Subject",
                            EmployeeId = registration.EmployeeId,
                            RejectedAt = DateTime.UtcNow,
                            RejectedByAdminId = request.AdminId,
                            Reason = request.Remarks ?? "No reason provided",
                            Message = $"Your registration for class '{classInfo?.Name}' teaching subject '{subject?.Name}' has been rejected"
                        });
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRegistrationAsync(int adminId, int registrationId)
        {
            try
            {
                // Find the registration in TeacherClassRegistration
                var classRegistration = await _teacherClassRegistrationRepository.GetByIdAsync(registrationId);
                
                if (classRegistration != null)
                {
                    // Get class and subject details for notification before deleting
                    var classInfo = await _classRepository.GetByIdAsync(classRegistration.ClassId);
                    var subject = await _subjectRepository.GetByIdAsync(classRegistration.SubjectId);
                    
                    // Delete the class registration
                    await _teacherClassRegistrationRepository.DeleteAsync(registrationId);
                    
                    // Send SignalR notification to teacher about deletion
                    await _hubContext.Clients.Group($"Teacher_{classRegistration.TeacherId}")
                        .SendAsync("TeacherRegistrationDeleted", new
                        {
                            RegistrationId = registrationId,
                            TeacherId = classRegistration.TeacherId,
                            ClassId = classRegistration.ClassId,
                            ClassName = classInfo?.Name ?? "Unknown Class",
                            SubjectId = classRegistration.SubjectId,
                            SubjectName = subject?.Name ?? "Unknown Subject",
                            EmployeeId = classRegistration.EmployeeId,
                            DeletedAt = DateTime.UtcNow,
                            DeletedByAdminId = adminId,
                            Message = $"Your registration for class '{classInfo?.Name}' teaching subject '{subject?.Name}' has been deleted by admin"
                        });
                    
                    return true;
                }
                
                // Registration not found
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
