using MediatR;
using Scheduling.Application.Contracts;

namespace Scheduling.Application.Configurations.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
