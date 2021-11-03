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
    [Route("api/Application")]
    public class ApplicationController : Controller
    {
        private readonly IApplicationRepository _applicationRepository;
        public ApplicationController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Application>>> GetApplications()
        {
            return Ok(await _applicationRepository.GetApplications());
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddApplication(Application application)
        {
            if(await _applicationRepository.AddApplication(application))
            {
                return Created("", 200);
            }
                return new StatusCodeResult(500);
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateApplication(Application application)
        {
            if(await _applicationRepository.UpdateApplication(application)>0)
            {
                return Created("", application);
            }
            return new StatusCodeResult(500);
        }
        [HttpDelete]
        [Route("{ID}")]
        public async Task<ActionResult<int>> DeleteApplication(int ID)
        {
            if(await _applicationRepository.DeleteApplication(ID)>0)
            {
                return NoContent();
            }
            return new StatusCodeResult(500);
        }
    }
}
