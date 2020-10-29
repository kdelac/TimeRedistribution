using MediatR;

namespace Scheduling.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
