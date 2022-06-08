using AgentAdminCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgentAdminCore.Common
{
    public interface ITenantRepository
    {
        Task<List<Tenant>> GetTenants();
    }
}
