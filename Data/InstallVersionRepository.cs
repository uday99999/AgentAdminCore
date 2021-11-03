using AgentAdminCore.Common;
using AgentAdminCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAdminCore.Data
{
    public class InstallVersionRepository : IInstallVersionRepository
    {
        private readonly IAppSettings _appSettings;
        public InstallVersionRepository(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task<bool> AddVersion(InstallVersion installVersion)
        {
            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using(var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO InstallVersion(Version, Description, RelativePath, IsRetired) OUTPUT INSERTED.ID Values
                                     (@Version, @Description, @RelativePath, @IsRetired)";
                    cmd.Parameters.Add(new SqlParameter("@Version", SqlDbType.NVarChar)).Value = installVersion.Version;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = installVersion.Description;
                    cmd.Parameters.Add(new SqlParameter("@RelativePath", SqlDbType.NVarChar)).Value = installVersion.RelativePath;
                    cmd.Parameters.Add(new SqlParameter("@IsRetired", SqlDbType.Bit)).Value = installVersion.IsRetired;
                    await con.OpenAsync();
                    installVersion.ID = await cmd.ExecuteScalarAsync() as int?;
                }
            }
            return installVersion.ID.HasValue && installVersion.ID > 0;
        }
        public async Task<List<InstallVersion>> GetInstallVersions()
        {
            var installVersions = new List<InstallVersion>();
            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using(var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID, Version, Description, RelativePath, IsRetired FROM InstallVersion";
                    await con.OpenAsync();
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            var iv = new InstallVersion();
                            iv.ID = dr["ID"] as int? ?? 0;
                            iv.Version = dr["Version"] as string ?? string.Empty;
                            iv.Description = dr["Description"] as string ?? string.Empty;
                            iv.RelativePath = dr["RelativePath"] as string ?? string.Empty;
                            iv.IsRetired = dr["IsRetired"] as bool? ?? false;
                            installVersions.Add(iv);
                        }
                    }
                }
            }
            return installVersions;
        }

        public async Task<int> UpdateInstallVersion(InstallVersion iv)
        {
            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using(var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE InstallVersion SET Version = @Version, Description= @Description, RelativePath = @RelativePath, IsRetired = @IsRetired WHERE ID = @ID";
                    cmd.Parameters.Add(new SqlParameter("@Version", SqlDbType.NVarChar)).Value = iv.Version;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = iv.Description;
                    cmd.Parameters.Add(new SqlParameter("@RelativePath", SqlDbType.NVarChar)).Value = iv.RelativePath;
                    cmd.Parameters.Add(new SqlParameter("@IsRetired", SqlDbType.Bit)).Value = iv.IsRetired;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = iv.ID;
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task<int> DeleteInstallVersion(int ID)
        {
            using(var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using(var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM InstallVersion WHERE ID = @ID";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
