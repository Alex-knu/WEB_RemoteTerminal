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
    internal class MachineController : Controller
    {
        IMachineRepository _MachineRepository;
        public MachineController(IMachineRepository machineRepository)
        {
            _MachineRepository = machineRepository;
        }

        [HttpGet("{id}", Name = "Get")]
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
            return CreatedAtRoute("Get", new { id = machine.Id }, machine);
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

            _MachineRepository.UpdateAsync(updatedMachine);
            //return CreatedAtRoute("Get", new { id = machine.Id }, machine);
            return new ObjectResult(machine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var deletedmachine = _MachineRepository.DeleteAsync(guid);

            if (deletedmachine == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedmachine);
        }
    }
}
