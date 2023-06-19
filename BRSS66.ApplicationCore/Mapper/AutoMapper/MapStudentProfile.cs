using AutoMapper;
using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Mapper.AutoMapper;

public class MapStudentProfile : Profile
{
    public MapStudentProfile()
    {
        CreateMap<Student, StudentResponse>()
            .ForMember(d => d.Name, src => src.MapFrom(s => s.Name))
            .ForMember(d => d.Code, src => src.MapFrom(s => s.Code))
            .ForMember(d => d.Course, src => src.MapFrom(s => s.Enrollments));
    }
}