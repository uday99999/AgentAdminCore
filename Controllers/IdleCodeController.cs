using AgentAdminCore.Common;
using AgentAdminCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAdminCore.Controllers
{
    [ApiController]
    [Route("api/IdleCode")]
    public class IdleCodeController : Controller
    {
        private readonly IIdleCodeRepository _idleCodeRepository;
        public IdleCodeController(IIdleCodeRepository idleCodeRepository)
        {
            _idleCodeRepository = idleCodeRepository;
        }
        [HttpGet]
        [Route("GetIdleCodes")]
        public async Task<ActionResult<List<IdleCode>>> GetIdleCodes()
        {
            return Ok(await _idleCodeRepository.GetIdleCodes());
        }
        [HttpGet]
        [Route("GetIdleCodes/{idleCodeId}")]
        public async Task<ActionResult<IdleCode>> GetIdleCodes(int idleCodeId)
        {
            return Ok(await _idleCodeRepository.GetIdleCodes(idleCodeId));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddIdleCode(IdleCode idleCode)
        {
            if (await _idleCodeRepository.AddIdleCode(idleCode))
            {
                return Created("", idleCode);
            }
            return new StatusCodeResult(500);

        }
        [HttpPut]
        //[Route("{idleCode}")]
        public async Task<ActionResult<int>> UpdateIdleCode(IdleCode idleCode)
        {
            if (await _idleCodeRepository.UpdateIdleCode(idleCode) > 0)
            {
                return Created("", idleCode);
            }
            return new StatusCodeResult(500);

        }
        [HttpDelete]
        [Route("{ID}")]
        public async Task<ActionResult<int>> DeleteIdleCode(int ID)
        {
            if (await _idleCodeRepository.DeleteIdleCode(ID) > 0)
            {
                return NoContent();
            }
            return new StatusCodeResult(500);

        }
    }
}
