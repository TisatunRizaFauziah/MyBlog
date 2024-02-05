using Microsoft.EntityFrameworkCore;
namespace MyBlog.Models;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {

    }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
}
