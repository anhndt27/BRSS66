using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BRSS66.Infrastructure.DependencyInjection;

public static class MapperRegistration
{
    public static IServiceCollection AddMapper(this IServiceCollection mapperService)
    {
        
        return mapperService;
    }
}