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
    
    public DbSet<Show> Show { get; set; }
    
}