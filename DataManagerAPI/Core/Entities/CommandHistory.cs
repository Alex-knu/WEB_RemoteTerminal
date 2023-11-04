using System;
using System.ComponentModel.DataAnnotations;

namespace DataManagerAPI.Core.Entities
{
    public class CommandHistory
    {
        public Guid Id { get; set; }
        public MachineUser MachineUser { get; set; }
        public string Command { get; set; }
        public DateTime Time { get; set; }
    }
}