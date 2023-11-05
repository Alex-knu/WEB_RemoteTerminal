using DataManagerAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

internal class DataManagerApiDbContext : DbContext
{
    public DataManagerApiDbContext(DbContextOptions<DataManagerApiDbContext> options) : base(options)
    { }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<MachineUser> MachineUsers { get; set; }
    
    public DbSet<CommandHistory> CommandsHistory { get; set; }
    public DbSet<SystemUserToMachineUser> SystemUserToMachineUser { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SystemUserToMachineUser>()
            .Property(b => b.Id)
            .HasDefaultValue(Guid.NewGuid());
    }
}