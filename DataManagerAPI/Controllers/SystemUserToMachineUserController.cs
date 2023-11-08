using DataManagerAPI.Core.Entities;
using DataManagerAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemUserToMachineUserController : Controller
    {
        private readonly ISystemUserToMachineUserRepository _SystemUserToMachineUserRepository;

        public SystemUserToMachineUserController(ISystemUserToMachineUserRepository systemUserToMachineUserRepository)
        {
            _SystemUserToMachineUserRepository = systemUserToMachineUserRepository;
            
        }

        [HttpGet("collection/{guid}", Name = "GetMachineUsers")]
        public async Task<IActionResult> GetMachineUsers(Guid guid)
        {
            IEnumerable<SystemUserToMachineUser> systemUserToMachineUser = await _SystemUserToMachineUserRepository.GetSystemUserMachineUsersAsync(guid);

            // if (systemUserToMachineUser.Count() == 0)
            // {
            //     return NotFound();
            // }

            /*IEnumerable<Guid> machineUsersGuids = new List<Guid>();
            foreach (var machineUser in systemUserToMachineUser)
            {
                machineUsersGuids.Append(machineUser.MachineUserId);
            }*/

            return new ObjectResult(systemUserToMachineUser);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SystemUserToMachineUser systemUserToMachineUser)
        {
            if (systemUserToMachineUser == null)
            {
                return BadRequest();
            }
            await _SystemUserToMachineUserRepository.AddMachineUserForSystemUserAsync(systemUserToMachineUser);
            return new ObjectResult(systemUserToMachineUser);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] SystemUserToMachineUser systemUserToMachineUser)
        {
            var deletedSystemUserToMachineUser = await _SystemUserToMachineUserRepository.DeleteMachineUserForSystemUserAsync(systemUserToMachineUser);

            if (deletedSystemUserToMachineUser == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedSystemUserToMachineUser);
        }
    }
}
