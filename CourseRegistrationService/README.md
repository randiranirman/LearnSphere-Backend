# Course Registration Service - Student Registration Feature

## Overview
This feature allows students to register for classes with multiple subjects, includes admin approval workflow, and provides real-time notifications using SignalR.

## Key Features

### 1. Student Registration
- Students can register for a class with multiple subjects
- Validation for class and subject existence
- Prevents duplicate registrations
- Creates `StudentClassRegistration` records for each subject
- Creates `StudentSubject` mappings (initially inactive)

### 2. Admin Approval Workflow
- Admins can view all pending registrations
- Approve or reject registrations with reasons
- Approved registrations activate student-subject mappings
- Rejected registrations include rejection reasons

### 3. Real-time Notifications (SignalR)
- Admins receive notifications when new registrations are submitted
- Students receive notifications when their registrations are approved/rejected
- SignalR groups for targeted messaging

### 4. Student View
- Students can view all their registrations
- See status (Pending, Approved, Rejected)
- View approval/rejection details and timestamps

## API Endpoints

### Student Registration
```
POST /api/studentregistration/register
```
Register a student for a class with multiple subjects.

### View Student Registrations
```
GET /api/studentregistration/student/{studentId}
```
Get all registrations for a specific student.

### Admin - View Pending Registrations
```
GET /api/studentregistration/pending
```
Get all pending registrations awaiting admin approval.

### Admin - Approve Registration
```
POST /api/studentregistration/{registrationId}/approve?adminId={adminId}
```
Approve a specific registration.

### Admin - Reject Registration
```
POST /api/studentregistration/{registrationId}/reject?adminId={adminId}
Body: "Rejection reason"
```
Reject a specific registration with a reason.

### View Specific Registration
```
GET /api/studentregistration/{registrationId}
```
Get details of a specific registration.

## SignalR Hub
- **Hub URL**: `/registrationHub`
- **Admin Group**: `Admins`
- **Student Group**: `Student_{studentId}`

### Client Methods
- `JoinAdminGroup()` - Join admin group for notifications
- `JoinStudentGroup(studentId)` - Join student-specific group
- `LeaveAdminGroup()` - Leave admin group
- `LeaveStudentGroup(studentId)` - Leave student group

### Server Events
- `NewRegistration` - Sent to admins when new registration is submitted
- `RegistrationApproved` - Sent to student when registration is approved
- `RegistrationRejected` - Sent to student when registration is rejected

## Data Models

### StudentRegistrationRequestDto
```json
{
  "studentId": 1,
  "classId": 1,
  "subjectIds": [1, 2, 3],
  "indexNumber": "STU001"
}
```

### StudentRegistrationDto
```json
{
  "studentRegistrationId": 1,
  "studentId": 1,
  "classId": 1,
  "className": "Grade 10A",
  "subjectId": 1,
  "subjectName": "Mathematics",
  "indexNumber": "STU001",
  "status": "Pending",
  "registeredAt": "2024-01-15T10:00:00Z",
  "approvedAt": null,
  "approvedByAdminId": null,
  "remarks": null
}
```

## Implementation Details

### Repositories
- `StudentClassRegistrationRepository` - Manages class registrations
- `StudentSubjectRepository` - Manages student-subject relationships
- `ClassRepository` - Manages class data
- `SubjectRepository` - Manages subject data

### Services
- `StudentRegistrationService` - Main business logic
- `RegistrationHub` - SignalR hub for real-time notifications

### Database Tables
- `StudentClassRegistration` - Registration records (one per subject)
- `StudentSubject` - Student-subject mappings
- `Class` - Class information
- `Subject` - Subject information

## Usage Flow

1. **Student Registration**
   - Student submits registration with class and multiple subjects
   - System validates class and subjects exist
   - Creates registration records in "Pending" status
   - Notifies admins via SignalR

2. **Admin Review**
   - Admin views pending registrations
   - Reviews and approves/rejects registrations
   - System updates status and sends notifications to students

3. **Student View**
   - Students can view their registration status
   - Receive real-time notifications about approvals/rejections

## Testing
Use the provided `RegistrationAPI.http` file to test all endpoints with sample data.

## Configuration
- SignalR is configured in `Program.cs`
- External service URLs are in `appsettings.json`
- All repositories and services are registered in DI container
