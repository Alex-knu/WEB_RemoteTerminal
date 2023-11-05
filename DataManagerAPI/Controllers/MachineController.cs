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
    public class MachineController : ControllerBase
    {
        private readonly IMachineRepository _MachineRepository;
        
        public MachineController(IMachineRepository machineRepository)
        {
            _MachineRepository = machineRepository;
        }
        
        [HttpGet("{guid}", Name = "Get")]
        public async Task<IActionResult> Get(Guid guid)
        {
            Machine machine = await _MachineRepository.GetAsync(guid);

            if (machine == null)
            {
                return NotFound();
            }

            return new ObjectResult(machine);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Machine machine)
        {
            if (machine == null)
            {
                return BadRequest();
            }
            await _MachineRepository.AddAsync(machine);
            return new ObjectResult(machine);
            //return CreatedAtRoute("Get", new { id = machine.Id }, machine);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Machine updatedMachine)
        {
            if (updatedMachine == null)
            {
                return BadRequest();
            }

            var machine = await _MachineRepository.GetAsync(updatedMachine.Id);
            if (machine == null)
            {
                return NotFound();
            }

            await _MachineRepository.UpdateAsync(updatedMachine);
            //return CreatedAtRoute("Get", new { id = machine.Id }, machine);
            return new ObjectResult(machine);
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var deletedMachine = await _MachineRepository.DeleteAsync(guid);

            if (deletedMachine == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedMachine);
        }
    }
}
