using AutoMapper;
using CourseManagementService.Models.Domains;
using CourseManagementService.Models.DTOs;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementService.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<AddTeacherRequestDTO, Teacher>().ReverseMap();
            CreateMap<UpdateTeacherRequestDTO, Teacher>().ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<AddSubjectRequestDTO, Subject>().ReverseMap();
            CreateMap<Subject, UpdateSubjectRequestDTO>().ReverseMap();
            CreateMap<TeacherSubject, TeacherSubjectDTO>().ReverseMap();
            CreateMap<TeacherSubject, CreatedSubjectDTO>()
                .ForMember(x => x.Code, opt => opt.MapFrom(x => x.SubjectCode))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.SubjectCodeNavigation.Title))
                .ForMember(x => x.Grade, opt => opt.MapFrom(x => x.SubjectCodeNavigation.Grade))
                .ReverseMap();
        }
    }
}

//public partial class TeacherSubject
//{
//    [Key]
//    [Column(Order = 1)]
//    public Guid TeacherId { get; set; }

//    [Key]
//    [Column(Order = 2)]
//    public Guid SubjectCode { get; set; }
//    public virtual Subject SubjectCodeNavigation { get; set; } = null!;
//    public virtual Teacher Teacher { get; set; } = null!;
//}

//public class CreatedSubjectDTO
//{
//    public Guid Code { get; set; }
//    public string Title { get; set; } = null!;
//    public int Grade { get; set; }
//    public Guid? TeacherID { get; set; }
//}