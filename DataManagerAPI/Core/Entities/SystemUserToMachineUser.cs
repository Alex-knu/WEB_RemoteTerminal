using System;
using System.ComponentModel.DataAnnotations;

namespace DataManagerAPI.Core.Entities
{
    public class SystemUserToMachineUser
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SystemUserId { get; set; }
        public MachineUser? MachineUser { get; set; }
        public Guid MachineUserId { get; set; }

    }
}
