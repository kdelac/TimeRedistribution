using Autofac;
using Scheduling.Application.Contracts;
using Scheduling.Infrastructure;

namespace Scheduling.API.Modules.Scheduling
{
    public class SchedulingAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SchedulingModule>()
                .As<ISchedulingModule>()
                .InstancePerLifetimeScope();
        }
    }
}
