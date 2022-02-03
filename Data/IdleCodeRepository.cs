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
    public class IdleCodeRepository : IIdleCodeRepository
    {
        private readonly IAppSettings _appSettings; 
        public IdleCodeRepository(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<bool> AddIdleCode(IdleCode idleCode)
        {
            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO IdleCode(Name, Description, ModifiedBy, ModifiedOn, ReasonCode, IsDefault, TimerFormat) OUTPUT INSERTED.ID
                                        Values(@Name, @Description, @ModifiedBy, @ModifiedOn, @ReasonCode, @IsDefault, @TimerFormat)";
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = idleCode.Name;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = idleCode.Description;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", SqlDbType.NVarChar)).Value = idleCode.ModifiedBy;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedOn", SqlDbType.DateTime)).Value = idleCode.ModifiedOn;
                    cmd.Parameters.Add(new SqlParameter("@ReasonCode", SqlDbType.Int)).Value = idleCode.ReasonCode;
                    cmd.Parameters.Add(new SqlParameter("@IsDefault", SqlDbType.Bit)).Value = idleCode.IsDefault;
                    cmd.Parameters.Add(new SqlParameter("@TimerFormat", SqlDbType.TinyInt)).Value = idleCode.TimerFormat;
                    await con.OpenAsync();
                    idleCode.ID = await cmd.ExecuteScalarAsync() as int?;
                }
            }
            return idleCode.ID.HasValue && idleCode.ID.Value > 0;
        }

        public async Task<int> UpdateIdleCode(IdleCode idleCode)
        {
            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE IdleCode SET Name=@Name, Description=@Description, ModifiedBy=@ModifiedBy, ModifiedOn=@ModifiedOn, ReasonCode=@ReasonCode, IsDefault=@IsDefault, TimerFormat=@TimerFormat Where ID=@ID";
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = idleCode.Name;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = idleCode.Description;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", SqlDbType.NVarChar)).Value = idleCode.ModifiedBy;
                    cmd.Parameters.Add(new SqlParameter("@ModifiedOn", SqlDbType.DateTime)).Value = idleCode.ModifiedOn;
                    cmd.Parameters.Add(new SqlParameter("@ReasonCode", SqlDbType.Int)).Value = idleCode.ReasonCode;
                    cmd.Parameters.Add(new SqlParameter("@IsDefault", SqlDbType.Bit)).Value = idleCode.IsDefault;
                    cmd.Parameters.Add(new SqlParameter("@TimerFormat", SqlDbType.TinyInt)).Value = idleCode.TimerFormat;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = idleCode.ID;
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
            
        }
        public async Task<int> DeleteIdleCode(int ID)
        {
            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM IdleCode Where ID=@ID";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    await con.OpenAsync();
                    var result = await cmd.ExecuteNonQueryAsync();
                    return result;
                }
            }

        }

        public async Task<List<IdleCode>> GetIdleCodes()
        {
            var idleCodes = new List<IdleCode>();

            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID, Name, Description, ModifiedBy, ModifiedOn, ReasonCode, IsDefault, TimerFormat FROM IdleCode";
                    //open the connection 

                    await con.OpenAsync();
                    //executing the command
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while(await dr.ReadAsync())
                        {
                            var ic = new IdleCode();
                            ic.ID = dr["ID"] as int? ?? 0;
                            ic.Name = dr["Name"] as string ?? string.Empty;
                            ic.Description = dr["Description"] as string ?? string.Empty;
                            ic.ModifiedBy = dr["ModifiedBy"] as string ?? string.Empty;
                            ic.ModifiedOn = dr["ModifiedOn"] as DateTime? ?? DateTime.MinValue;
                            ic.ReasonCode = dr["ReasonCode"] as int? ?? 0;
                            ic.IsDefault = dr["IsDefault"] as bool? ?? false;
                            ic.TimerFormat = dr["TimerFormat"] as byte? ?? 0;
                            idleCodes.Add(ic);   
                        }
                    }
                }
            }
            return idleCodes;
        }
        public async Task<IdleCode> GetIdleCodes(int ID)
        {
            var idleCode = new IdleCode();

            using (var con = new SqlConnection(_appSettings.AgentAdminCoreConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ID, Name, Description, ModifiedBy, ModifiedOn, ReasonCode, IsDefault, TimerFormat FROM IdleCode WHERE ID =@ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                    //open the connection 

                    await con.OpenAsync();
                    //executing the command
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            return new IdleCode()
                            {
                                ID = dr["ID"] as int? ?? 0,
                                Name = dr["Name"] as string ?? string.Empty,
                                Description = dr["Description"] as string ?? string.Empty,
                                ModifiedBy = dr["ModifiedBy"] as string ?? string.Empty,
                                ModifiedOn = dr["ModifiedOn"] as DateTime? ?? DateTime.MinValue,
                                ReasonCode = dr["ReasonCode"] as int? ?? 0,
                                IsDefault = dr["IsDefault"] as bool? ?? false,
                                TimerFormat = dr["TimerFormat"] as byte? ?? 0
                            };
                        }
                    }
                }
            }
            return idleCode;
        }
    }
}
