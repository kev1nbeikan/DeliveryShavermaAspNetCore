using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
}