using System.Data;

namespace DDD.BuildingBlocks.Application.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
        IDbConnection CreateNewConnection();
        string GetConnectionString();
    }
}
