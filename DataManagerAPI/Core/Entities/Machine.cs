using System;
using System.ComponentModel.DataAnnotations;

namespace DataManagerAPI.Core.Entities
{
    internal class Machine
    {
        public Guid Id { get; set; }
        //public Guid UserId { get; set; }
        //[ForeignKey("UserId")]
        public User User { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string MachineUser { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }

        public ICollection<CommandHistory> CommandsHistory { get; }
    }
}