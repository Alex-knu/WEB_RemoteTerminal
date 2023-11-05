using System;
using System.ComponentModel.DataAnnotations;

namespace DataManagerAPI.Core.Entities
{
    public class Machine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        //public ICollection<MachineUser>? MachineUsers { get; }
    }
}