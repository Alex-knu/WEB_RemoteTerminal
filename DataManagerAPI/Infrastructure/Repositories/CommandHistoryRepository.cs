using DataManagerAPI.Core.Entities;
using DataManagerAPI.Core.Interfaces;

namespace DataManagerAPI.Infrastructure.Repositories
{
    internal class CommandHistoryRepository : ICommandHistoryRepository
    {
        private DataManagerApiDbContext Context;
        public CommandHistoryRepository(DataManagerApiDbContext context)
        {
            Context = context;
        }

        public async Task<CommandHistory> GetAsync(Guid guid)
        {
            return await Context.CommandsHistory.FindAsync(guid);
        }

        public async Task<CommandHistory> AddAsync(CommandHistory commandHistory)
        {
            await Task.Run(async () =>
            {
                await Context.CommandsHistory.AddAsync(commandHistory);
                Context.SaveChangesAsync();
            });
            return commandHistory;
        }

        public async Task<CommandHistory> UpdateAsync(CommandHistory commandHistory)
        {
            CommandHistory currentCommandHistory = await GetAsync(commandHistory.Id);
            currentCommandHistory.MachineUser = commandHistory.MachineUser;
            currentCommandHistory.Command = commandHistory.Command;
            currentCommandHistory.Time = commandHistory.Time;

            await Task.Run(async () =>
            {
                Context.CommandsHistory.Update(currentCommandHistory);
                Context.SaveChangesAsync();
            });
            return currentCommandHistory;
        }

        public async Task<CommandHistory> DeleteAsync(Guid guid)
        {
            CommandHistory currentCommandHistory = await GetAsync(guid);

            if (currentCommandHistory != null)
            {
                await Task.Run(async () =>
                {
                    Context.CommandsHistory.Remove(currentCommandHistory);
                    Context.SaveChangesAsync();
                });
            }
            return currentCommandHistory;
        }
        
        public async Task<IEnumerable<CommandHistory>> GetCommandHistoryOfMachineUserAsync(Guid machineUserGuid)
        {
            return Context.CommandsHistory.Where(ch => ch.MachineUser.Id == machineUserGuid);
        }

    }
}
