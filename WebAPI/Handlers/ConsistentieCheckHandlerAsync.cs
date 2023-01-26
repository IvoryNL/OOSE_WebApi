using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPI.Entities;
using WebAPI.Handlers.Interfaces;

namespace WebAPI.Handlers
{
    public class ConsistentieCheckHandlerAsync : IHandlerAsync<bool>
    {
        private readonly DataContext _dataContext;

        public ConsistentieCheckHandlerAsync(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> ConsistentieCheckCoverageHandlerAsync(int id)
        {
            var onderwijsmoduleId = new SqlParameter();
            onderwijsmoduleId.DbType = DbType.Int32;
            onderwijsmoduleId.Direction = ParameterDirection.Input;
            onderwijsmoduleId.ParameterName = "@onderwijsmoduleId";
            onderwijsmoduleId.Value = id;

            var outputParam = new SqlParameter();
            outputParam.DbType = DbType.Boolean;
            outputParam.Direction = ParameterDirection.Output;
            outputParam.ParameterName = "@result";

            await _dataContext.Database.ExecuteSqlRawAsync("dbo.proc_ConsistentieCheckCoverage @onderwijsmoduleId, @result output", onderwijsmoduleId, outputParam);
            var result = Convert.ToBoolean(outputParam.Value);
            return result;
        }

        public async Task<bool> ConsistentieCheckTentamenPlanningHandlerAsync(int id)
        {
            var onderwijsuitvoeringId = new SqlParameter();
            onderwijsuitvoeringId.DbType = DbType.Int32;
            onderwijsuitvoeringId.Direction = ParameterDirection.Input;
            onderwijsuitvoeringId.ParameterName = "@onderwijsuitvoeringId";
            onderwijsuitvoeringId.Value = id;

            var outputParam = new SqlParameter();
            outputParam.DbType = DbType.Boolean;
            outputParam.Direction = ParameterDirection.Output;
            outputParam.ParameterName = "@result";

            await _dataContext.Database.ExecuteSqlRawAsync("dbo.proc_ConsistentieCheckTentamenPlanning @onderwijsuitvoeringId, @result output", onderwijsuitvoeringId, outputParam);
            var result = Convert.ToBoolean(outputParam.Value);
            return result;
        }
    }
}
