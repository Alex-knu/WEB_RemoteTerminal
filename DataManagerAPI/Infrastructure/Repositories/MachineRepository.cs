using DataManagerAPI.Core.Entities;
using DataManagerAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagerAPI.Infrastructure.Repositories
{
    internal class MachineRepository : IMachineRepository
    {
        private DataManagerApiDbContext Context;
        public MachineRepository(DataManagerApiDbContext context)
        {
            Context = context;
        }
        public async Task<Machine> GetAsync(Guid guid)
        {
            return await Context.Machines.FindAsync(guid);
        }
        public async Task<Machine> AddAsync(Machine machine)
        {
            await Task.Run(async () =>
            {
                await Context.Machines.AddAsync(machine);
                Context.SaveChangesAsync();
            });
            return machine;
        }
        public async Task<Machine> UpdateAsync(Machine machine)
        {
            Machine currentMachine = await GetAsync(machine.Id);
            currentMachine.Name = machine.Name;
            currentMachine.Host = machine.Host;
            currentMachine.Port = machine.Port;

            await Task.Run(async () =>
            {
                Context.Machines.Update(currentMachine);
                Context.SaveChangesAsync();
            });
            return currentMachine;
        }
        public async Task<Machine> DeleteAsync(Guid guid)
        {
            Machine currentMachine = await GetAsync(guid);

            if (currentMachine != null)
            {
                await Task.Run(async () =>
                {
                    Context.Machines.Remove(currentMachine);
                    Context.SaveChangesAsync();
                });
            }
            return currentMachine;
        }
    }
}
