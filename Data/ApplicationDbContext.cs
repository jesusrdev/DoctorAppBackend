using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}