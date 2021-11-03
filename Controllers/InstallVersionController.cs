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
    [Route("api/InstallVersion")]
    public class InstallVersionController : Controller
    {
        private readonly IInstallVersionRepository _installVersionRepository;
        public InstallVersionController(IInstallVersionRepository installVersionRepository)
        {
            _installVersionRepository = installVersionRepository;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddVersion(InstallVersion installVersion)
        {
            if (await _installVersionRepository.AddVersion(installVersion))
            {
                return (Created("", installVersion));
            }
            return new StatusCodeResult(500);
        }
        [HttpGet]
        public async Task<ActionResult<List<InstallVersion>>> GetInstallVersion()
        {
            return Ok(await _installVersionRepository.GetInstallVersions());
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateInstallVersion(InstallVersion iv)
        {
            if (await _installVersionRepository.UpdateInstallVersion(iv) > 0)
            {
                return Created("", iv);
            }
            return new StatusCodeResult(500);
        }

        [HttpDelete]
        [Route("{ID}")]
        public async Task<ActionResult<int>> DeleteInstallVersion(int ID)
        {
            if(await _installVersionRepository.DeleteInstallVersion(ID) > 0)
            {
                return NoContent();
            }
            return new StatusCodeResult(500);
        }
    }
}
