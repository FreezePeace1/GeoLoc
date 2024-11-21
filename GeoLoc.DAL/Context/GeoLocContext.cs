using Microsoft.EntityFrameworkCore;

namespace GeoLoc.DAL.Context;

public class GeoLocContext : DbContext
{
    public GeoLocContext(DbContextOptions<GeoLocContext> opts) : base(opts)
    {
    } 
}