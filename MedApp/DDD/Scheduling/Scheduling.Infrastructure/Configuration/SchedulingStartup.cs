using Autofac;
using DDD.BuildingBlocks.Application;
using Scheduling.Infrastructure.Configuration.Mediation;
using Scheduling.Infrastructure.DataAccess;

namespace Scheduling.Infrastructure.Configuration
{
    public class SchedulingStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor)
        {

            ConfigureCompositionRoot(
                connectionString, executionContextAccessor);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));

            containerBuilder.RegisterModule(new MediatorModule());


            containerBuilder.RegisterInstance(executionContextAccessor);
            _container = containerBuilder.Build();
            SchedulingCompositionRoot.SetContainer(_container);
        }
    }
}
