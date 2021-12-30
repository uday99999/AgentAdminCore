using AgentAdminCore.Common;
using AgentAdminCore.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAdminCore.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IAppSettings _appSettings;
        private readonly ILogger<ApplicationRepository> _logger;
        public ApplicationRepository(IAppSettings appSettings, ILogger<ApplicationRepository> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
        }
        public async Task<List<Application>> GetApplications()
        {
            var applications = new List<Application>();
            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID,Name,Description,LoginUrl,ModifiedBy,ModifiedOn from Application";

                    await con.OpenAsync();
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var ap = new Application();
                            ap.ID = dr["ID"] as int? ?? 0;
                            ap.Name = dr["Name"] as string ?? string.Empty;
                            ap.Description = dr["Description"] as string ?? string.Empty;
                            ap.LoginUrl = dr["LoginUrl"] as string ?? string.Empty;
                            ap.ModifiedBy = dr["ModifiedBy"] as string ?? string.Empty;
                            ap.ModifiedOn = dr["ModifiedOn"] as DateTime? ?? DateTime.MinValue;
                            applications.Add(ap);
                        }
                    }
                }
            }
            _logger.LogInformation("Applications:" + applications);
            return applications;
        }
        public async Task<bool> AddApplication(Application application)
        {
            using(var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using( var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Application(Name, Description, LoginUrl, ModifiedBy, ModifiedOn) OUTPUT INSERTED.ID 
                                        VALUES (@Name, @Description, @LoginUrl, @ModifiedBy, @ModifiedOn)";
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = application.Name;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = application.Description;
                    cmd.Parameters.Add(new SqlParameter("@LoginUrl", SqlDbType.NVarChar)).Value = application.LoginUrl;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", SqlDbType.NVarChar)).Value = application.ModifiedBy;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedOn", SqlDbType.NVarChar)).Value = application.ModifiedOn;
                    await con.OpenAsync();
                    application.ID = await cmd.ExecuteScalarAsync() as int?;
                }
            }
            return application.ID.HasValue && application.ID.Value > 0;
        }
        public async Task<int> UpdateApplication(Application application)
        {
            using(var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using(var cmd= con.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Application SET Name=@Name, Description=@Description, LoginUrl=@LoginUrl, ModifiedBy=@ModifiedBy, ModifiedOn=@ModifiedOn WHERE ID=@ID";
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = application.Name;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = application.Description;
                    cmd.Parameters.Add(new SqlParameter("@LoginUrl", SqlDbType.NVarChar)).Value = application.LoginUrl;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", SqlDbType.NVarChar)).Value = application.ModifiedBy;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedOn", SqlDbType.NVarChar)).Value = application.ModifiedOn;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = application.ID;
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<int> DeleteApplication(int ID)
        {
            using(var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using(var cmd  = con.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Application WHERE ID=@ID";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    await con.OpenAsync();
                    var result=  await cmd.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }
    }
}
