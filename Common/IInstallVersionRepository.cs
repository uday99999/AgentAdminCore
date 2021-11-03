using AgentAdminCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAdminCore.Common
{
    public interface IInstallVersionRepository 
    {
       Task<bool> AddVersion(InstallVersion installVersion);
        Task<List<InstallVersion>> GetInstallVersions();
        Task<int> UpdateInstallVersion(InstallVersion iv);
        Task<int> DeleteInstallVersion(int ID);
    }
}
