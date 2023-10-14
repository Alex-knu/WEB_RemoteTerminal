using System;
using System.ComponentModel.DataAnnotations;

namespace DataManagerAPI.Core.Entities
{
    internal class CommandHistory
    {
        public Guid Id { get; set; }
        //public Guid MachineId { get; set; }
        //[ForeignKey("MachineId")]
        public Machine Machine { get; set; }
        public string Command { get; set; }
        public DateTime Time { get; set; }
    }
}