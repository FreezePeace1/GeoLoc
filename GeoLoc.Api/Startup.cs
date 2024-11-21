using Microsoft.AspNetCore.Http.Features;

namespace GeoLoc;

public static class Startup
{
    public static void AddSmth(this IServiceCollection service)
    {
        service.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = 10485760); // 10MB
    }
}