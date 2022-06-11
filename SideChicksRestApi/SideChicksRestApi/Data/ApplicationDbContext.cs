using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SideChicksRestApi.Models;

namespace SideChicksRestApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public ApplicationDbContext()
    {
        throw new NotImplementedException();
    }

    public DbSet<Show>? Shows { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<News> News { get; set; }
    
}