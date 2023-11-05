using System;

namespace DataManagerAPI.Core.Entities
{
    public class MachineUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Machine? Machine { get; set; }
        public Guid MachineId { get; set; }

        //public ICollection<SystemUserToMachineUser> SystemUserToMachineUsers { get; }
        //public ICollection<CommandHistory> CommandHistory { get; }
    }
}