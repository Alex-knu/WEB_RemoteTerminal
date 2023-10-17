using DataManagerAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

internal class DataManagerApiDbContext : DbContext
{
    public DataManagerApiDbContext(DbContextOptions<DataManagerApiDbContext> options) : base(options)
    { }
    public DbSet<User> Users { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<CommandHistory> CommandsHistory { get; set; }
}