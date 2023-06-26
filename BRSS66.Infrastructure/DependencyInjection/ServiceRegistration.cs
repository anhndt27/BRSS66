using BRSS66.ApplicationCore.DomainService;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.Infrastructure.Repositories;
using BRSS66.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BRSS66.Infrastructure.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //DI Repository
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ICourseRepository, CourseRepository>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
        services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        //DI Service
        services.AddTransient<IStudentServices, StudentService>();
        services.AddTransient<ICourseServices, CourseService>();
        return services;
    }
}