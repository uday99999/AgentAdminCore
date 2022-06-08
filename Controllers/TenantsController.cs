using AgentAdminCore.Common;
using AgentAdminCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgentAdminCore.Controllers
{
    [ApiController]
    [Route("api/Tenants")]
    public class TenantsController : Controller
    {
        private readonly ITenantRepository _tenantRepository;
        public TenantsController(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        [HttpGet]
        [Route("GetTenants")]
        public async Task<ActionResult<List<Tenant>>> GetTenants()
        {
            return Ok(await _tenantRepository.GetTenants());
        }
    }
    }
