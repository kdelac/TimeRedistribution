using Scheduling.Application.Contracts;
using System.Threading.Tasks;
using MediatR;
using Scheduling.Infrastructure.Configuration.Processing;
using Scheduling.Infrastructure.Configuration;
using Autofac;

namespace Scheduling.Infrastructure
{
    public class SchedulingModule : ISchedulingModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = SchedulingCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
