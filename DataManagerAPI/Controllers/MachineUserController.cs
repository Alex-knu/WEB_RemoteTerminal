using DataManagerAPI.Core.Entities;
using DataManagerAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineUserController : ControllerBase
    {
        private readonly IMachineUserRepository _MachineUserRepository;
        
        public MachineUserController(IMachineUserRepository machineUserRepository)
        {
            _MachineUserRepository = machineUserRepository;
        }

        [HttpGet("{guid}", Name = "GetMachineUser")]
        public async Task<IActionResult> Get(Guid guid)
        {
            MachineUser machineUser = await _MachineUserRepository.GetAsync(guid);

            if (machineUser == null)
            {
                return NotFound();
            }

            return new ObjectResult(machineUser);
        }

        //[Route("api/[controller]/machine")]
        [HttpGet("machine/{guid}", Name = "GetMachineAllMachineUsers")]
        public async Task<IActionResult> GetMachineAllMachineUsers(Guid guid)
        {
            IEnumerable<MachineUser> machineUsers = await _MachineUserRepository.GetMachineUsersOfMachineAsync(guid);

            if (machineUsers.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(machineUsers);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MachineUser machineUser)
        {
            if (machineUser == null)
            {
                return BadRequest();
            }
            await _MachineUserRepository.AddAsync(machineUser);
            return new ObjectResult(machineUser);
            //return CreatedAtRoute("Get", new { id = machineUser.Id }, machineUser);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MachineUser updatedMachineUser)
        {
            if (updatedMachineUser == null)
            {
                return BadRequest();
            }

            var machineUser = await _MachineUserRepository.GetAsync(updatedMachineUser.Id);
            if (machineUser == null)
            {
                return NotFound();
            }

            await _MachineUserRepository.UpdateAsync(updatedMachineUser);
            return new ObjectResult(machineUser);
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var deletedMachineUser = await _MachineUserRepository.DeleteAsync(guid);

            if (deletedMachineUser == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedMachineUser);
        }
    }
}
