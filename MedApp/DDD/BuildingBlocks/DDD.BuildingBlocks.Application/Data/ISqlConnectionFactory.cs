using System.Data;

namespace DDD.BuildingBlocks.Application.Data
{
    public interface ISqlConnectionFactory
    {
        public interface ISqlConnectionFactory
        {
            IDbConnection GetOpenConnection();

            IDbConnection CreateNewConnection();

            string GetConnectionString();
        }
    }
}
