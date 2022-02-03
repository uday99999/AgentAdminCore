using AgentAdminCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAdminCore.Common
{
    public interface IIdleCodeRepository
    {
        Task<List<IdleCode>> GetIdleCodes();
        Task<IdleCode> GetIdleCodes(int idleCodeId);
        Task<bool> AddIdleCode(IdleCode idleCode);
        Task<int> UpdateIdleCode(IdleCode idleCode);
        Task<int> DeleteIdleCode(int ID);
    }
}
