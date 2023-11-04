using DataManagerAPI.Core.Entities;
using DataManagerAPI.Core.Interfaces;

namespace DataManagerAPI.Infrastructure.Repositories
{
    internal class MachineUserRepository : IMachineUserRepository
    {
        private DataManagerApiDbContext Context;
        public MachineUserRepository(DataManagerApiDbContext context)
        {
            Context = context;
        }

        public async Task<MachineUser> GetAsync(Guid guid)
        {
            return await Context.MachineUsers.FindAsync(guid);
        }

        public async Task<MachineUser> AddAsync(MachineUser machineUser)
        {
            await Task.Run(async () =>
            {
                await Context.MachineUsers.AddAsync(machineUser);
                Context.SaveChangesAsync();
            });
            return machineUser;
        }

        public async Task<MachineUser> UpdateAsync(MachineUser machineUser)
        {
            MachineUser currentMachineUser = await GetAsync(machineUser.Id);
            currentMachineUser.Username = machineUser.Username;
            currentMachineUser.Password = machineUser.Password;
            currentMachineUser.Machine = machineUser.Machine;

            await Task.Run(async () =>
            {
                Context.MachineUsers.Update(currentMachineUser);
                Context.SaveChangesAsync();
            });
            return currentMachineUser;
        }

        public async Task<MachineUser> DeleteAsync(Guid guid)
        {
            MachineUser currentMachineUser = await GetAsync(guid);

            if (currentMachineUser != null)
            {
                await Task.Run(async () =>
                {
                    Context.MachineUsers.Remove(currentMachineUser);
                    Context.SaveChangesAsync();
                });
            }
            return currentMachineUser;
        }
        public async Task<IEnumerable<MachineUser>> GetMachineUsersOfMachineAsync(Guid machineGuid)
        {
            return Context.MachineUsers.Where(mu => mu.Machine.Id == machineGuid);
        }
    }
}
