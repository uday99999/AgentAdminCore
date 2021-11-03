using AgentAdminCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAdminCore.Common
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetApplications();
        Task<bool> AddApplication(Application application);
        Task<int> UpdateApplication(Application application);
        Task<int> DeleteApplication(int ID);
    }
}
