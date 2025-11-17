using AutoMapper;
using practice.Requests.Test;
using practice.Responses.Test;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings;

public class TestProfile : Profile
{
    public TestProfile()
    {
        CreateMap<TestDTO, TestResponse>();
        CreateMap<TestDTO, TestForManagerResponse>();
        CreateMap<TestDTO, TestForStudentResponse>();
        CreateMap<CreateTestRequest, TestDTO>()
            .ForMember(d => d.Id, m => m.Ignore())
            .ForMember(d => d.Title, m => m.MapFrom(s => s.Title))
            .ForMember(d => d.Description, m => m.MapFrom(s => s.Description))
            .ForMember(d => d.IsRepeatable, m => m.MapFrom(s => s.IsRepeatable))
            .ForMember(d => d.Type, m => m.MapFrom(s => s.Type))
            .ForMember(d => d.PublishedAt, m => m.MapFrom(s => s.PublishedAt))
            .ForMember(d => d.Deadline, m => m.MapFrom(s => s.Deadline))
            .ForMember(d => d.DurationMinutes, m => m.MapFrom(s => s.DurationMinutes))
            .ForMember(d => d.PassingScore, m => m.MapFrom(s => s.PassingScore))
            .ForMember(d => d.MaxAttempts, m => m.MapFrom(s => s.MaxAttempts))
            .ForMember(d => d.IsPublic, m => m.Ignore())
            .ForMember(d => d.Students, m => m.Ignore())
            .ForMember(d => d.Projects, m => m.Ignore())
            .ForMember(d => d.Courses, m => m.Ignore())
            .ForMember(d => d.Groups, m => m.Ignore())
            .ForMember(d => d.Directions, m => m.Ignore());
            //.AfterMap((src, dest) => {
            //    dest.Students = src.Students.Select(id => new StudentDTO { Id = id }).ToList();
            //    dest.Projects = src.Projects.Select(id => new ProjectDTO { Id = id }).ToList();
            //    dest.Courses = src.Courses.Select(id => new CourseDTO { Id = id }).ToList();
            //    dest.Directions = src.Directions.Select(id => new DirectionDTO { Id = id }).ToList();
            //});
        CreateMap<UpdateTestRequest, TestDTO>()
            .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
            .ForMember(d => d.Title, m => m.MapFrom(s => s.Title))
            .ForMember(d => d.Description, m => m.MapFrom(s => s.Description))
            .ForMember(d => d.IsRepeatable, m => m.MapFrom(s => s.IsRepeatable))
            .ForMember(d => d.Type, m => m.MapFrom(s => s.Type))
            .ForMember(d => d.PublishedAt, m => m.MapFrom(s => s.PublishedAt))
            .ForMember(d => d.Deadline, m => m.MapFrom(s => s.Deadline))
            .ForMember(d => d.DurationMinutes, m => m.MapFrom(s => s.DurationMinutes))
            .ForMember(d => d.PassingScore, m => m.MapFrom(s => s.PassingScore))
            .ForMember(d => d.MaxAttempts, m => m.MapFrom(s => s.MaxAttempts))
            .ForMember(d => d.IsPublic, m => m.Ignore())
            .ForMember(d => d.Students, m => m.Ignore())
            .ForMember(d => d.Projects, m => m.Ignore())
            .ForMember(d => d.Courses, m => m.Ignore())
            .ForMember(d => d.Groups, m => m.Ignore())
            .ForMember(d => d.Directions, m => m.Ignore());
            //.AfterMap((src, dest) => {
            //    dest.Students = src.Students.Select(id => new StudentDTO { Id = id }).ToList();
            //    dest.Projects = src.Projects.Select(id => new ProjectDTO { Id = id }).ToList();
            //    dest.Courses = src.Courses.Select(id => new CourseDTO { Id = id }).ToList();
            //    dest.Directions = src.Directions.Select(id => new DirectionDTO { Id = id }).ToList();
            //});
    }
}