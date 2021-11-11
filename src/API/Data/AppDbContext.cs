using Microsoft.EntityFrameworkCore;
using Minimal.API.Models;

namespace Minimal.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("DataSource=app.db;Cache=Shared");
}
