using AgentAdminCore.Common;
using AgentAdminCore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AgentAdminCore.Data
{
    public class TenantRepository :ITenantRepository
    {
        private readonly IAppSettings _appSettings;
        public TenantRepository(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<List<Tenant>> GetTenants()
        {
            var tenants = new List<Tenant>();

            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID, Name, ModifiedBy, ModifiedOn FROM Tenant";
                    //open the connection 

                    await con.OpenAsync();
                    //executing the command
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var tc = new Tenant();
                            tc.ID = dr["ID"] as int? ?? 0;
                            tc.Name = dr["Name"] as string ?? string.Empty;
                            tc.ModifiedBy = dr["ModifiedBy"] as string ?? string.Empty;
                            tc.ModifiedOn = dr["ModifiedOn"] as DateTime? ?? DateTime.MinValue;
                            tenants.Add(tc);
                        }
                    }
                }
            }
            return tenants;
        }
    }
}
