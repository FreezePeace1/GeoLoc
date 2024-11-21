using GeoLoc.Application.Services;
using GeoLoc.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GeoLoc.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<IFileService,FileService>();
        service.AddScoped<IChartService, ChartService>();
        service.AddScoped<IMapService, MapService>();
        service.AddSingleton(Log.Logger);
        
    }
}