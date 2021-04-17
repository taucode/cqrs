using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Abstractions;

namespace TauCode.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery> 
        where TQuery : IQuery
    {
        void Execute(TQuery query);
        Task ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
