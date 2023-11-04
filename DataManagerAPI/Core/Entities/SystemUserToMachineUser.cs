using System;
using System.ComponentModel.DataAnnotations;

namespace DataManagerAPI.Core.Entities
{
    internal class SystemUserToMachineUser
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SystemUserId { get; set; }
        public MachineUser MachineUser { get; set; }
        
    }
}
