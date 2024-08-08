using Microsoft.EntityFrameworkCore;
using SMS.Entities;

namespace SMS.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<School> Schools { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
}