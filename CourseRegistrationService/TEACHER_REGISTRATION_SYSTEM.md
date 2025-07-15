# Teacher Registration System

## Overview
The Teacher Registration System allows teachers to register for multiple classes and subjects, and enables admins to approve or reject these registrations. The system includes real-time notifications using SignalR.

## Features

### 1. Teacher Registration
- Teachers can register for multiple classes
- Teachers can register for multiple subjects
- Automatic validation of teacher, class, and subject existence
- Prevents duplicate registrations
- Real-time notifications to admins

### 2. Admin Approval System
- Admins can view all pending registrations
- Admins can approve or reject registrations
- Admins can add remarks to registrations
- Real-time notifications to teachers about status changes

### 3. Real-time Notifications
- SignalR integration for real-time updates
- Notifications for new registrations (to admins)
- Notifications for status changes (to teachers)

## API Endpoints

### Teacher Registration Controller (`/api/TeacherRegistration`)

#### 1. Register Teacher
**POST** `/api/TeacherRegistration/register`

Request Body:
```json
{
  "teacherId": 1,
  "employeeId": "EMP001",
  "classIds": [1, 2, 3],
  "subjectIds": [1, 2],
  "remarks": "Optional remarks"
}
```

Response:
```json
{
  "teacherId": 1,
  "classRegistrationIds": [1, 2, 3],
  "subjectRegistrationIds": [1, 2],
  "message": "Teacher registration submitted successfully. Waiting for admin approval.",
  "isSuccess": true,
  "errors": [],
  "registeredAt": "2024-01-15T10:30:00Z"
}
```

#### 2. Get Pending Registrations (Admin)
**GET** `/api/TeacherRegistration/pending`

Response:
```json
{
  "classRegistrations": [
    {
      "teacherRegistrationId": 1,
      "teacherId": 1,
      "classId": 1,
      "subjectId": 1,
      "employeeId": "EMP001",
      "status": 0,
      "registeredAt": "2024-01-15T10:30:00Z",
      "class": { "classId": 1, "name": "Grade 10A" },
      "subject": { "subjectId": 1, "name": "Mathematics" }
    }
  ],
  "subjectRegistrations": [
    {
      "id": 1,
      "teacherId": 1,
      "subjectId": 1,
      "employeeId": "EMP001",
      "status": 0,
      "registeredAt": "2024-01-15T10:30:00Z",
      "subject": { "subjectId": 1, "name": "Mathematics" }
    }
  ]
}
```

#### 3. Get Teacher Registrations
**GET** `/api/TeacherRegistration/teacher/{teacherId}`

Response:
```json
{
  "teacherId": 1,
  "classRegistrations": [...],
  "subjectRegistrations": [...]
}
```

#### 4. Get Registrations by Status
**GET** `/api/TeacherRegistration/status/{status}`

Status values:
- 0: Pending
- 1: Approved
- 2: Rejected
- 3: Cancelled

#### 5. Approve/Reject Registration (Admin)
**POST** `/api/TeacherRegistration/approve`

Request Body:
```json
{
  "registrationId": 1,
  "status": 1,
  "adminId": 1,
  "remarks": "Approved with conditions"
}
```

#### 6. Get Registration Details
**GET** `/api/TeacherRegistration/details/{registrationId}`

#### 7. Get Registrations by Class
**GET** `/api/TeacherRegistration/class/{classId}`

#### 8. Get Registrations by Subject
**GET** `/api/TeacherRegistration/subject/{subjectId}`

## Database Schema

### TeacherClassRegistration
- `TeacherRegistrationId` (Primary Key)
- `TeacherId` (Foreign Key to Teacher)
- `ClassId` (Foreign Key to Class)
- `SubjectId` (Foreign Key to Subject)
- `EmployeeId` (Teacher's Employee ID)
- `Status` (Pending/Approved/Rejected/Cancelled)
- `RegisteredAt` (Registration timestamp)
- `ApprovedAt` (Approval timestamp)
- `ApprovedByAdminId` (Admin who approved)
- `Remarks` (Admin remarks)

### TeacherSubject
- `Id` (Primary Key)
- `TeacherId` (Foreign Key to Teacher)
- `SubjectId` (Foreign Key to Subject)
- `EmployeeId` (Teacher's Employee ID)
- `Status` (Pending/Approved/Rejected/Cancelled)
- `RegisteredAt` (Registration timestamp)
- `ApprovedAt` (Approval timestamp)
- `ApprovedByAdminId` (Admin who approved)
- `Remarks` (Admin remarks)
- `IsActive` (Boolean flag)

## Services and Repositories

### Services
- `ITeacherRegistrationService` / `TeacherRegistrationService`
  - `RegisterTeacherAsync()` - Handles teacher registration
  - `ApproveRegistration()` - Handles admin approval/rejection

### Repositories
- `ITeacherClassRegistrationRepository` / `TeacherClassRegistrationRepository`
- `ITeacherSubjectRepository` / `TeacherSubjectRepository`

### External Services
- `ITeacherHttpService` - Validates teacher existence via HTTP calls

## SignalR Integration

### Hub: `RegistrationHub`
- **Groups**: 
  - `Admins` - For admin notifications
  - `Teacher_{teacherId}` - For teacher-specific notifications

### Events:
- `NewTeacherRegistration` - Sent to admins when a new registration is submitted
- `RegistrationStatusUpdated` - Sent to teachers when registration status changes

## Usage Examples

### 1. Teacher Registration Flow
1. Teacher submits registration request with class and subject IDs
2. System validates teacher, classes, and subjects existence
3. System creates registration records with "Pending" status
4. SignalR notification sent to admins
5. Admin reviews and approves/rejects registration
6. SignalR notification sent to teacher with status update

### 2. Admin Approval Flow
1. Admin fetches pending registrations
2. Admin reviews registration details
3. Admin approves/rejects with optional remarks
4. System updates registration status
5. SignalR notification sent to teacher

## Error Handling

The system includes comprehensive error handling:
- Validation errors for non-existent teachers, classes, or subjects
- Duplicate registration prevention
- Transaction rollback on errors
- Proper HTTP status codes and error messages

## Security Considerations

- All endpoints should be secured with appropriate authentication
- Admin endpoints should have admin role authorization
- Teacher endpoints should validate teacher ownership
- Input validation on all request parameters

## Future Enhancements

1. **Bulk Operations**: Allow bulk approval/rejection of registrations
2. **Email Notifications**: Send email notifications alongside SignalR
3. **Audit Trail**: Track all changes to registrations
4. **Dashboard**: Admin dashboard for registration statistics
5. **Reporting**: Generate reports on teacher registrations
6. **Calendar Integration**: Show teacher schedules based on approved registrations
