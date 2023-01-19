using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

        public async Task<bool> GetAsync(int id)
        {
            var outputParam = new SqlParameter();
            outputParam.DbType = DbType.Boolean;
            outputParam.Direction = ParameterDirection.Output;
            outputParam.ParameterName = "@result";
            
            await _dataContext.Database.ExecuteSqlRawAsync("dbo.proc_ConsistentieCheck @result output", outputParam);
            var result = Convert.ToBoolean(outputParam.Value);
            return result;
        }
    }
}
