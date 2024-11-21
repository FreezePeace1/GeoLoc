using GeoLoc.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoLoc.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDAL(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<GeoLocContext>(opts =>
        {
            opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}