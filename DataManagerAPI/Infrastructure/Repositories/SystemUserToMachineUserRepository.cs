using DataManagerAPI.Core.Entities;
using DataManagerAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagerAPI.Infrastructure.Repositories
{
    internal class SystemUserToMachineUserRepository : ISystemUserToMachineUserRepository
    {
        private DataManagerApiDbContext Context;
        public SystemUserToMachineUserRepository(DataManagerApiDbContext context)
        {
            Context = context;
        }

        public async Task<SystemUserToMachineUser> AddMachineUserForSystemUserAsync(SystemUserToMachineUser systemUserToMachineUser)
        {
            await Task.Run(async () =>
            {
                await Context.SystemUserToMachineUser.AddAsync(systemUserToMachineUser);
                await Context.SaveChangesAsync();
            });
            return systemUserToMachineUser;
        }
        public async Task<SystemUserToMachineUser> DeleteMachineUserForSystemUserAsync(SystemUserToMachineUser systemUserToMachineUser)
        {
            SystemUserToMachineUser currentSystemUserToMachineUser = Context.SystemUserToMachineUser.Where(su => 
                su.MachineUserId == systemUserToMachineUser.MachineUserId && su.SystemUserId == systemUserToMachineUser.SystemUserId).FirstOrDefault();

            if (currentSystemUserToMachineUser != null)
            {
                await Task.Run(async () =>
                {
                    Context.SystemUserToMachineUser.Remove(currentSystemUserToMachineUser);
                    await Context.SaveChangesAsync();
                });
            }
            return currentSystemUserToMachineUser;
        }

        public async Task<IEnumerable<SystemUserToMachineUser>> GetSystemUserMachineUsersAsync(Guid systemUserGuid)
        {
            return await Context.SystemUserToMachineUser
                        .Include(t => t.MachineUser)
                        .ThenInclude(mu => mu.Machine)
                        .Where(su => su.SystemUserId == systemUserGuid)
                        .ToListAsync();
        }
    }
}
