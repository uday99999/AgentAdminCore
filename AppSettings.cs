using AgentAdminCore.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAdminCore
{
    public class AppSettings: IAppSettings
    {
        private readonly string _agentAdminConnectionString;
        public AppSettings(IConfiguration configuartion)
        {
            _agentAdminConnectionString = configuartion["AgentAdminCoreConnectionString"];
        }
        public string AgentAdminCoreConnectionString => _agentAdminConnectionString;
    }
}
